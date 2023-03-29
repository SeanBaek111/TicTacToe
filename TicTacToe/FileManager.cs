using System;

namespace TicTacToe;

public interface IFileManager
{
    /// <summary>
    /// Check if the save file exists.
    /// </summary>
    public bool FileExists(string? fileName = "save.cfg");
}

public class FileManager : IFileManager
{
    public bool FileExists(string? fileName = "save.cfg")
    {
        // Suppose to return if the file exists.
        return File.Exists("save.cfg");
    }
}
