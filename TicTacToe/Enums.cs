using System;

namespace TicTacToe;


public static class Enums
{

    public enum PlayerTypeEnum
    {
        Player_A,
        Player_B,
        Computer_Player,
        Monte_Carlo_Computer_Player,
        Human_Player,
    }
    public enum BoardTypeEnum
    {
        Tic_Tac_Toe_Board = 1,
    }
    public enum GameTypeEnum
    {
        Human_VS_Computer = 1,
        Human_VS_Human = 2,
    }

    public enum GameModeEnum
    {
        Wild_Tic_Tac_Toe = 1,
        Numeric_Tic_Tac_Toe = 2,
    }

    public enum ConfirmationEnum
    {
        Yes = 1,
        No = 2,
        Quit = 3,
    }

    public enum NavigationEnum
    {
        Back = 1,
        Foward = 2,
    }

    public enum Command
    {
        Help,
        Save,
        Load,
        Undo,
        Redo,
        Quit,
        None,
        InvalidMove,
        About,
        Restart,
        InvalidInput
    }
}
