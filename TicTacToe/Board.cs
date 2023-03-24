using System;
namespace TicTacToe
{
    public abstract class Board
    {
        protected string[,] gameBoard;
        
        public abstract void DisplayBoard();

        public abstract bool MarkBoard(string[] arrInput);
       // public abstract bool SetBoard(int row, int col, string mark);
        public abstract bool IsAvailableMove(string[] arrInput);
        public string[,] GetBoard()
        {
            return gameBoard;
        }

        //public void SetBoard(int row, int col, string mark)
        //{
        //    gameBoard[row-1,col-1] = mark;
        //}
    }
}

