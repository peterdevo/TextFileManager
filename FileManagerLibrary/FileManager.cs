using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManagerLibrary
{
    public static class FileManager
    {
        public static Dictionary<string, string> Files { get; set; } = new Dictionary<string, string>();

        public static void ReadFile(string input)
        {
            bool exists = File.Exists(input);

            if (Path.GetExtension(input) != ".txt")
            {
                throw new InvalidOperationException("Incorrect file type");
            }

            if (exists)
                Files.Add(input, File.ReadAllText(input));
            
            else
                throw new FileNotFoundException("File not found!");
        }



        private static void SplitText(string text)
        {
            List<string> temp = new List<string>();
            temp = text.Split(' ').Select(x => x.Trim(',', '.', '-', '?', '!')).ToList();
        }
    }
}
