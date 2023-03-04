using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var books = await BibleApi.GetAllBooksOfTheBible();

                // Assert
                Assert.NotNull(books);
                Assert.IsType<List<object>>(books);

                var bookList = books as List<object>;
                Assert.Equal(expectedBookCount, bookList.Count);

                var firstBook = bookList.FirstOrDefault();
                Assert.NotNull(firstBook);
                Assert.True(firstBook.GetType().GetProperty("Id") != null);
                Assert.True(firstBook.GetType().GetProperty("Book") != null);
            }
        }

    }
}
