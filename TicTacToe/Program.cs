using System;
using static System.Console;
using static TicTacToe.Enums;

namespace TicTacToe;

public class Program
{
    public static void Main()
    {

        //   Console.ForegroundColor = ConsoleColor.Green;
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
        bool saveFile = File.Exists("save.cfg");

        Menu menu = new Menu();

        // Declare the user decision variable
        int nSelection;

        // If Save file exists, do the confirmation.
        if (saveFile)
        {
            while (true)
            {
                menu.SetQuestion("Load Last Save Game?");
                menu.AddEnumMenu(Confirmation.Yes);
                menu.AddEnumMenu(Confirmation.No);
                menu.AddMenu("Quit");
                nSelection = menu.GetUserAnswer();

                switch (nSelection)
                {
                    case 1:
                        LoadGame();
                        break;
                    case 2:
                        Menu confirmWipe = new();
                        confirmWipe.SetQuestion("If you choose to start a new game,");
                        confirmWipe.SetQuestion("Save file will be wiped and");
                        confirmWipe.SetQuestion("YOU WILL LOST ALL YOUR PROGRESS.");
                        confirmWipe.AddEnumMenu(Confirmation.Yes);
                        confirmWipe.AddEnumMenu(Confirmation.No);
                        confirmWipe.AddMenu("Quit");
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

        // TODO: DO new game.
        //
        //throw new NotImplementedException();

        Game game = null;
        Player[] players = new Player[2];

        GameModeEnum sGame;
        GameTypeEnum sPlayers;
        BoardTypeEnum sBoard;

        Menu menu = new Menu();
        menu.SetQuestion("Welcome to TTT");
        menu.SetQuestion("Select an option");
        menu.AddEnumMenu(GameModeEnum.Wild_Tic_Tac_Toe);
        menu.AddEnumMenu(GameModeEnum.Numeric_Tic_Tac_Toe);
        menu.AddMenu("Help");
        menu.AddMenu("Quit");
        nSelection = menu.GetUserAnswer();

        menu = new Menu();
        if (nSelection == 4)
        {
            menu.SetQuestion("Save Help Message");
            menu.AddMenu("Wild Numerical TTT help");
            menu.AddMenu("Numerical TTT Help");
            menu.AddMenu("Back");
            nSelection = menu.GetUserAnswer();
            throw new NotImplementedException();
        }
        else if (nSelection == 5)
        {
            Environment.Exit(0);
        }

        sGame = nSelection.ToEnum<GameModeEnum>();
        // Prefixed becasue only one game mode is being used.
        sBoard = BoardTypeEnum.Tic_Tac_Toe_Board;

        menu.SetEnumQuestion(sGame);
        menu.SetQuestion("Who is playing?");
        menu.AddEnumMenu(GameTypeEnum.Human_VS_Human);
        menu.AddEnumMenu(GameTypeEnum.Human_VS_Computer);
        menu.AddMenu("Back");
        nSelection = menu.GetUserAnswer();

        sPlayers = nSelection.ToEnum<GameTypeEnum>();

        game = GameFactory.GetInstance().CreateGame(sGame, sPlayers, sBoard);
        game.Play();

    }

    static void LoadGame()
    {
        // TODO: Do load save file
        WriteLine("Load Game");
        throw new NotImplementedException();

        // game = GameFactory.GetInstance().LoadGame();
        // game.Play();
    }
}
