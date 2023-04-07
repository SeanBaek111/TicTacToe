using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    [Serializable]
    public class HumanPlayer : Player
    {

        public HumanPlayer() {
            base.name = PlayerTypeEnum.Human_Player;
        }
        public HumanPlayer(PlayerTypeEnum name)
        {
            base.name = name;
        }

        public override Command MakeFinalDecision()
        { 
            
                Console.WriteLine("Help: H, Undo: U, Save game: S, Quit game: Q");
                Console.Write($"{name}: >>> ");

                string sInput = Console.ReadLine();
                string[] arrInput = sInput.Split(' ');
                Console.Clear();

                if (arrInput.Length == 1)
                {
                    return ParseCommand(arrInput[0]);
                }
                else 
                {
                    return Command.InvalidInput;
                }
            
           
        }
        public override Command MakeMovement(Board board)
        {
            while (true)
            {
                var availablePieces = board.GetAvailablePieces(GetIsFirstTurn());
                Console.Write("Available Pieces: ");
                Console.WriteLine(string.Join(", ", availablePieces));

                Console.WriteLine("Help: H, Undo: U, Redo: R, Save game: S, Quit game: Q");
                Console.Write($"{name}: (Position Piece) >>> ");

                string sInput = Console.ReadLine().ToUpper();
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

             //   Console.WriteLine($"{name} chose position {arrInput[0]} with piece {arrInput[1]}");

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

    }
}

