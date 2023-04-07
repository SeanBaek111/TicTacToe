using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    [Serializable]
    public abstract class Player
    {
        public bool isPlayerTurn { get; set; }
        public bool isFirstTurn { get; set; }

        public bool bTurn { get; set; }
        public PlayerTypeEnum name { get; set; }
        public abstract Command MakeMovement(Board board);
        public abstract Command MakeFinalDecision();


        public virtual Command ParseCommand(string input)
        {
            switch (input.ToUpper())
            {
                case "H":
                    return Command.Help;
                case "S":
                    return Command.Save;
                case "U":
                    return Command.Undo;
                case "R":
                    return Command.Redo;
                case "Q":
                    return Command.Quit;
                default:
                    Console.WriteLine("Invalid command. Please enter a valid command (S: Save, U: Undo, R: Redo, Q: Quit).");
                    return Command.InvalidInput;
            }
        }

        public Player() { }
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
        public string GetNameStr()
        {
            return name.ToStringExt();
        }
        // abstract char[] Get
    }
}

