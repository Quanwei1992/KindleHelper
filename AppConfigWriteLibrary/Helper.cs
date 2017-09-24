using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
namespace AppConfigWriteLibrary
{
    public class Helper : IHelper
    {
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public string ReadConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="Key">节点的键</param>
        /// <param name="Value">节点的值</param>
        public void SaveConfig(string Key, string Value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            // string  strFileName= AppDomain.CurrentDomain.BaseDirectory + "\\exe.config";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att.Value == Key)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["value"];
                    att.Value = Value;
                    break;
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}

