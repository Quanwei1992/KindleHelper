using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KindleHelper.lib
{
    public interface IKindlegen
    { 
        /// <summary>
        /// 将下载的书转换为txt，外部可调用此方法获取txt文件
        /// </summary>
        /// <param name="book">书的对象</param>
        /// <param name="savePath">保存路径</param>
        void Book2Txt(Book book, string savePath);
        /// <summary>
        /// 将下载的书转换为mobi，外部可调用此方法获取mobi文件
        /// </summary>
        /// <param name="book">书的对象</param>
        /// <param name="savePath">保存路径</param>
        void Book2Mobi(Book book, string savePath);
    }
}
