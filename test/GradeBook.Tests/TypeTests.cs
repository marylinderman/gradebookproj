using System;
using Xunit;


namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void ValueWriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            //log = new WriteLogDelegate(ReturnMessage); 
            // log = ReturnMessage;
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello!");

            //Assert.Equal("Hello!", result);

            //Invoked ReturnMessage twice and IncrementCount once.
            Assert.Equal(3, count);
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }

        [Fact]
        public void StringBehaveLikeValueType()
        {
            string name = "Scott";
            var upper = MakeUppercase(name);
            Assert.Equal("SCOTT", upper);

        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);

        }
        private int GetInt()
        {

            return 3;
        }

        private void SetInt(ref int z)
        {
            z = 42;

        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);

        }

        private void GetBookSetName(ref Book book, string name)
        {
            book = new Book(name);
            //book.Name = name;
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);

        }

        private void GetBookSetName(Book book, string name)
        {
            book = new Book(name);
            book.Name = name;
        }






        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);

        }

        private void SetName(Book book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");
            var book3 = book1;
            var book4 = book1;

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.Equal("Book 1", book3.Name);
            Assert.Equal("Book 1", book4.Name);

        }
        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;


            Assert.Same(book1, book2);


        }

        Book GetBook(string name)
        {
            return new Book(name);
        }
    }
}
