using System;
namespace TicTacToe
{
    public class GameFactory
    {
        public static GameFactory gameFactory = new GameFactory();

        public static GameFactory GetInstance()
        {
            return gameFactory;
        }

        private GameFactory()
        {
        }

        public Game CreateGame(string name, string players, string board)
        {
            Game game;
            Player[] listPlayer;
            if (name == "numericTTT")
            {
                game = new NumericTTT(); 
            }
            else if (name == "wildTTT")
            {
                game = new WildTTT(); 
            } 
            else
            {
                throw new Exception( name + " is not exsist");
            }

            if(players == "HumanVsHuman")
            {
                listPlayer = new Player[2];
                listPlayer[0] = new HumanPlayer("Player 1");
                listPlayer[1] = new HumanPlayer("Player 2"); 
                listPlayer[0].SetTurn(true);
                listPlayer[1].SetTurn(false);
                game.SetPlayers(listPlayer);

            }
            else if (players == "HumanVsComputer")
            {
                listPlayer = new Player[2];
                listPlayer[0] = new HumanPlayer();
                listPlayer[1] = new ComputerPlayer();
                listPlayer[0].SetTurn(true);
                listPlayer[1].SetTurn(false);
                game.SetPlayers(listPlayer);
            }
            else
            {
                throw new Exception(name + " is not exsist");
            }

            if (board == "TicTacToeBoard")
            {
                game.SetBoard(new TicTacToeBoard());
            } 
            else
            {
                throw new Exception(name + " is not exsist");
            }

            return game;

        }
    }
}

