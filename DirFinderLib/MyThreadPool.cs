using System;
using System.Threading;

namespace DirFinderLib
{
    public class MyThreadPool
    {
        private static readonly object Locker = new();
        private static readonly int TotalThreads;
        private static int _files;

        static MyThreadPool()
        {
            TotalThreads = Count = Environment.ProcessorCount;
        }

        public static int Count { get; private set; }

        public static int FilesCounter
        {
            get => _files;
            set
            {
                lock (Locker)
                {
                    _files = value;
                }
            }
        }

        public static void ReleaseThread()
        {
            lock (Locker)
            {
                if (Count < TotalThreads) Count++;
                Console.WriteLine("     Thread is released");
            }
        }

        public static void SuppressThread(ThreadStart func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (Count > 0)
            {
                lock (Locker)
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
    }
}