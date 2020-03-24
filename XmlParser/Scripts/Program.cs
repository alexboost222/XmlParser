using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XmlParser.Scripts
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            string folderPath = CmdInput.GetFolderPath();
            
            //List<(XmlDocument doc, string path)> docs = CmdInput.LoadAllXmlWithPath(folderPath);
            List<XmlDocument> docs = CmdInput.LoadAllXml(folderPath);
            
            Console.OutputEncoding = Encoding.UTF8;

            /*foreach ((XmlDocument doc, string path) in docs)
            {
                LevelInfoExtractor.SubscriptFormatter(doc.DocumentElement);
                
                Console.WriteLine((LevelInfoExtractor.SubscriptValidator(doc.DocumentElement) ? "YES " : "NO ") + path);

                doc.Save(path);
            }*/

            List<LevelInfo> infos = docs.Select(LevelInfoExtractor.ExtractLevelInfo).ToList();
            
            infos.ForEach(t => Console.WriteLine(t.name));

            string name = CmdInput.ExportPathDialog();

            Console.WriteLine("Enter file format(txt/csv):");
            while (true)
            {
                string format = Console.ReadLine();

                if (format == "txt")
                {
                    Exporter.ExportTXT($"{name}.txt", infos);
                    break;
                }

                if (format == "csv")
                {
                    Exporter.ExportCSV($"{name}.csv", infos);
                    break;
                }

                Console.WriteLine("Format not recognized. Please repeat:");
            }
            
            Console.WriteLine("Export successful");
            Console.ReadKey();
        }
    }
}