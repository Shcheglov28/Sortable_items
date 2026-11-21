using System;
using System.IO;

namespace Sortable_Items
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = current_config.text_color;
            //MENU REALIZATION
            while (true)
            {
                Console.WriteLine("---SORTABLE_ITEMS---");
                Console.WriteLine("1.Start sort.");
                Console.WriteLine("2.Program information.");
                Console.WriteLine("3.Folder for sort.");
                Console.WriteLine("4.Themes.");
                Console.WriteLine("0.Exit");
                Console.WriteLine($"Current folder: {current_sort_folder ?? "Not selected."}\n");
                string input = Console.ReadLine() ?? string.Empty;

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

                            if (!folder_create())
                            {
                                Console.WriteLine("Press any key to return to the menu . . .");
                                Console.ReadKey();
                                Console.Clear();
                                continue;
                            }

                            Console.WriteLine("Folders were succesfuly created.");
                            Console.ReadKey();
                            Console.Clear();
                            SortResult result = realize_sort();
                            Console.WriteLine($"Sorted: {result.moved}. Skipped: {result.failed}.");
                            Console.WriteLine("Press any key to return to the menu . . .");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        case 2:
                            Console.WriteLine($"Developer: https://github.com/Shcheglov28");
                            Console.WriteLine("Instructions: https://github.com/Shcheglov28/Sortable_items/blob/main/README.md");
                            Console.WriteLine($"Sort counter: {current_config.all_sort_items_counter}");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        case 3:
                            current_sort_folder = sort_folder_picker();
                            if (current_sort_folder != null)
                            {
                                current_config.sort_folder = current_sort_folder;
                                AppConfig.save_config(current_config);
                                Console.WriteLine("Your path was succesfully saved!");
                                continue;
                            }
                            continue;
                        case 4:
                            Console.WriteLine("Choose color:");
                            Console.WriteLine("1 - Red");
                            Console.WriteLine("2 - Green");
                            Console.WriteLine("3 - Blue");
                            Console.WriteLine("4 - Yellow");
                            string color_input = Console.ReadLine()?.Trim() ?? string.Empty;
                            switch (color_input)
                            {
                                case "1":
                                    current_config.text_color = ConsoleColor.Red;
                                    Console.Clear();
                                    Console.ForegroundColor = current_config.text_color;
                                    AppConfig.save_config(current_config);
                                    continue;
                                case "2":
                                    current_config.text_color = ConsoleColor.Green;
                                    Console.Clear();
                                    Console.ForegroundColor = current_config.text_color;
                                    AppConfig.save_config(current_config);
                                    continue;
                                case "3":
                                    current_config.text_color = ConsoleColor.Blue;
                                    Console.Clear();
                                    Console.ForegroundColor = current_config.text_color;
                                    AppConfig.save_config(current_config);
                                    continue;
                                case "4":
                                    current_config.text_color = ConsoleColor.Yellow;
                                    Console.Clear();
                                    Console.ForegroundColor = current_config.text_color;
                                    AppConfig.save_config(current_config);
                                    continue;
                                default:
                                    Console.WriteLine("Unknown color. Choose 1-4.");
                                    Console.ReadKey();
                                    Console.Clear();
                                    continue;
                            }
                        case 0:
                            Console.Clear();
                            Console.WriteLine("GoodBye.");
                            return;
                        default:
                            Console.WriteLine("Unknown menu item.");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
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

        //DEFAULT
        static AppConfig current_config = AppConfig.load_config();
        //RESTORED SORT FOLDER
        static string? current_sort_folder =
            !string.IsNullOrWhiteSpace(current_config.sort_folder) && Directory.Exists(current_config.sort_folder)
                ? current_config.sort_folder
                : null;
    }
}
