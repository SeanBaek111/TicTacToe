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
            // 1 2 3 4 5 6 7 8 9 로 바꾸고
            // GetAvailablePos() 로 바꾸
            List<int> positions = board.GetEmptyPositions();

            List<char> pieces = board.GetAvailablePieces();

            Random rnd = new Random();

            string sPos = (positions[rnd.Next(positions.Count)] + 1).ToString();
            string sPiece = pieces[rnd.Next(pieces.Count)].ToString();

            string[] arrInput = { sPos, sPiece };
            Thread.Sleep(1000);
            board.AddPiece(arrInput);
            Console.Clear();
        }
    }
}
