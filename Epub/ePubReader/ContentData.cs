using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace eBdb.EpubReader {
	public class ContentData {
		private readonly ZipEntry _ZipEntry;
		public string FileName { get; private set; }
		public string Content {
			get {
				using (MemoryStream memoryStream = new MemoryStream()) {
					_ZipEntry.Extract(memoryStream);
					memoryStream.Position = 0;

					using (StreamReader reader = new StreamReader(memoryStream)) return reader.ReadToEnd();
				}
			}
		}

		public ContentData(string fileName, ZipEntry zipEntry) {
			FileName = fileName;
			_ZipEntry = zipEntry;
		}

		public string GetContentAsPlainText() {
			Match m = Regex.Match(Content, @"<body[^>]*>.+</body>", Utils.REO_csi);
			return m.Success ? Utils.ClearText(m.Value) : "";
		}
	}
}
