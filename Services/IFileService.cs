using System.Collections.Generic;

namespace A5Movie.Services;

/// <summary>
///     Interface for the FileService classes
/// </summary>
public interface IFileService
{
    void Read(string file);
    void Write(string file, string json);
    bool FileCheck(string file);
}
