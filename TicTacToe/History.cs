using System;

namespace TicTacToe
{
    public class History
    {
        public static History history = new History();

        public Stack<GameStatus> gameHistory;

        private Stack<GameStatus> redoStack;

        public static History GetInstance()
        {
            return history;
        }
        private History()
        {
            gameHistory = new Stack<GameStatus>();
            redoStack = new Stack<GameStatus>();
        }

        public int Push(GameStatus status)
        {
            gameHistory.Push(status);
            // Clear the Redo stack when a new status is added
            redoStack.Clear();

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

        // Method to undo the game state
        public bool Undo(Board board)
        {
            bool bRes = false;
            if (gameHistory.Count > 1)
            {
                // Save the current state to the Redo stack
                redoStack.Push(gameHistory.Pop());
                // Get the previous state
                GameStatus previousState = gameHistory.Peek();

                board.AddAvailablePiece(redoStack.Peek().GetLastPiece());
                // Apply the previous state to the game board                
                board.SetStatus(previousState.GetBoardStatus());
                bRes = true;

            }
            else
            {
                Console.WriteLine("Cannot undo further!");
            }

            return bRes;
        }

        // Method to redo the game state
        public bool Redo(Board board)
        {
            bool bRes = false;
            if (redoStack.Count > 0)
            {
                // Get the state from the Redo stack
                GameStatus redoState = redoStack.Pop();


                board.RemoveAvailablePiece(redoState.GetLastPiece());
                // Apply the state to the game board
                board.SetStatus(redoState.GetBoardStatus());
                // Add the redone state to the game history
                gameHistory.Push(redoState);
                bRes = true;
            }
            else
            {
                Console.WriteLine("Cannot redo further!");
            }
            return bRes;
        }
    }
}

