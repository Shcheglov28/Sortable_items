using System;
using System.Collections.Generic;

namespace Sortable_Items
{
    static class SortFolders
    {
        public const string documents = "Documents";
        public const string photos = "Photos";
        public const string videos = "Videos";
        public const string other = "Other";
        public const string applications = "Application files";
        public const string archives = "Archives";

        public static readonly string[] all =
        {
            documents, photos, videos, other, applications, archives
        };

        static readonly Dictionary<string, string> extension_map =
            new(StringComparer.OrdinalIgnoreCase)
            {
                [".txt"] = documents,
                [".doc"] = documents,
                [".docx"] = documents,
                [".pdf"] = documents,
                [".xls"] = documents,
                [".xlsx"] = documents,

                [".png"] = photos,
                [".jpg"] = photos,
                [".jpeg"] = photos,
                [".ico"] = photos,
                [".gif"] = photos,
                [".bmp"] = photos,

                [".mp4"] = videos,
                [".avi"] = videos,
                [".mkv"] = videos,
                [".mov"] = videos,

                [".msi"] = applications,
                [".exe"] = applications,

                [".zip"] = archives,
                [".rar"] = archives,
                [".7z"] = archives,
            };

        public static string get_target_folder(string extension) =>
            extension_map.TryGetValue(extension, out string? folder) ? folder : other;
    }
}
