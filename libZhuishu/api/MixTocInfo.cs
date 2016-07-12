using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libZhuishu
{

    public class tocChaperInfo
    {
        public string title;
        public string link;
        public bool unreadble;
    }

    public class MixTocInfo
    {
        public string _id;
        public string book;
        public string chaptersUpdated;
        public string updated;
        public tocChaperInfo[] chapters;
    }
}
