using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public class NumericTTT : Game
    {
        string name;
        public NumericTTT()
        {
            name = "Numeric TicTacToe";
        }

        protected override Command MakeMovement()
        {
            return currentPlayer.MakeMovement(gameBoard);
        }

        protected override Command MakeFinalDecision()
        {
            return currentPlayer.MakeFinalDecision();
            /*if (currentPlayer is HumanPlayer)
            {
                return currentPlayer.MakeFinalDecision();
            }
            else
            {
                return Command.Quit;
            }*/
        }
    }
}

