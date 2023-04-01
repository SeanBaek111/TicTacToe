using System;
namespace TicTacToe
{
    public class HumanPlayer : Player
    {

        public HumanPlayer() {
            base.name = "Human Player";
        }
        public HumanPlayer(string name)
        {
            base.name = name;
        }
        public override void MakeMovement(Board board)
        {
            while (true)
            {
                var availablePieces = board.GetAvailablePieces();
                Console.Write("Available Pieces: ");
                Console.WriteLine(string.Join(", ", availablePieces));

                Console.WriteLine("Save game: S, Quit game: Q");
                Console.Write($"{name}: (Position Piece) >>> ");

                string sInput = Console.ReadLine();
                string[] arrInput = sInput.Split(' ');

                Console.Clear();

                if (arrInput.Length == 1)
                {
                    if (arrInput[0] == "S")
                    {
                        // Save game
                        if (FileManager.GetInstance().SaveProgress(History.GetInstance().GetLastStack()))
                        {
                            Console.WriteLine("File Saved");
                        }
                    }
                    else if (arrInput[0] == "Q")
                    {
                        // Quit
                        Environment.Exit(0);
                    }
                }

                if (arrInput.Length != 2)
                {
                    board.DisplayBoard();
                    Console.WriteLine("\nInput Error. Try again.\n");
                    continue;
                }

                Console.WriteLine($"{name} chose position {arrInput[0]} with piece {arrInput[1]}");

                if (board.AddPiece(arrInput))
                {
                    break;
                }
                else
                {
                    board.DisplayBoard();
                    Console.WriteLine("\nInput Error. Try again.\n");
                }
            }
        }
    }
}

