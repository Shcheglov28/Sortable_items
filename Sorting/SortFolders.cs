using System;
using System.Collections.Generic;

namespace Sortable_Items
{
    static class SortFolders
    {
        public const string Documents = "Documents";
        public const string Photos = "Photos";
        public const string Videos = "Videos";
        public const string Other = "Other";
        public const string Applications = "Application files";
        public const string Archives = "Archives";

        public static readonly string[] All =
        {
            Documents, Photos, Videos, Other, Applications, Archives
        };

        static readonly Dictionary<string, string> extension_map =
            new(StringComparer.OrdinalIgnoreCase)
            {
                [".txt"] = Documents,
                [".doc"] = Documents,
                [".docx"] = Documents,
                [".pdf"] = Documents,
                [".xls"] = Documents,
                [".xlsx"] = Documents,

                [".png"] = Photos,
                [".jpg"] = Photos,
                [".jpeg"] = Photos,
                [".ico"] = Photos,
                [".gif"] = Photos,
                [".bmp"] = Photos,

                [".mp4"] = Videos,
                [".avi"] = Videos,
                [".mkv"] = Videos,
                [".mov"] = Videos,

                [".msi"] = Applications,
                [".exe"] = Applications,

                [".zip"] = Archives,
                [".rar"] = Archives,
                [".7z"] = Archives,
            };

        public static string GetTargetFolder(string extension) =>
            extension_map.TryGetValue(extension, out string? folder) ? folder : Other;
    }
}
