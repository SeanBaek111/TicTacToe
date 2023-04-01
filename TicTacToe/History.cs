using System;

namespace TicTacToe
{
    public class History
    {
        public static History history = new History();

        public Stack<GameStatus> gameHistory;

        public static History GetInstance()
        {
            return history;
        }
        private History()
        {
            gameHistory = new Stack<GameStatus>();
        }

        public int Push(GameStatus status)
        {
            gameHistory.Push(status);

            return gameHistory.Count;
        }


        public int Pop()
        {
            if (gameHistory.Count > 0)
                gameHistory.Pop();
            else
            {
                Console.WriteLine("Gamehistory is empty!");
            }
            return gameHistory.Count;
        }

        public GameStatus GetLastStatus()
        {
            if (gameHistory.Count > 0)
            {
                return gameHistory.Peek();
            }
            else
            {
                throw new InvalidOperationException("Game history is empty!");
            }
        }

        public Stack<GameStatus> GetLastStack()
        {
            if (gameHistory.Count > 0)
            {
                Stack<GameStatus> stack = new Stack<GameStatus>();
                stack.Push(gameHistory.Peek());
                return stack;
            }
            else
            {
                throw new InvalidOperationException("Game history is empty!");
            }
        }
    }
}

