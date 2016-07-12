using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libZhuishu
{
    public class LibZhuiShu
    {

        public static QueryBookInfo[] fuzzySearch(string query, int start, int limit)
        {
            List<QueryBookInfo> bookList = new List<QueryBookInfo>();
            string host = "http://api.zhuishushenqi.com/book/fuzzy-search";
            var param_query = new KeyValuePair<string, string>("query", query);
            var param_start = new KeyValuePair<string, string>("start", start.ToString());
            var param_limit = new KeyValuePair<string, string>("limit", limit.ToString());
            var ret = HttpHelper.GET_JsonObject(host, param_query, param_start, param_limit);
            var ok = ret["ok"].ToObject<bool>();
            if (ok) {
                var books = ret["books"];
                foreach (var book in books) {
                    QueryBookInfo bookInfo = book.ToObject<QueryBookInfo>();
                    bookList.Add(bookInfo);
                }
            }
            return bookList.ToArray();
        }

        public static MixTocInfo getMixToc(string bookid)
        {
            string host = string.Format("http://api.zhuishushenqi.com/mix-toc/{0}",bookid);
            var ret = HttpHelper.GET_JsonObject(host);
            var ok = ret["ok"].ToObject<bool>();
            if (ok) {
                var info = ret.ToObject<MixTocInfo>();
                return info;
            }
            return null;
        }

        public static string getTocSummary(string bookid)
        {
            string host = string.Format("http://api.zhuishushenqi.com/toc?view=summary&book=" + bookid);
            var ret = HttpHelper.GET_JsonArray(host);
            return ret.ToString();
        }
    }
}
