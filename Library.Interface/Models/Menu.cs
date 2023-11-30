using Library.Logic.Enums;
using Library.Logic.Models;
using System;
using System.Drawing;
using System.Numerics;
using System.Text.Json;

namespace Library.Interface.Models
{
    public class Menu
    {
        public List<Book> _books = new();
        private static string jsonPath = "json-test-file.json";
        private static string txtPath = "txt-test-file.txt";

        public void AddBook()
        {
            int input;

            Console.WriteLine("Choose add type: ");

            do
            {
                Console.WriteLine("Default book will be added - 1");
                Console.WriteLine("Book without author with given params - 2");
                Console.WriteLine("Book without author with given params - 3");
                Console.WriteLine("Book by parsing string - 4");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid input. You should enter only numbers.");
                    Console.WriteLine();
                }
            } while (input <= 0 || input > 4);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            switch (input)
            {
                case 1:
                    _books.Add(BookDefault());
                    break;

                case 2:
                    _books.Add(BookWithoutAuthor());
                    break;

                case 3:
                    _books.Add(BookWithAuthor());
                    break;

                case 4:
                    Console.WriteLine("Please, enter parsing string. (example - Novel;1949;1984;George Orwell");
                    string? stringToParse = Console.ReadLine();
                    try
                    {
                        Book parsedBook = BookByParse(stringToParse);

                        if (parsedBook != null)
                        {
                            Console.WriteLine();
                            parsedBook.PrintInfo();
                            Console.WriteLine("\nBoook was added successfully.");
                        }
                        else throw new ArgumentNullException("Parsing is not complete.");
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;
            }
        }

        public Book BookDefault()
        {
            Book defaultBook = new Book() { Name = "1984", Available = true, Year = 1949, Author = "George Orwell", Genre = Genre.Novel };
            Console.WriteLine("Book was added.");
            return defaultBook;
        }

        public Book BookWithoutAuthor()
        {
            int input;
            Book givenBook = null;
            Genre genre = Genre.Novel;

            Console.WriteLine("Enter book's info: ");

            do
            {
                Console.WriteLine("Please, choose genre: ");
                Console.WriteLine("Novel - 0");
                Console.WriteLine("Detective - 1");
                Console.WriteLine("Historical - 2");
                Console.WriteLine("Drama - 3");
                Console.WriteLine("Comics - 4");
                Console.WriteLine("Biography - 5");
                Console.WriteLine("Humor - 6");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid input. You should enter only numbers.");
                    input = -1;
                    Console.WriteLine();
                }
            } while (input < 0 || input > 6);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            switch (input)
            {
                case 0:
                    genre = Genre.Novel;
                    break;

                case 1:
                    genre = Genre.Detective;
                    break;

                case 2:
                    genre = Genre.Historical;
                    break;

                case 3:
                    genre = Genre.Drama;
                    break;

                case 4:
                    genre = Genre.Comics;
                    break;

                case 5:
                    genre = Genre.Biography;
                    break;

                case 6:
                    genre = Genre.Humor;
                    break;
            }

            try
            {
                Console.Write("Book name: ");
                string name = Convert.ToString(Console.ReadLine());
                Console.Write("Book author: ");
                string author = Convert.ToString(Console.ReadLine());
                Console.Write("Book year: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    throw new ArgumentException("Invalid input. You should enter only numbers.");
                }

                givenBook = new Book(genre, name);

                givenBook.Author = author;
                givenBook.Year = year;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return givenBook;
        }

        public Book BookWithAuthor()
        {
            int input;
            Book givenBook = null;
            Genre genre = Genre.Novel;

            Console.WriteLine("Enter book's info: ");

            do
            {
                Console.WriteLine("Please, choose genre: ");
                Console.WriteLine("Novel - 0");
                Console.WriteLine("Detective - 1");
                Console.WriteLine("Historical - 2");
                Console.WriteLine("Drama - 3");
                Console.WriteLine("Comics - 4");
                Console.WriteLine("Biography - 5");
                Console.WriteLine("Humor - 6");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid input. You should enter only numbers.");
                    input = -1;
                    Console.WriteLine();
                }
            } while (input < 0 || input > 6);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            switch (input)
            {
                case 0:
                    genre = Genre.Novel;
                    break;

                case 1:
                    genre = Genre.Detective;
                    break;

                case 2:
                    genre = Genre.Historical;
                    break;

                case 3:
                    genre = Genre.Drama;
                    break;

                case 4:
                    genre = Genre.Comics;
                    break;

                case 5:
                    genre = Genre.Biography;
                    break;

                case 6:
                    genre = Genre.Humor;
                    break;
            }

            try
            {
                Console.Write("Book name: ");
                string name = Convert.ToString(Console.ReadLine());
                Console.Write("Book author: ");
                string author = Convert.ToString(Console.ReadLine());
                Console.Write("Book year: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    throw new ArgumentException("Invalid input. You should enter only numbers.");
                }

                givenBook = new Book(genre, year, name, author);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return givenBook;
        }

        public Book BookByParse(string parsingString)
        {
            Book parseBook = null;

            if (Book.TryParse(parsingString, out parseBook))
            {
                return parseBook;
            }

            return parseBook;
        }

        public void OutputBooks()
        {
            if (!_books.Any())
            {
                Console.WriteLine("There's no books in the list.");
                return;
            }

            Console.WriteLine($"Total amount: {Book.CountBooks}");
            Console.WriteLine("List of books:");
            Console.WriteLine();
            foreach (var book in _books)
            {
                book.PrintInfo();
                Console.WriteLine();
            }
        }

        public void SearchBook()
        {
            if (!_books.Any())
            {
                Console.WriteLine("There's no books in the list.");
                return;
            }

            Console.WriteLine("Search by category: ");
            Console.WriteLine("Name - 1");
            Console.WriteLine("Year - 2");

            if (!int.TryParse(Console.ReadLine(), out int option) || option < 1 || option > 2)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Console.Write("Enter the value to search for: ");
            string searchValue = Console.ReadLine();
            List<Book> searchResults = new();

            switch (option)
            {
                case 1:
                    searchResults = _books.Where(book => book.Name.Equals(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case 2:
                    searchResults = _books.Where(book => book.Year.ToString() == searchValue).ToList();
                    break;
            }

            if (searchResults.Count == 0)
            {
                Console.WriteLine("There's no such books.");
            }
            else
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Result:");
                Console.WriteLine();
                foreach (Book book in searchResults)
                {
                    book.PrintInfo();
                    Console.WriteLine();
                }
            }
        }

        public void DeleteBook()
        {
            if (!_books.Any())
            {
                Console.WriteLine("There's no books in the list.");
                return;
            }

            Console.WriteLine("Delete by category: ");
            Console.WriteLine("Name - 1");
            Console.WriteLine("Year - 2");

            if (!int.TryParse(Console.ReadLine(), out int option) || option < 1 || option > 2)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Console.Write("Enter the value to delete objects by: ");
            string deleteValue = Console.ReadLine();

            _books.RemoveAll(book =>
            {
                switch (option)
                {
                    case 1:
                        return book.Name.Equals(deleteValue, StringComparison.OrdinalIgnoreCase);

                    case 2:
                        return book.Year.ToString() == deleteValue;

                    default:
                        return false;
                }
            });
            Console.WriteLine("Books were removed.");
            Console.WriteLine();
        }

        public void DemonstrateBehavior()
        {
            AddBook();
            Console.WriteLine($"In 2120 this book will be {_books[0].CalculateAge(2120)} y.o.");
            OutputBooks();
            SearchBook();
            DeleteBook();
        }

        public void DemonstrateStaticBehavior()
        {
            for (int i = 0; i < 3; i++)
            {
                AddBook();
                Console.WriteLine();
            }
            Book oldestBook = Book.FindOldestBook(_books);
            Console.WriteLine($"Oldest book: {oldestBook.Name}");
            Console.WriteLine("Book to string: ");
            string bookInString = _books[0].ToString();
            Console.WriteLine(bookInString);
            Console.WriteLine("Parsing book from string:");

            Book parseBook = null;

            if (Book.TryParse(bookInString, out parseBook))
            {
                parseBook.PrintInfo();
            }
            else Console.WriteLine("String cannot be parsed.");
        }

        public static void SaveBooksToTxtFIle(List<Book> books)
        {
            List<string> lines = new();

            foreach (var book in books)
            {
                lines.Add(book.ToString());
            }
            try
            {
                File.WriteAllLines(txtPath, lines);
                Console.WriteLine($"Check out the TXT file at: {Path.GetFullPath(txtPath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SaveBooksToJsonFIle(List<Book> books)
        {
            try
            {
                string jsonstring = "";
                foreach (var book in books)
                {
                    jsonstring += JsonSerializer.Serialize(book);
                    jsonstring += "\n";
                }
                File.WriteAllText(jsonPath, jsonstring);
                Console.WriteLine($"Check out the JSON file at: {Path.GetFullPath(jsonPath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Book> ReadTxtFile(List<Book> books)
        {
            try
            {
                List<string> lines = new();

                lines = File.ReadAllLines(txtPath).ToList();
                if (lines.Count == 0)
                {
                    throw new Exception("There are no lines in file.");
                }
                Console.WriteLine("Books in file:\n");

                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                    bool result = Book.TryParse(item, out Book? book);
                    if (result) books.Add(book);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Reading TXT file error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return books;
        }

        public static List<Book> ReadJsonFile(List<Book> books)
        {
            try
            {
                List<string> lines = new();
                lines = File.ReadAllLines(jsonPath).ToList();
                if (lines.Count == 0)
                {
                    throw new Exception("There are no lines in file.");
                }
                Console.WriteLine("\nBooks in file:\n");

                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                    Book? book = JsonSerializer.Deserialize<Book>(line);
                    if (book != null) books.Add(book);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Reading JSON file error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return books;
        }

        public void Bar()
        {
            while (true)
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Options to choose:");
                Console.WriteLine("Add a book - 1");
                Console.WriteLine("Show books - 2");
                Console.WriteLine("Search for a book - 3");
                Console.WriteLine("Delete a book from list - 4");
                Console.WriteLine("Demonstrate behavior - 5");
                Console.WriteLine("Demonstrate  static behavior - 6");
                Console.WriteLine("Books to file - 7");
                Console.WriteLine("Read books from file - 8");
                Console.WriteLine("Clear list - 9");
                Console.WriteLine("Exit - 0");

                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Please enter an option from 0 to 5.");
                    continue;
                }
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                switch (input)
                {
                    case 1:
                        AddBook();
                        break;

                    case 2:
                        OutputBooks();
                        break;

                    case 3:
                        SearchBook();
                        break;

                    case 4:
                        DeleteBook();
                        break;

                    case 5:
                        DemonstrateBehavior();
                        break;

                    case 6:
                        DemonstrateStaticBehavior();
                        break;

                    case 7:
                        Console.WriteLine("Save book to file: ");
                        Console.WriteLine(".txt file - 1");
                        Console.WriteLine(".json file - 2");

                        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 2)
                        {
                            Console.WriteLine("Invalid option.");
                            return;
                        }

                        switch (choice)
                        {
                            case 1:
                                SaveBooksToTxtFIle(_books);
                                break;

                            case 2:
                                SaveBooksToJsonFIle(_books);
                                break;
                        }
                        break;

                    case 8:
                        Console.WriteLine("Read books from file: ");
                        Console.WriteLine(".txt file - 1");
                        Console.WriteLine(".json file - 2");

                        if (!int.TryParse(Console.ReadLine(), out choice)
                            || choice < 1
                            || choice > 2)
                        {
                            Console.WriteLine("Invalid option.");
                            return;
                        }

                        switch (choice)
                        {
                            case 1:
                                ReadTxtFile(_books);
                                break;

                            case 2:
                                ReadJsonFile(_books);
                                break;
                        }
                        break;

                    case 9:
                        _books.Clear();
                        Book.CountBooks = 0;
                        Console.WriteLine("Books list was successfully cleared.");
                        break;

                    case 0:
                        Console.WriteLine("Exiting the program.");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please enter a valid option.");
                        break;
                }
            }
        }
    }
}