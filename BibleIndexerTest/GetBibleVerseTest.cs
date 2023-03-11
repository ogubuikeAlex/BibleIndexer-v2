using BibleIndexerV2.Models.Request;
using BibleIndexerV2.Models.Response;
using BibleIndexerV2.Services.Implementations;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BibleIndexerTest
{
    public class GetBibleVerseTest
    {
        [Fact]
        public async Task GetBibleVerse_ReturnsNull_WhenBookNotFound()
        {
            // Arrange
            GetBibleVerseRequest request = new GetBibleVerseRequest()
            {
                BookNameInFull = "Nonexistent Book",
                ChapterNumber = 1,
                VerseNumber = 1
            };

            // Act
            BibleVerseResponse? result = await BibleService.GetBibleVerse(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBibleVerse_ReturnsNull_WhenVerseIndexIsOutOfRange()
        {
            // Arrange
            GetBibleVerseRequest request = new GetBibleVerseRequest()
            {
                BookNameInFull = "Genesis",
                ChapterNumber = 1,
                VerseNumber = 1000
            };

            // Act
            BibleVerseResponse? result = await BibleService.GetBibleVerse(request);

            // Assert           
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBibleVerse_ReturnsCorrectVerseContent_WithCorrectVerseNumber()
        {
            // Arrange
            GetBibleVerseRequest request = new GetBibleVerseRequest()
            {
                BookNameInFull = "Genesis",
                ChapterNumber = 1,
                VerseNumber = 1
            };

            // Act
            BibleVerseResponse? result = await BibleService.GetBibleVerse(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("In the beginning God created the heaven and the earth.", result.VerseContent);
        }
    }
}
