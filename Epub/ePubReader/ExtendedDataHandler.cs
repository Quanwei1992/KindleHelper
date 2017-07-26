using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace eBdb.EpubReader {
	public class ExtendedDataHandler : IHttpHandler {
		public void ProcessRequest(HttpContext context) {
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;

			//Simple protection from external domain requests.
			if (request.UrlReferrer != null && !string.Equals(request.Url.Host, request.UrlReferrer.Host, StringComparison.InvariantCultureIgnoreCase)) {
				response.Write("<html>\r\n");
				response.Write("<head><title>Access Denied</title></head>\r\n");
				response.Write("<body>\r\n");
				response.Write("<h1>Access Denied</h1>\r\n");
				response.Write("</body>\r\n");
				response.Write("</html>");
				return;
			}

			if (request.QueryString["epub"] == null || request.QueryString["epub"].Trim() == string.Empty) throw new FileNotFoundException();
			Epub epub = new Epub(ConfigurationManager.AppSettings["EpubFilesPath"] + request.QueryString["epub"]);

			ExtendedData extendedData = epub.ExtendedData[request.QueryString["filePath"]] as ExtendedData;
			if (extendedData == null) return;
			response.ContentType = extendedData.MimeType;
			response.BinaryWrite(extendedData.GetContentAsBinary());
		}

		public bool IsReusable
		{
			get { return true; }
		}
	}
}
