using System;
using static TicTacToe.Enums;

namespace TicTacToe
{
    [Serializable]
    public class GameStatus
    {
        //  string pieceStatus = "-,-,3,-,-,6,-,9,-";
        /* char[,] boardStatus2= {{'-','-','3'},
                                 {'-','-','6'},
                                 {'-','2','-'}};*/
        public string BoardStatus { get; set; }
        public Player CurrentPlayer { get; set; }

        public Player[] Players { get; set; }
        public Board Board { get; set; }

        public PlayerTypeEnum PlayerTypeEnum { get; set; }
        public GameModeEnum GameMode { get; set; }
        public GameTypeEnum GameType { get; set; }
        public BoardTypeEnum BoardType { get; set; }

        public char lastPiece { get; set; }

        private List<char> listAvailablePieces;


        public GameStatus() {}

        public GameStatus(Player player, string status)
        {
            CurrentPlayer = player;
            BoardStatus = status;
           /* GameMode = GameModeEnum.Wild_Tic_Tac_Toe;
            GameType = GameTypeEnum.Human_VS_Computer;
            BoardType = BoardTypeEnum.Tic_Tac_Toe_Board;*/
        }

        public string GetBoardStatus()
        {
            return BoardStatus;
        }

        // set listAvailablePieces
        public void SetAvailablePieces(List<char> list)
        {
            listAvailablePieces = list;
        }

        public List<char> GetAvailablePieces() { return listAvailablePieces;}
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

