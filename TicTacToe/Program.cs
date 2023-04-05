using System;
using static System.Console;
using static TicTacToe.Enums;

namespace TicTacToe;

public class Program
{

    public static void Main()
    {
        GameMenu.GetInstance().StartGameMenu();
    }
}
