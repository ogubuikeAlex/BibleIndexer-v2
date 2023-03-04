using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            BlobResponse? result = await BibleApi.GetBookOfTheBible(bookName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBookOfTheBible_Returns_Correct_Book()
        {
            // Arrange
            string bookName = "john";

            // Act
            BlobResponse? result = await BibleApi.GetBookOfTheBible(bookName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
        }
    }
}
