using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XmlParser.Scripts
{
    internal static class Program
    {
        public const string DocPath = @"D:\XmlParser\XmlParser\XmlDocs\Level1.xml";
        public const string SearchingAttribute = "Type";
        public const string TabletType = "Tablet";
        
        public static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new StreamReader(DocPath));
            
            //Console.Write(level1Doc.InnerXml);

            List<string> tabletsText = new List<string>();
            
            ExtractTabletsText(doc.DocumentElement, ref tabletsText);

            foreach (string text in tabletsText) 
                Console.Write(text);
        }

        private static void ExtractTabletsText(XmlElement element, ref List<string> tabletsText)
        {
            string text = element.GetAttribute(SearchingAttribute);
            
            if (!string.IsNullOrEmpty(text) && text == TabletType)
                tabletsText.Add(element.InnerText);

            XmlNodeList childNodes = element.ChildNodes;

            for (int i = 0; i < childNodes.Count; ++i)
            {
                if (childNodes[i] is XmlElement childElement)
                    ExtractTabletsText(childElement, ref tabletsText);
            }
        }
    }
}