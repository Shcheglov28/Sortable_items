using System;
using System.IO;
using System.Text.Json;

namespace Sortable_Items
{
    class AppConfig //JSONCFG
    {
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
        public int AllSortItems_Counter { get; set; }
        public string? SortFolder { get; set; }

        // Конфиг первой версии лежал рядом с программой или в текущей папке запуска.
        static readonly string[] legacy_config_paths =
        {
            Path.Combine(AppContext.BaseDirectory, "config.json"),
            "config.json",
        };

        public static AppConfig LoadConfig()
        {
            try
            {
                string config_path = AppPaths.ConfigPath;

                if (!File.Exists(config_path))
                {
                    MigrateLegacyConfig(config_path);
                }

                if (File.Exists(config_path))
                {
                    string json_text = File.ReadAllText(config_path);
                    AppConfig? loaded_config = JsonSerializer.Deserialize<AppConfig>(json_text);
                    if (loaded_config != null)
                    {
                        return loaded_config;
                    }
                }
            }
            catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
            {
                Logs.LogError($"Config load failed: {ex.Message}");
            }

            return new AppConfig();
        }

        static void MigrateLegacyConfig(string config_path)
        {
            string? legacy_path = Array.Find(legacy_config_paths, File.Exists);
            if (legacy_path == null)
            {
                return;
            }

            try
            {
                AppPaths.EnsureFolder();
                File.Copy(legacy_path, config_path, overwrite: false);
                Console.WriteLine("Config migrated to %APPDATA%.");
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Logs.LogError($"Config migration failed: {ex.Message}");
            }
        }

        public static void SaveConfig(AppConfig config)
        {
            try
            {
                AppPaths.EnsureFolder();
                string json_text = JsonSerializer.Serialize(config);
                File.WriteAllText(AppPaths.ConfigPath, json_text);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Logs.LogError($"Config save failed: {ex.Message}");
            }
        }
    }
}
