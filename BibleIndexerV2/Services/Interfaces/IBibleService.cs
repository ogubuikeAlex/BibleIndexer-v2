using BibleIndexerV2.Models.Request;
using BibleIndexerV2.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibleIndexerV2.Services.Interfaces
{
    public interface IBibleService
    {
        Task<BibleVerseResponse?> GenerateRandomBibleVerse();
        Task<object> GetAllBooksOfTheBible();
        Task<VersesResponse?> GetAllVersesInAChapterOFTheBible(GetBibleVerseRequest request);
        Task<BibleVerseResponse?> GetBibleVerse(GetBibleVerseRequest request);
        Task<BlobResponse?> GetBookOfTheBible(string bookName);
        Task<ChaptersResponse?> GetChaptersInABookOfTheBible(string name);
        Task<IEnumerable<BibleVerseResponse>> SearchBible(string query);
    }
}
