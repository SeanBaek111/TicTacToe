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
                Console.Write(name + " : row col mark >>> ");
                string sInput = Console.ReadLine();
                string[] arrInput = sInput.Split(' ');


                if (  board.AddPiece( arrInput ) )
                {
                    Console.Clear();
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

