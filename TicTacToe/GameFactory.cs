using System;
using System.Xml.Linq;
using static TicTacToe.Enums;

namespace TicTacToe
{
    public class GameFactory
    {
        public static GameFactory _instance = new GameFactory();

        public static GameFactory GetInstance()
        {
            return _instance;
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
                    Data.GetInstance().GameMode = GameModeEnum.Numeric_Tic_Tac_Toe; 
                    game = new NumericTTT();
                    game.SetBoard(new NumericTicTacToeBoard());
                    break;
                case GameModeEnum.Wild_Tic_Tac_Toe:
                    Data.GetInstance().GameMode = GameModeEnum.Wild_Tic_Tac_Toe;
                    game = new WildTTT();
                    game.SetBoard(new WildTicTacToeBoard());
                    break;
                default:
                    throw new Exception($"Game mode: {name.ToStringExt()} is not exists.");
            }

            if (players == GameTypeEnum.Human_VS_Human)
            {
                Data.GetInstance().GameType = GameTypeEnum.Human_VS_Human;
                Data.GetInstance().PlayerTypeEnum = PlayerTypeEnum.Human_Player;

                listPlayer = new Player[2];
                listPlayer[0] = new HumanPlayer(PlayerTypeEnum.Player_A);
                listPlayer[1] = new HumanPlayer(PlayerTypeEnum.Player_B);

                listPlayer[0].SetIsFirstTurn(true);
                listPlayer[1].SetIsFirstTurn(false);

            }
            else if (players == GameTypeEnum.Human_VS_Computer)
            {
                Data.GetInstance().GameType = GameTypeEnum.Human_VS_Computer;
                Data.GetInstance().PlayerTypeEnum = PlayerTypeEnum.Monte_Carlo_Computer_Player;
                listPlayer = new Player[2];
                 listPlayer[0] = new HumanPlayer();
              //listPlayer[1] = new ComputerPlayer();
                listPlayer[1] = new MonteCarloComputerPlayer();

                listPlayer[0].SetIsFirstTurn(true);
                listPlayer[1].SetIsFirstTurn(false);


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

        public Game LoadGame()
        {
            Game game = null; 

            Stack<GameStatus> gameStatuses = FileManager.Instance.LoadProgress();

            if (gameStatuses.Count < 1)
            {
                Console.WriteLine("There's no saved game.");
                return game;
            }

            GameStatus gameStatus = gameStatuses.Peek();
            Data.GetInstance().GameStatus = gameStatus;
        

            switch (gameStatus.GameMode)
            {
                case GameModeEnum.Numeric_Tic_Tac_Toe:
                    game = new NumericTTT();   
                    break;
                case GameModeEnum.Wild_Tic_Tac_Toe:
                    game = new WildTTT(); 
                   
                    break;
                default:
                    throw new Exception($"Game mode: {""} is not exists.");
            }

            game.SetBoard(gameStatus.Board);

            Data.GetInstance().GameMode =  gameStatus.GameMode;
            Data.GetInstance().GameType =  gameStatus.GameType;
            Data.GetInstance().CurrentPlayer = gameStatus.CurrentPlayer;

            game.SetPlayers(gameStatus.Players);


            return game;
        }
    }
}

