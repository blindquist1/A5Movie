using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5Movie.Services
{
    internal class MovieService
    {
        //Properties
        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        public MovieService()
        {
        }
        
        //Entry of a new movie
        public MovieService(List<MovieService> movie)
        {
            Title = InputService.GetStringWithPrompt("Enter movie title: ", "Entry must be text: ");

            // check for duplicate title
            int Counter = 0;
            if (movie.Count > 0)
            {
                List<MovieService> LowerCaseMovieTitles = movie.Where(x => x.Title.ToLower() == Title.ToLower()).ToList();
                Counter = LowerCaseMovieTitles.Count;
            }

            if (Counter > 0 )
            {
                Console.WriteLine("That movie has already been entered");
                //logger.Info("Duplicate movie title {Title}", movieTitle);
            }
            else
            {
                // generate movie id - use max Id value in movies list + 1
                if (movie.Count > 0)
                {
                    Id = movie.Max(x => x.Id) + 1;
                }
                else
                {
                    Id = 1;
                }

                // input genres
                List<string> genres = new List<string>();
                string genre;
                do
                {
                    // ask user to enter genre
                    genre = InputService.GetStringWithPrompt("Enter genre (or done to quit): ", "Entry must be text: ");
                    // if user enters "done"
                    // or does not enter a genre do not add it to list
                    if (genre != "done" && genre.Length > 0)
                    {
                        genres.Add(genre);
                    }
                } while (genre != "done");
                // specify if no genres are entered
                if (genres.Count == 0)
                {
                    genres.Add("(no genres listed)");
                }
                // use "|" as delimeter for genres
                string genresString = string.Join(",", genres);
                Genres = genresString;

                // if there is a comma(,) in the title, wrap it in quotes
                Title = Title.IndexOf(',') != -1 ? $"\"{Title}\"" : Title;

                // display movie id, title, genres
                Console.WriteLine($"{Id},{Title},{genresString}");
            }

        }
    }
}
