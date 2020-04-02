using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManagerLibrary
{
    public static class FileManager
    {
        public static Dictionary<string, List<string>> Files { get; set; } = new Dictionary<string, List<string>>();

        public static void ReadFile(string filePath)
        {
            bool exists = File.Exists(filePath);

            if (Path.GetExtension(filePath) != ".txt")
            {
                throw new InvalidOperationException("Incorrect file type!");
            }

            if (exists)
                AddWordsToCollection(filePath, File.ReadAllText(filePath));

            else
                throw new FileNotFoundException("File not found!");
        }

        public static void SaveFile(string textToSave, string filePath) 
        {
            string modifiedFilePath = Path.GetFullPath(filePath.Substring(0, filePath.Length - 4) + "_Modified.txt");

            File.WriteAllText(modifiedFilePath, textToSave);
        }

        private static void AddWordsToCollection(string filePath, string text)
        {
            Files.Add(filePath, SplitText(text));
        }

        private static List<string> SplitText(string text)
        {
            return text.Split(
                new[] { ' ', ',', '.', '-', '?', '!', '\n', '\r', '\r\n' }
                ).ToList();
        }
    }
}
