using System;
using System.Diagnostics;
using System.Threading;
using DirFinderLib;

namespace PrjModule18
{
    internal static class Program
    {
        //Thread myThread;  
        private static void Main()
        {
            var stopwatch = Stopwatch.StartNew();
            MultiDirFinder.CountDirs(@"C:\Users\Argiziont\Downloads\Westworld");
            Thread.Sleep(1000);
            stopwatch.Stop();

        }
    }
}