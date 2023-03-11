using BibleIndexerV2.Data;
using BibleIndexerV2.Models.Request;
using BibleIndexerV2.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BibleIndexerV2.Services.Implementations
{
    public class BibleService
    {
        const int first = 1;
        const int bibleBookCount = 66;
        private static IEnumerable<dynamic>? _bibleBlob;

        ///<Summary>Get the chapters in a book of the bible using the full name of the boo or via the book abbreviation. This will also return a cascading dropdown for all chapters in specified book</Summary>        
        public static async Task<ChaptersResponse?> GetChaptersInABookOfTheBible(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            BlobResponse? bibleResult = await GetBookOfTheBible(name);
            if (bibleResult is null) return null;
            int count = 0;
            
            IEnumerable<dynamic>? chaptersDropdown = bibleResult.Chapters?.Select(x => new { Id = count += 1 });

            return new ChaptersResponse()
            {
                DropDown = chaptersDropdown ?? Enumerable.Empty<dynamic>(),
                BookName = bibleResult?.Name ?? string.Empty,
                Resource = bibleResult?.Chapters ?? Enumerable.Empty<List<string>>(),
            };
        }

        ///<Summary>Generate a random bible verse</Summary>
        public static async Task<BibleVerseResponse?> GenerateRandomBibleVerse()
        {
            int randomBibleBookIndex = RandomNumberGenerator.GetInt32(bibleBookCount);

            IEnumerable<dynamic>? bibleBlob = await GetBlob();
            if (bibleBlob is null || !bibleBlob.Any()) return null;

            var book = bibleBlob.ElementAt(randomBibleBookIndex);
            var bookName = book.Name;
            var chapterCount = book.Chapters.Count;

            int randomBibleChapterIndex = RandomNumberGenerator.GetInt32(chapterCount);
            var chapter = JsonConvert.DeserializeObject<List<List<string>>>(Convert.ToString(book.Chapters));

            var verseCount = chapter.Count;
            var randomVerseIndex = RandomNumberGenerator.GetInt32(verseCount);
            return await GetBibleVerse(new GetBibleVerseRequest() { BookNameInFull = bookName, ChapterNumber = randomBibleChapterIndex, VerseNumber = randomVerseIndex });
        }

        ///<Summary>Gets all books of the bible together with their abbreviations</Summary>
        public static async Task<IEnumerable<GetAllBookResonse>> GetAllBooksOfTheBible()
        {
            IEnumerable<dynamic>? bibleBlob = await GetBlob();

            if (bibleBlob is null) return Enumerable.Empty<GetAllBookResonse>();

            return bibleBlob.Select(bibleBlob => new GetAllBookResonse()
            {
                Id = bibleBlob.Abbreviation,
                BookName = bibleBlob.Name,
            });
        }

        ///<Summary>Get all verses in a chapter of the bible. This will also return a cascading dropdown for all verses in specified chapter</Summary>
        public static async Task<VersesResponse?> GetAllVersesInAChapterOFTheBible(GetBibleVerseRequest request)
        {
            BlobResponse? bookResponse = await GetBookOfTheBible(request.BookNameInFull);
            if (bookResponse is null) return null;
            int count = 0;
            IEnumerable<string> verses = bookResponse.Chapters.ElementAtOrDefault(request.ChapterNumber - first);
            IEnumerable<dynamic> versesDropdown = verses != null && verses.Any() ? verses.Select(x => new { Id = count += 1 }) : Enumerable.Empty<dynamic>();

            return new VersesResponse()
            {
                DropDown = versesDropdown,
                BookName = bookResponse?.Name,
                Resource = verses,
            };
        }

        ///<Summary>Get a book of bible</Summary>
        public static async Task<BlobResponse?> GetBookOfTheBible(string bookName)
        {
            IEnumerable<dynamic>? bibleBlob = await GetBlob();

            if (bibleBlob is null || !bibleBlob.Any()) return null;
            dynamic? result = bibleBlob.FirstOrDefault(x => Convert.ToString(x.Name).ToLower() == bookName.ToLower().Trim()
            || Convert.ToString(x.Abbreviation).ToLower() == bookName.ToLower().Trim());

            if (result is null) return null;
            return JsonConvert.DeserializeObject<BlobResponse>(Convert.ToString(result));
        }

        ///<Summary>Get a bible verse</Summary>
        public static async Task<BibleVerseResponse?> GetBibleVerse(GetBibleVerseRequest request)
        {
            BlobResponse? bookResponse = await GetBookOfTheBible(request.BookNameInFull);
            if (bookResponse is null) return null;

            return new BibleVerseResponse()
            {
                BookName = request.BookNameInFull,
                ChapterNumber = request.ChapterNumber,
                VerseNumber = request.VerseNumber,
                VerseContent = bookResponse.Chapters[request.ChapterNumber - first][request.VerseNumber - first]
            };
        }

        /// <Summary>Get all occurences of a word in the bible</Summary>
        public static async Task<IEnumerable<BibleVerseResponse>> SearchBible(string query)
        {
            query = string.IsNullOrEmpty(query) ? throw new InvalidOperationException("Invalid query") : query.ToLower().Trim();

            IEnumerable<dynamic> bibleBlob = await GetBlob();

            if (!bibleBlob.Any()) return Enumerable.Empty<BibleVerseResponse>();

            List<BibleVerseResponse> result = new List<BibleVerseResponse>();

            Parallel.ForEach(bibleBlob, (blob, state, index) =>
            {
                BlobResponse deserializedBlob = JsonConvert.DeserializeObject<BlobResponse>(Convert.ToString(blob));

                Parallel.ForEach(deserializedBlob.Chapters, (chapter, state, chapterIndex) =>
                {
                    Parallel.ForEach(chapter, (verse, state, verseIndex) =>
                    {
                        if (verse.ToLower().Contains(query))
                        {
                            result.Add(new BibleVerseResponse()
                            {
                                BookName = deserializedBlob.Name,
                                BookNumber = (int)index + first,
                                ChapterNumber = (int)chapterIndex + first,
                                VerseNumber = (int)verseIndex + first,
                                VerseContent = verse
                            });
                        }
                    });
                });

            });

            return result;

        }

        public static async Task<IEnumerable<BibleVerseResponse>> SearchBibleV2(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Invalid query", nameof(query));
            }

            IEnumerable<dynamic> bibleBlob = await GetBlob();

            if (!bibleBlob.Any())
            {
                return Enumerable.Empty<BibleVerseResponse>();
            }

            ConcurrentBag<BibleVerseResponse> result = new ConcurrentBag<BibleVerseResponse>();

            await Task.WhenAll(bibleBlob.Select(async (blob, index) =>
            {
                BlobResponse deserializedBlob = JsonConvert.DeserializeObject<BlobResponse>(Convert.ToString(blob));
                int bookNumber = (int)index + first;

                await Task.WhenAll(deserializedBlob.Chapters.Select(async (chapter, chapterIndex) =>
                {
                    int chapterNumber = (int)chapterIndex + first;

                    await Task.WhenAll(chapter.Select(async (verse, verseIndex) =>
                    {
                        if (verse.ToLower().Contains(query))
                        {
                            result.Add(new BibleVerseResponse()
                            {
                                BookName = deserializedBlob.Name,
                                BookNumber = bookNumber,
                                ChapterNumber = chapterNumber,
                                VerseNumber = (int)verseIndex + first,
                                VerseContent = verse
                            });
                        }
                    }));
                }));
            }));

            return result;
        }

        private static async Task<IEnumerable<dynamic>?> GetBlob() => _bibleBlob = _bibleBlob == null ? await ApiV2.GetBibleBlob() : _bibleBlob ?? null;
    }
}
