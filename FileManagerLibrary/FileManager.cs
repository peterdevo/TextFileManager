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
        /// Reads a file from the specified filepath, splits it into words, instead of one string, and saves it to the 'Files' Property
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


        /// <summary>
        /// Finds all occurrences of a word amongst all loaded files
        /// </summary>
        /// <param name="word">The word to search for</param>
        /// <returns></returns>
        public static string[] WordOccurrences(string word)
        {
            if (word.Contains(" "))
                return null;

            int total = 0, max = 0;
            string maxFilePath = string.Empty;
            

            foreach (var item in Files)
            {
                var sortedList = item.Value;
                QuickSort<string>.SortQuick(ref sortedList, 0, sortedList.Count - 1);
                
                int occurrences = sortedList.CountOccurencesOf(word);
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
        /// Separates all words in a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static List<string> SplitText(string text)
        {
            return text.Split(
                new[] { ' ', ',', '.', '-', '?', '!', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries
                ).ToList();
        }


    }
}
