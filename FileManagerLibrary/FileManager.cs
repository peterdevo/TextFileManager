using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManagerLibrary
{
    public static class FileManager
    {
        public static Dictionary<string, List<string>> Files { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Reads a file from the specified filepath, splits it into words, instead of one string, and saves it to the 'Files' Dictionary
        /// </summary>
        /// <param name="filePath"></param>
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

        public static void SortCollection(string fileName)
        {
            var collection = Files[fileName];

            QuickSort<string>.SortQuick(ref collection, 0, collection.Count - 1);
        }

        public static string[] WordOccurrences(string word)
        {
            int total = 0, max = 0;
            string maxFilePath = string.Empty;

            foreach (var item in Files)
            {
                int occurrences = item.Value.CountOccurencesOf(word);
                total += occurrences;

                if (occurrences > max)
                {
                    max = occurrences;
                    maxFilePath = item.Key;
                }
                    
            }

            return new string[] { total.ToString(), maxFilePath, max.ToString() };
        }

        /// <summary>
        /// Saves a string to a textfile
        /// </summary>
        /// <param name="filePath">Path to save the provided string at</param>
        /// <param name="textToSave">Text to save as a text file</param>
        public static void SaveFile(string filePath, string textToSave)
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
                new[] { ' ', ',', '.', '-', '?', '!', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries
                ).ToList();
        }


    }
}
