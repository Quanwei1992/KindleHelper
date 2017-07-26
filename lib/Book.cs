using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libZhuishu;
using System.Runtime.Serialization;
namespace KindleHelper
{
    public class Book 
    {   
        /// <summary>
        /// 标识号
        /// </summary>
        public string id;   
        /// <summary>
        /// 名称
        /// </summary>
        public string name;  
        /// <summary>
        /// 作者
        /// </summary>
        public string author;
        /// <summary>
        /// 内容,epub需要此信息,其余情况留空
        /// </summary>
        public string content_plaintext;
        /// <summary>
        /// 章节信息
        /// </summary>
        public ChapterInfo[] chapters;
    }
}
