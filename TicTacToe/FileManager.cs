using System;
using static TicTacToe.Enums;

namespace TicTacToe;

public interface IFileManager
{
    /// <summary>
    /// Check if the save file exists.
    /// </summary>
    public bool FileExists(string? fileName = "save.csv");
}

public class FileManager : IFileManager
{
    public FileManager()
    {

    }

    public bool FileExists(string fileName = "save.csv")
    {
        // Suppose to return if the file exists.
        return File.Exists(fileName);
    }

    public bool CreateSaveFile(string fileName = "save.csv")
    {
        if (File.Exists(fileName))
        {
            Menu menu = new Menu();
            menu.SetQuestion("Save file detected... override?");
            EnumExtension.Query<ConfirmationEnum>().All(a =>
            {
                menu.AddMenuEnum(a);
                return true;
            });
        }

        File.Create(fileName).Dispose();

        return true;
    }
}
