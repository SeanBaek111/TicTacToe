using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public class Data
    {
        public GameModeEnum GameMode { get; set; }
        public GameTypeEnum GameType { get; set; } 
        public PlayerTypeEnum PlayerTypeEnum { get; set; }

        public Player CurrentPlayer
        {
            get
            {
                return GameStatus.CurrentPlayer;
            }
            set
            {
                GameStatus.CurrentPlayer = value;
            }
        }
        public GameStatus GameStatus { get; set; }
        public static Data _instance = new Data();

        public static Data GetInstance()
        {
            return _instance;
        } 


        private Data()
        {
        }


    }
}
