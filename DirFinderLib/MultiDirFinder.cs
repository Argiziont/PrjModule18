using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DirFinderLib
{
    public static class MultiDirFinder
    {
        public static int DirsNumber;
        //Counts total Directories with creation thread
        public static void CountDirs(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            if (!Directory.Exists(path))
                throw new ArgumentException($"Dir {path} doesn't exist");

            //There isn't no internal dirs in current
            var totalInternalDirs = GetDirectories(path);
            if (totalInternalDirs == null) return;
            var tasks= totalInternalDirs.Select(subDir => Task.Factory.StartNew(() => CountFiles(subDir))).ToList();
            Task.WaitAll(tasks.ToArray());
            //dirThread.Start();
        }

        //Counts total Files
        private static void CountFiles(string dir)
        {
            //Files in given directory
            //Internal directories in current
            var totalInternalDirs = GetDirectories(dir);

            if (totalInternalDirs != null)
                foreach (var internalDir in totalInternalDirs)
                    CountDirs(internalDir);


            //Get Files in current directory
            var filesInCurrentDir = GetFiles(dir);
            if (filesInCurrentDir == null) return;

            var tasks = filesInCurrentDir.Select(file => Task.Factory.StartNew(() =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "    " + file);
                DirsNumber++;
            })).ToList();
            Task.WaitAll(tasks.ToArray());
            //If Files in current directory exist add them to total files list
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