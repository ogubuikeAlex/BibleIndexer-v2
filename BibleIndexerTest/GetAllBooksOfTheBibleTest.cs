using BibleIndexerV2.Models.Response;
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

           


        }
    }
}
