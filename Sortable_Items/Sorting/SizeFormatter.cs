using System;
using System.Globalization;

namespace Sortable_Items
{
    static class SizeFormatter
    {
        static readonly string[] units = { "B", "KB", "MB", "GB", "TB", "PB" };

        // format(1536) -> "1.5 KB", format(512) -> "512 B", format(1048576) -> "1.0 MB"
        public static string format(long bytes)
        {
            double size = bytes;
            int unit_index = 0;

            while (size >= 1024 && unit_index < units.Length - 1)
            {
                size /= 1024;
                unit_index++;
            }

            string number = unit_index == 0
                ? size.ToString("F0", CultureInfo.InvariantCulture)
                : size.ToString("F1", CultureInfo.InvariantCulture);

            return $"{number} {units[unit_index]}";
        }
    }
}
