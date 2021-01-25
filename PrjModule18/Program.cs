using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PrjModule18
{
    internal static class Program
    {
        static Semaphore sem = new Semaphore(Environment.ProcessorCount, Environment.ProcessorCount);

        private static int FilesFound = 0;
        //Thread myThread;  
        private static void Main()
        {
            CountDirs(@"C:\Users\Argiziont\downloads");
            Console.WriteLine(FilesFound);
        }

        //Counts total Files
        private static void CountFiles(string dir)
        {
            //Files in given directory
            sem.WaitOne();
            //Internal directories in current
            var totalInternalDirs = GetDirectories(dir);

            if (totalInternalDirs != null)
                foreach (var internalDir in totalInternalDirs)
                {
                    CountDirs(internalDir);
                }


            //Get Files in current directory
            var filesInCurrentDir = GetFiles(dir);
            if (filesInCurrentDir != null)
            {
                foreach (var file in filesInCurrentDir)
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId+"    "+file);
                }
            }
            //If Files in current directory exist add them to total files list

            sem.Release();
        }

        //Counts total Directories with creation thread
        private static void CountDirs(string path)
        {
            //There isn't no internal dirs in current
            var totalInternalDirs = GetDirectories(path);
            if (totalInternalDirs == null) return;
            foreach (var subDir in totalInternalDirs)
            {
                //Decide who will search in internal directories 
                var dirThread = new Thread(() => CountFiles(subDir));
                dirThread.Start();
            }
        }

        //Misc methods
        private static IEnumerable<string> GetFiles(string currentDir)
        {
            string[] files;
            try
            {
                files = Directory.GetFiles(currentDir);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return files;
        }
        private static IEnumerable<string> GetDirectories(string currentDir)
        {
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(currentDir);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return directories;
        }

    }
}
