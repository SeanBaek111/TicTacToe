using System;
namespace TicTacToe
{
    public abstract class Player
    {
        private bool bTurn;
        protected string name;
        public abstract void MakeMovement(Board board);
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

