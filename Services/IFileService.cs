using System.Collections.Generic;

namespace A5Movie.Services;

/// <summary>
///     This service interface only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public interface IFileService
{
    void Read(string file);
    void Write(string file, string json);
    bool FileCheck(string file);
}
