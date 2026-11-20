using System;
using System.IO;

namespace Sortable_Items
{
    partial class Program
    {
        //FOLDER CREATE
        static bool folder_create()
        {
            try
            {
                foreach (string folder_name in SortFolders.All)
                {
                    string full_path = Path.Combine(current_sort_folder!, folder_name);
                    Directory.CreateDirectory(full_path);
                }
                return true;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Logs.LogError($"Failed to create sort folders: {ex.Message}");
                Console.WriteLine($"Cannot create sort folders: {ex.Message}");
                return false;
            }
        }

        //SORT REALIZATION
        static SortResult realize_sort()
        {
            int moved = 0;
            int failed = 0;
            string[] all_files;

            try
            {
                all_files = Directory.GetFiles(current_sort_folder!);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Logs.LogError($"Cannot read folder \"{current_sort_folder}\": {ex.Message}");
                Console.WriteLine($"Cannot read folder: {ex.Message}");
                return new SortResult(0, 0);
            }

            foreach (string files in all_files)
            {
                string ext = Path.GetExtension(files).ToLower();
                string target_folder = SortFolders.GetTargetFolder(ext);
                string file_name = Path.GetFileName(files);
                //CASES
                Console.WriteLine($"file name: {file_name} --> Folder name: {target_folder}");
                string dest_path = Path.Combine(current_sort_folder!, target_folder, file_name);

                try
                {
                    File.Move(files, dest_path);
                    Logs.LogMove(file_name, target_folder);
                    current_config.AllSortItems_Counter++;
                    moved++;
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
                {
                    Logs.LogError($"Cannot move \"{file_name}\": {ex.Message}");
                    Console.WriteLine($"Skipped: {file_name} ({ex.Message})");
                    failed++;
                }
            }

            AppConfig.SaveConfig(current_config);
            Logs.LogSummary(moved, failed);
            return new SortResult(moved, failed);
        }
    }
}
