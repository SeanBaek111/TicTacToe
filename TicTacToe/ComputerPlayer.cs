using System;
namespace TicTacToe
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            base.name = "Computer Player";
        }
        public ComputerPlayer(string name)
        {
            base.name = name;
        }
        public override void MakeMovement(Board board)
        {
            Console.Write(name + " : row col mark >>> ");
            string sInput = Console.ReadLine() ;
            string[] arrInput = sInput.Split(' ');

            board.MarkBoard( arrInput);
            
        }
    }
}
