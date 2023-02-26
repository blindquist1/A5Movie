using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace A5Movie.Services;

/// <summary>
///     You would need to inject your interfaces here to execute the methods in Invoke()
///     See the commented out code as an example
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
            List<MovieService> movie = new List<MovieService>();

            //Display of the main menu
            int choice = 0;
            do
            {
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("3) Quit");

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

                // Logic would need to exist to validate inputs and data prior to writing to the file
                // You would need to decide where this logic would reside.
                // Is it part of the FileService or some other service?
                if (choice == 1)
                {
                    //MovieService movie = new MovieService();
                    movie.Add(new MovieService(movie));
                    //This seems to write the entire list of movies to the file rather than just the one recently added, how do I get this to just write one record?
                    string json = JsonConvert.SerializeObject(movie);
                    _fileService.Write(file,json);
                }
                else if (choice == 2)
                {
                    _fileService.Read(file);
                }
            }
            while (choice != 3);
        }
    }
}
