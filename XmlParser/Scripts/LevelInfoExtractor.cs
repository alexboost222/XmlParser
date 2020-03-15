using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlParser.Scripts
{
    public static class LevelInfoExtractor
    {
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

        public static LevelInfo ExtractLevelInfo(XmlDocument doc)
        {
            LevelInfo levelInfo = new LevelInfo();

            levelInfo.pack = ExtractInnerTextByNodeName(doc.DocumentElement, "LevelPacks");
            levelInfo.name = ExtractInnerTextByNodeName(doc.DocumentElement, "Name");
            
            ExtractTabletsText(doc.DocumentElement, ref levelInfo.tabletsText);
            levelInfo.journalValid = JournalValidator(in levelInfo.tabletsText);

            levelInfo.subscriptValid = SubscriptValidator(doc.DocumentElement);

            levelInfo.dotValid = DotValidator(in levelInfo.tabletsText);

            return levelInfo;
        }

        private static string ExtractInnerTextByNodeName(XmlNode node, string searchingName)
        {
            XmlNode resultNode = node.FirstChild.ChildNodes.Cast<XmlNode>()
                .FirstOrDefault(childNode => childNode.Name == searchingName);

            return resultNode != null ? resultNode.InnerText : "NotFound";
        }

        private static bool JournalValidator(in List<string> tabletsText) 
            => tabletsText.All(text => !text.Contains("Журнал") && !text.Contains("журнал"));

        private static bool SubscriptValidator(XmlElement element)
        {
            const string searchingAttribute = "Label";

            string label = element.GetAttribute(searchingAttribute);

            label = label.Contains('/') ? label.Split('/')[0] : label;

            if (!string.IsNullOrEmpty(label) && label.Any(char.IsDigit))
                return false;

            for (int i = 0; i < element.ChildNodes.Count; ++i)
            {
                if (element.ChildNodes[i] is XmlElement childElement)
                {
                    if (!SubscriptValidator(childElement))
                        return false;
                }
            }

            return true;
        }

        private static bool DotValidator(in List<string> tabletsText)
        {
            foreach (string text in tabletsText)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];

                    if (c == '\n')
                    {
                        if (i - 1 >= 0 && text[i - 1] == '\r')
                        {
                            if (i - 2 >= 0 && !IsDotChar(text[i - 2]))
                                return false;
                        }
                        else if ( i - 1 >= 0 && !IsDotChar(text[i - 1]))
                            return false;
                    }
                }
            }

            return true;
        }

        private static bool IsDotChar(char symbol) => ".,:!?".Contains(symbol);
    }
}