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
            public async Task GetAllVersesInAChapterOFTheBible_ShouldReturnCorrectBookName_WhenValidBookName()
            {
                // Arrange
                var bookNameInfull = "genesis"; 
                var chapterNumber = 1; 

                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = bookNameInfull,
                    ChapterNumber = chapterNumber,
                };

                // Act
                var response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert                
                Assert.Equal(request.BookNameInFull, response.BookName.ToLower());               
            }

            [Fact]
            public async Task GetAllVersesInAChapterOFTheBible_ShouldReturnCorrectVerseCount_WhenValidBookNameAndValidChapterNumber()
            {
                // Arrange
                var bookNameInfull = "genesis";
                var chapterNumber = 1;
                var expectedVersesCount = 31;

                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = bookNameInfull,
                    ChapterNumber = chapterNumber,
                };

                // Act
                var response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert                
                Assert.Equal(expectedVersesCount, response.DropDown.Count());
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
                Assert.Empty(response.DropDown);
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
