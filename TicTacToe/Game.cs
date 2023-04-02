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



        private int nCurrentTurn;
        const int MAX_TURN = 9;
        public Game()
        {
            nCurrentTurn = 0; 
        }

        private Player GetCurrentPlayer()
        {
            Player player = null;

            foreach(Player p in players)
            {
                if( p.IsTurn() )
                {
                    player = p;
                    break;
                }
            }
            Console.WriteLine("Current Player : " + player.GetName().ToStringExt());
            return player;
        }

        private bool IsHumanVsHuman()
        {
            return players.All(p => p is HumanPlayer);
        }

        public void Play()
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
        }

        private void DisplayCurrentBoard()
        {
            gameBoard.DisplayBoard();
        }

        private void UpdateBoardAndHistory()
        {
            GameStatus gameStatus = new GameStatus(currentPlayer, gameBoard.GetCurrentStatus());
            gameStatus.SetLastPiece(gameBoard.LastPlacedPiece);
            nCurrentTurn = AddHistory(gameStatus);
        }

        private bool IsGameOver()
        {
            return IsWin() || IsQuit();
        }

        private void DisplayGameOverMessage()
        {
            if (winPlayer != null)
            {
                Console.WriteLine("Winner is " + winPlayer.GetName().ToStringExt());
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
                if (players[i].IsTurn())
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

