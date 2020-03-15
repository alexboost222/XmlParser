using System;
using System.IO;
using System.Xml;

namespace XmlParser
{
    internal static class Program
    {
        public const string Level1Path = @"D:\XmlParser\XmlParser\XmlDocs\Level1.xml";
        
        public static void Main(string[] args)
        {
            XmlDocument level1Doc = new XmlDocument {PreserveWhitespace = true};
            level1Doc.Load(new StreamReader(Level1Path));
            
            Console.Write(level1Doc.InnerXml);
        }
    }
}