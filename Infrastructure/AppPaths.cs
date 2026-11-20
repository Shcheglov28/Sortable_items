using System;
using System.IO;

namespace Sortable_Items
{
    static class AppPaths
    {
        static readonly string app_data_folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Sortable_Items");

        public static string ConfigPath => Path.Combine(app_data_folder, "config.json");

        public static string LogPath => Path.Combine(app_data_folder, "sort_log.txt");

        public static string EnsureFolder()
        {
            Directory.CreateDirectory(app_data_folder);
            return app_data_folder;
        }
    }
}
