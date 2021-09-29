using System;
using Xunit;


namespace GradeBook.Tests
{
    public class BookTests
    {
        //Test classes for gradebook
        [Fact]
        public void BookCalculatesAnAverageGrade()
        {
            //arrange

            var book = new Book("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            //act
            var result = book.GetStatistics();

            //asset

            Assert.Equal(85.6, result.Average, 1);
            Assert.Equal(90.5, result.High, 1);
            Assert.Equal(77.3, result.Low, 1);
            Assert.Equal('B', result.Letter);
        }
        [Fact]
        public void OnlyValidGradesAddedToBook()
        {
            var book = new Book("Valid Grade Book");

            //book.AddGrade(105.0);

            // Assert.Empty(book.grades);

        }

    }
}
