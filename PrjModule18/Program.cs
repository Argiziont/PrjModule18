using System;
using System.Diagnostics;
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

            stopwatch.Stop();
            Console.WriteLine($"Treads number: {Process.GetCurrentProcess().Threads.Count}");
            Console.WriteLine($"Tread time work {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"Files number {MultiDirFinder.DirsNumber}");
        }
    }
}