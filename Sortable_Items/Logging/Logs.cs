using System;
using System.Diagnostics;
using System.IO;

namespace Sortable_Items
{
    class Logs //LOGS
    {
        public static void log_move(string file_name, string target_folder, long size)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] \"{file_name}\" ({SizeFormatter.format(size)}) перенесён в \"{target_folder}\"";
            write(line);
        }

        public static void log_error(string message)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {message}";
            write(line);
        }

        public static void log_summary(int moved, int failed)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Итого: перенесено {moved}, ошибок {failed}";
            write(line);
        }

        static void write(string line)
        {
            try
            {
                AppPaths.ensure_folder();
                File.AppendAllText(AppPaths.log_path, line + Environment.NewLine);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                // Логирование не должно ронять приложение.
                Debug.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}
