using System;
namespace TicTacToe
{
    public abstract class Board
    {
        protected char[,] gameBoard;
        protected char[] pieces;
        public char LastPlacedPiece { get; protected set; }
        public abstract void DisplayBoard();
        public abstract bool IsWin();
        public abstract bool IsQuit();
        public abstract bool AddPiece(string[] arrInput);
        public abstract bool AddPiece(string[] arrInput, bool isFirstTurn);
        public abstract void RemovePiece(string[] arrInput);
        public abstract void AddAvailablePiece(char piece);
        public abstract void RemoveAvailablePiece(char piece);
        public abstract string GetCurrentStatus();
        public abstract Board Clone();

        public abstract List<char> GetAvailablePieces();
        public abstract List<char> GetAvailablePieces(bool isFirstTurn);
        // public abstract bool SetBoard(int row, int col, string mark);
        public abstract bool IsValidMove(string[] arrInput);

        private int GetPos(int row, int col)
        {
            return row * 3 + col;
        }
        public List<int> GetEmptyPositions()
        {
            List<int> resList = new List<int>();
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] == '-')
                        resList.Add(GetPos(i, j));
                }

            }

            return resList;
        }

        public List<int> GetEmptyRows()
        {
            List<int> resList = new List<int>();
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                resList.Add(i);
            }

            return resList;
        }
        public List<int> GetEmptyCols()
        {
            List<int> resList = new List<int>();
            for (int i = 0; i < gameBoard.GetLength(1); i++)
            {
                resList.Add(i);
            }

            return resList;
        }

        public char[,] GetBoard()
        {
            return gameBoard;
        }

        protected bool IsValidPiece(char piece)
        {
            return pieces.Contains(piece);
        } 
      
        //public void SetBoard(int row, int col, string mark)
        //{
        //    gameBoard[row-1,col-1] = mark;
        //}

        // Set the board status based on the provided string
        public void SetStatus(string status)
        {
            string[] positions = status.Split(',');
            int index = 0;
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    gameBoard[row, col] = char.Parse(positions[index]);
                    index++;
                }
            }
        }
    }
}

