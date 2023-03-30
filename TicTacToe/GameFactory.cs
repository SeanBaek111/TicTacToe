using System;
using static TicTacToe.Enums;

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

        public Game CreateGame(GameModeEnum name, GameTypeEnum players, BoardTypeEnum board)
        {
            Game game;
            Player[] listPlayer;
            switch (name)
            {
                case GameModeEnum.Numeric_Tic_Tac_Toe:
                    game = new NumericTTT();
                    game.SetBoard(new NumericTicTacToeBoard());
                    break;
                case GameModeEnum.Wild_Tic_Tac_Toe:
                    game = new WildTTT();
                    game.SetBoard(new WildTicTacToeBoard());
                    break;
                default:
                    throw new Exception($"Game mode: {name.ToStringExt()} is not exists.");
            }

            if (players == GameTypeEnum.Human_VS_Human)
            {
                listPlayer = new Player[2];
                listPlayer[0] = new HumanPlayer("Player 1");
                listPlayer[1] = new HumanPlayer("Player 2");

            }
            else if (players == GameTypeEnum.Human_VS_Computer)
            {
                listPlayer = new Player[2];
                listPlayer[0] = new HumanPlayer();
                listPlayer[1] = new ComputerPlayer();
            }
            else
            {
                throw new Exception(name + " is not exsist");
            }

            listPlayer[0].SetTurn(true);
            listPlayer[1].SetTurn(false);
            game.SetPlayers(listPlayer);


            return game;

        }
    }
}

