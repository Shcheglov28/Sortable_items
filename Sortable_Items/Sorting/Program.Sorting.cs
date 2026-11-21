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
                foreach (string folder_name in SortFolders.all)
                {
                    string full_path = Path.Combine(current_sort_folder!, folder_name);
                    Directory.CreateDirectory(full_path);
                }
                return true;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Logs.log_error($"Failed to create sort folders: {ex.Message}");
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
                Logs.log_error($"Cannot read folder \"{current_sort_folder}\": {ex.Message}");
                Console.WriteLine($"Cannot read folder: {ex.Message}");
                return new SortResult(0, 0);
            }

            foreach (string files in all_files)
            {
                string ext = Path.GetExtension(files).ToLower();
                string target_folder = SortFolders.get_target_folder(ext);
                string file_name = Path.GetFileName(files);
                //CASES
                Console.WriteLine($"file name: {file_name} --> Folder name: {target_folder}");
                string dest_path = Path.Combine(current_sort_folder!, target_folder, file_name);

                try
                {
                    long file_size = new FileInfo(files).Length;
                    File.Move(files, dest_path);
                    Logs.log_move(file_name, target_folder, file_size);
                    current_config.all_sort_items_counter++;
                    moved++;
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
                {
                    Logs.log_error($"Cannot move \"{file_name}\": {ex.Message}");
                    Console.WriteLine($"Skipped: {file_name} ({ex.Message})");
                    failed++;
                }
            }

            AppConfig.save_config(current_config);
            Logs.log_summary(moved, failed);
            return new SortResult(moved, failed);
        }
    }
}
