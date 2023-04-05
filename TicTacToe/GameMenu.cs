using System;
using System.IO.Enumeration;
using static TicTacToe.Enums;

namespace TicTacToe;

public class GameMenu : Menu
{
    // Defines Variables.
    GameModeEnum sGame;
    GameTypeEnum sPlayers;
    BoardTypeEnum sBoard;
    Player[] players;
    int nSelection;
    Game game;

    const string DEF_SPLASH = "Splash.txt";

    public static GameMenu Instance = new();

    public static GameMenu GetInstance()
    {
        return Instance;
    }

    private GameMenu()
    {
    }

    public override void SplashScreen(string fileName = DEF_SPLASH)
    {
        FileManager.Instance.LoadFile(fileName);
        string[] lines = FileManager.Instance.LoadFileContent(fileName);
        foreach (string i in lines)
        {
            i.PrintCenter(1);
        }
        "Press Any Key To Continue...".PrintCenter();
        Console.ReadKey();
        this.StartGameMenu();
    }

    public override void StartGameMenu()
    {
        FileManager fm = FileManager.Instance;
        // Determind if the save file is exists and have content in it.
        if (fm.IsFileExists() && fm.IsFileEmpty())
        {
            this.LoadMenu();
        }
        this.GameModeMenu();
    }

    public override void GameModeMenu()
    {
        base.ResetMenu();
        base.SetQuestions("Welcome to TTT");
        base.SetQuestions("Select an option");

        // Loop all the possible options from typeof(GameModeEnum)
        Extensions.Query<GameModeEnum>().All(a =>
        {
            base.AddMenuEnum(a);
            return true;
        });
        base.AddMenuEnum(Command.Help);
        base.AddMenuEnum(Command.About);
        base.AddMenuEnum(ConfirmationEnum.Quit);

        this.nSelection = base.GetUserAnswer();
        switch (this.nSelection)
        {
            default:
                this.sGame = this.nSelection.ToEnum<GameModeEnum>();
                // Prefixed becasue only one game mode is being used.
                this.sBoard = BoardTypeEnum.Tic_Tac_Toe_Board;
                this.GameTypeMenu();

                break;

            case 3:
                this.HelpMenu(nameof(this.GameModeMenu));
                break;
            case 4:
                this.SplashScreen();
                break;
            case 5:
                Environment.Exit(0);
                break;
        }
    }


    public override void GameTypeMenu()
    {
        base.ResetMenu();
        base.SetQuestionEnum(sGame);
        base.SetQuestions("Who is playing?");

        Extensions.Query<GameTypeEnum>().All(a =>
        {
            this.AddMenuEnum(a);
            return true;
        });
        base.AddMenuEnum(Command.Help);
        base.AddMenuEnum(NavigationEnum.Back);

        this.nSelection = base.GetUserAnswer();
        switch (this.nSelection)
        {
            default:
                this.sPlayers = this.nSelection.ToEnum<GameTypeEnum>();
                game = GameFactory.GetInstance().CreateGame(sGame, sPlayers, sBoard);
                game.Play();
                break;
            case 3:
                // access to help menu
                this.HelpMenu(nameof(this.GameTypeMenu));
                break;
            case 4:
                this.GameModeMenu();
                break;
        }
    }

    public override void HelpMenu(string className)
    {
        base.ResetMenu();
        base.SetQuestions("Help Message");
        Extensions.Query<GameModeEnum>().All(a =>
        {
            base.AddMenuEnum(a);
            return true;
        });
        base.AddMenuEnum(NavigationEnum.Back);
        this.nSelection = base.GetUserAnswer();
        switch (this.nSelection)
        {
            default:
                GameModeEnum gameMode = this.nSelection.ToEnum<GameModeEnum>();
                OnlineHelp.GetInstance().ShowHelp(gameMode);
                break;
        }
        switch (className)
        {
            case "GameModeMenu":
                this.GameModeMenu();
                break;
            case "GameTypeMenu":
                this.GameTypeMenu();
                break;
        }
    }

    public override bool LoadMenu()
    {
        base.ResetMenu();
        while (true)
        {
            base.SetQuestions("Save file detected!");
            base.SetQuestions("Load game?");
            foreach (string name in Enum.GetNames(typeof(ConfirmationEnum)))
            {
                base.AddMenuEnum(name.ToEnum<ConfirmationEnum>());
            }

            switch (base.GetUserAnswer())
            {
                case 1:
                    this.LoadGame();
                    break;
                case 2:
                    this.OverrideSaveFile();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }

        }
    }

    public void EndGameMenu(PlayerTypeEnum winner)
    {
        // Display board at EndGame
        Board currentBoard = game.GetBoard();
        currentBoard.DisplayBoard();
        base.ResetMenu();
        base.SetQuestions("Game Over");
        base.SetQuestions($"{winner.ToStringExt()} wins");

        base.AddMenuEnum(Command.Restart);
        base.AddMenuEnum(Command.Help);
        base.AddMenuEnum(Command.Undo);
        base.AddMenuEnum(Command.Quit);

        nSelection = base.GetUserAnswer(false);
        switch (nSelection)
        {
            case 1:
                // return same mode new game
                game = GameFactory.GetInstance().CreateGame(sGame, sPlayers, sBoard);
                game.Play();
                break;
            case 2:
                // return Help Menu
                this.HelpMenu(nameof(this.GameModeMenu));
                break;
            case 3:
                // perform undo
                History.GetInstance().Undo(currentBoard);
                break;
            case 4:
                Environment.Exit(0);
                break;
        }
    }

    public bool OverrideConfirm()
    {
        base.ResetMenu();
        base.SetQuestions("Save file detected... override?");
        while (true)
        {
            Extensions.Query<ConfirmationEnum>().All(a =>
            {
                base.AddMenuEnum(a);
                return true;
            });
            switch (base.GetUserAnswer())
            {
                // YES
                case 1:
                    return true;
                    break;
                // NO
                case 2:
                    return false;
                // QUIT
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void OverrideSaveFile()
    {
        base.ResetMenu();
        base.SetQuestions("Save file detected... are you sure?");
        base.SetQuestions("If you choose to start a new game,");
        base.SetQuestions("Save file will be wiped and");
        base.SetQuestions("YOU WILL LOST ALL YOUR PROGRESS.");
        foreach (string name in Enum.GetNames(typeof(ConfirmationEnum)))
        {
            base.AddMenuEnum(name.ToEnum<ConfirmationEnum>());
        }
        int confirmWipeAnswer = base.GetUserAnswer();

        switch (confirmWipeAnswer)
        {
            case 1:
                this.LoadGame();
                break;
            case 2:
                this.GameModeMenu();
                break;
            default:
                Environment.Exit(0);
                break;
        }
    }

    private void LoadGame()
    {
        "Load Game".PrintCenter();

        Game game = GameFactory.GetInstance().LoadGame();
        if (game != null)
        {
            game.Play(Data.GetInstance().GameStatus);
        }
    }
}
