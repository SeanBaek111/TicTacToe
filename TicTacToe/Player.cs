using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public abstract class Player
    {
        public bool bTurn { get; set; }
        public string name { get; set; }
        public abstract Command MakeMovement(Board board);
        public void SetTurn(bool turn)
        {
            bTurn = turn;
        }

        public bool IsTurn()
        {
            return bTurn;
        }

        public string GetName() { return name; }
       // abstract char[] Get
    }
}

