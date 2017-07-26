using System;
using System.IO;
using Ionic.Zip;

namespace eBdb.EpubReader {
	public class ExtendedData {
		private readonly ZipEntry _ExtendedZipEntry;
		public string FileName { get; private set; }
		public string Content {
			get {
				using (MemoryStream memoryStream = new MemoryStream()) {
					_ExtendedZipEntry.Extract(memoryStream);
					memoryStream.Position = 0;

					if (IsText) using (StreamReader reader = new StreamReader(memoryStream)) return reader.ReadToEnd();
					else return Convert.ToBase64String(GetContentAsBinary());
				}
			}
		}
		public bool IsText {
			get { return _ExtendedZipEntry.IsText || Path.GetExtension(FileName).Equals(".ncx", StringComparison.InvariantCultureIgnoreCase) || Path.GetExtension(FileName).Equals(".css", StringComparison.InvariantCultureIgnoreCase); }
		}
		public string MimeType { get; private set; }

		public ExtendedData(string fileName, string mimeType, ZipEntry extendedZipEntry) {
			FileName = fileName;
			MimeType = mimeType;
			_ExtendedZipEntry = extendedZipEntry;
		}

		public byte[] GetContentAsBinary() {
			using (MemoryStream memoryStream = new MemoryStream()) {
				_ExtendedZipEntry.Extract(memoryStream);
				memoryStream.Position = 0;
				return memoryStream.ToArray();
			}
		}
	}
}
