using System;
using System.IO;
using System.Text.Json;

namespace Sortable_Items
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = current_config.TextColor;
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
                            realize_sort();
                            continue;
                        case 2:
                            Console.WriteLine($"Developer: https://github.com/Shcheglov28");
                            Console.WriteLine("Instructions: https://github.com/Shcheglov28/Sortable_items/blob/main/README.md");
                            Console.WriteLine($"Sort counter: {current_config.AllSortItems_Counter}");
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
                            Console.WriteLine("Choose color:");
                            Console.WriteLine("1 - Red");
                            Console.WriteLine("2 - Green");
                            Console.WriteLine("3 - Blue");
                            Console.WriteLine("4 - Yellow");
                            string ColorInput = Console.ReadLine();
                            switch (ColorInput)
                            {
                                case "1":
                                    current_config.TextColor = ConsoleColor.Red;
                                    Console.Clear();
                                    Console.ForegroundColor = current_config.TextColor;
                                    AppConfig.SaveConfig(current_config);
                                    continue;
                                case "2":
                                    current_config.TextColor = ConsoleColor.Green;
                                    Console.Clear();
                                    Console.ForegroundColor = current_config.TextColor;
                                    AppConfig.SaveConfig(current_config);
                                    continue;
                                case "3":
                                    current_config.TextColor = ConsoleColor.Blue;
                                    Console.Clear();
                                    Console.ForegroundColor = current_config.TextColor;
                                    AppConfig.SaveConfig(current_config);
                                    continue;
                                case "4":
                                    current_config.TextColor = ConsoleColor.Yellow;
                                    Console.Clear();
                                    Console.ForegroundColor = current_config.TextColor;
                                    AppConfig.SaveConfig(current_config);
                                    continue;
                            }
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
        //SORT FOLDER PICK METHOD
        static string sort_folder_picker()
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
        //DEFAULT
        static AppConfig current_config = AppConfig.LoadConfig();
        //STATS COUNTER

        //FOLDER CREATE
        static void folder_create()
        {
            string[] folders = { "Documents", "Photos", "Videos", "Other", "Application files", "Archives" };
            foreach (string folder_name in folders)
            {
                string full_path = Path.Combine(current_sort_folder, folder_name);
                Directory.CreateDirectory(full_path);
            }
        }
        //SORT REALIZATION
        static void realize_sort()
        {
            string[] all_files = Directory.GetFiles(current_sort_folder);
            foreach (string files in all_files)
            {
                string ext = Path.GetExtension(files).ToLower();
                string target_folder = "";
                //CASES
                switch (ext)
                {
                    case ".txt":
                    case ".doc":
                    case ".docx":
                    case ".pdf":
                    case ".xls":
                    case ".xlsx":
                        target_folder = "Documents";
                        break;

                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".ico":
                    case ".gif":
                    case ".bmp":
                        target_folder = "Photos";
                        break;

                    case ".mp4":
                    case ".avi":
                    case ".mkv":
                    case ".mov":
                        target_folder = "Videos";
                        break;

                    case ".msi":
                    case ".exe":
                        target_folder = "Application files";
                        break;

                    case ".zip":
                    case ".rar":
                    case ".7z":
                        target_folder = "Archives";
                        break;

                    default:
                        target_folder = "Other";
                        break;
                }
                Console.WriteLine($"file name: {Path.GetFileName(files)} --> Folder name: {target_folder}");
                string dest_path = Path.Combine(current_sort_folder, target_folder, Path.GetFileName(files));
                File.Move(files, dest_path);
                Logs.LogMove(Path.GetFileName(files), target_folder);
                current_config.AllSortItems_Counter++;
            }
            AppConfig.SaveConfig(current_config);
        }
    }

    class AppConfig //JSONCFG
    {
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
        public int AllSortItems_Counter { get; set; }
        static string config_path = "config.json";

        public static AppConfig LoadConfig()
        {
            if (File.Exists(config_path))
            {
                string json_text = File.ReadAllText(config_path);
                AppConfig? loaded_config = JsonSerializer.Deserialize<AppConfig>(json_text);
                if (loaded_config != null)
                {
                    return loaded_config;
                }
            }
            return new AppConfig();
        }
        public static void SaveConfig(AppConfig config)
        {
            string json_text = JsonSerializer.Serialize(config);
            File.WriteAllText(config_path, json_text);
        }
    }
    class Logs //LOGS
    {
        static string log_path = Path.Combine(AppContext.BaseDirectory, "sort_log.txt");

        public static void LogMove(string fileName, string targetFolder)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] \"{fileName}\" перенесён в \"{targetFolder}\"";
            File.AppendAllText(log_path, line + Environment.NewLine);
        }
    }




}
