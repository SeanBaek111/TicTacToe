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
            Player[] listPlayer;

            Player currentPlayer = null;

            Stack<GameStatus> gameStatuses = FileManager.Instance.LoadProgress();

            if (gameStatuses.Count < 1)
            {
                Console.WriteLine("There's no saved game.");
                return game;
            }

            GameStatus gameStatus = gameStatuses.Peek();
            Data.GetInstance().GameStatus = gameStatus;
            Board board = null;

            switch (gameStatus.GameMode)
            {
                case GameModeEnum.Numeric_Tic_Tac_Toe:
                    game = new NumericTTT();
                    board = new NumericTicTacToeBoard();
                    board.SetStatus(gameStatus.BoardStatus);
                    game.SetBoard(board);
                    break;
                case GameModeEnum.Wild_Tic_Tac_Toe:
                    game = new WildTTT();
                    board = new WildTicTacToeBoard();
                    board.SetStatus(gameStatus.BoardStatus);
                    game.SetBoard(board);
                    break;
                default:
                    throw new Exception($"Game mode: {""} is not exists.");
            }

            if (gameStatus.GameType == GameTypeEnum.Human_VS_Human)
            {

                Data.GetInstance().PlayerTypeEnum = PlayerTypeEnum.Human_Player;
                currentPlayer = new HumanPlayer(gameStatus.CurrentPlayer.GetName());
                currentPlayer.bTurn = gameStatus.CurrentPlayer.bTurn;
                currentPlayer.isPlayerTurn = gameStatus.CurrentPlayer.isPlayerTurn;
                currentPlayer.isFirstTurn = gameStatus.CurrentPlayer.isFirstTurn;

                listPlayer = new Player[2];

                listPlayer[0] = new HumanPlayer(PlayerTypeEnum.Player_A);
                listPlayer[1] = new HumanPlayer(PlayerTypeEnum.Player_B);

                if (currentPlayer.GetName() == PlayerTypeEnum.Player_A && currentPlayer.isPlayerTurn)
                {

                    listPlayer[0].SetTurn(true);
                    listPlayer[1].SetTurn(false);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Player_A && !currentPlayer.isPlayerTurn)
                {

                    listPlayer[0].SetTurn(false);
                    listPlayer[1].SetTurn(true);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Player_B && currentPlayer.isPlayerTurn)
                {

                    listPlayer[0].SetTurn(false);
                    listPlayer[1].SetTurn(true);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Player_B && !currentPlayer.isPlayerTurn)
                {

                    listPlayer[0].SetTurn(true);
                    listPlayer[1].SetTurn(false);
                }


                if (currentPlayer.GetName() == PlayerTypeEnum.Player_A && currentPlayer.isFirstTurn)
                {

                    listPlayer[0].SetIsFirstTurn(true);
                    listPlayer[1].SetIsFirstTurn(false);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Player_A && !currentPlayer.isFirstTurn)
                {

                    listPlayer[0].SetIsFirstTurn(false);
                    listPlayer[1].SetIsFirstTurn(true);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Player_B && currentPlayer.isFirstTurn)
                {

                    listPlayer[0].SetIsFirstTurn(false);
                    listPlayer[1].SetIsFirstTurn(true);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Player_B && !currentPlayer.isFirstTurn)
                {

                    listPlayer[0].SetIsFirstTurn(true);
                    listPlayer[1].SetIsFirstTurn(false);
                }

            }
            else if (gameStatus.GameType == GameTypeEnum.Human_VS_Computer)
            {
                Data.GetInstance().PlayerTypeEnum = PlayerTypeEnum.Monte_Carlo_Computer_Player;

                // 여기서 name에 따라
                if(gameStatus.CurrentPlayer.GetName() == PlayerTypeEnum.Human_Player)
                {
                    currentPlayer = new HumanPlayer();
                }
                else
                {
                    currentPlayer = new MonteCarloComputerPlayer();
                }
                
                currentPlayer.bTurn = gameStatus.CurrentPlayer.bTurn;
                currentPlayer.isPlayerTurn = gameStatus.CurrentPlayer.isPlayerTurn;
                currentPlayer.isFirstTurn = gameStatus.CurrentPlayer.isFirstTurn;

                listPlayer = new Player[2];
                listPlayer[0] = new HumanPlayer();
                //listPlayer[1] = new ComputerPlayer();
                listPlayer[1] = new MonteCarloComputerPlayer();



                if (currentPlayer.GetName() == PlayerTypeEnum.Human_Player && currentPlayer.isPlayerTurn)
                {
                    listPlayer[0].SetTurn(true);
                    listPlayer[1].SetTurn(false);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Human_Player && !currentPlayer.isPlayerTurn)
                {
                    listPlayer[0].SetTurn(false);
                    listPlayer[1].SetTurn(true);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Monte_Carlo_Computer_Player && currentPlayer.isPlayerTurn)
                {
                    listPlayer[0].SetTurn(false);
                    listPlayer[1].SetTurn(true);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Monte_Carlo_Computer_Player && !currentPlayer.isPlayerTurn)
                {
                    listPlayer[0].SetTurn(true);
                    listPlayer[1].SetTurn(false);
                }


                if (currentPlayer.GetName() == PlayerTypeEnum.Human_Player && currentPlayer.isFirstTurn)
                {
                    listPlayer[0].SetIsFirstTurn(true);
                    listPlayer[1].SetIsFirstTurn(false);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Human_Player && !currentPlayer.isFirstTurn)
                {
                    listPlayer[0].SetIsFirstTurn(false);
                    listPlayer[1].SetIsFirstTurn(true);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Monte_Carlo_Computer_Player && currentPlayer.isFirstTurn)
                {
                    listPlayer[0].SetIsFirstTurn(false);
                    listPlayer[1].SetIsFirstTurn(true);
                }
                else if (currentPlayer.GetName() == PlayerTypeEnum.Monte_Carlo_Computer_Player && !currentPlayer.isFirstTurn)
                {
                    listPlayer[0].SetIsFirstTurn(true);
                    listPlayer[1].SetIsFirstTurn(false);
                }

            }
            else
            {
                throw new Exception(" is not exsist");
            }


            Data.GetInstance().GameMode  =  gameStatus.GameMode;
            Data.GetInstance().GameType =  gameStatus.GameType;
            Data.GetInstance().CurrentPlayer = currentPlayer;
            game.SetPlayers(listPlayer);


            return game;
        }
    }
}

