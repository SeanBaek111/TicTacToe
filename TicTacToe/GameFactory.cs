using System;
namespace TicTacToe
{
    public class GameFactory
    {
        public GameFactory()
        {
        }

        public Game CreateGame(string name, Player[] players)
        {
            if (name == "numericTTT")
            {
                Game numericTTT = new NumericTTT();
                numericTTT.SetPlayers(players);
                return numericTTT;
            }
            else if (name == "wildTTT")
            {
                Game wildTTT = new WildTTT();
                wildTTT.SetPlayers(players);
                return wildTTT;
            } 
            else
            {
                throw new Exception( name + " is not exsist");
            }

        }
    }
}

