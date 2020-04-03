using System;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManagerLibrary;
using System.Diagnostics;
using System.Text;

namespace FileManagerApp
{
    class Program
    {
         
        static void Main(string[] args)
        {
            while (true)
            {
                int choice = 0;
                string filePath;

                Console.Clear();
                Console.Write("1. Add File \n2. Sort Words Alphabetically \n3. Search for a specific word \n4. View file \n5. Save File \n6. Exit Application \nChoice: ");
                if (int.TryParse(Console.ReadLine(), out int res))
                    choice = res;
                else
                    continue;

                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter filepath: ");

                        try
                        {
                            FileManager.ReadFile(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message); Thread.Sleep(1000);
                        }

                        break;

                    case 2:
                        filePath = GetSelectedFile();
                        
                        if (filePath == null)
                            break;

                        FileManager.SortCollection(filePath);
                        Console.WriteLine("Sorted!"); Thread.Sleep(1000);
                        break;

                    case 3:
                        Console.WriteLine("Specify a word to search for: ");
                        string word = Console.ReadLine();
                        
                        var result = FileManager.WordOccurrences(word);

                        if (result == null)
                        {
                            Console.WriteLine("One word only."); Thread.Sleep(1000);
                            break;
                        }
                            
                        Console.Clear();
                        Console.WriteLine("Total Occurrences: {0} \nFile with highest accuracy: {1} -> {2}", result[0], Path.GetFileName(result[1]), result[2]);

                        Console.WriteLine("Press any key to return to menu");
                        Console.ReadKey();

                        break;

                    case 4:
                        filePath = GetSelectedFile();

                        if (filePath == null)
                            break;

                        foreach (var item in FileManager.Files[filePath])
                        {
                            Console.Write(item + " ");
                        }

                        Console.WriteLine("Press any key to return to menu");
                        Console.ReadKey();

                        break;

                    case 5:
                        filePath = GetSelectedFile();

                        if (filePath == null)
                            break;

                        StringBuilder textToSave = new StringBuilder();

                        foreach (var item in FileManager.Files[filePath])
                        {
                            textToSave.Append(item + " ");
                        }

                        FileManager.SaveFile(filePath, textToSave.ToString());

                        break;

                    case 6:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }

        private static string GetSelectedFile()
        {
            string[] filePath = new string[FileManager.Files.Count];
            int count = 1;
            foreach (var item in FileManager.Files)
            {
                filePath[count - 1] = item.Key;
                Console.WriteLine(count + ". " + Path.GetFileNameWithoutExtension(item.Key));
                count++;
            }

            Console.WriteLine("Enter choice: ");
            if (int.TryParse(Console.ReadLine(), out int res) && res <= filePath.Length && res > 0)
                return filePath[res - 1];

            else
                return null;
        }
    }
}
