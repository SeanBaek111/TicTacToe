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

        public GameStatus(Player player, string status)
        {
            currentPlayer = player;
            boardStatus = status;
        }
    }
}

