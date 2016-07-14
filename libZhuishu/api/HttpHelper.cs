using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Drawing;
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

            var request = WebRequest.CreateHttp(url + param);
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



        public static void AsyncGET(Action<string> callback, string url, params KeyValuePair<string, string>[] args)
        {

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
