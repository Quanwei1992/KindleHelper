using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using KindleHelper.Plugin.Interface;

namespace KindleHelper.lib
{
    /// <summary>
    /// 插件加载类，项目中第三个核心类
    /// </summary>
    internal class PluginHelper
    {
        /// <summary>
        /// 查找所有插件的路径
        /// </summary>
        /// <returns>包含所有插件路径的列表</returns>
        internal List<string> FindPlugin()
        {
            List<string> pluginpath = new List<string>();
            try
            {
                //获取程序的基目录
                string path = AppDomain.CurrentDomain.BaseDirectory;
                //合并路径，指向插件所在目录。
                path = Path.Combine(path, "plugin");
                foreach (string filename in Directory.GetFiles(path, "*.dll"))
                {
                    pluginpath.Add(filename);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return pluginpath;
        }
        /// <summary>
        /// 载入插件，在Assembly中查找类型
        /// </summary>
        /// <param name="asm">一个程序集对象</param>
        /// <param name="className">类的名称</param>
        /// <param name="interfacename">接口名称</param>
        /// <param name="param">参数</param>
        /// <returns>载入的插件对象</returns>
        internal object LoadObject(Assembly asm, string className, string interfacename, object[] param)
        {
            try
            {
                //取得className的类型
                Type t = asm.GetType(className);
                if (t == null
                    || !t.IsClass
                    || !t.IsPublic
                    || t.IsAbstract
                    || t.GetInterface(interfacename) == null
                   )
                {
                    return null;
                }
                //创建对象
                Object o = Activator.CreateInstance(t, param);
                if (o == null)
                {
                    //创建失败，返回null
                    return null;
                }
                return o;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 移除无效的的插件，返回正确的插件路径列表
        /// </summary>
        /// <param name="PlunginPath">插件路径</param>
        /// <returns></returns>
        internal List<string> DeleteInvalidPlungin(List<string> PlunginPath)
        {
            string interfacename = typeof(IPlugin).FullName;
            List<string> rightPluginPath = new List<string>();
            //遍历所有插件。
            foreach (string filename in PlunginPath)
            {
                try
                {
                    Assembly asm = Assembly.LoadFile(filename);
                    //遍历导出插件的类。
                    foreach (Type t in asm.GetExportedTypes())
                    {
                        //查找指定接口
                        Object plugin = LoadObject(asm, t.FullName, interfacename, null);
                        //如果找到，将插件路径添加到rightPluginPath列表里，并结束循环。
                        if (plugin != null)
                        {
                            rightPluginPath.Add(filename);
                            break;
                        }
                    }
                }
                catch
                {
                    throw new Exception(filename + "不是有效插件");
                }
            }
            return rightPluginPath;
        }
    }
}
