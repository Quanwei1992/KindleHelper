using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using Ionic.Zip;
using eBdb.EpubReader.Properties;

namespace eBdb.EpubReader {
	public class Epub {

		#region Properties
		//Note: Mandatory fields defined by OPF standard
		public string UUID { get; private set; }
		public List<string> ID { get; private set; }
		public List<string> Title { get; private set; }
		public List<string> Language { get; private set; }

		//Note: Not mandatory fields by OPF standard
		public List<string> Creator { get; private set; }
		public List<string> Description { get; private set; }
		public List<DateData> Date { get; private set; }
		public List<string> Publisher { get; private set; }
		public List<string> Contributer { get; private set; }
		public List<string> Type { get; private set; }
		public List<string> Format { get; private set; }
		public List<string> Subject { get; private set; }
		public List<string> Source { get; private set; }
		public List<string> Relation { get; private set; }
		public List<string> Coverage { get; private set; }
		public List<string> Rights { get; private set; }

		public OrderedDictionary Content { get; private set; }
		public OrderedDictionary ExtendedData { get; private set; }
		public List<NavPoint> TOC { get; private set; }

		private readonly ZipFile _EpubFile;
		private readonly string _ContentOpfPath;
		private string _TocFileName;
		private readonly Hashtable _LinksMapping = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		private string _CurrentFileName;
		private static Regex _RefsRegex = new Regex(@"(?<prefix><\w+[^>]*?href\s*=\s*(""|'))(?<href>[^""']*)(?<suffix>(""|')[^>]*>)", Utils.REO_ci);
		private static Regex _ExternalLinksRegex = new Regex(@"^\s*(http(s)?://|mailto:|ftp(s)?://)", Utils.REO_ci);
		#endregion

		#region Constructor
		public Epub(string ePubPath) {
			ID = new List<string>();
			Title = new List<string>();
			Language = new List<string>();

			Creator = new List<string>();
			Description = new List<string>();
			Date = new List<DateData>();
			Publisher = new List<string>();
			Contributer = new List<string>();
			Type = new List<string>();
			Format = new List<string>();
			Subject = new List<string>();
			Source = new List<string>();
			Relation = new List<string>();
			Coverage = new List<string>();
			Rights = new List<string>();

			Content = new OrderedDictionary();
			ExtendedData = new OrderedDictionary();
			TOC = new List<NavPoint>();
			
			if (File.Exists(ePubPath)) _EpubFile = ZipFile.Read(ePubPath);
			else throw new FileNotFoundException();

			string opfFilePath = GetOpfFilePath(_EpubFile);
			if (string.IsNullOrEmpty(opfFilePath)) throw new Exception("Invalid epub file.");

			Match m = Regex.Match(opfFilePath, @"^.*/", Utils.REO_c);
			_ContentOpfPath = m.Success ? m.Value : "";

			LoadEpubMetaDataFromOpfFile(opfFilePath);
			if (_TocFileName != null) LoadTableOfContents();
		}
		#endregion

		#region Public Functions
		public string GetContentAsPlainText() {
			StringBuilder builder = new StringBuilder();
			for (int i = 0, y = Content.Count; i < y; ++i) builder.Append(((ContentData)Content[i]).GetContentAsPlainText());
			return builder.ToString();
		}

		public string GetContentAsHtml() {
			StringBuilder body = new StringBuilder();
			body.AppendFormat("<h2>Table of Contents</h2>{0}", GetTocHtml(TOC));

			//Note: run through all content items and collect collection of replacement links (solves problem with client id value replacement)
			for (int i = 0, y = Content.Count; i < y; ++i) {
				var contentData = (ContentData)Content[i];
				CollectReplacementLinks(_LinksMapping, GetTrimmedFileName(contentData.FileName, false), contentData.Content);
			}

			for (int i = 0, y = Content.Count; i < y; ++i) {
                var contentData = (ContentData)Content[i];
				Match m = Regex.Match(contentData.Content, @"<body[^>]*>(?<body>.+)</body>", Utils.REO_csi);
                if (m.Success) {
                    //Note: add link to top of page so they can be found by the table of contents
                    //then update links within body
	                _CurrentFileName = GetTrimmedFileName(contentData.FileName, false);
					string fullContentHtml = NormalizeRefs("<a id=\"" + _CurrentFileName.Replace('.', '_') + "\"/>" + m.Groups["body"].Value);

                    //embed base64 images & append
                    fullContentHtml = EmbedImages(fullContentHtml);
                    body.Append(fullContentHtml);
	                _CurrentFileName = null;
                }
			}

			string headPart = "";
			Match match = Regex.Match(((ContentData)Content[Content.Count - 1]).Content, @"<head[^>]*>(?<head>.+?)</head>", Utils.REO_csi);
			if (match.Success) headPart = Regex.Replace(match.Groups["head"].Value, @"<title[^>]*>.+?</title>", "", Utils.REO_csi);
            
			if (!Regex.IsMatch(headPart, @"<meta\s+[^>]*?http-equiv\s*=\s*(""|')Content-Type(""|')", Utils.REO_csi)) 
	            headPart += "<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\" />";

			headPart = EmbedCssData(headPart);

			string bodyTag = "<body>";
			match = Regex.Match(((ContentData)Content[Content.Count - 1]).Content, @"<body[^>]*>", Utils.REO_ci);
			if (match.Success) bodyTag = match.Value;


			if (Language.Count > 0 && CultureInfo.CreateSpecificCulture(Language[0]).TextInfo.IsRightToLeft)
				body = body.Insert(body.Length - 2, " dir=\"rtl\"");

            return string.Format(_HtmlTemplate, string.Join(", ", Creator) + " - " + Title[0], headPart.Trim(), bodyTag, body);
		}

		
		#endregion

        #region Private Functions
        private string EmbedImages(string html) {
			return Regex.Replace(html, @"(?<prefix><\w+[^>]*?src\s*=\s*(""|'))(?<src>[^""']+)(?<suffix>(""|')[^>]*>)", SrcEvaluator, Utils.REO_ci);
        }

		private string SrcEvaluator(Match match) {
			var extendedData = ExtendedData[GetTrimmedFileName(match.Groups["src"].Value, true)] as ExtendedData;
			return extendedData != null
				       ? match.Groups["prefix"].Value + "data:" + extendedData.MimeType + ";base64," + extendedData.Content +
				         match.Groups["suffix"].Value : match.Value;
		}

        //
        //Developer: Brian Kenney
        //Date: 7/29/2012
        //Change: remove namespace prefix
        //Details: 
        //some opf files come with the namespace prefix of odfc, so remvoe the prefix before processing
        //
        private static string GetOpfFilePath(ZipFile epubFile)
        {
            string tmpXMLStream;
			ZipEntry zipEntry = epubFile.Entries.FirstOrDefault(e => e.FileName.Equals(@"meta-inf/container.xml", StringComparison.InvariantCultureIgnoreCase));
			if (zipEntry != null) {
				XElement containerXml;
				using (MemoryStream memoryStream = new MemoryStream()) {
					zipEntry.Extract(memoryStream);
					memoryStream.Position = 0;

					containerXml = XElement.Load(memoryStream);
                    
                    //get stream just in case we have a namespace prefix
                    memoryStream.Position = 0;
                    var sr = new System.IO.StreamReader(memoryStream);
                    tmpXMLStream = sr.ReadToEnd();
				}

				XNamespace xNamespace = containerXml.Attribute("xmlns") != null ? containerXml.Attribute("xmlns").Value : XNamespace.None;
                if (xNamespace != XNamespace.None)
                {
                    return containerXml.Descendants(xNamespace + "rootfile").FirstOrDefault(p => p.Attribute("media-type") != null && p.Attribute("media-type").Value.Equals("application/oebps-package+xml", StringComparison.InvariantCultureIgnoreCase)).Attribute("full-path").Value;
                }
                else
                {
                    //remove odfc namespace prefix and process
                    XDocument xDocument = XDocument.Parse(tmpXMLStream);
                    xDocument.Root.Add(new XAttribute("xmlns", "urn:oasis:names:tc:opendocument:xmlns:container"));
                    xDocument.Root.Attributes(XNamespace.Xmlns + "odfc").Remove();
                    containerXml = XElement.Parse(xDocument.ToString());
                    xNamespace = containerXml.Attribute("xmlns") != null ? containerXml.Attribute("xmlns").Value : XNamespace.None;
                    if (xNamespace != XNamespace.None)
                    {
                        return containerXml.Descendants(xNamespace + "rootfile").FirstOrDefault(p => p.Attribute("media-type") != null && p.Attribute("media-type").Value.Equals("application/oebps-package+xml", StringComparison.InvariantCultureIgnoreCase)).Attribute("full-path").Value;
                    }
                }
			}
			return null;
		}

		private void LoadEpubMetaDataFromOpfFile(string opfFilePath) {
			ZipEntry zipEntry = _EpubFile.Entries.FirstOrDefault(e => e.FileName.Equals(opfFilePath, StringComparison.InvariantCultureIgnoreCase));
			if (zipEntry == null) throw new Exception("Invalid epub file.");

			XElement contentOpf;
			using (MemoryStream memoryStream = new MemoryStream()) {
				zipEntry.Extract(memoryStream);
				memoryStream.Position = 0;
				contentOpf = XElement.Load(memoryStream);
			}

			XNamespace xNamespace = contentOpf.Attribute("xmlns") != null ? contentOpf.Attribute("xmlns").Value : XNamespace.None;

			string uniqueIdentifier = contentOpf.Attribute("unique-identifier").Value;
			UUID = contentOpf.Elements(xNamespace + "metadata").Elements().FirstOrDefault(e => e.Name.LocalName == "identifier" && e.Attribute("id").Value == uniqueIdentifier).Value;
			foreach (var metadataElement in contentOpf.Elements(xNamespace + "metadata").Elements().Where(e => e.Value.Trim() != string.Empty)) {
				switch (metadataElement.Name.LocalName) {
					case "title": Title.Add(metadataElement.Value); break;
					case "creator": Creator.Add(metadataElement.Value); break;
					case "date": 
						var attribute = metadataElement.Attributes().FirstOrDefault(a => a.Name.LocalName == "event");
						if (attribute != null) Date.Add(new DateData(attribute.Value, metadataElement.Value)); 
						break;
					case "publisher": Publisher.Add(metadataElement.Value); break;
					case "subject": Subject.Add(metadataElement.Value); break;
					case "source": Source.Add(metadataElement.Value); break;
					case "rights": Rights.Add(metadataElement.Value); break;
					case "description": Description.Add(metadataElement.Value); break;
					case "contributor": Contributer.Add(metadataElement.Value); break;
					case "type": Type.Add(metadataElement.Value); break;
					case "format": Format.Add(metadataElement.Value); break;
					case "identifier": ID.Add(metadataElement.Value); break;
					case "language": Language.Add(metadataElement.Value); break;
					case "relation": Relation.Add(metadataElement.Value); break;
					case "coverage": Coverage.Add(metadataElement.Value); break;
				}
			}

			LoadManifestSectionFromOpfFile(contentOpf, xNamespace);
		}
   
		private void LoadManifestSectionFromOpfFile(XElement contentOpf, XNamespace xNamespace) {
            //NOTE: with the content.opf file
			//NOTE: grab the idref from the spine element and
			//NOTE: find a match corresponding to the elements listed under the manifest section
			HashSet<string> alreadyProcessedFiles = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			foreach (var spinElement in contentOpf.Elements(xNamespace + "spine").Elements()) {
				var itemElement = contentOpf.Elements(xNamespace + "manifest").Elements().FirstOrDefault(
					                                                                       e =>
					                                                                       e.Attribute("id").Value == spinElement.Attribute("idref").Value);
				if (itemElement == null && Settings.Default.FailIfEpubNotValid)
					throw new Exception("Invalid epub file.");
				else if (itemElement == null) continue; 

				string fileName = HttpUtility.UrlDecode(itemElement.Attribute("href").Value);
				ZipEntry contentZipEntry = _EpubFile.Entries.FirstOrDefault(e => e.FileName.Equals(_ContentOpfPath + fileName, StringComparison.InvariantCultureIgnoreCase));
				if (contentZipEntry == null && Settings.Default.FailIfEpubNotValid) throw new Exception("Invalid epub file.");
				else if (contentZipEntry == null) continue;
                //
                //Developer: Brian Kenney
                //Date: 7/29/2012
                //Change: duplicate dictionary key
                //Details: ran into a mis-packaged epub file added key check 
                //to ensure that we don't crash should someone mispackage
                //
                //check to see if fileName has already been added to Content dictionary
                if (!Content.Contains(fileName)) Content.Add(fileName, new ContentData(fileName, contentZipEntry)); 
				if (!alreadyProcessedFiles.Contains(spinElement.Attribute("idref").Value)) alreadyProcessedFiles.Add(spinElement.Attribute("idref").Value);
			}

            //grab the rest of the elements not already processed in the manifest
			IEnumerable<XElement> manifestElements = contentOpf.Elements(xNamespace + "manifest").Elements().Where(e => !alreadyProcessedFiles.Contains(e.Attribute("id").Value));
			foreach (var manifestElement in manifestElements) {
				string fileName = manifestElement.Attribute("href").Value;
				ZipEntry extendedZipEntry = _EpubFile.Entries.FirstOrDefault(e => e.FileName.Equals(_ContentOpfPath + fileName, StringComparison.InvariantCultureIgnoreCase));
				if (extendedZipEntry == null) continue;
                //check to see if fileName has already been added to Extended dictionary
				string trimmedFileName = GetTrimmedFileName(fileName, true);
				if (!ExtendedData.Contains(trimmedFileName)) ExtendedData.Add(trimmedFileName, new ExtendedData(fileName, manifestElement.Attribute("media-type").Value, extendedZipEntry));
				if (string.Equals(manifestElement.Attribute("media-type").Value, "application/x-dtbncx+xml", StringComparison.InvariantCultureIgnoreCase)) _TocFileName = manifestElement.Attribute("href").Value;
			}
		}

		private static void CollectReplacementLinks(Hashtable linksMapping, string fileName, string text) { 
			MatchCollection matches = _RefsRegex.Matches(text);
			foreach (Match match in matches) {
				if (!_ExternalLinksRegex.IsMatch(match.Groups["href"].Value)) {
					string targetFileName = (GetTrimmedFileName(match.Groups["href"].Value, true) ?? GetTrimmedFileName(fileName, true)) + GetAnchorValue(match.Groups["href"].Value);
					linksMapping[targetFileName] = GetNormalizedSrc(match.Groups["href"].Value);
				}
			}
		}

		private string NormalizeRefs(string text) {
			if (text == null) return null;
			text = _RefsRegex.Replace(text, RefsEvaluator);
			text = Regex.Replace(text, @"(?<prefix>\bid\s*=\s*(""|'))(?<id>[^""']+)", IdsEvaluator, Utils.REO_ci);

			return text;
		}

		private static string RefsEvaluator(Match match) {
			return !_ExternalLinksRegex.IsMatch(match.Groups["href"].Value)
				       ? match.Groups["prefix"].Value + GetNormalizedSrc(match.Groups["href"].Value) + match.Groups["suffix"].Value
				       : match.Value.Insert(match.Value.Length - 2, "target=\"_blank\"");
		}

		private static string GetAnchorValue(string fileName) { 
			var match = Regex.Match(fileName, @"\#(?<anchor>.+)", Utils.REO_c);
			return match.Success? "#" + match.Groups["anchor"].Value : "";
		}

		private string IdsEvaluator(Match match) {
			string originalFileName = GetTrimmedFileName(_CurrentFileName, true) + "#" + match.Groups["id"].Value;
			return _LinksMapping.Contains(originalFileName)? match.Groups["prefix"].Value + ((string)_LinksMapping[originalFileName]).Replace("#", ""): match.Value;
		}

        private void LoadTableOfContents() {
			ExtendedData extendedData = ExtendedData[_TocFileName] as ExtendedData;
			if (extendedData == null) return;

			XElement xElement = XElement.Parse(extendedData.Content);
			XNamespace xNamespace = xElement.Attribute("xmlns") != null ? xElement.Attribute("xmlns").Value : XNamespace.None;
			
            //Developer: Brian Kenney
            //Date: 7/29/2012
            //
            //some files have the namespace prefix of ncx
            //if it does then then xNamespace will evaluate to None
            if (xNamespace != XNamespace.None)
            {
                TOC = GetNavigationChildren(xElement.Element(xNamespace + "navMap").Elements(xNamespace + "navPoint"), xNamespace);
            }
            else
            {
                //Change: Brian Kenney
                //Date: 7/29/2012
                //Change: duplicate dictionary key
                //Details: the file may have an ncx namespace prefix
                //romeve the ncx prefix itself
                XDocument xDocument = XDocument.Parse(@extendedData.Content);
                xDocument.Root.Add(new XAttribute("xmlns", "http://www.daisy.org/z3986/2005/ncx/"));
                xDocument.Root.Attributes(XNamespace.Xmlns + "ncx").Remove();
                xElement = XElement.Parse(xDocument.ToString());
                xNamespace = xElement.Attribute("xmlns") != null ? xElement.Attribute("xmlns").Value : XNamespace.None;
                if (xNamespace != XNamespace.None)
                {
                    TOC = GetNavigationChildren(xElement.Element(xNamespace + "navMap").Elements(xNamespace + "navPoint"), xNamespace);
                }
            }
		}

		private List<NavPoint> GetNavigationChildren(IEnumerable<XElement> elements, XNamespace nameSpace) {
			List<NavPoint> navigationPoints = new List<NavPoint>(elements.Count());
			if (!elements.Any()) return navigationPoints;
			navigationPoints.AddRange(elements.Select(navPoint => 
				new NavPoint(navPoint.Attribute("id").Value,
							navPoint.Element(nameSpace + "navLabel").Element(nameSpace + "text").Value, 
							HttpUtility.UrlDecode(navPoint.Element(nameSpace + "content").Attribute("src").Value),
							int.Parse(navPoint.Attribute("playOrder").Value), Content[NormalizeFileName(HttpUtility.UrlDecode(navPoint.Element(nameSpace + "content").Attribute("src").Value))] as ContentData, 
							GetNavigationChildren(navPoint.Elements(nameSpace + "navPoint"), 
							nameSpace))));
			return navigationPoints;
		}

		private static string NormalizeFileName(string fileName) {
			return fileName == null? null : Regex.Replace(fileName, @"\#.*$", "", Utils.REO_c);
		}

		private string GetTocHtml(List<NavPoint> navPoints) {
			if (navPoints == null || navPoints.Count == 0) return "";
			StringBuilder result = new StringBuilder("<ul>");
			foreach (var navPoint in navPoints) {
				string normalizedNavPointSrc = GetNormalizedSrc(navPoint.Source);
				result.AppendFormat("<li><a href=\"{0}\">{1}</a>", normalizedNavPointSrc, navPoint.Title);
				_LinksMapping[GetTrimmedFileName(navPoint.Source, false)] = normalizedNavPointSrc;
				result.AppendFormat("{0}</li>", GetTocHtml(navPoint.Children));
			}
			result.Append("</ul>");
			return result.ToString();
		}

		private static string GetNormalizedSrc(string originalSrc) {
			string trimmedFileName = GetTrimmedFileName(originalSrc, false);
			return trimmedFileName != null ? "#" + trimmedFileName.Replace('.', '_').Replace('#', '_') : null;
		}

		private static string GetTrimmedFileName(string fileName, bool removeAnchor) {
			Match m = Regex.Match(fileName, @"/?(?<fileName>[^/]+)$", Utils.REO_c);
			if (m.Success) {
				if (removeAnchor) {
					string fileNameWithoutAnchor = Regex.Replace(m.Groups["fileName"].Value, @"\#.*", "", Utils.REO_c);
					return fileNameWithoutAnchor.Trim() != string.Empty? fileNameWithoutAnchor : null;
				} 
				return m.Groups["fileName"].Value;
			}
			return null;
		}

		private string EmbedCssData(string head) {
			return Regex.Replace(head, @"<link\s+[^>]*?(href\s*=\s*(""|')(?<href>[^""']+)(""|')[^>]*?|type\s*=\s*(""|')text/css(""|')[^>]*?){2}[^>]*?/>", CssEvaluator, Utils.REO_ci);
		}

		private string CssEvaluator(Match match) { 
			var extendedData = ExtendedData[GetTrimmedFileName(match.Groups["href"].Value, true)] as ExtendedData;
			return extendedData != null
					   ? string.Format("<style type=\"text/css\">{0}</style>", extendedData.Content) : match.Value;
		}

		private const string _HtmlTemplate = @"<!DOCTYPE html
			  PUBLIC ""-//W3C//DTD XHTML 1.1//EN"" ""http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd"">
			<html>
			   <head>
				  <title>{0}</title>
				  {1}
			   </head>
			   {2}
				  {3}
			   </body>
			</html>";
		#endregion
	}
}
