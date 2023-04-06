using System;
using System.Collections.Generic;
using static TicTacToe.Enums;

namespace TicTacToe
{
    [Serializable]
    public class MonteCarloComputerPlayer : Player
    {
        // Keep track of whose turn it is in simulation
        private bool bTempTurn;

        public MonteCarloComputerPlayer()
        {
            base.name = PlayerTypeEnum.Monte_Carlo_Computer_Player;
        }

        public MonteCarloComputerPlayer(PlayerTypeEnum name)
        {
            base.name = name;
        }

        public override Command MakeMovement(Board board)
        {
            bTempTurn = true;

            // Make copies of the available positions and pieces
            List<int> emptyPositions = new List<int>(board.GetEmptyPositions());
            List<char> availablePieces = new List<char>(board.GetAvailablePieces(GetIsFirstTurn()));

            int maxScore = int.MinValue;
            string[] bestMove = null;

            // Number of simulations to run
            int iterations = 1000;

            // Loop through each empty position and available piece
            foreach (int position in emptyPositions)
            {
                foreach (char piece in availablePieces)
                {
                    // Set score to 0
                    int score = 0;

                    // Place a piece at the current position on a temporary board
                    string[] move = new string[] { (position + 1).ToString(), piece.ToString() };
                    board.AddPiece(move);

                    // Run simulations to determine the score
                    for (int i = 0; i < iterations; i++)
                    {
                        Board tempBoard = board.Clone();
                        score += MonteCarloSimulation(tempBoard);
                    }

                    
                    // Keep track of the best move
                    if (score > maxScore)
                    {
                        maxScore = score;
                        bestMove = move;
                    }

                    // Remove the piece from the temporary board
                    board.RemovePiece(move);
                }
            }

            // Place the piece at the best position
            board.AddPiece(bestMove);
            Console.Clear();
        //    Console.WriteLine(name + " chose position " + bestMove[0] + " with piece " + bestMove[1]);

            return Command.None;
        }

        private int MonteCarloSimulation(Board board)
        {
            int nRes = 0;
            if (board.IsWin() || board.IsQuit())
            {
                return MonteCarloScore(board);
            }

            // Switch to the other player's turn
            bTempTurn = !bTempTurn;

            // Make copies of the available positions and pieces
            List<int> emptyPositions = new List<int>(board.GetEmptyPositions());
            List<char> pieces = new List<char>(board.GetAvailablePieces());
            Random rnd = new Random();

            // Choose a random piece to place
            char nextPiece = pieces[rnd.Next(pieces.Count)];

            // Choose a random position to place the piece
            int randomIndex = new Random().Next(emptyPositions.Count);
            int position = emptyPositions[randomIndex];

            // Place the piece at the position on a temporary board
            string[] move = new string[] { (position + 1).ToString(), nextPiece.ToString() };
            board.AddPiece(move);

            // Run another simulation
            nRes = MonteCarloSimulation(board);

            // Remove the piece from the temporary board
            board.RemovePiece(move);

            return nRes;
        }

        private int MonteCarloScore(Board board)
        {
            if (board.IsWin() && bTempTurn)
            {
                return 1;
            }
            else if (board.IsWin() && !bTempTurn)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public override Command MakeFinalDecision()
        {
            throw new NotImplementedException();
        }
    }
}
