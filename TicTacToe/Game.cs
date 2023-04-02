using System;
using System.Security.Cryptography;
using System.Xml.Linq;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public class Game
    {
        Player[] players;        
        Player currentPlayer;
        Player winPlayer;
        
        Board gameBoard;



        private int historyCount;
        const int MAX_TURN = 9;
        public Game()
        {
            historyCount = 0; 
        }

        private Player GetCurrentPlayer()
        {
            Player player = null;

            foreach(Player p in players)
            {
                if( p.IsPlayerTurn() )
                {
                    player = p;
                    break;
                }
            }
            Console.WriteLine("Current Player : " + player.GetName());
            return player;
        }

        private bool IsHumanVsHuman()
        {
            return players.All(p => p is HumanPlayer);
        }

        /* public void Play()
         {
             Console.WriteLine("Game started");
             DisplayCurrentBoard();
             UpdateBoardAndHistory();
             while (!IsGameOver())
             {
                 currentPlayer = GetCurrentPlayer();
                 Command command = currentPlayer.MakeMovement(gameBoard);


                 if (command == Command.Save)
                 {
                     // Save game
                     if (FileManager.Instance.SaveProgress(History.GetInstance().GetLastStack()))
                     {
                         Console.WriteLine("File Saved");
                     }
                 }
                 else if (command == Command.Undo)
                 {
                     // Undo 
                     // if it's Human vs Human mode -> Undo only 1 step
                     // else Undo 2 steps

                     int steps = IsHumanVsHuman() ? 1 : 2;
                     Console.WriteLine("Undo");
                     for (int i = 0; i < steps; i++)
                     {
                         if (History.GetInstance().Undo(gameBoard))
                         {

                             SwapPlayer();
                         }
                     }
                 }
                 else if (command == Command.Redo)
                 {
                     // Redo
                     // if it's Human vs Human mode -> Redo only 1 step
                     // else Redo 2 steps

                     int steps = IsHumanVsHuman() ? 1 : 2;
                     Console.WriteLine("Redo");
                     for (int i = 0; i < steps; i++)
                     {
                         if(History.GetInstance().Redo(gameBoard))
                         { 
                             SwapPlayer();
                         } 
                     } 
                 }
                 else if (command == Command.Quit)
                 {
                     // Quit
                     Environment.Exit(0);
                 }
                 else
                 {
                     UpdateBoardAndHistory();

                     SwapPlayer();
                 }

                 DisplayCurrentBoard();

             }

             DisplayGameOverMessage();
             WaitForUserInputBeforeExiting();
         }*/

        public void Play()
        {
            History.GetInstance().Init();
            Console.WriteLine("Game started");
            DisplayCurrentBoard();
            UpdateBoardAndHistory();
            currentPlayer = GetCurrentPlayer();
            Command command = currentPlayer.MakeMovement(gameBoard);

            while (true)
            {
               

                if (command == Command.Save)
                {
                    // Save game
                    if (FileManager.Instance.SaveProgress(History.GetInstance().GetLastStack()))
                    {
                        Console.WriteLine("File Saved");
                    }
                }
                else if (command == Command.Undo)
                {
                    // Undo 
                    // if it's Human vs Human mode -> Undo only 1 step
                    // else Undo 2 steps

                    
                   

                    if( historyCount > 2)
                    {
                        int steps = IsHumanVsHuman() ? 2 : 2;
                        Console.WriteLine("Undo");

                        for (int i = 0; i < steps; i++)
                        {
                            if (History.GetInstance().Undo(gameBoard))
                            {

                                SwapPlayer();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Undo is not available");
                    }
                   
                }
                else if (command == Command.Redo)
                {
                    // Redo
                    // if it's Human vs Human mode -> Redo only 1 step
                    // else Redo 2 steps

                    int steps = IsHumanVsHuman() ? 2 : 2;
                    Console.WriteLine("Redo");
                    for (int i = 0; i < steps; i++)
                    {
                        if (History.GetInstance().Redo(gameBoard))
                        {
                            SwapPlayer();
                        }
                    }
                }
                else if (command == Command.Quit)
                {
                    // Quit
                    Environment.Exit(0);
                }
                else
                {
                    UpdateBoardAndHistory();

                    SwapPlayer();
                }

                DisplayCurrentBoard();

                if (IsGameOver())
                {
                    DisplayGameOverMessage();
                    currentPlayer = GetCurrentPlayer();

                    if( currentPlayer is HumanPlayer)
                    {
                        command = currentPlayer.MakeMovement(gameBoard);
                    }
                    else
                    {
                        command  = Command.Quit;
                    }
                  

                    if (command == Command.Quit)
                    {
                       
                        // Quit
                        Environment.Exit(0);
                        break;
                    }
                   
                }
                else
                {
                    currentPlayer = GetCurrentPlayer();
                    command = currentPlayer.MakeMovement(gameBoard); 
                }

            }

            DisplayGameOverMessage();
            WaitForUserInputBeforeExiting();
        }

        private void DisplayCurrentBoard()
        {
            gameBoard.DisplayBoard();
        }

        private void UpdateBoardAndHistory()
        {
            GameStatus gameStatus = new GameStatus(currentPlayer, gameBoard.GetCurrentStatus());
            gameStatus.SetLastPiece(gameBoard.LastPlacedPiece);
            historyCount = AddHistory(gameStatus);
        }

        private bool IsGameOver()
        {
            return IsWin() || IsQuit();
        }

        private void DisplayGameOverMessage()
        {
            if (winPlayer != null)
            {
                Console.WriteLine("Winner is " + winPlayer.GetName());
            }
            else
            {
                Console.WriteLine("Draw!");
            }

            Console.WriteLine("Game Finished");
        }

        private void WaitForUserInputBeforeExiting()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private bool IsWin()
        {
            bool bRes = false;
            
            if(gameBoard.IsWin())
            {
                winPlayer = currentPlayer;
                bRes = true;
            }

            return bRes;
        }
        private bool IsQuit()
        {
            bool bRes = false;

            if (gameBoard.IsQuit())
            { 
                bRes = true;
            }

            return bRes;
        }

        private void SwapPlayer()
        {            
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].IsPlayerTurn())
                {
                    players[i].SetTurn(false);
                    if( i == players.Length -1)
                    {
                        players[0].SetTurn(true);
                    }
                    else
                    {
                        players[i+1].SetTurn(true);
                    }
                    break;
                }
                
            }

        }

        public void SetBoard(Board board)
        {
            this.gameBoard= board;
        }
        internal void SetPlayers(Player[] players)
        {
            this.players = players;
        }

        private int AddHistory(GameStatus gameStatus)
        {
            int nHistoryCnt = History.GetInstance().Push(gameStatus);
          //  Console.WriteLine("nHistoryCnt " + nHistoryCnt);

            return nHistoryCnt;

        }

    }
}

