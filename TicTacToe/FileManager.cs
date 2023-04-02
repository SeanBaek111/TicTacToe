using System;
using System.ComponentModel;
using System.Globalization;
using CsvHelper;
using static TicTacToe.Enums;

namespace TicTacToe;

public class FileManager
{
    /// <summary>
    /// It's the default file manager.
    /// </summary>
    const string DEFAULT_FILENAME = "save.csv";

    /// <summary>
    /// private static readonly Lazy<FileManager> lazy =
    ///     new Lazy<FileManager>(() => new FileManager());
    /// </summary>
    private static readonly Lazy<FileManager> lazy = new(() => new());

    public static FileManager Instance { get { return lazy.Value; } }

    private FileManager()
    {

    }

    /// <summary>
    /// Check if the file is exists.
    /// </summary>
    public bool FileExists(string fileName = DEFAULT_FILENAME)
    {
        // Suppose to return if the file exists.
        return File.Exists(fileName);
    }

    /// <summary>
    /// Create a save file.
    /// </summary>
    public bool CreateSaveFile(string fileName = DEFAULT_FILENAME)
    {
        try
        {
            File.Create(fileName).Dispose();
        }
        catch (IOException e)
        {
            Menu menu = new Menu();
            menu.SetQuestion("Save file detected... override?");
            EnumExtension.Query<ConfirmationEnum>().All(a =>
            {
                menu.AddMenuEnum(a);
                return true;
            });
            switch (menu.GetUserAnswer())
            {
                // YES
                case 1:
                    File.WriteAllText(fileName, string.Empty);
                    break;
                // NO
                case 2:
                    return false;
                // QUIT
                case 3:
                    Environment.Exit(0);
                    break;

                default:
                    break;
            };
        }
        return true;
    }

    /// <summary>
    /// Save the game progress.
    /// </summary>
    public bool SaveProgress(Stack<GameStatus> logs, string fileName = DEFAULT_FILENAME)
    {
        // Use local method to create a save file.
        bool create = this.CreateSaveFile(fileName);

        // Use extenion method to save the progress to file.
        bool saveResult = logs.SaveToCsv<GameStatus>(fileName);

        // Return true or false based on how file are saved.
        return saveResult;
    }

    /// <summary>
    /// Load the game progress from csv file and return the Stack object
    /// </summary>
    public Stack<GameStatus> LoadProgress(string fileName = DEFAULT_FILENAME)
    {
        if (!this.FileExists())
        {
            // File doesn't exists.
            throw new FileLoadException();
        }

        Stack<GameStatus> logs = new();
        using StreamReader streamReader = new(fileName);
        using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);
        List<GameStatus> records = csvReader.GetRecords<GameStatus>().ToList();

        foreach (GameStatus item in records)
        {
            logs.Push(item);
        }
        return logs;
    }
}
