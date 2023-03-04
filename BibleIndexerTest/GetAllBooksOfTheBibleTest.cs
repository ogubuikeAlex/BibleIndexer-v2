using BibleIndexerV2.Services.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BibleIndexerTest
{
    public class GetAllBooksOfTheBibleTest
    {
        public class BibleApiTests
        {
            [Fact]
            public async Task GetAllBooksOfTheBible_ShouldReturnListOfBooks()
            {
                // Arrange
                var expectedBookCount = 66; // The Bible has 66 books

                // Act
                object books = await BibleService.GetAllBooksOfTheBible();

                // Assert
                Assert.NotNull(books);
                Assert.IsType<List<object>>(books);

                var bookList = books as List<object>;
                Assert.Equal(expectedBookCount, bookList.Count);

                object firstBook = bookList.FirstOrDefault();
                Assert.NotNull(firstBook);
                Assert.True(firstBook.GetType().GetProperty("Id") != null);
                Assert.True(firstBook.GetType().GetProperty("Book") != null);
            }
        }
    }
}
