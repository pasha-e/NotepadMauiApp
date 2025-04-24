using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NotepadMauiApp.DataSources;

namespace NotepadMauiApp.Classes
{
    public static  class DataStorage
    {
        static string  _filePath = Path.Combine(FileSystem.AppDataDirectory, "data.json");

        public static List<NotepadInfo>? Restore()
        {
            List<NotepadInfo>? data = null;

            try
            {
                data = JsonSerializer.Deserialize<List<NotepadInfo>>(File.ReadAllText(_filePath));
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка загрузки JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка : {ex.Message}");
            }

            return data;
        }

        public static void Save(List<NotepadInfo> dataList)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(dataList, options));
        }

    }
}
