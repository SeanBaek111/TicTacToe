using System;
using static System.Console;
using static TicTacToe.Enums;

namespace TicTacToe;

public class Program
{

    public static void Main()
    {
        FileManager fm = FileManager.Instance;

        // Console.ForegroundColor = ConsoleColor.Green;
        // Console.BackgroundColor = ConsoleColor.Yellow;

        //Console.Write("This ");
        //Console.ForegroundColor = ConsoleColor.White;
        //Console.BackgroundColor = ConsoleColor.Blue;

        //Console.Write("is");
        //Console.ForegroundColor = ConsoleColor.DarkBlue;
        //Console.BackgroundColor = ConsoleColor.Gray;

        //Console.Write("test");

        //Console.ResetColor();  
        //Console.WriteLine("\nPress any key to exit...");
        //Console.ReadKey();

        //return;
        // Check if the save file exists.

        Menu menu = new Menu();

        // If Save file exists, do the confirmation.
        if (fm.IsFileExists() && fm.IsFileEmpty())
        {
            while (true)
            {
                menu.SetQuestion("Load Last Save Game?");
                foreach (string name in Enum.GetNames(typeof(ConfirmationEnum)))
                {
                    menu.AddMenuEnum(name.ToEnum<ConfirmationEnum>());
                }

                switch (menu.GetUserAnswer())
                {
                    case 1:
                        LoadGame();
                        break;
                    case 2:
                        Menu confirmWipe = new();
                        confirmWipe.SetQuestion("If you choose to start a new game,");
                        confirmWipe.SetQuestion("Save file will be wiped and");
                        confirmWipe.SetQuestion("YOU WILL LOST ALL YOUR PROGRESS.");
                        foreach (string name in Enum.GetNames(typeof(ConfirmationEnum)))
                        {
                            confirmWipe.AddMenuEnum(name.ToEnum<ConfirmationEnum>());
                        }
                        int confirmWipeAnswer = confirmWipe.GetUserAnswer();

                        if (confirmWipeAnswer == 1)
                        {
                            // Go to new Game.
                            NewGame();
                        }
                        else if (confirmWipeAnswer == 2)
                        {
                            // back to first section. 
                            continue;


                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        else
        {
            NewGame();
        }
    }


    static void NewGame()
    {
        // Declare the user decision variable
        int nSelection;

        // Define null game object
        Game game = null;
        Player[] players = new Player[2];

        // Defines Variables.
        GameModeEnum sGame;
        GameTypeEnum sPlayers;
        BoardTypeEnum sBoard;

        Menu menu = new Menu();
        menu.SetQuestion("Welcome to TTT");
        menu.SetQuestion("Select an option");

        // Loop all the possible options from typeof(GameModeEnum)
        EnumExtension.Query<GameModeEnum>().All(a =>
        {
            menu.AddMenuEnum(a);
            return true;
        });
        menu.AddMenuEnum(Command.Help);
        menu.AddMenuEnum(ConfirmationEnum.Quit);

        nSelection = menu.GetUserAnswer();

        menu = new Menu();

        switch (nSelection)
        {
            default:
                sGame = nSelection.ToEnum<GameModeEnum>();
                // Prefixed becasue only one game mode is being used.
                sBoard = BoardTypeEnum.Tic_Tac_Toe_Board;

                menu.SetQuestionEnum(sGame);
                menu.SetQuestion("Who is playing?");

                EnumExtension.Query<GameTypeEnum>().All(a =>
                {
                    menu.AddMenuEnum(a);
                    return true;
                });

                menu.AddMenuEnum(NavigationEnum.Back);

                nSelection = menu.GetUserAnswer();

                sPlayers = nSelection.ToEnum<GameTypeEnum>();

                game = GameFactory.GetInstance().CreateGame(sGame, sPlayers, sBoard);
                game.Play();
                break;

            case 3:
                menu.SetQuestion("Help Message");
                EnumExtension.Query<GameTypeEnum>().All(a =>
                {
                    menu.AddMenu(a.ToStringExt());
                    return true;
                });
                menu.AddMenuEnum(NavigationEnum.Back);
                nSelection = menu.GetUserAnswer();
                throw new NotImplementedException();

            case 4:
                Environment.Exit(0);
                break;
        }
    }

    static void LoadGame()
    {
        // TODO: Do load save file
        WriteLine("Load Game");


        Game game = GameFactory.GetInstance().LoadGame();
        if (game != null)
        {
            game.Play(Data.GetInstance().GameStatus);
        }

    }
}
