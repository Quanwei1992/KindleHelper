using libZhuishu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace KindleHelper
{
    public class BookDownloader
    {


        public class ChapterDownloadContext
        {
            public string title;
            public string content;
            public List<string> links = new List<string>();
        }


        private string mBookID;

        public BookDownloader(string id)
        {
            mBookID = id;
        }


        List<ChapterDownloadContext> mChapters = new List<ChapterDownloadContext>();
        Queue<ChapterDownloadContext> mChaptersDownloadQueue = new Queue<ChapterDownloadContext>();
        private object _lock_obj = new object();
        int mChaptersDownloadComplteCount = 0;
        List<Thread> mWorkThreads = new List<Thread>();
        public void StartDownload()
        {
            // 获得章节列表和所有书源
            var mixTocInfo = LibZhuiShu.getMixToc(mBookID);
            // 获得所有书源
            var tocs = LibZhuiShu.getTocSummary(mBookID);
            List<TocChaperListInfo> tocChaperListInfoList = new List<TocChaperListInfo>();
            foreach (var toc in tocs)
            {
                if (toc.name == "优质书源") continue;
                var tocChaperList = LibZhuiShu.getChaperList(toc._id);
                tocChaperListInfoList.Add(tocChaperList);
            }
            foreach (var chapter in mixTocInfo.chapters) {
                ChapterDownloadContext context = new ChapterDownloadContext();
                context.title = chapter.title;
                context.links.Add(chapter.link);
                foreach (var tocChaterListInfo in tocChaperListInfoList) {
                    foreach (var tocChapter in tocChaterListInfo.chapters){
                        if (tocChapter.title.Replace(" ","").ToLower() == context.title.Replace(" ", "").ToLower()) {
                            if (!context.links.Contains(tocChapter.link)) {
                                context.links.Add(tocChapter.link);
                            }         
                            break;
                        }
                    }
                }
                mChapters.Add(context);
                mChaptersDownloadQueue.Enqueue(context);
            }

            for (int i = 0; i < 10; i++) {
                Thread workThread = new Thread(DownLoadThread);
                mWorkThreads.Add(workThread);
                workThread.Start();
            }

        }

        void DownLoadThread()
        {
            System.Diagnostics.Debug.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " Start");
            while (mChaptersDownloadQueue.Count > 0)
            {
                ChapterDownloadContext context = null;
                lock (mChaptersDownloadQueue) {
                    context = mChaptersDownloadQueue.Dequeue();
                }

                // 每个源尝试下载3次
                foreach (var link in context.links) {
                    for (int i = 0; i < 3; i++) {
                        System.Diagnostics.Debug.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " " + context.title + " " + link);
                        try
                        {
                            context.content = LibZhuiShu.getChapter(link).body;
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    if (!string.IsNullOrEmpty(context.content))
                    {
                        break;
                    }
                    
                }

                if (!string.IsNullOrEmpty(context.content))
                {
                    System.Diagnostics.Debug.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " complate " + context.title);
                    lock (_lock_obj)
                    {
                        mChaptersDownloadComplteCount++;
                    }
                }
                else {
                    // 下载失败
                    OnFail();
                    return;
                }

            }
            System.Diagnostics.Debug.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " Quit");
        }

        void OnFail()
        {
            foreach (var workThread in mWorkThreads)
            {
                workThread.Abort();
            }

            System.Diagnostics.Debug.WriteLine("download failed!");
        }

    }
}
