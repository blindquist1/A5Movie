using A5Movie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace A5Movie.Services;

/// <summary>
///     Main service to display the menu and prompt for a choice.
/// </summary>
public class MainService : IMainService
{
    private readonly IFileService _fileService;
    public MainService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public void Invoke()
    {
        // path to movie data file
        string file = $"{Environment.CurrentDirectory}/data/movies.json";

        // make sure movie file exists
        if (_fileService.FileCheck(file))
        {
            //Creates list object to track movies
            List<Movie> movies = new List<Movie>();

            //Display of the main menu
            int choice = 0;
            do
            {
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("3) Quit");
                Console.WriteLine();

                bool validEntry = false;

                //Keep looping through until user chooses a valid entry, an integer and between 1 and 3.
                while (!validEntry)
                {
                    choice = InputService.GetIntWithPrompt("Select an option: ", "Entry must be an integer");
                    if (choice < 1 || choice > 3)
                    {
                        Console.WriteLine("Entry must be between 1 and 3");
                    }
                    else
                    {
                        validEntry = true;
                    }
                }

                //Add the movie
                if (choice == 1)
                {
                    //Create a movie service that tracks the list of movies
                    MovieService movieService = new MovieService(movies);

                    //Call the movie service to prompt the user for the new movie info
                    var movie = movieService.CreateMovie();

                    //Add that movie to the list of movies
                    if (movie != null)
                    {
                        movies.Add(movie);

                        //Write the single movie to the file
                        string json = JsonConvert.SerializeObject(movie);
                        _fileService.Write(file, json);
                    }
                }
                //Display all movies
                else if (choice == 2)
                {
                    Console.WriteLine();

                    if (movies.Count > 0)
                    {
                        _fileService.Read(file);
                    }
                    else
                    {
                        Console.WriteLine("There are no movies to display.");
                    }
                    Console.WriteLine();
                }
            }
            while (choice != 3);
        }
    }
}
