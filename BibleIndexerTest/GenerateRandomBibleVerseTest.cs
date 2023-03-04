using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BibleIndexerTest
{
    public class GenerateRandomBibleVerseTest
    {
        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsNonEmptyResult()
        {
            // Arrange

            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithNullBibleBlob_ReturnsNullResult()
        {
            // Arrange
            IEnumerable<dynamic>? bibleBlob = null;
            _mockedService.Setup(s => s.GetBlob()).ReturnsAsync(bibleBlob);

            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithEmptyBibleBlob_ReturnsNullResult()
        {
            // Arrange
            IEnumerable<dynamic>? bibleBlob = Enumerable.Empty<dynamic>();
            _mockedService.Setup(s => s.GetBlob()).ReturnsAsync(bibleBlob);

            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsVerse()
        {
            // Arrange

            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result?.Verse);
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsVerseWithExpectedBookName()
        {
            // Arrange

            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result?.Verse);
            Assert.Equal(expectedBookName, result?.Verse?.BookNameInFull); // set expectedBookName to the expected name of the book
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsVerseWithExpectedChapterNumber()
        {
            // Arrange

            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result?.Verse);
            Assert.Equal(expectedChapterNumber, result?.Verse?.ChapterNumber); // set expectedChapterNumber to the expected chapter number
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsVerseWithExpectedVerseNumber()
        {
            // Arrange

            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result?.Verse);
            Assert.Equal(expectedVerseNumber, result?.Verse?.VerseNumber); // set expectedVerseNumber to the expected verse number

        }
    }
}