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
                Console.Write("Available Pieces : ");
                for (int i = 0; i < board.GetAvailablePieces().Count; i++)
                {
                    Console.Write(board.GetAvailablePieces()[i]);
                    if (i < board.GetAvailablePieces().Count - 1)
                        Console.Write(", ");
                }
                Console.WriteLine();
                
                Console.Write(name + " : (Position Piece) >>> ");
                
                string sInput = Console.ReadLine();
                string[] arrInput = sInput.Split(' ');


                if (  board.AddPiece( arrInput ) )
                {
                    Console.Clear();
                    Console.WriteLine(name + " chose position " + arrInput[0] + " with piece " + arrInput[1]);
                    break;
                }
                else
                {
                    Console.Clear(); 
                    board.DisplayBoard();
                    Console.Write("\nInput Error. Try again.\n\n");
                }
            } 
        }
    }
}

