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
            Console.Write(name + " : row col mark >>> ");
            string sInput = Console.ReadLine();
            string[] arrInput = sInput.Split(' ');
            board.SetBoard(Int32.Parse(arrInput[0]), Int32.Parse(arrInput[1]), arrInput[2]);
        }
    }
}

