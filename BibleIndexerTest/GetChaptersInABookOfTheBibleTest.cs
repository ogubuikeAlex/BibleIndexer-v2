using BibleIndexerV2.Models.Response;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static BibleIndexerV2.Services.Implementations.BibleService;

namespace BibleIndexerTest
{
    public class GetChaptersInABookOfTheBibleTest
    {
        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithNullInput_ReturnsNull()
        {
            // Act
            var result = await GetChaptersInABookOfTheBible(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithUnknownBookName_ReturnsNull()
        {
            // Arrange
            string nonexistentBibleName = "AlexTheKing";

            // Act
            var result = await GetChaptersInABookOfTheBible(nonexistentBibleName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithValidBookName_ReturnsChapters()
        {
            // Arrange
            string genesis = "genesis";

            // Act
            var result = await GetChaptersInABookOfTheBible(genesis);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.DropDown);
            Assert.NotEmpty(result.Resource);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithValidBookName_ReturnsCorrectNumberOfChapters()
        {
            // Arrange
            byte expectedNumberOfChapters = 50;
            string bookname = "genesis";

            // Act
            var result = await GetChaptersInABookOfTheBible(bookname);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedNumberOfChapters, result.DropDown.Count()); 
            Assert.Equal(expectedNumberOfChapters, result.Resource.Count()); 
        }
       
        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithValidBookName_ReturnsExpectedBookName()
        {
            // Arrange
            string expectedBookName = "genesis";

            // Act
            var result = await GetChaptersInABookOfTheBible(expectedBookName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBookName, result.BookName);
        }

        [Fact]
        public async Task GetChaptersInABookOfTheBible_WithInValidBookName_ReturnsNull()
        {
            // Arrange
            string nonexistentBibleName = "AlexTheKing";

            // Act
            var result = await GetChaptersInABookOfTheBible("BookName");

            // Assert
            Assert.Null(result);            
        }
    }
}
