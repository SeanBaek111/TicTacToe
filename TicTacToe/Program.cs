using System;
using static System.Console;

namespace TicTacToe;

public class Program
{
    public static void Main()
    {
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
                menu.AddMenu("Yes");
                menu.AddMenu("No");
                menu.AddMenu("Quit");
                nSelection = menu.GetUserAnswer();

                switch (nSelection)
                {
                    case 1:
                        LoadGame();
                        break;
                    case 2:
                        Menu confirmWipe = new();
                        confirmWipe.SetQuestion("If you choose to start a new game,\nSave file will be wiped and\nYOU WILL LOST ALL YOUR PROGRESS.");
                        confirmWipe.AddMenu("Yes");
                        confirmWipe.AddMenu("No");
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


        Menu menu = new Menu();
        menu.SetQuestion("Welcome to TTT\nSelect an Option");
        menu.AddMenu("Wild Tic Tac Toe");
        menu.AddMenu("Numerical Tic Tac Toe");
        menu.AddMenu("Load Last Save Game?");
        menu.AddMenu("Help");
        menu.AddMenu("Quit");
        nSelection = menu.GetUserAnswer();

        Menu menu3 = new Menu();
        if (nSelection == 1)
        {
            game = new WildTTT();

            menu.SetQuestion("Wild Tic Tac Toe\nWho is playing?");
            menu.AddMenu("Player vs Player");
            menu.AddMenu("Player vs Computer");
            menu.AddMenu("Back");
            nSelection = menu.GetUserAnswer();

            if (nSelection == 1)
            {
                players[0] = new HumanPlayer("Player 1");
                players[1] = new HumanPlayer("Player 2");

                players[0].SetTurn(true);
                players[1].SetTurn(false);
            }
            else if (nSelection == 2)
            {
                players[0] = new HumanPlayer();
                players[1] = new ComputerPlayer();

                players[0].SetTurn(true);
                players[1].SetTurn(false);
            }

            game.SetPlayers(players);
            game.SetBoard(new TicTacToeBoard());
            game.Play();
        }
        else if (nSelection == 2)
        {
            game = new NumericTTT();

            menu.SetQuestion("Numerical Tic Tac Toe\nWho is playing?");
            menu.AddMenu("Player vs Player");
            menu.AddMenu("Player vs Computer");
            menu.AddMenu("Back");
            nSelection = menu.GetUserAnswer();

            if (nSelection == 1)
            {
                players[0] = new HumanPlayer("Player 1");
                players[1] = new HumanPlayer("Player 2");

                players[0].SetTurn(true);
                players[1].SetTurn(false);
            }
            else if (nSelection == 2)
            {
                players[0] = new HumanPlayer();
                players[1] = new ComputerPlayer();

                players[0].SetTurn(true);
                players[1].SetTurn(false);
            }

            game.SetPlayers(players);
            game.SetBoard(new TicTacToeBoard());
            game.Play();
        }
        else if (nSelection == 3)
        {
            menu.SetQuestion("Save Game\nDo you want to load your last saved game?");
            menu.AddMenu("Load");
            menu.AddMenu("Delete");
            menu.AddMenu("Back");
            nSelection = menu.GetUserAnswer();
        }
        else if (nSelection == 4)
        {
            menu.SetQuestion("Save Help Message");
            menu.AddMenu("Wild Numerical TTT help");
            menu.AddMenu("Numerical TTT Help");
            menu.AddMenu("Back");
            nSelection = menu.GetUserAnswer();
        }
    }
    static void LoadGame()
    {
        // TODO: Do load save file
        WriteLine("Load Game");
        throw new NotImplementedException();
    }
}

