using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public abstract class Player
    {
        public bool isPlayerTurn { get; set; }
        public bool isFirstTurn { get; set; }
        
        public bool bTurn { get; set; }
        public PlayerTypeEnum name { get; set; }
        public abstract Command MakeMovement(Board board);

        public Player() {}
        public void SetTurn(bool turn)
        {
            isPlayerTurn = turn;
        }

        public bool IsPlayerTurn()
        {
            return isPlayerTurn;
        }

        public bool GetIsFirstTurn()
        {
            return isFirstTurn;
        }

        public void SetIsFirstTurn(bool value)
        {
            isFirstTurn = value;
        }

        public PlayerTypeEnum GetName() { return name; }
       // abstract char[] Get
    }
}

