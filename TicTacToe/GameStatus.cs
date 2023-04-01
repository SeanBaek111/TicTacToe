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
        string boardStatus;
        Player currentPlayer;        
        GameModeEnum name;
        GameTypeEnum players;
        BoardTypeEnum board;

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

