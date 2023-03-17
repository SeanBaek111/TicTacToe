using System;
namespace TicTacToe
{
    public class NumericTicTacToeBoard : Board
    {
       
        public NumericTicTacToeBoard():base()
        {
            base.gameBoard = new string[3,3] {
                { "-","-","-" },
                        { "-","-","-" },
                        { "-","-","-" }
            };
        }

       

        public override void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine("┌─────┬─────┬─────┐");
            Console.WriteLine("│     │     │     │");
            Console.WriteLine("│  {0}  │  {1}  │  {2}  │", gameBoard[0,0], gameBoard[0, 1], gameBoard[0, 2]);
            Console.WriteLine("├─────┼─────┼─────┤ ");
            Console.WriteLine("│     │     │     │");
            Console.WriteLine("│  {0}  │  {1}  │  {2}  │", gameBoard[1, 0], gameBoard[1, 1], gameBoard[1, 2]);
            Console.WriteLine("├─────┼─────┼─────┤ ");
            Console.WriteLine("│     │     │     │");
            Console.WriteLine("│  {0}  │  {1}  │  {2}  │", gameBoard[2, 0], gameBoard[2, 1], gameBoard[2, 2]);
            Console.WriteLine("└─────┴─────┴─────┘");
        }

        public override bool IsAvailableMove(int row, int col, string mark)
        {
            return true;
        }

        public override bool SetBoard(int row, int col, string mark)
        {
            if (IsAvailableMove(row, col, mark))
            {
                gameBoard[row - 1, col - 1] = mark;
                return true;
            }
            else
                return false;
          
        }
    }
}

