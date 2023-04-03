using System;
using System.Text;

namespace TicTacToe
{
    [Serializable]
    public class NumericTicTacToeBoard : Board
    {
        const int BOARD_SIZE = 3;

        public List<char> listAvailablePieces { get; set; }
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
        protected bool IsValidPiece(char piece, bool isFirst)
        {
            if (isFirst)
            {
                return (piece - '0') % 2 != 0 && pieces.Contains(piece); // Check if the piece is odd and contained in pieces
            }
            else
            {
                return (piece - '0') % 2 == 0 && pieces.Contains(piece); // Check if the piece is even and contained in pieces
            }
        }
        public override bool IsValidMove(string[] arrInput )
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
        public bool IsValidMove(string[] arrInput, bool isFirst)
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


            if (!IsValidPiece(cPiece, isFirst))
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
            if (IsValidMove(arrInput  ))
            { 
                int input = Int32.Parse(arrInput[0]);
                GetRowAndCol(input, out int row, out int col);
                char piece = char.Parse(arrInput[1]);

                UpdateGameBoard(row, col, piece);
                return true;
            }
            else
            { 
                return false;
            }
        }

        public override bool AddPiece(string[] arrInput, bool isFirstTurn)
        {
            if (IsValidMove(arrInput, isFirstTurn))
            {
                
                int input = Int32.Parse(arrInput[0]);
                GetRowAndCol(input, out int row, out int col);
                char piece = char.Parse(arrInput[1]);

                UpdateGameBoard(row, col, piece);
                return true;
            }
            else
            { 
                return false;
            }
        }


        private void UpdateGameBoard(int row, int col, char piece)
        {
            gameBoard[row, col] = piece;
            listAvailablePieces.Remove(piece);
            LastPlacedPiece = piece; // Store the last placed piece
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
            List<char> sortedAvailablePieces = new List<char>(listAvailablePieces);
            sortedAvailablePieces.Sort(); // Sort the list in ascending order
            return sortedAvailablePieces;
        }
        public override List<char> GetAvailablePieces(bool isFirstTurn)
        {
            List<char> sortedAvailablePieces = new List<char>(listAvailablePieces);
            sortedAvailablePieces.Sort(); // Sort the list in ascending order

            List<char> filteredAvailablePieces = new List<char>();
            foreach (char piece in sortedAvailablePieces)
            {
                if (isFirstTurn && (piece - '0') % 2 != 0) // isFirstTurn is true and the piece is odd
                {
                    filteredAvailablePieces.Add(piece);
                }
                else if (!isFirstTurn && (piece - '0') % 2 == 0) // isFirstTurn is false and the piece is even
                {
                    filteredAvailablePieces.Add(piece);
                }
            }

            return filteredAvailablePieces;
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
                        sStatus.Append(".");
                    }
                }
                if (i < BOARD_SIZE - 1)
                {
                    sStatus.Append(".");
                }
            }

            return sStatus.ToString();
        }

        public override void AddAvailablePiece(char piece)
        {
            if (IsValidPiece(piece) && !listAvailablePieces.Contains(piece))
            {
                listAvailablePieces.Insert(0, piece);
            }
        }

        public override void RemoveAvailablePiece(char piece)
        {
            if ( listAvailablePieces.Contains(piece))
            {
                listAvailablePieces.Remove(piece);
            }
        }

       
    }
}

