using BibleIndexerV2.Models.Response;
using BibleIndexerV2.Services.Implementations;
using System.Threading.Tasks;
using Xunit;

namespace BibleIndexerTest
{
    public class GetBookOfTheBibleTest
    {
        [Fact]
        public async Task GetBookOfTheBible_Returns_Null_When_Book_Not_Found()
        {
            // Arrange
            string bookName = "Nonexistent Book";

            // Act
            BlobResponse? result = await BibleService.GetBookOfTheBible(bookName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBookOfTheBible_Returns_Correct_Book()
        {
            // Arrange
            string bookName = "john";

            // Act
            BlobResponse? result = await BibleService.GetBookOfTheBible(bookName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
        }
    }
}
