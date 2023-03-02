using System;
using System.Collections.Generic;
using System.IO;
using A5Movie.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace A5Movie.Services;

/// <summary>
///     Class to handle file services such as read, write and check
/// </summary>
public class FileService : IFileService
{
    private readonly ILogger<IFileService> _logger;

    public FileService(ILogger<IFileService> logger)
    {
        _logger = logger;
    }
    //Display the file contents
    public void Read(string file)
    {
        //_logger.Log(LogLevel.Information, "Reading");
        //Console.WriteLine("*** I am reading");

        using (StreamReader sr = new StreamReader(file))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Movie m = JsonConvert.DeserializeObject<Movie>(line);
                Console.WriteLine($"Movie: {m.Id} {m.Title} {m.Genres}");
            }
            sr.Close();
        };
    }

    //Write movie to the file
    public void Write(string file, string json)
    {
        //_logger.Log(LogLevel.Information, "Writing");
        //Console.WriteLine("*** I am writing");

        // create file from data
        StreamWriter sw = new StreamWriter(file, true);
        sw.WriteLine($"{json}");
        sw.Close();

    }
    //Check if the file exists
    public bool FileCheck(string file)
    {
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
