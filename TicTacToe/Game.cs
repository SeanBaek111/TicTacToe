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
        Player latestPlayer;
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


            foreach (Player p in players)
            {
                if (p.IsPlayerTurn())
                {
                    player = p;
                    break;
                }
            }
            //    Console.WriteLine("Current Player : " + player.GetName().ToStringExt());
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

        private void ShowHelp()
        {
            OnlineHelp.GetInstance().ShowHelp(gameBoard.GetMode());

            // WaitForUserInputBeforeExiting();
        }


        public void Play(GameStatus gameStatus = null)
        {
            History.GetInstance().Init();
            Console.WriteLine("Game started");
            DisplayCurrentBoard();

            if (gameStatus != null)
            {
                currentPlayer = gameStatus.CurrentPlayer;
                UpdateBoardAndHistory(gameStatus);
            }
            else
            {
                currentPlayer = GetCurrentPlayer();
                UpdateBoardAndHistory();
            }
            Command command = currentPlayer.MakeMovement(gameBoard);

            while (true)
            {
                if (command == Command.Help)
                {
                    // Show help menu  
                    ShowHelp();
                }
                else if (command == Command.Save)
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
                    if (historyCount >= 2)
                    {
                        Console.WriteLine("Undo");

                        int steps = 2;

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
                    Console.WriteLine("Redo");

                    int steps = 2;

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
                else if (command == Command.Invalid)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Move");
                }
                else
                {
                    latestPlayer = currentPlayer;
                    if (IsWin())
                    {
                        winPlayer = currentPlayer;
                    }
                    SwapPlayer();
                    currentPlayer = GetCurrentPlayer();
                    UpdateBoardAndHistory();
                }

                DisplayCurrentBoard();

                if (IsGameOver())
                {
                    DisplayGameOverMessage();
                    currentPlayer = GetCurrentPlayer();

                    if (currentPlayer is HumanPlayer)
                    {
                        // Display EndGame menu 
                        command = currentPlayer.MakeMovement(gameBoard);
                        // Console.WriteLine($"{currentPlayer.GetName()} chose position {gameBoard.LastPosition} with piece {gameBoard.LastPlacedPiece}");
                    }
                    else
                    {
                        command = Command.Quit;
                    }


                    if (command == Command.Quit)
                    {
                        // Quit
                        // Environment.Exit(0);
                        break;
                    }
                }
                else
                {
                    currentPlayer = GetCurrentPlayer();
                    command = currentPlayer.MakeMovement(gameBoard);

                }

            }

            //  DisplayGameOverMessage();
            // WaitForUserInputBeforeExiting();
            this.DisplayEndGameMenu();
        }

        private void DisplayEndGameMenu()
        {
            GameMenu.GetInstance().EndGameMenu();
        }
        
        private void DisplayCurrentBoard()
        {
            if (latestPlayer != null && gameBoard.LastPosition != 0)
                Console.WriteLine($"{latestPlayer.GetNameStr()} chose position {gameBoard.LastPosition} with piece {gameBoard.LastPlacedPiece}");
            gameBoard.DisplayBoard();
        }

        private void UpdateBoardAndHistory(GameStatus status = null)
        {
            GameStatus gameStatus = null;

            if (gameStatus == null)
            {
                gameStatus = new GameStatus(currentPlayer, gameBoard.GetCurrentStatus());
                gameStatus.GameMode = Data.GetInstance().GameMode;
                gameStatus.GameType = Data.GetInstance().GameType;
                gameStatus.PlayerTypeEnum = Data.GetInstance().PlayerTypeEnum;
            }
            else
            {
                gameStatus = status;
                gameStatus.GameMode = Data.GetInstance().GameStatus.GameMode;
                gameStatus.GameType = Data.GetInstance().GameStatus.GameType;
                gameStatus.PlayerTypeEnum = Data.GetInstance().PlayerTypeEnum;
            }

            gameStatus.Board = gameBoard;
            gameStatus.Players = players;
            gameStatus.SetLastPiece(gameBoard.LastPlacedPiece);
            gameStatus.SetAvailablePieces(gameBoard.GetAvailablePieces());
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
            Console.Clear();
        }

        private bool IsWin()
        {
            bool bRes = false;

            if (gameBoard.IsWin())
            {
                // winPlayer = currentPlayer;
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

        public void SwapPlayer()
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].IsPlayerTurn())
                {
                    players[i].SetTurn(false);
                    if (i == players.Length - 1)
                    {
                        players[0].SetTurn(true);
                    }
                    else
                    {
                        players[i + 1].SetTurn(true);
                    }
                    break;
                }

            }

        }

        public void SetBoard(Board board)
        {
            this.gameBoard = board;
        }
        public Board GetBoard()
        {
            return gameBoard;
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

