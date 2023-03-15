using System;
using static System.Console;

namespace TicTacToe
{
    class Program
    {
        public static void Main()
        {
            // Check if the save file exists.
            bool saveFile = File.Exists("save.cfg");
            // Declare the user decision variable
            int nSelection;

            Menu menu1 = new Menu();

            // If Save file exists, do the confirmation.
            //
            if (saveFile)
            {
                menu1.SetQuestion("Load Last Save Game?");
                menu1.AddMenu("Yes");
                menu1.AddMenu("No");
                menu1.AddMenu("Quit");
                nSelection = menu1.GetUserAnswer();

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
                        }
                        else if (confirmWipeAnswer == 2) {
                            // back to first section. 
                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
            else
            {
                Game game = null;
                Player[] players = new Player[2];


                Menu menu2 = new Menu();
                menu2.SetQuestion("Welcome to TTT\nSelect an Option");
                menu2.AddMenu("Wild Tic Tac Toe");
                menu2.AddMenu("Numerical Tic Tac Toe");
                menu2.AddMenu("Load Last Save Game?");
                menu2.AddMenu("Help");
                menu2.AddMenu("Quit");
                nSelection = menu2.GetUserAnswer();


                Menu menu3 = new Menu();
                if (nSelection == 1)
                {
                    game = new WildTTT();

                    menu3.SetQuestion("Wild Tic Tac Toe\nWho is playing?");
                    menu3.AddMenu("Player vs Player");
                    menu3.AddMenu("Player vs Computer");
                    menu3.AddMenu("Back");
                    nSelection = menu3.GetUserAnswer();

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

                    menu3.SetQuestion("Numerical Tic Tac Toe\nWho is playing?");
                    menu3.AddMenu("Player vs Player");
                    menu3.AddMenu("Player vs Computer");
                    menu3.AddMenu("Back");
                    nSelection = menu3.GetUserAnswer();

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
                    menu3.SetQuestion("Save Game\nDo you want to load your last saved game?");
                    menu3.AddMenu("Load");
                    menu3.AddMenu("Delete");
                    menu3.AddMenu("Back");
                    nSelection = menu3.GetUserAnswer();
                }
                else if (nSelection == 4)
                {
                    menu3.SetQuestion("SaveHelp Message");
                    menu3.AddMenu("Wild Numerical TTT help");
                    menu3.AddMenu("Numerical TTT Help");
                    menu3.AddMenu("Back");
                    nSelection = menu3.GetUserAnswer();
                }


            }
        }

        static void LoadGame()
        {
            WriteLine("Load Game");
        }
    }
}
