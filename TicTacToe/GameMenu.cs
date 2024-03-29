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
        
        base.ResetMenu();
        foreach (string i in lines)
        {
            base.SetQuestions(i);
        }
        base.GetUserNotice();
        this.StartGameMenu(true);
    }

    public override void StartGameMenu(bool noticed = false)
    {
        FileManager fm = FileManager.Instance;
        // Determind if the save file is exists and have content in it.
        if (fm.IsFileExists() && !fm.IsFileEmpty())
        {
            this.LoadMenu();
        }
        else if (!noticed)
        {
            base.ResetMenu();
            base.SetQuestions("Welcome to TTT");
            base.SetQuestions("No save file found");
            base.GetUserNotice();
        }

        if( this.GameModeMenu() )
        {
            StartGameMenu(noticed);
        }
    }

    public override bool GameModeMenu()
    {
        bool bRes = true;
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
                bRes = false;
                break;
        }

        return bRes;
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
            case 3:
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
       
        while (true)
        {
            base.ResetMenu();
            base.SetQuestions("Save file detected!");
            base.SetQuestions("Load game?");
            foreach (string name in Enum.GetNames(typeof(ConfirmationEnum)))
            {
                base.AddMenuEnum(name.ToEnum<ConfirmationEnum>());
            }

            int selection = base.GetUserAnswer();

            switch (selection)
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
                    FileManager.Instance.WipeSaveFile();
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
        base.SetQuestions("Your save file will be wiped and");
        base.SetQuestions("YOU WILL LOSE ALL OF YOUR PROGRESS!!!");
        foreach (string name in Enum.GetNames(typeof(ConfirmationEnum)))
        {
            base.AddMenuEnum(name.ToEnum<ConfirmationEnum>());
        }
        int confirmWipeAnswer = base.GetUserAnswer();

        switch (confirmWipeAnswer)
        {
            case 1:
                FileManager.Instance.WipeSaveFile();
                this.GameModeMenu();
                break;
            case 2:
                this.LoadGame();
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
