using BibleIndexerV2.Models.Request;
using BibleIndexerV2.Models.Response;
using BibleIndexerV2.Services.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BibleIndexerTest
{
    public class GetAllChaptersOfTheBibleTest
    {
        public class BibleApiTests
        {
            [Fact]
            public async Task GetAllVersesInAChapterOFTheBible_ShouldReturnListOfVerses()
            {
                // Arrange
                var expectedVersesCount = 31; // Genesis 1 has 31 verses

                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = "Genesis",
                    ChapterNumber = 1,
                };

                // Act
                var response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert
                Assert.NotNull(response);
                Assert.IsType<VersesResponse>(response);
                Assert.Equal(request.BookNameInFull, response.BookName);

                var versesDropdown = response.DropDown as List<dynamic>;
                Assert.NotNull(versesDropdown);
                Assert.Equal(expectedVersesCount, versesDropdown.Count);

                var firstVerse = versesDropdown.FirstOrDefault();
                Assert.NotNull(firstVerse);
                Assert.True(firstVerse.GetType().GetProperty("Id") != null);
            }

            [Fact]
            public async Task GetAllVersesInAChapterOFTheBible_ShouldReturnEmptyList_WhenInvalidChapterNumber()
            {
                // Arrange
                const int invalidChapterNumber = 100;
                const int verseNumber = 1;
                const string bookName = "genesis";

                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = bookName,
                    ChapterNumber = invalidChapterNumber,
                    VerseNumber = verseNumber
                };

                // Act
                var response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert
                Assert.Null(response.Resource);
                Assert.True(!response.DropDown.Any());
                Assert.IsAssignableFrom<IEnumerable<dynamic>>(response.DropDown);
            }

            [Fact]
            public async Task GetAllVersesInAChapterOFTheBible_ShouldReturnNull_WhenInvalidBookName()
            {
                // Arrange
                const int chapterNumber = 1;
                const int verseNumber = 1;
                const string invalidBookName = "InvalidBook";
                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = "InvalidBook",
                    ChapterNumber = chapterNumber,
                    VerseNumber = verseNumber
                };

                // Act
                var response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert
                Assert.Null(response);
            }

            [Fact]
            public async Task GetAllVersesInAChapterOFTheBible_ShouldNotReturnNull_WhenValidBookName()
            {
                // Arrange
                const int invalidChapterNumber = 1;
                const int verseNumber = 1;
                const string bookName = "genesis";

                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = bookName,
                    ChapterNumber = invalidChapterNumber,
                    VerseNumber = verseNumber
                };

                // Act
                VersesResponse response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert
                Assert.NotNull(response);
            }

            [Theory]
            [InlineData("exodus")]
            [InlineData("genesis")]
            public async Task GetAllVersesInAChapterOFTheBible_ShouldReturnVersesResponse_WhenValidBookName(string bookName)
            {
                // Arrange
                const int invalidChapterNumber = 1;
                const int verseNumber = 1;                

                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = bookName,
                    ChapterNumber = invalidChapterNumber,
                    VerseNumber = verseNumber
                };

                // Act
                VersesResponse response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert
                Assert.IsType<VersesResponse>(response);
            }
        }

    }
}
