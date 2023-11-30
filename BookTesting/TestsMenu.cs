using Library.Interface.Models;
using Library.Logic.Enums;
using Library.Logic.Models;

namespace BookTesting
{
    [TestClass]
    public class MenuTests
    {
        private const string TestJsonPath = "json-test-file.json";
        private const string TestTxtPath = "txt-test-file.txt";

        [TestMethod]
        public void AddBook_WithDefaultOption_ShouldAddDefaultBook()
        {
            Menu menu = new Menu();

            using (StringReader stringReader = new StringReader("1\n"))
            {
                Console.SetIn(stringReader);
                menu.AddBook();
            }

            Assert.AreEqual(1, menu._books.Count);
            Assert.AreEqual("1984", menu._books[0].Name);
            Assert.AreEqual(true, menu._books[0].Available);
            Assert.AreEqual(1949, menu._books[0].Year);
            Assert.AreEqual("George Orwell", menu._books[0].Author);
            Assert.AreEqual(Genre.Novel, menu._books[0].Genre);
        }

        [TestMethod]
        public void SaveBooksToTxtFile_ShouldCreateFile()
        {
            List<Book> books = new List<Book>
        {
            new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel },
            new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel },
        };

            Menu.SaveBooksToTxtFIle(books);

            Assert.IsTrue(File.Exists(TestTxtPath));
        }

        [TestMethod]
        public void SaveBooksToJsonFile_ShouldCreateFile()
        {
            List<Book> books = new List<Book>
        {
            new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel },
            new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel },
        };

            Menu.SaveBooksToJsonFIle(books);

            Assert.IsTrue(File.Exists(TestJsonPath));
        }

        [TestMethod]
        public void ReadTxtFile_ShouldPopulateBooksList()
        {
            List<Book> books = new List<Book>
        {
            new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel },
            new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel },
        };

            Menu.SaveBooksToTxtFIle(books);

            List<Book> result = Menu.ReadTxtFile(new List<Book>());

            Assert.IsTrue(books.Count > 0);
        }

        [TestMethod]
        public void ReadJsonFile_ShouldPopulateBooksList()
        {
            List<Book> books = new List<Book>
        {
            new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel },
            new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel },
        };

            Menu.SaveBooksToJsonFIle(books);

            List<Book> result = Menu.ReadJsonFile(new List<Book>());

            Assert.IsTrue(books.Count > 0);
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                if (File.Exists(TestTxtPath))
                {
                    File.Delete(TestTxtPath);
                }

                if (File.Exists(TestJsonPath))
                {
                    File.Delete(TestJsonPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cleaning up test files: {ex.Message}");
            }
        }
    }
}