using Library.Logic.Enums;
using Library.Logic.Models;

namespace BookTesting
{
    [TestClass]
    public class TestsBook
    {
        [TestMethod]
        public void DefaultConstructor_InitializesCorrectly()
        {
            Book book = new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel };

            Assert.AreEqual(1949, book.Year);
            Assert.AreEqual("1984", book.Name);
            Assert.AreEqual("George Orwell", book.Author);
            Assert.AreEqual(Genre.Novel, book.Genre);
            Assert.IsTrue(book.Available);
        }

        [TestMethod]
        public void ParameterizedConstructorWithGenreAndName_InitializesCorrectly()
        {
            Genre genre = Genre.Novel;
            string name = "1984";

            Book book = new Book(genre, name);

            Assert.AreEqual(genre, book.Genre);
            Assert.AreEqual(name, book.Name);
            Assert.IsTrue(book.Available);
            Assert.IsTrue(Book.Sheets >= 70 && Book.Sheets <= 500);
        }

        [TestMethod]
        public void ParameterizedConstructorWithAllProperties_InitializesCorrectly()
        {
            Genre genre = Genre.Novel;
            int year = 1949;
            string name = "1984";
            string author = "George Orwell";

            Book book = new Book(genre, year, name, author);

            Assert.AreEqual(genre, book.Genre);
            Assert.AreEqual(year, book.Year);
            Assert.AreEqual(name, book.Name);
            Assert.AreEqual(author, book.Author);
            Assert.IsTrue(book.Available);
            Assert.IsTrue(Book.Sheets >= 70 && Book.Sheets <= 500);
        }

        [TestMethod]
        public void CalculateAge_ReturnsCorrectAge()
        {
            Book book = new Book(Genre.Novel, 1990, "Sample Book", "Sample Author");
            int currentYear = 2023;

            int calculatedAge = book.CalculateAge(currentYear);

            Assert.AreEqual(33, calculatedAge);
        }

        [TestMethod]
        public void FindOldestBook_ReturnsOldestBook()
        {
            List<Book> books = new List<Book>
            {
                new Book(Genre.Novel, 1990, "Book A", "Author A"),
                new Book(Genre.Historical, 1985, "Book B", "Author B"),
                new Book(Genre.Detective, 2000, "Book C", "Author C")
            };

            Book oldestBook = Book.FindOldestBook(books);

            Assert.AreEqual(1985, oldestBook.Year);
        }

        [TestMethod]
        public void FindOldestBook_EmptyList_ThrowsArgumentException()
        {
            List<Book> emptyList = new List<Book>();

            Assert.ThrowsException<ArgumentException>(() => Book.FindOldestBook(emptyList));
        }

        [TestMethod]
        public void Parse_ValidInput_ParsesCorrectly()
        {
            string input = "Novel;2000;Sample Book;Sample Author";

            Book book = Book.Parse(input);

            Assert.AreEqual(Genre.Novel, book.Genre);
            Assert.AreEqual(2000, book.Year);
            Assert.AreEqual("Sample Book", book.Name);
            Assert.AreEqual("Sample Author", book.Author);
        }

        [TestMethod]
        public void Parse_InvalidInput_ThrowsArgumentException()
        {
            string input = "InvalidInput";

            Assert.ThrowsException<ArgumentException>(() => Book.Parse(input));
        }

        [TestMethod]
        public void TryParse_ValidInput_ReturnsTrueAndParsesCorrectly()
        {
            string input = "Novel;1949;1984;George Orwell";
            Book parsedBook;

            bool result = Book.TryParse(input, out parsedBook);

            Assert.IsTrue(result);
            Assert.AreEqual(Genre.Novel, parsedBook.Genre);
            Assert.AreEqual(1949, parsedBook.Year);
            Assert.AreEqual("1984", parsedBook.Name);
            Assert.AreEqual("George Orwell", parsedBook.Author);
        }

        [TestMethod]
        public void TryParse_InvalidInput_ReturnsFalseAndDoesNotParseBook()
        {
            string invalidInput = "Invalid Input";
            Book parsedBook;

            bool result = Book.TryParse(invalidInput, out parsedBook);

            Assert.IsFalse(result);
            Assert.IsNull(parsedBook);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectString()
        {
            Book defaultBook = new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel };
            string bookStringed = defaultBook.ToString();

            Assert.AreEqual("Novel;1949;1984;George Orwell", defaultBook.ToString());
        }
    }
}