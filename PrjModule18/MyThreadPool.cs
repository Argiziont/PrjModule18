using System;
using System.Threading;

namespace PrjModule18
{
    internal class MyThreadPool
    {
        public static int Count { get; private set; }
        public int FilesCounter
        {
            get => _files;
            set {
                lock (_locker)
                {
                    _files = value;
                }
            } }
        
        private static readonly object _locker = new object();
        private static int _files=0;
        public static void ReleaseThread()
        {
            lock (_locker)
            {
                Count++;
                Console.WriteLine("     Thread is released");
            }
        }

        public static void SuppressThread(ThreadStart func)
        {

            if (Count > 0)
            {
                lock (_locker)
                {
                    Count--;
                    Console.WriteLine("     Thread is suppressed");
                }
                
                var childThread = new Thread(func);
                childThread.Start();
            }
            else
            {
                func.Invoke();
            }

        }

        public MyThreadPool(int count)
        {
            Count = count;
        }


    }
}
