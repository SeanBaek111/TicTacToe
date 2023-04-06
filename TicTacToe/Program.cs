using System;
using static System.Console;
using static TicTacToe.Enums;

namespace TicTacToe;

public class Program
{

    static void GameThread()
    {
        GameMenu.GetInstance().StartGameMenu();
    }
    public static void Main()
    {
        Thread tr = new Thread(GameThread);
        tr.Start();
    }
}
