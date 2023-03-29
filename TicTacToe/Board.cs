using System;
namespace TicTacToe
{
    public abstract class Board
    {
        protected char[,] gameBoard;
        protected char[] pieces;
        
        public abstract void DisplayBoard();
        public abstract bool IsWin();
        public abstract bool IsQuit();
        public abstract bool AddPiece(string[] arrInput);
       // public abstract bool SetBoard(int row, int col, string mark);
        public abstract bool IsValidMove(string[] arrInput);
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
    }
}

