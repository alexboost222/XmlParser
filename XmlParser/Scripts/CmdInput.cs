using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XmlParser.Scripts
{
    public static class CmdInput
    {
        public static string GetFolderPath()
        {
            Console.WriteLine("Enter folder path with *.xml files in:");
            string folderPath;
            while (true)
            {
                folderPath = Console.ReadLine();

                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("Directory doesn't exists, please retry:");
                }
                else if (Directory.GetFiles(folderPath).Length == 0)
                {
                    Console.WriteLine("Directory is empty, please enter folder path with *.xml files in:");
                }
                else
                    break;
            }
            
            return folderPath;
        }
        
        public static List<XmlDocument> LoadAllXml(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, "*.xml");
            List<XmlDocument> answer = new List<XmlDocument>(files.Length);

            foreach (string file in files)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(file);
                answer.Add(xml);
            }

            return answer;
        }
    }
}