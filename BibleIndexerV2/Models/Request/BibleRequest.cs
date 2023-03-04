using System;
using System.Collections.Generic;
using System.Text;

namespace BibleIndexerV2.Models.Request
{
    internal class BibleRequest
    {
    }

    public class GetBibleVerseRequest
    {
        public int VerseNumber { get; set; }
        public string BookNameInFull { get; set; }
        public int ChapterNumber { get; set; }
    }
}
