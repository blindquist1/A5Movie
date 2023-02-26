using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace A5Movie.Services;

/// <summary>
///     This concrete service and method only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public class FileService : IFileService
{
    private readonly ILogger<IFileService> _logger;

    public FileService(ILogger<IFileService> logger)
    {
        _logger = logger;
    }
    public void Read(string file)
    {
        //_logger.Log(LogLevel.Information, "Reading");
        //Console.WriteLine("*** I am reading");

        using (StreamReader sr = new StreamReader(file))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                //Console.WriteLine(JsonConvert.DeserializeObject(line));
                //MovieService m = JsonConvert.DeserializeObject(line);
                MovieService m = (MovieService)JsonConvert.DeserializeObject(line);
                Console.WriteLine($"Movie ID: {m.Id}");
                Console.WriteLine($"Movie Title: {m.Title}");
                Console.WriteLine($"Movie Genres: {m.Genres}");
            }
            sr.Close();
        };

        /* I don't get how the example below works??? In my code above the intellisense said to add (MovieService) in front of JsonConvert, but that fails when I run it.
        string json = @"{
            'Name': 'Bad Boys',
            'ReleaseDate': '1995-4-7T00:00:00',
            'Genres': [
                'Action',
                'Comedy'
            ]
        }";
        Movie m = JsonConvert.DeserializeObject(json);
        string name = m.Name;
        */
    }

    public void Write(string file, string json)
    {
        //_logger.Log(LogLevel.Information, "Writing");
        //Console.WriteLine("*** I am writing");

        // create file from data
        StreamWriter sw = new StreamWriter(file, true);
        sw.WriteLine($"{json}");
        sw.Close();

    }
    public bool FileCheck(string file)
    {
        // make sure movie file exists
        if (!File.Exists(file))
        {
            Console.WriteLine($"File does not exist: {file}");
            return false;
        }
        else
        {
            return true;
        }
    }
}
