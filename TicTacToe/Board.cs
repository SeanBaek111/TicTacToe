using System;
namespace TicTacToe
{
    public abstract class Board
    {
        protected string[,] gameBoard;
        
        public abstract void DisplayBoard();

        public abstract bool SetBoard(int row, int col, string mark);
        public abstract bool IsAvailableMove(int row, int col, string mark);
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

