using System.Collections.Generic;
using System.Xml;

namespace XmlParser.Scripts
{
    public static class LevelInfoExtractor
    {
        public struct LevelInfo
        {
            public List<string> TabletsText;
            public string Name;
            public string Pack;
            public bool DotValid;
            public bool JournalValid;
            public bool SubscriptValid;
        }
        
        private const string SearchingAttribute = "Type";
        private const string TabletType = "Tablet";
        
        public static void ExtractTabletsText(XmlElement element, ref List<string> tabletsText)
        {
            if (tabletsText == null)
                tabletsText = new List<string>();
            
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