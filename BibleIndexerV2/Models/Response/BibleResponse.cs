using System;
using System.Collections.Generic;
using System.Text;

namespace BibleIndexerV2.Models.Response
{
    internal class BibleResponse
    {
    }
    public class BibleVerseResponse
    {
        public int VerseNumber { get; set; }
        public string VerseContent { get; set; }
        public int ChapterNumber { get; set; }
        public int BookNumber { get; set; }
        public string BookName { get; set; }
    }

    public class ChapterResonse
    {
        public string BookName { get; set; }
        public List<ChapterResonse> Chapters { get; set; }
    }

    public class GetAllBookResonse
    {
        public string Id { get; set; }
        public string BookName { get; set; }
    }

    public class BlobResponse
    {
        public string Abbrev { get; set; }
        public string Name { get; set; }
        public List<List<string>> Chapters { get; set; }
    }

    public class ChaptersResponse : ResourseAndDropdownResponse<List<string>>
    {
    }

    public class VersesResponse : ResourseAndDropdownResponse<string>
    {
    }

    public class ResourseAndDropdownResponse<T>
    {
        public string BookName { get; set; }
        public IEnumerable<dynamic> DropDown { get; set; }
        public IEnumerable<T> Resource { get; set; }
    }
}
