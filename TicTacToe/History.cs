using System;
namespace TicTacToe
{
    public class History
    {
        public static History history = new History();

        public Stack<BoardStatus> gameHistory;

        public static History GetInstance()
        {
            return history;
        }
        private History()
        {
            gameHistory = new Stack<BoardStatus>();
        }

        public int Push(BoardStatus status)
        {
            gameHistory.Push(status);

            return gameHistory.Count;
        }

        
        public int Pop()
        {
            if( gameHistory.Count > 0)
                gameHistory.Pop();
            else
            {
                Console.WriteLine("Gamehistory is empty!");
            }
            return gameHistory.Count;
        }
       

    }
}

