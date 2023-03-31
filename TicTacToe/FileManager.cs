using System;
using static TicTacToe.Enums;

namespace TicTacToe;

public class FileManager
{
    // TODO: Define class properties, turn csv save file as a object.
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
}
