using System.Collections.Generic;

namespace XmlParser.Scripts
{
    public struct LevelInfo
    {
            public List<string> tabletsText;
            public string name;
            public string pack;
            public string description;
            public bool dotValid;
            public bool journalValid;
            public bool subscriptValid;

            public string Pack => pack;

            public string Name => name;
            public bool DotValid => dotValid;
            public bool JournalValid => journalValid;
            public bool SubscriptValid => subscriptValid;

            public string Phase1 => tabletsText.Count > 0 ? tabletsText[0] : "";
            public string Phase2 => tabletsText.Count > 1 ? tabletsText[1] : "";
            public string Phase3 => tabletsText.Count > 2 ? tabletsText[2] : "";
            public string Phase4 => tabletsText.Count > 3 ? tabletsText[3] : "";
            public string Phase5 => tabletsText.Count > 4 ? tabletsText[4] : "";
            public string Phase6 => tabletsText.Count > 5 ? tabletsText[5] : "";
            public string Phase7 => tabletsText.Count > 6 ? tabletsText[6] : "";

            public static LevelInfo DummyLevelInfo => new LevelInfo
            {
                name = "asdasd",
                pack = "vsavoasv",
                dotValid = true,
                tabletsText = new List<string>
                {
                    "asvas,\nsdvslksdvlkm\t asfasc;lmasv", "asvas,\nsdvslksdvlkm\t asfasfsv\""
                }
            };

    }
}