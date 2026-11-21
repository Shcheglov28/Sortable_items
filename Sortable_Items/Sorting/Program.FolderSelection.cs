using System;
using System.IO;

namespace Sortable_Items
{
    partial class Program
    {
        //SORT FOLDER PICK METHOD
        static string? sort_folder_picker()
        {
            Console.Clear();
            Console.WriteLine("Write path to sort_folder");
            string user_folder = Console.ReadLine()?.Trim().Trim('"') ?? string.Empty;

            if (user_folder.Length > 0 && Directory.Exists(user_folder))
            {
                Console.WriteLine("Folder is exist and added to sort_folder.");
                Console.WriteLine($"Folder: {user_folder}");
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
                Console.Clear();
                return user_folder;
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
    }
}
