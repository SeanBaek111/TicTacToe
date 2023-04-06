using System;
using System.Xml.Linq;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public class WildTTT: Game
    {
        string name;
        public WildTTT()
        {
            name = "Wild TicTacToe";
        } 

        protected override Command MakeMovement()
        {
            return currentPlayer.MakeMovement(gameBoard);
        }

        protected override Command MakeFinalDecision()
        {
            if (currentPlayer is HumanPlayer)
            {
                return currentPlayer.MakeFinalDecision();
            }
            else
            {
                return Command.Quit;
            }
        }
    }
}

