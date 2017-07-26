using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
namespace libZhuishu
{
    public class HttpHelper
    {
        public static string GET(string url, params KeyValuePair<string, string>[] args)
        {
            string param = "";
            if (args.Length > 0) {
                param = "?";
                foreach (var arg in args) {
                    param += arg.Key + "=" + arg.Value + "&";
                }
            }
            var request = creathttp.CreateHttp(url + param);
            request.Method = "GET";
            request.ProtocolVersion = new Version(1, 1);
            request.UserAgent = "YouShaQi/2.25.2 (iPhone; iOS 9.3.2; Scale/2.00)";
            //request.Headers.Add("X-Device-Id", "631cf212b409f949264fad9ba1ba1daa");
            var response = request.GetResponse();
            var repStream = response.GetResponseStream();
            var reader = new StreamReader(repStream);
            var result = reader.ReadToEnd();
            reader.Close();
            return result;
        }
        public static string GET(string url,object o,params Dictionary<string, string>[] args)
        {
            KeyValuePair<string, string>[] pair = new KeyValuePair<string, string>[] { };
            for (int i = 0; i < pair.Length; i++)
            {
                foreach (var item in args)
                {
                    foreach (var item2 in item)
                    {
                        pair[i] = item2;
                    }
                }
            }
            return GET(url, pair);
        }
        public static JObject GET_JsonObject(string url, params KeyValuePair<string, string>[] args)
        {
            string result = GET(url, args);
            JObject obj = JObject.Parse(result);
            return obj;
        }
        public static JArray GET_JsonArray(string url, params KeyValuePair<string, string>[] args)
        {
            string result = GET(url, args);
            JArray array = JArray.Parse(result);
            return array;
        }       
    }
}
