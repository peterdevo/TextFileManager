using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManagerLibrary
{
    public static class FileManager
    {
        public static Dictionary<string, List<string>> Files { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Reads a file from the specified filepath, splits it into words, instead of one string, and saves it to the 'Files' Property
        /// </summary>
        /// <param name="filePath"></param>
        public static void ReadFile(string filePath)
        {
            bool exists = File.Exists(filePath);

            if (Path.GetExtension(filePath) != ".txt")
                throw new InvalidOperationException("Incorrect file type!");

            if (exists)
                AddWordsToCollection(filePath, File.ReadAllText(filePath));
            else
                throw new FileNotFoundException("File not found!");
        }

        /// <summary>
        /// Takes an already read file from the 'Files' Property and sorts it
        /// </summary>
        /// <param name="fileName"></param>
        public static void SortCollection(string fileName)
        {
            if (File.ReadAllText(fileName) == string.Empty)
                throw new InvalidOperationException("File is empty!");

            var collection = Files[fileName];

            QuickSort<string>.Sort(collection);
        }

        /// <summary>
        /// Finds all occurrences of a key amongst all loaded files
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns></returns>
        public static string[] WordOccurrences(string key)
        {
            if (key.Contains(" "))
                return null;

            int total = 0, max = 0;
            string maxFilePath = string.Empty;

            foreach (var item in Files)
            {
                if (item.Value.Count < 1)
                    continue;

                var sortedList = new List<string>(item.Value);

                QuickSort<string>.Sort(sortedList);
                
                int occurrences = sortedList.CountOccurencesOf(key);
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
        public static void SaveFile(string filePath)
        {
            StringBuilder textToSave = new StringBuilder();

            foreach (var item in FileManager.Files[filePath])
            {
                textToSave.Append(item + " ");
            }

            string modifiedFilePath = Path.GetFullPath(filePath.Substring(0, filePath.Length - 4) + "_Modified.txt");

            File.WriteAllText(modifiedFilePath, textToSave.ToString());
        }

        /// <summary>
        /// Splits all the words in a string and saves them in the 'Files' Property along with the filepath
        /// </summary>
        /// <param name="filePath">Filepath that the text comes from</param>
        /// <param name="text">All the text from the file</param>
        private static void AddWordsToCollection(string filePath, string text)
        {
            Files.Add(filePath, SplitText(text));
        }

        /// <summary>
        /// Separates all characters in a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static List<string> SplitText(string text)
        {
            return text.Split(
                new[] { ' ', ',', '.', '?', '!', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries
                ).ToList();
        }


    }
}
