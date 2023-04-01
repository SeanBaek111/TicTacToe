﻿using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            base.name = "Computer Player";
        }
        public ComputerPlayer(string name)
        {
            base.name = name;
        }
        public override Command MakeMovement(Board board)
        {
            // 1 2 3 4 5 6 7 8 9  
            // GetAvailablePos()  
            List<int> positions = board.GetEmptyPositions();
            
            List<char> pieces = board.GetAvailablePieces();

            Random rnd = new Random();

            string sPos = (positions[rnd.Next(positions.Count)] + 1).ToString();             
            string sPiece = pieces[rnd.Next(pieces.Count)].ToString();

            string[] arrInput =   { sPos, sPiece };
            Thread.Sleep(1000);
            board.AddPiece( arrInput);
            Console.Clear();

            return Command.None;
        }
    }
}
