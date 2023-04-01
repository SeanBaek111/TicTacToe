using System;
using System.Text;

namespace TicTacToe
{
    public class NumericTicTacToeBoard : Board
    {
        const int BOARD_SIZE = 3;

        private List<char> listAvailablePieces;
        public NumericTicTacToeBoard():base()
        {
            base.gameBoard = new char[BOARD_SIZE, BOARD_SIZE];
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    gameBoard[i, j] = '-';
                }
            }
            base.pieces = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            
            listAvailablePieces = new List<char>(base.pieces);
        }

       


        public override void DisplayBoard()
        {
        //    Console.Clear();
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

        private void GetRowAndCol(int input, out int row, out int col)
        {
            input--;

            row = input / BOARD_SIZE;  
            col = input % BOARD_SIZE;
        }

        public override bool IsValidMove(string[] arrInput)
        {
            if (arrInput.Length != 2)
            {
                return false;
            }
 
            int row, col;
            if (!Int32.TryParse(arrInput[0], out int input))
            {
                return false;
            }
            GetRowAndCol(input, out row, out col);



            char cPiece = ' ';
            if (char.TryParse(arrInput[1], out cPiece) == false)
            {
                return false;
            }


            if (!IsValidPiece(cPiece))
            {
                return false;
            }

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (gameBoard[i, j] == cPiece)
                    {
                        return false;
                    }
                }
            }


            if (row >= BOARD_SIZE || col >= BOARD_SIZE || row < 0 || col < 0)
            {
                return false;
            }
            if (gameBoard[row, col] != '-')
            {
              //  Console.WriteLine(row + " " + col + " already taken");
                return false;
            }

            return true;
        }
 
        public override bool AddPiece(string[] arrInput)
        {            
            if (IsValidMove(arrInput))
            {
                //int rowIndex = GetRow(Int32.Parse(arrInput[0])) - 1;
                //int colIndex = GetCol(Int32.Parse(arrInput[0])) - 1;
                //char piece = char.Parse(arrInput[1]);
                int input = Int32.Parse(arrInput[0]);
                GetRowAndCol(input, out int row, out int col);
                char piece = char.Parse(arrInput[1]);

              //  Console.WriteLine("IsAvailableMove True");
                gameBoard[row, col] = piece;
                listAvailablePieces.Remove(piece);
                return true;
            }
            else
            {
       //         Console.WriteLine("IsAvailableMove False");
                return false;
            }
        }

        //       
        public override bool IsWin()
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                if (IsWinningLine(gameBoard[i, 0], gameBoard[i, 1], gameBoard[i, 2]))
                {
                    return true;
                }
            }

            for (int i = 0; i < gameBoard.GetLength(1); i++)
            {
                if (IsWinningLine(gameBoard[0, i], gameBoard[1, i], gameBoard[2, i]))
                {
                    return true;
                }
            }

            if (IsWinningLine(gameBoard[0, 0], gameBoard[1, 1], gameBoard[2, 2]))
            {
                return true;
            }

            if (IsWinningLine(gameBoard[0, 2], gameBoard[1, 1], gameBoard[2, 0]))
            {
                return true;
            }


            return false;
        }

        // check if a line is a winning line
        private bool IsWinningLine(char a, char b, char c)
        {
            bool allNotEmpty = a != '-' && b != '-' && c != '-';
            int sum = (a - '0') + (b - '0') + (c - '0');

            return allNotEmpty && sum == 15;
 
        }

        // check if the board is full
        public override bool IsQuit()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (gameBoard[i, j] == '-')
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        public override List<char> GetAvailablePieces()
        {
            return listAvailablePieces;
        }

        public override Board Clone()
        {
            NumericTicTacToeBoard cloneBoard = new NumericTicTacToeBoard();
            cloneBoard.gameBoard = (char[,])gameBoard.Clone();
            cloneBoard.listAvailablePieces = new List<char>(listAvailablePieces);
            return cloneBoard;
        }

        public override void RemovePiece(string[] arrInput)
        { 
            int input = Int32.Parse(arrInput[0]);
            GetRowAndCol(input, out int row, out int col);
            gameBoard[row, col] = '-';
            listAvailablePieces.Add(char.Parse(arrInput[1]));
        }

        public override string GetCurrentStatus()
        {
            StringBuilder sStatus = new StringBuilder();

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    sStatus.Append(gameBoard[i, j]);

                    if (j < BOARD_SIZE - 1)
                    {
                        sStatus.Append(",");
                    }
                }
                if (i < BOARD_SIZE - 1)
                {
                    sStatus.Append(",");
                }
            }

            return sStatus.ToString();
        }
    }
}

