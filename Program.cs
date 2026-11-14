using System;
using System.IO;
using System.Security.Cryptography;

namespace Sortable_Items
{
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("---SORTABLE_ITEMS---");
                Console.WriteLine("1.Start sort.");
                Console.WriteLine("2.Program information.");
                Console.WriteLine("3.Folder for sort.");
                Console.WriteLine("4.Themes.");
                Console.WriteLine("0.Exit");
                Console.WriteLine($"Current folder: {current_sort_folder ?? "Not selected."}\n");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number))
                {
                    switch (number)
                    {
                        case 1:
                            if (current_sort_folder == null)
                            {
                                Console.WriteLine("Choose folder. (3)");
                                Console.ReadKey();
                                Console.Clear();
                                continue;
                            }
                            folder_create();
                            Console.WriteLine("Folders were succesfuly created.");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        case 2:
                            Console.WriteLine("Заглушка пункта 2.");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        case 3:
                            current_sort_folder = sort_folder_picker();
                            if (current_sort_folder != null)
                            {
                                Console.WriteLine("Your path was succesfully saved!");
                                continue;
                            }
                            continue;
                        case 4:
                            Console.WriteLine("Заглушка пункта 4.");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        case 0:
                            Console.Clear();
                            Console.WriteLine("GoodBye.");
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Write a number please.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
        }
        static string sort_folder_picker() //Метод запрашивания пути к папке.
        {
            Console.Clear();
            Console.WriteLine("Write path to sort_folder");
            string user_folder = Console.ReadLine();
            if (Directory.Exists(user_folder))
            {
                Console.WriteLine("Folder is exist and added to sort_folder.");
                Console.WriteLine($"Folder: {user_folder}");
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
                Console.Clear();
                return (user_folder);
            }
            else
            {
                Console.WriteLine("Path does not exist, try again.");
                Console.WriteLine("Press any key to go to the Main_menu.");
                Console.ReadKey();
                Console.Clear();
                return null;
            }
        }
        static string current_sort_folder = null;
        static void folder_create()
        {
            string[] folders = { "Documents", "Phtotos", "Videos", "Other", "Application files", "Archives" };
            foreach (string folder_name in folders)
            {
                string full_path = Path.Combine(current_sort_folder, folder_name);
                Directory.CreateDirectory(full_path);
            }
        }
    }
}
