using System;
using System.ComponentModel;
using System.Globalization;
using CsvHelper;
using static TicTacToe.Enums;

namespace TicTacToe;

public class FileManager
{
    const string DEFAULT_FILENAME = "save.csv";
    // TODO: Define class properties, turn csv save file as a object.
    public FileManager()
    {

    }

    public bool FileExists(string fileName = DEFAULT_FILENAME)
    {
        // Suppose to return if the file exists.
        return File.Exists(fileName);
    }

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

    public bool SaveProgress(Stack<BoardStatus> logs, string fileName = DEFAULT_FILENAME)
    {
        bool create = this.CreateSaveFile(fileName);

        bool saveResult = logs.SaveToCsv<BoardStatus>(fileName);

        return saveResult;
    }

    public Stack<BoardStatus> LoadProgress(string fileName = DEFAULT_FILENAME)
    {
        if (!this.FileExists())
        {
            // File doesn't exists.
            throw new FileLoadException();
        }

        Stack<BoardStatus> logs = new();
        using StreamReader streamReader = new(fileName);
        using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);
        List<BoardStatus> records = csvReader.GetRecords<BoardStatus>().ToList();

        foreach (BoardStatus item in records)
        {
            logs.Push(item);
        }
        return logs;
    }
}
