# Sortable Items

Консольная утилита для Windows: раскладывает файлы в выбранной папке по типам —
документы, фото, видео, архивы, программы и всё остальное.

## Возможности

- 6 папок категорий, остальное уходит в `Other`
- Выбранная папка запоминается между запусками
- Счётчик отсортированных файлов
- Цвет текста консоли (Red / Green / Blue / Yellow)
- Лог каждого переноса и ошибки
- Ошибка на одном файле не останавливает сортировку
- Конфиг и лог хранятся в `%APPDATA%\Sortable_Items`

## Требования и запуск

Windows 10/11 и [.NET SDK 10.0+](https://dotnet.microsoft.com/download)

```powershell
dotnet build Sortable_Items.slnx
dotnet run --project Sortable_Items/Sortable_Items.csproj
```

## Использование

| Пункт | Действие |
|---|---|
| `1` | Создать папки и отсортировать файлы |
| `2` | Информация и счётчик |
| `3` | Указать папку (кавычки из «Копировать как путь» снимаются) |
| `4` | Цвет текста |
| `0` | Выход |

Сортируется только верхний уровень выбранной папки. Существующие файлы
не перезаписываются — такой перенос пропускается.

## Категории

| Папка | Расширения |
|---|---|
| `Documents` | `.txt` `.doc` `.docx` `.pdf` `.xls` `.xlsx` |
| `Photos` | `.png` `.jpg` `.jpeg` `.ico` `.gif` `.bmp` |
| `Videos` | `.mp4` `.avi` `.mkv` `.mov` |
| `Application files` | `.msi` `.exe` |
| `Archives` | `.zip` `.rar` `.7z` |
| `Other` | всё остальное |

Правила заданы в `Sortable_Items/Sorting/SortFolders.cs`.

## Данные

```
%APPDATA%\Sortable_Items\config.json     цвет, счётчик, выбранная папка
%APPDATA%\Sortable_Items\sort_log.txt    переносы и ошибки
```

Битый конфиг не роняет программу: ошибка пишется в лог, берутся значения по умолчанию.

## Структура

```
Sortable_Items/
├─ Program.cs                      меню и точка входа
├─ Sorting/                        создание папок, сортировка, категории, размер файла
├─ Configuration/AppConfig.cs      JSON-конфиг
├─ Logging/Logs.cs                 лог
└─ Infrastructure/AppPaths.cs      пути в %APPDATA%
```

Исходный дизайн-документ: [docs/DESIGN.md](docs/DESIGN.md).

## Планы

- Юнит-тесты (xUnit) и CI
- Разрешение конфликтов имён при переносе
- Рекурсивная сортировка вложенных папок

Автор: [Shcheglov28](https://github.com/Shcheglov28)
