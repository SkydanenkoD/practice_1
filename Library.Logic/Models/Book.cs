using Library.Logic.Enums;

namespace Library.Logic.Models
{
    public class Book
    {
        private int _year;
        private string? _name;
        private string? _author;
        private bool _available;
        private Genre _genre;
        private double _price;
        private int _sheets;
        private readonly Bookcase _bookcase = new();

        public static int Sheets
        {
            get
            {
                Random random = new Random();
                return random.Next(70, 501);
            }
        }

        public Book()
        {
            _price = CalculatePrice();
            _sheets = Sheets;
            CountBooks++;
        }

        public Book(Genre genre, string name) : this()
        {
            _genre = genre;
            _name = name;
            Available = _bookcase.IsBookHere(genre.ToString(), name);
        }

        public Book(Genre genre, int year, string name, string author) : this(genre, name)
        {
            _author = author;
            _year = year;
        }

        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                if (value <= 0 || value > 2023)
                    throw new ArgumentOutOfRangeException("Wrong year.");
                _year = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length <= 0 || value.Length >= 20)
                {
                    throw new ArgumentException("Length of input is incorrect.");
                }

                _name = value;
            }
        }

        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                if (value.Length <= 0 || value.Length >= 20)
                {
                    throw new ArgumentException("Length of input is incorrect.");
                }

                _author = value;
            }
        }

        public Genre Genre { get; set; }

        public bool Available { get; set; } = false;

        public static int CountBooks { get; set; } = 0;

        private double CalculatePrice()
        {
            double priceFactor = 0.2;

            if (!Available)
            {
                return Math.Round(_year * priceFactor + 150, 2);
            }

            return Math.Round(_year * priceFactor, 2);
        }

        private int CalculateAge()
        {
            int currentYear = DateTime.Now.Year;

            int age = currentYear - _year;
            return age;
        }

        public int CalculateAge(int currentYear)
        {
            int age = currentYear - _year;
            return age;
        }

        public void PrintInfo()
        {
            Console.WriteLine("Book information:");
            Console.WriteLine($"- Name: {_name}");
            Console.WriteLine($"- Author: {_author}");
            Console.WriteLine($"- Genre: {_genre}");
            Console.WriteLine($"- Year: {_year}");
            Console.WriteLine($"- Age: {CalculateAge()}");
            Console.WriteLine($"- Available: {_available}");
            Console.WriteLine($"- Price: {_price}");
        }

        public static Book FindOldestBook(List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                throw new ArgumentException("List is empty.");
            }

            Book oldestBook = books[0];
            foreach (var book in books)
            {
                if (book.Year < oldestBook.Year)
                {
                    oldestBook = book;
                }
            }

            return oldestBook;
        }

        public static Book Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input cannot be empty.");
            }

            string[] bookInfo = input.Split(';');
            if (bookInfo.Length != 4)
            {
                throw new ArgumentException("Invalid input format. Expected format: Genre;Year;Name;Author");
            }

            Genre genre;
            if (!Enum.TryParse(bookInfo[0], out genre))
            {
                throw new ArgumentException("Invalid Genre value.");
            }

            int year;
            if (!int.TryParse(bookInfo[1], out year) || year <= 0 || year > 2023)
            {
                throw new ArgumentException("Invalid Year value.");
            }

            string name = bookInfo[2].Trim();
            if (name.Length <= 0 || name.Length >= 20)
            {
                throw new ArgumentException("Invalid Name length.");
            }

            string author = bookInfo[3].Trim();
            if (author.Length <= 0 || author.Length >= 20)
            {
                throw new ArgumentException("Invalid Author length.");
            }

            Book book = new Book(genre, year, name, author);
            return book;
        }

        public static bool TryParse(string input, out Book book)
        {
            book = null;

            try
            {
                book = Parse(input);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{_genre};{_year};{_name};{_author}";
        }
    }
}