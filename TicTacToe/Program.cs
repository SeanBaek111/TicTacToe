using System;
using static System.Console;

namespace TicTacToe
{
    class Program
    {
        public static void Main()
        {
            Menu menu1 = new Menu();
            menu1.SetQuestion("Load Last Save Game?");
            menu1.AddMenu("Yes");
            menu1.AddMenu("No");
            menu1.AddMenu("Quit");

            int nSelection = menu1.GetUserAnswer(); 
       
            if (nSelection == 1)
            {
                LoadGame();
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