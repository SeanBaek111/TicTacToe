using System;
using static TicTacToe.Enums;

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
        public override Command MakeMovement(Board board)
        {
            while (true)
            {
                var availablePieces = board.GetAvailablePieces(GetIsFirstTurn());
                Console.Write("Available Pieces: ");
                Console.WriteLine(string.Join(", ", availablePieces));

                Console.WriteLine("Undo: U, Redo: R, Save game: S, Quit game: Q");
                Console.Write($"{name}: (Position Piece) >>> ");

                string sInput = Console.ReadLine();
                string[] arrInput = sInput.Split(' ');

                Console.Clear();

                if (arrInput.Length == 1)
                {
                    return ParseCommand(arrInput[0]); 
                }

                if (arrInput.Length != 2)
                {
                    board.DisplayBoard();
                    Console.WriteLine("\nInput Error. Try again.\n");
                    continue;
                }

                Console.WriteLine($"{name} chose position {arrInput[0]} with piece {arrInput[1]}");

                if (board.AddPiece(arrInput, GetIsFirstTurn()))
                {
                    break;
                }
                else
                {
                    board.DisplayBoard();
                    Console.WriteLine("\nInput Error. Try again.\n");
                }
            }
            return Command.None;
        }

        private Command ParseCommand(string input)
        {
            switch (input.ToUpper())
            {
                case "S":
                    return Command.Save;
                case "U":
                    return Command.Undo;
                case "R":
                    return Command.Redo;
                case "Q":
                    return Command.Quit;
                default:
                    Console.WriteLine("Invalid command. Please enter a valid command (S: Save, U: Undo, R: Redo, Q: Quit).");
                    return Command.Invalid;
            }
        }
    }
}

