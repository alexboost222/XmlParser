using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace XmlParser.Scripts
{
    public static class Exporter
    {
        public static void ExportCSV(string fileName, List<LevelInfo> levelInfos)
        {
            using (Stream stream = new FileStream(fileName, FileMode.OpenOrCreate))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<FooMap>();
                csv.Configuration.IncludePrivateMembers = true;
                csv.WriteRecords(levelInfos);
            }
        }

        public static void ExportTXT(string fileName, List<LevelInfo> levelInfos)
        {
            using (Stream stream = new FileStream(fileName, FileMode.OpenOrCreate))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                foreach (LevelInfo level in levelInfos)
                {
                    writer.Write("Пак: ");
                    writer.WriteLine(level.pack);
                    
                    writer.Write("Название: ");
                    writer.WriteLine(level.name);

                    writer.WriteLine($"Описание: ");
                    writer.WriteLine(level.description);
                    
                    /*for (int i = 0; i < level.tabletsText.Count; i++)
                    {
                        writer.WriteLine($"Фаза {i}: ");
                        writer.WriteLine(level.tabletsText[i]);
                    }*/
                }
            }
        }
        
        public sealed class FooMap : ClassMap<LevelInfo>
        {
            public FooMap()
            {
                Map(m => m.Pack).Name("Пак");
                Map(m => m.Name).Name("Название");
                Map(m => m.DotValid).Name("Точки есть?");
                Map(m => m.JournalValid).Name("Слов \"журнал\" нету?");
                Map(m => m.SubscriptValid).Name("Сабскрипты в этикетках стоят?");
                Map(m => m.Phase1).Name("Фаза 1");
                Map(m => m.Phase2).Name("Фаза 2");
                Map(m => m.Phase3).Name("Фаза 3");
                Map(m => m.Phase4).Name("Фаза 4");
                Map(m => m.Phase5).Name("Фаза 5");
                Map(m => m.Phase6).Name("Фаза 6");
                Map(m => m.Phase7).Name("Фаза 7");
            }
        }
    }
}