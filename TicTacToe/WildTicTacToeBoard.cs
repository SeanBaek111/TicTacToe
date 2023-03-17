using System;
namespace TicTacToe
{
    public class WildTicTacToeBoard : Board
    {
        const int BOARD_SIZE = 3;
        public WildTicTacToeBoard():base()
        {
            base.gameBoard = new string[3,3] {
                { "-","-","-" },
                        { "-","-","-" },
                        { "-","-","-" }
            };
        }

       

        public override void DisplayBoard()
        {
          //  Console.Clear();
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

        public override bool IsAvailableMove(string[] arrInput)
        {

            if (arrInput.Length != 3)
            {
                return false;
            }

            int nInput = 0;

            if (Int32.TryParse(arrInput[0], out nInput) == false)
            {
                return false;
            }
            int row = nInput;

            if (Int32.TryParse(arrInput[1], out nInput) == false)
            {
                return false;
            }
            int col = nInput;

            char cMark = ' ';
            if (char.TryParse(arrInput[2], out cMark) == false)
            {
                return false;
            }

            char mark = cMark;

            if (row > BOARD_SIZE || col > BOARD_SIZE || row < 1 || col < 1)
            {
                return false;
            }
            if (gameBoard[row-1, col-1] != "-")
            {
                Console.WriteLine(row + " " + col + " already taken");
                return false;
            }





            return true;
        }

        public override bool MarkBoard(string[] arrInput)
        {
            if (IsAvailableMove(arrInput))
            {
                Console.WriteLine("IsAvailableMove True");
                gameBoard[Int32.Parse(arrInput[0]) - 1, Int32.Parse(arrInput[1]) - 1] = arrInput[2];
                return true;
            }
            else
            {
                Console.WriteLine("IsAvailableMove False");
                return false;
            }
        }
    }
}

