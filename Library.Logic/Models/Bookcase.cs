using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.Models
{
    internal class Bookcase
    {
        private List<string> Novels { get; } = new List<string> { "1984", "Looking for Alaska", "Kafka on the shore", "Fight club", "Blood Meridian" };
        private List<string> Detectives { get; } = new List<string> { "Murder on the orient express", "The Hound of the Baskervilles", "The Maltese Falcon", "The Big Sleep", "The Name of the Rose" };
        private List<string> HistoricalBooks { get; } = new List<string> { "The Japanese Myths", "Lost Kingdom", "Bloodlands", "History of Britain", "History of Ukraine", "USA History" };
        private List<string> Dramas { get; } = new List<string> { "Romeo and Juliet", "Hamlet", "To Kill a Mockingbird", "After Dark", "The Family Upstairs" };
        private List<string> Comics { get; } = new List<string> { "Batman", "Spider-man", "Superman", "Flesh", "Wolverine", "X-men", "Hulk" };
        private List<string> Biographies { get; } = new List<string> { "Winston Churchill", "Tom Cruise", "Will Smith" };
        private List<string> HumorBooks { get; } = new List<string> { "Laughs", "Jokes", "Comedy" };

        public bool IsBookHere(string genre, string name)
        {
            List<string> selectedGenreList = GetGenreList(genre);

            if (selectedGenreList != null)
            {
                return selectedGenreList.Contains(name);
            }

            return false;
        }

        private List<string> GetGenreList(string genre)
        {
            switch (genre)
            {
                case "Novel":
                    return Novels;

                case "Detective":
                    return Detectives;

                case "Historical":
                    return HistoricalBooks;

                case "Drama":
                    return Dramas;

                case "Comics":
                    return Comics;

                case "Biography":
                    return Biographies;

                case "Humor":
                    return HumorBooks;

                default:
                    return null;
            }
        }
    }
}