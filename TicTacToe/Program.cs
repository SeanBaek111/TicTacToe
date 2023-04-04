using System;
using static System.Console;
using static TicTacToe.Enums;

namespace TicTacToe;

public class Program
{

    public static void Main()
    {
        // Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
        // Console.ForegroundColor = ConsoleColor.Green;
        // Console.BackgroundColor = ConsoleColor.Yellow;

        //Console.Write("This ");
        //Console.ForegroundColor = ConsoleColor.White;
        //Console.BackgroundColor = ConsoleColor.Blue;

        //Console.Write("is");
        //Console.ForegroundColor = ConsoleColor.DarkBlue;
        //Console.BackgroundColor = ConsoleColor.Gray;

        //Console.Write("test");

        //Console.ResetColor();  
        //Console.WriteLine("\nPress any key to exit...");
        //Console.ReadKey();

        GameMenu.GetInstance().SplashScreen();
    }
}
