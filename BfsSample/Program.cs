using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BfsSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Test("F:\\");

            sw.Stop();

            Console.SetCursorPosition(0, 10);
            Console.WriteLine($"耗时:{sw.ElapsedMilliseconds}毫秒,file:{_fileCount}个,dir:{_dirCount}个");

            Console.ReadLine();
        }

        private static void Test(string root)
        {
            Queue<string> visited = new Queue<string>();
            visited.Enqueue(root);

            while (visited.Count > 0)
            {
                DirectoryInfo dir = new DirectoryInfo(visited.Dequeue());
                Process(dir.FullName, false); //Process

                DirectorySecurity ds = new DirectorySecurity(dir.FullName, AccessControlSections.Access);
                if (!ds.AreAccessRulesProtected)
                {
                    foreach (DirectoryInfo item in dir.GetDirectories())
                    {
                        visited.Enqueue(item.FullName);
                    }

                    foreach (FileInfo item in dir.GetFiles())
                    {
                        Process(item.FullName, true); //Process
                    }
                }
            }
        }

        private static int _fileCount = 0;
        private static int _dirCount = 0;

        private static void Process(string path, bool file)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{path}\t{file}");

            if (file)
            {
                _fileCount++;
            }
            else
            {
                _dirCount++;
            }
        }
    }
}