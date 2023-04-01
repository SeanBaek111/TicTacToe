using System;
using System.ComponentModel;
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

        bool saveResult = this.SaveToCsv<BoardStatus>(logs, fileName);
        return true;
    }

    private bool SaveToCsv<T>(Stack<BoardStatus> gameData, string path)
    {
        try
        {
            // Reverse it because the list coming from Stack
            List<BoardStatus> bs = this.ConvertToList(gameData);
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            lines.Add(header);
            var valueLines = bs.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            File.WriteAllLines(path, lines.ToArray());
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private List<BoardStatus> ConvertToList(Stack<BoardStatus> logs)
    {
        List<BoardStatus> newList = new();
        foreach (BoardStatus i in logs)
        {
            newList.Add(i);
        }

        newList.Reverse();
        return newList;
    }
}
