using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sortable_Items
{
    class AppConfig //JSONCFG
    {
        [JsonPropertyName("TextColor")]
        public ConsoleColor text_color { get; set; } = ConsoleColor.White;

        [JsonPropertyName("AllSortItems_Counter")]
        public int all_sort_items_counter { get; set; }

        [JsonPropertyName("SortFolder")]
        public string? sort_folder { get; set; }

        // Конфиг первой версии лежал рядом с программой или в текущей папке запуска.
        static readonly string[] legacy_config_paths =
        {
            Path.Combine(AppContext.BaseDirectory, "config.json"),
            "config.json",
        };

        public static AppConfig load_config()
        {
            try
            {
                string path = AppPaths.config_path;

                if (!File.Exists(path))
                {
                    migrate_legacy_config(path);
                }

                if (File.Exists(path))
                {
                    string json_text = File.ReadAllText(path);
                    AppConfig? loaded_config = JsonSerializer.Deserialize<AppConfig>(json_text);
                    if (loaded_config != null)
                    {
                        return loaded_config;
                    }
                }
            }
            catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
            {
                Logs.log_error($"Config load failed: {ex.Message}");
            }

            return new AppConfig();
        }

        static void migrate_legacy_config(string path)
        {
            string? legacy_path = Array.Find(legacy_config_paths, File.Exists);
            if (legacy_path == null)
            {
                return;
            }

            try
            {
                AppPaths.ensure_folder();
                File.Copy(legacy_path, path, overwrite: false);
                Console.WriteLine("Config migrated to %APPDATA%.");
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Logs.log_error($"Config migration failed: {ex.Message}");
            }
        }

        public static void save_config(AppConfig config)
        {
            try
            {
                AppPaths.ensure_folder();
                string json_text = JsonSerializer.Serialize(config);
                File.WriteAllText(AppPaths.config_path, json_text);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Logs.log_error($"Config save failed: {ex.Message}");
            }
        }
    }
}
