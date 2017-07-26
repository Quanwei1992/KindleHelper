using System.Collections.Generic;

namespace eBdb.EpubReader {
	public class NavPoint{
		public string ID { get; private set; }
		public string Title { get; private set; }
		public string Source { get; private set; }
		public int Order { get; private set; }
		public ContentData ContentData { get; private set; }
		public List<NavPoint> Children { get; private set; }
		public NavPoint(string id, string title, string source, int order, ContentData contentData, List<NavPoint> children) {
			ID = id;
			Title = title;
			Source = source;
			Order = order;
			ContentData = contentData;
			Children = children;
		}
	}
}
