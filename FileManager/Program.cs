using System;
using System.IO;
using System.Reflection;
using System.Threading;
using FileManagerLibrary;

namespace FileManagerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int choice = 0;

                Console.Clear();
                Console.WriteLine("1. Add File \n2. Sort Words Alphabetically \n3. Search for a specific word \n4. Save File \n5.Exit Application ");
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

                        break;

                    case 3:
                        break;

                    case 4:
                        int count = 1;
                        foreach (var item in FileManager.Files)
                        {
                            Console.WriteLine(count + ". " + Path.GetFileNameWithoutExtension(item.Key));
                            count++;
                        }



                        break;

                    case 5:
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
