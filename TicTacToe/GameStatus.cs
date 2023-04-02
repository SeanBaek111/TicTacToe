using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public class GameStatus
    {
        //  string pieceStatus = "-,-,3,-,-,6,-,9,-";
        /* char[,] boardStatus2= {{'-','-','3'},
                                 {'-','-','6'},
                                 {'-','2','-'}};*/
        public string boardStatus { get; }
        public Player currentPlayer { get; }
        public GameModeEnum name { get; set; }
        public GameTypeEnum players { get; set; }
        public BoardTypeEnum board { get; set; }

        char lastPiece;

        public GameStatus(Player player, string status )
        {
            currentPlayer = player;
            boardStatus = status;
        }

        public string GetBoardStatus()
        {
            return boardStatus;
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

