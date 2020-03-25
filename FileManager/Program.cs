using System;
using FileManagerLibrary;

namespace FileManagerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileManager = new FileManager();
            fileManager.ReadFile(@"C:\read\test.txt");
            foreach(var word in fileManager.Words)
            {
                Console.WriteLine(word);
            }
        }
    }
}
