using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BibleIndexerTest
{
    public class GetChaptersInABookOfTheBibleTest
    {
        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithNullInput_ReturnsNull()
        {
            // Arrange

            // Act
            var result = await GetChaptersInABookOfTheBible(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithUnknownBookName_ReturnsNull()
        {
            // Arrange

            // Act
            var result = await GetChaptersInABookOfTheBible("UnknownBookName");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithValidBookName_ReturnsChapters()
        {
            // Arrange

            // Act
            var result = await GetChaptersInABookOfTheBible("BookName");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.DropDown);
            Assert.NotEmpty(result.Resource);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithValidBookName_ReturnsCorrectNumberOfChapters()
        {
            // Arrange

            // Act
            var result = await GetChaptersInABookOfTheBible("BookName");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedNumberOfChapters, result.DropDown.Count()); // set expectedNumberOfChapters to the expected number of chapters for the given book
            Assert.Equal(expectedNumberOfChapters, result.Resource.Count()); // set expectedNumberOfChapters to the expected number of chapters for the given book
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithBookWithNoChapters_ReturnsEmptyList()
        {
            // Arrange

            // Act
            var result = await GetChaptersInABookOfTheBible("BookWithNoChapters");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.DropDown);
            Assert.Empty(result.Resource);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithValidBookName_ReturnsExpectedBookName()
        {
            // Arrange

            // Act
            var result = await GetChaptersInABookOfTheBible("BookName");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBookName, result.BookName); // set expectedBookName to the expected name of the book
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithNullBookChapters_ReturnsEmptyList()
        {
            // Arrange
            var bibleResult = new BlobResponse() { Chapters = null };

            // Act
            var result = await GetChaptersInABookOfTheBible("BookName");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.DropDown);
            Assert.Empty(result.Resource);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithEmptyBookChapters_ReturnsEmptyList()
        {
            // Arrange
            var bibleResult = new BlobResponse() { Chapters = "[]" };

            // Act
            var result = await GetChaptersInABookOfTheBible("BookName");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.DropDown);
            Assert.Empty(result.Resource);
        }

    }
}
