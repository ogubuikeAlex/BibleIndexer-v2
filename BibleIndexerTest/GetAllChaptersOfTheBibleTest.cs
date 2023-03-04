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
            public async Task GetAllVersesInAChapterOFTheBible_ShouldReturnNull_WhenInvalidChapterNumber()
            {
                // Arrange
                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = "Genesis",
                    ChapterNumber = 100,
                };

                // Act
                var response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert
                Assert.Null(response);
            }

            [Fact]
            public async Task GetAllVersesInAChapterOFTheBible_ShouldReturnNull_WhenInvalidBookName()
            {
                // Arrange
                var request = new GetBibleVerseRequest
                {
                    BookNameInFull = "InvalidBook",
                    ChapterNumber = 1,
                };

                // Act
                var response = await BibleService.GetAllVersesInAChapterOFTheBible(request);

                // Assert
                Assert.Null(response);
            }
        }

    }
}
