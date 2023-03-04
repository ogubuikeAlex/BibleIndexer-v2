using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BibleIndexerTest
{
    public class GetBibleVerseTest
    {
        [Fact]
        public async Task GetBibleVerse_Returns_Null_When_Book_Not_Found()
        {
            // Arrange
            GetBibleVerseRequest request = new GetBibleVerseRequest()
            {
                BookNameInFull = "Nonexistent Book",
                ChapterNumber = 1,
                VerseNumber = 1
            };

            // Act
            BibleVerseResponse? result = await BibleApi.GetBibleVerse(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBibleVerse_Returns_Null_When_Verse_Not_Found()
        {
            // Arrange
            GetBibleVerseRequest request = new GetBibleVerseRequest()
            {
                BookNameInFull = "Genesis",
                ChapterNumber = 1,
                VerseNumber = 1000
            };

            // Act
            BibleVerseResponse? result = await BibleApi.GetBibleVerse(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBibleVerse_Returns_Correct_Verse_Content()
        {
            // Arrange
            GetBibleVerseRequest request = new GetBibleVerseRequest()
            {
                BookNameInFull = "Genesis",
                ChapterNumber = 1,
                VerseNumber = 1
            };

            // Act
            BibleVerseResponse? result = await BibleApi.GetBibleVerse(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("In the beginning God created the heavens and the earth.", result.VerseContent);
        }
    }
}
