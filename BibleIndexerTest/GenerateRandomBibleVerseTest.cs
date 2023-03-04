using BibleIndexerV2.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static BibleIndexerV2.Services.Implementations.BibleService;


namespace BibleIndexerTest
{
    public class GenerateRandomBibleVerseTest
    {
        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsNonEmptyResult()
        {
            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result);
        }       

        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsVerse()
        {
            // Act
            BibleVerseResponse? result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result?.VerseContent);
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsVerseWithExpectedBookName()
        {
            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result?.VerseContent);           
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsVerseWithExpectedChapterNumber()
        {
            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result?.ChapterNumber);           
        }

        [Fact]
        public async Task GenerateRandomBibleVerse_WithValidBibleBlob_ReturnsVerseWithExpectedVerseNumber()
        {
            // Arrange

            // Act
            var result = await GenerateRandomBibleVerse();

            // Assert
            Assert.NotNull(result?.VerseNumber);
        }
    }
}