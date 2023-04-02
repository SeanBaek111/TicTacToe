﻿using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public class GameStatus
    {
        //  string pieceStatus = "-,-,3,-,-,6,-,9,-";
        /* char[,] boardStatus2= {{'-','-','3'},
                                 {'-','-','6'},
                                 {'-','2','-'}};*/
        public string BoardStatus { get; set; }
        public Player CurrentPlayer { get; set; }
        public GameModeEnum GameMode { get; set; }
        public GameTypeEnum GameType { get; set; }
        public BoardTypeEnum BoardType { get; set; }

        public char lastPiece { get; set; }
        
        public GameStatus() {}

        public GameStatus(Player player, string status)
        {
            CurrentPlayer = player;
            BoardStatus = status;
            // TODO: Prefixed, need to modify
            GameMode = GameModeEnum.Wild_Tic_Tac_Toe;
            GameType = GameTypeEnum.Human_VS_Computer;
            BoardType = BoardTypeEnum.Tic_Tac_Toe_Board;
        }

        public string GetBoardStatus()
        {
            return BoardStatus;
        }

        public void SetLastPiece(char piece)
        {
            lastPiece = piece;
        }

        public char GetLastPiece()
        {
            return lastPiece;
        }
    }
}

