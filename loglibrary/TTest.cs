using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loglibrary
{
    public static class TTest
    {
        public static void Test()
        {
            try
            {
                throw new ApplicationException("Test",new OutOfMemoryException("Test"));
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                LogHelper.Flush();
            }
        }
        public static void InfoTest()
        {
            LogHelper.Info("Test");
            LogHelper.Flush();
        }
    }
}
