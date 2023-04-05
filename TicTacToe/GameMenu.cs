using System;
using System.IO.Enumeration;
using static TicTacToe.Enums;

namespace TicTacToe;

public class GameMenu
{
    // Defines Variables.
    GameModeEnum sGame;
    GameTypeEnum sPlayers;
    BoardTypeEnum sBoard;
    Player[] players;
    int nSelection;

    const string DEF_SPLASH = "Splash.txt";

    Menu menu = new();

    public static GameMenu Instance = new();

    public static GameMenu GetInstance()
    {
        return Instance;
    }

    private GameMenu()
    {
    }

    public void SplashScreen(string fileName = DEF_SPLASH)
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

    public void StartGameMenu()
    {
        FileManager fm = FileManager.Instance;
        // Determind if the save file is exists and have content in it.
        if (fm.IsFileExists() && fm.IsFileEmpty())
        {
            this.LoadMenu();
        }
        this.GameModeMenu();
    }

    public void GameModeMenu()
    {
        this.menu = new();
        this.menu.SetQuestions("Welcome to TTT");
        this.menu.SetQuestions("Select an option");

        // Loop all the possible options from typeof(GameModeEnum)
        EnumExtension.Query<GameModeEnum>().All(a =>
        {
            menu.AddMenuEnum(a);
            return true;
        });
        this.menu.AddMenuEnum(Command.Help);
        this.menu.AddMenuEnum(ConfirmationEnum.Quit);

        this.nSelection = menu.GetUserAnswer();
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
                Environment.Exit(0);
                break;
        }
    }


    public void GameTypeMenu()
    {
        this.menu = new();
        this.menu.SetQuestionEnum(sGame);
        this.menu.SetQuestions("Who is playing?");

        EnumExtension.Query<GameTypeEnum>().All(a =>
        {
            this.menu.AddMenuEnum(a);
            return true;
        });
        this.menu.AddMenuEnum(Command.Help);
        this.menu.AddMenuEnum(NavigationEnum.Back);

        this.nSelection = this.menu.GetUserAnswer();
        switch (this.nSelection)
        {
            default:
                this.sPlayers = this.nSelection.ToEnum<GameTypeEnum>();
                Game game = GameFactory.GetInstance().CreateGame(sGame, sPlayers, sBoard);
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

    public void HelpMenu(string className)
    {
        this.menu = new();
        this.menu.SetQuestions("Help Message");
        EnumExtension.Query<GameModeEnum>().All(a =>
        {
            this.menu.AddMenu(a.ToStringExt());
            return true;
        });
        this.menu.AddMenuEnum(NavigationEnum.Back);
        this.nSelection = menu.GetUserAnswer();
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

    public bool LoadMenu()
    {
        this.menu = new();
        while (true)
        {
            this.menu.SetQuestions("Save file detected!");
            this.menu.SetQuestions("Load game?");
            foreach (string name in Enum.GetNames(typeof(ConfirmationEnum)))
            {
                this.menu.AddMenuEnum(name.ToEnum<ConfirmationEnum>());
            }

            switch (this.menu.GetUserAnswer())
            {
                case 1:
                    this.LoadGame();
                    break;
                case 2:
                    Menu confirmWipe = new();
                    confirmWipe.SetQuestions("Are you sure?");
                    confirmWipe.SetQuestions("If you choose to start a new game,");
                    confirmWipe.SetQuestions("Save file will be wiped and");
                    confirmWipe.SetQuestions("YOU WILL LOST ALL YOUR PROGRESS.");
                    foreach (string name in Enum.GetNames(typeof(ConfirmationEnum)))
                    {
                        confirmWipe.AddMenuEnum(name.ToEnum<ConfirmationEnum>());
                    }
                    int confirmWipeAnswer = confirmWipe.GetUserAnswer();

                    if (confirmWipeAnswer == 1)
                    {
                        this.LoadGame();
                    }
                    else if (confirmWipeAnswer == 2)
                    {
                        this.GameModeMenu();
                    }
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }

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
