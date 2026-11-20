using System;
using System.Diagnostics;
using System.IO;

namespace Sortable_Items
{
    class Logs //LOGS
    {
        public static void LogMove(string fileName, string targetFolder)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] \"{fileName}\" перенесён в \"{targetFolder}\"";
            Write(line);
        }

        public static void LogError(string message)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {message}";
            Write(line);
        }

        public static void LogSummary(int moved, int failed)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Итого: перенесено {moved}, ошибок {failed}";
            Write(line);
        }

        static void Write(string line)
        {
            try
            {
                AppPaths.EnsureFolder();
                File.AppendAllText(AppPaths.LogPath, line + Environment.NewLine);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                // Логирование не должно ронять приложение.
                Debug.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}
