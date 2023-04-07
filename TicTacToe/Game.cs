using System;
using System.Security.Cryptography;
using System.Xml.Linq;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public abstract class Game
    {
        Player[] players;
        protected Player currentPlayer;
        Player winPlayer;
        Player latestPlayer;
        protected Board gameBoard; 

        private int historyCount;

        protected abstract Command MakeMovement();
        protected virtual Command MakeFinalDecision() { return Command.Quit; }

        public Game()
        {
            historyCount = 0;
        }

        private Player GetCurrentPlayer()
        {
            return players.FirstOrDefault(p => p.IsPlayerTurn());
        }

        private bool IsHumanVsHuman()
        {
            return players.All(p => p is HumanPlayer);
        } 

        private void ShowHelp()
        {
            OnlineHelp.GetInstance().ShowHelp(gameBoard.GetMode());

            // WaitForUserInputBeforeExiting();
        }

        private bool ProcessGameCommand(Command command)
        {
            bool bRes = true;
            switch (command)
            {
                case Command.Help:
                    ShowHelp();
                    break;
                case Command.Save:
                    SaveGame();
                    break;
                case Command.Undo:
                    Undo();
                    break;
                case Command.Redo:
                    Redo();
                    break;
                case Command.Quit:
                    bRes = false; break;
                case Command.InvalidMove:
                    Console.Clear();
                    Console.WriteLine("Invalid Move");
                    break;
                case Command.InvalidInput:
                    Console.Clear();
                    Console.WriteLine("Invalid Input");
                    break;
                default:
                    UpdateGameState();
                    break;
            }
            return bRes;
        }
        private void SaveGame()
        {
            if (FileManager.Instance.SaveProgress(History.GetInstance().GetLastStack()))
            {
                Console.WriteLine("File Saved");
            }
        }

        private void UpdateGameState()
        {
            latestPlayer = currentPlayer;
            if (IsWin())
            {
                winPlayer = currentPlayer;
            }
            else
            {
                winPlayer = null;
            }

            SwapPlayer();
            currentPlayer = GetCurrentPlayer();
            UpdateBoardAndHistory();
        }

        private void InitializeGame(GameStatus gameStatus = null)
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

        public void Play(GameStatus gameStatus = null)
        {
            InitializeGame(gameStatus);
             
            Command command = MakeMovement();

            while (true)
            {
                if (ProcessGameCommand(command) == false)
                {
                    return;
                }
   
                DisplayCurrentBoard();

                if (IsGameOver())
                { 
                    DisplayGameOverMessage();
                    currentPlayer = GetCurrentPlayer(); 
                    if (currentPlayer is  HumanPlayer)
                    {
                        command = MakeFinalDecision();
                    }
                    else
                    {
                        WaitForUserInputBeforeExiting();
                        return;
                    } 
                }
                else
                {
                    currentPlayer = GetCurrentPlayer();
                    command = currentPlayer.MakeMovement(gameBoard); 
                }

            } 
        }

        private void Undo()
        {
            if (historyCount > 2)
            {
                Console.WriteLine("Undo");
                int steps = 2;

                for (int i = 0; i < steps; i++)
                {
                    if (History.GetInstance().Undo(gameBoard))
                    {
                        SwapPlayer();
                    }
                    else
                        break;
                }
            }
            else
            {
                Console.WriteLine("Undo is not available");
            }
        }

        private void Redo()
        {
            Console.WriteLine("Redo");
            int steps = 2;

            for (int i = 0; i < steps; i++)
            {
                if (History.GetInstance().Redo(gameBoard))
                {
                    SwapPlayer();
                }
                else
                    break;
            }
        }


        private void DisplayCurrentBoard()
        {
            if (latestPlayer != null && gameBoard.LastPosition != 0)
                Console.WriteLine($"{latestPlayer.GetNameStr()} chose position {gameBoard.LastPosition} with piece {gameBoard.LastPlacedPiece}");
            gameBoard.DisplayBoard();
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

        private void SwapPlayer()
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
