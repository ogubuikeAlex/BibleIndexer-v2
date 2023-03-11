using BibleIndexerV2.Models.Response;
using BibleIndexerV2.Services.Implementations;
using System;
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

                // Act
                object books = await BibleService.GetAllBooksOfTheBible();

                // Assert
                Assert.NotNull(books);
                Assert.IsType<List<object>>(books);
            }

            [Fact]
            public async Task GetAllBooksOfTheBible_ShouldReturnSixtySixBooksOfTheBible()
            {
                // Arrange
                var expectedBookCount = 66; // The Bible has 66 books

                // Act
                IEnumerable<GetAllBookResonse> bookList = await BibleService.GetAllBooksOfTheBible();

                //Assert
                Assert.Equal(expectedBookCount, bookList.Count());
            }

            [Theory]
            [InlineData(1)]
            [InlineData(30)]
            [InlineData(66)]
            public async Task GetAllBooksOfTheBible_ShouldReturnGetAllBookResponse(int index)
            {
                // Arrange
                const int one = 1;
                var queryIndex = index - one;

                // Act
                IEnumerable<GetAllBookResonse> books = await BibleService.GetAllBooksOfTheBible();
                GetAllBookResonse book = books.ElementAt(index - 1);

                // Assert
                Assert.NotNull(book);
                Assert.IsType<GetAllBookResonse>(book);
                Assert.True(book.GetType().GetProperty("Id") != null);
                Assert.True(book.GetType().GetProperty("BookName") != null);
            }

            [Fact]
            public async Task GetAllBooksOfTheBible_ShouldReturnTheIdAndBookName()
            {
                // Arrange
                const int indexOne = 0;
                const string expectedBookName = "genesis";
                const string expectedBookId = "gn";

                // Act
                IEnumerable<GetAllBookResonse> books = await BibleService.GetAllBooksOfTheBible();
                GetAllBookResonse book = books.ElementAt(indexOne);

                // Assert                
                Assert.True(book.Id.ToLower() == expectedBookId);
                Assert.True(book.BookName.ToLower() == expectedBookName);
            }


        }
    }
}
