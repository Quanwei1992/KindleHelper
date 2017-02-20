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
        /// <summary>
        /// 搜索小说
        /// </summary>
        /// <param name="query">关键词</param>
        /// <param name="start">结果开始Index</param>
        /// <param name="limit">结果数量限制</param>
        /// <returns></returns>

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

        /// <summary>
        /// 获得混合书源
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static MixTocInfo getMixToc(string bookid)
        {
            string host = string.Format("http://api.zhuishushenqi.com/mix-toc/{0}",bookid);
            var ret = HttpHelper.GET_JsonObject(host);
            var ok = ret["ok"].ToObject<bool>();
            if (ok) {
                var info = ret["mixToc"].ToObject<MixTocInfo>();
                return info;
            }
            return null;
        }

        /// <summary>
        /// 获得书源
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static TocSummmaryInfo[] getTocSummary(string bookid)
        {
            string host = string.Format("http://api.zhuishushenqi.com/toc?view=summary&book=" + bookid);
            var ret = HttpHelper.GET_JsonArray(host);
            List<TocSummmaryInfo> infoList = new List<TocSummmaryInfo>();
            foreach (var summray in ret) {
                TocSummmaryInfo info = summray.ToObject<TocSummmaryInfo>();
                if (info.name != "优质书源") {
                    infoList.Add(info);
                }       
            }
            return infoList.ToArray();
        }

        /// <summary>
        /// 获得章节列表
        /// </summary>
        /// <param name="tocid">书源ID</param>
        /// <returns></returns>

        public static TocChaperListInfo getChaperList(string tocid)
        {
            string host = string.Format("http://api.zhuishushenqi.com/toc/{0}?view=chapters",tocid);
            var ret = HttpHelper.GET_JsonObject(host);
            return ret.ToObject<TocChaperListInfo>();
        }
        /// <summary>
        /// 获得章节内容
        /// </summary>
        /// <param name="link">章节列表中的Link</param>
        /// <returns></returns>
        public static ChapterInfo getChapter(string chaperLink)
        {
            int timestamp = ConvertDateTimeInt(DateTime.Now);
            chaperLink = System.Web.HttpUtility.UrlEncode(chaperLink, Encoding.UTF8);
            string host = string.Format("http://chapter2.zhuishushenqi.com/chapter/{0}?k=2124b73d7e2e1945&t={1}", chaperLink,timestamp);
            var ret = HttpHelper.GET_JsonObject(host);
            var ok = ret["ok"].ToObject<bool>();
            if (ok) {
                var info = ret["chapter"].ToObject<ChapterInfo>();
                return info;
            }
            return null;
        }

        public static string[] autoComplate(string query)
        {
            string[] words = new string[0];
            string host = string.Format("http://api.zhuishushenqi.com/book/auto-complete?query={0}", query);
            var ret = HttpHelper.GET_JsonObject(host);
            
            var ok = ret["ok"].ToObject<bool>();
            if (ok) {
                var keywords = ret["keywords"];
                words = keywords.ToObject<string[]>();
            }
            return words;
        }




        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        private static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
