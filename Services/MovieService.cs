using A5Movie.Models;
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
        // global scope
        public List<Movie> Movies { get; set; }

        string TitleCheck;

        public MovieService()
        {
        }
        
        //Check if new movie is a duplicate of an existing movie
        public bool IsDuplicate()
        {
            if (Movies.Count > 0)
            {
                List<Movie> LowerCaseMovieTitles = Movies.Where(x => x.Title.ToLower() == TitleCheck.ToLower()).ToList();

                if (LowerCaseMovieTitles.Count > 0)
                {
                    Console.WriteLine("That movie has already been entered");
                    //logger.Info($"Duplicate movie title {TitleCheck}");

                    return true;
                }
            }
            // default fallback
            return false;
        }
        
        //List of existing movies
        public MovieService(List<Movie> movies)
        {
            Movies = movies;
        }

        //Entry of a new movie
        public Movie CreateMovie()
        {
            Console.WriteLine();
            TitleCheck = InputService.GetStringWithPrompt("Enter movie title: ", "Entry must be text: ");

            var isDuplicate = IsDuplicate();

            if (!isDuplicate)
            {
                var movie = new Movie();
                
                movie.Title = TitleCheck;
                
                // generate movie id - use max Id value in movies list + 1
                if (Movies.Count > 0)
                {
                    movie.Id = Movies.Max(x => x.Id) + 1;
                }
                else
                {
                    movie.Id = 1;
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
                string genresString = string.Join("|", genres);
                movie.Genres = genresString;

                // if there is a comma(,) in the title, wrap it in quotes
                movie.Title = movie.Title.IndexOf(',') != -1 ? $"\"{movie.Title}\"" : movie.Title;

                // display movie id, title, genres
                Console.WriteLine();
                Console.WriteLine($"Movie: {movie.Id} {movie.Title} {movie.Genres}");
                Console.WriteLine();

                return movie;
            }

            return null;
        }
    }
}
