using System;
namespace TicTacToe
{
    public struct BoardStatus
    {     
        string boardStatus1 = "-,-,3,-,-,6,-,9,-";
        char[,] boardStatus2= {{'-','-','3'},
                                {'-','-','6'},
                                {'-','2','-'}};
        Player currentPlayer;

        public BoardStatus(Player player, string status)
        {
            currentPlayer = player;
            boardStatus1 = status;
        }
    }
}

