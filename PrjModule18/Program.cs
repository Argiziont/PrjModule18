using System;
using System.Diagnostics;
using System.Threading;
using DirFinderLib;

namespace PrjModule18
{
    internal static class Program
    {
        private static readonly MyThreadPool ThreadPool = new(Environment.ProcessorCount);

        //Thread myThread;  
        private static void Main()
        {
            var stopwatch = Stopwatch.StartNew();
            MultiDirFinder.CountDirs(@"C:\Users\Argiziont\Downloads\Westworld");


           //while (MyThreadPool.Count <= Environment.ProcessorCount) Console.WriteLine($"Threads: {MyThreadPool.Count}");

            stopwatch.Stop();
            Console.WriteLine($"Total threads count: {Process.GetCurrentProcess().Threads.Count}");
            Console.WriteLine($"Files found: {MyThreadPool.FilesCounter}");
            Console.WriteLine($"Total time: {stopwatch.ElapsedMilliseconds}");
        }
    }
}