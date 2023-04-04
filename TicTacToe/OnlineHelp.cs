using System;
namespace TicTacToe
{
	public class OnlineHelp
	{

        public static OnlineHelp _instance = new OnlineHelp();

        public static OnlineHelp GetInstance()
        {
            return _instance;
        }

        private OnlineHelp()
        {
        }

        public void ShowNumericTTTHelp()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════╗");
            Console.WriteLine("║ Numerical TTT ║");
            Console.WriteLine("╚═══════════════╝");
            Console.WriteLine();
            Console.WriteLine("A variation of the classic Tic-Tac-Toe game. Two players take turns placing the numbers 1 to 9 on a 3x3 board.");
            Console.WriteLine("The first player plays with the odd numbers, the second player plays with the even numbers.");
            Console.WriteLine("The player who first puts down 15 points in a line (horizontal, vertical, or diagonal) wins the game."); ;
            Console.WriteLine();
            Console.WriteLine("╔═══════════════╗");
            Console.WriteLine("║  How to Play  ║");
            Console.WriteLine("╚═══════════════╝");
            Console.WriteLine();
            Console.WriteLine("The input for the player is (Position Number).");
            Console.WriteLine("Position = Number on the Square Board.");
            Console.WriteLine("First Player Numbers = 1, 3, 5, 7, 9");
            Console.WriteLine("Second Player Numbers = 2, 4, 6, 8");
            Console.WriteLine("Example:");
            Console.WriteLine("Human_Player: (Position Piece) >>> 3 1");
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║Human_Player chose position 3 with number 1                    ║");
            Console.WriteLine("║**Square Number**	    **GameBoard**                      ║");
            Console.WriteLine("║┌─────┬─────┬─────┐        ┌─────┬─────┬─────┐                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  1  │  2  │  3  │        │  -  │  -  │  1  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  4  │  5  │  6  │        │  -  │  -  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  7  │  8  │  9  │        │  -  │  -  │  -  │                ║");
            Console.WriteLine("║└─────┴─────┴─────┘        └─────┴─────┴─────┘                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝"); ;
            Console.WriteLine();
            Console.WriteLine("On the board you can see that the piece the player has inputted is on the 3 Square with a 1.");
            Console.WriteLine("╔══════════════╗");
            Console.WriteLine("║  How to Win  ║");
            Console.WriteLine("╚══════════════╝");
            Console.WriteLine("The player who first puts down 15 points in a line(sum of a horizontal, vertical, or diagonal row of three numbers) wins the game.");
            Console.WriteLine();
            Console.WriteLine("╔════════════╗");
            Console.WriteLine("║  Example   ║");
            Console.WriteLine("╚════════════╝");
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║Monte_Carlo_Computer_Player chose position 2 with number 8    ║");
            Console.WriteLine("║**Square Number**	    **GameBoard**                      ║");
            Console.WriteLine("║┌─────┬─────┬─────┐        ┌─────┬─────┬─────┐                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  1  │  2  │  3  │        │  3  │  8  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  4  │  5  │  6  │        │  7  │  2  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  7  │  8  │  9  │        │  5  │  -  │  -  │                ║");
            Console.WriteLine("║└─────┴─────┴─────┘        └─────┴─────┴─────┘                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("In this scenario, if the first player puts 5 in position 7 the sum of the first column will equal 15(3 + 7 + 5).");
            Console.WriteLine();
            Console.WriteLine("╔════════════╗");
            Console.WriteLine("║  Example   ║");
            Console.WriteLine("╚════════════╝");
            Console.WriteLine();
            Console.WriteLine("Human_Player: (Position Piece) >>> 7 5");
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║Human_Player chose position 7  with number 5                  ║");
            Console.WriteLine("║**Square Number**	    **GameBoard**                      ║");
            Console.WriteLine("║┌─────┬─────┬─────┐        ┌─────┬─────┬─────┐                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  1  │  2  │  3  │        │  3  │  8  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  4  │  5  │  6  │        │  7  │  2  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  7  │  8  │  9  │        │  5  │  -  │  -  │                ║");
            Console.WriteLine("║└─────┴─────┴─────┘        └─────┴─────┴─────┘                ║");
            Console.WriteLine("║Winner is Human Player                                        ║");
            Console.WriteLine("║Game Finished                                                 ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("╔════════════╗");
            Console.WriteLine("║    Tips    ║");
            Console.WriteLine("╚════════════╝");
            Console.WriteLine();
            Console.WriteLine("At any point in the game you can undo and redo moves if you aren't happy with the decision you made.");
            Console.WriteLine("You may also save a game and reload it if you need to quit the game if something comes up.");
            Console.WriteLine("*Warning* The game will only save the state the game was in, so undo and redo won't be available when you load.");
        }

        public void ShowWildTTTHelp()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════╗");
            Console.WriteLine("║    Wild TTT    ║");
            Console.WriteLine("╚════════════════╝");
            Console.WriteLine();
            Console.WriteLine("Like the classic Tic-Tac-Toe game, two players take turns placing an X or an O piece on");
            Console.WriteLine("a 3x3 board. However, in this game players can choose to place either X or O on each move.");
            Console.WriteLine("The player who first puts down 15 points in a line (horizontal, vertical, or diagonal) wins the game."); ;
            Console.WriteLine();
            Console.WriteLine("╔═══════════════╗");
            Console.WriteLine("║  How to Play  ║");
            Console.WriteLine("╚═══════════════╝");
            Console.WriteLine();
            Console.WriteLine("The input for the player is (Position Number).");
            Console.WriteLine("Position = Number on the Square Board.");
            Console.WriteLine("First Player Numbers = 1, 3, 5, 7, 9");
            Console.WriteLine("Second Player Numbers = 2, 4, 6, 8");
            Console.WriteLine("Example:");
            Console.WriteLine("Human_Player: (Position Piece) >>> 3 1");
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║Human_Player chose position 3 with number 1                    ║");
            Console.WriteLine("║**Square Number**	    **GameBoard**                      ║");
            Console.WriteLine("║┌─────┬─────┬─────┐        ┌─────┬─────┬─────┐                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  1  │  2  │  3  │        │  -  │  -  │  1  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  4  │  5  │  6  │        │  -  │  -  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  7  │  8  │  9  │        │  -  │  -  │  -  │                ║");
            Console.WriteLine("║└─────┴─────┴─────┘        └─────┴─────┴─────┘                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝"); ;
            Console.WriteLine();
            Console.WriteLine("On the board you can see that the piece the player has inputted is on the 3 Square with a 1.");
            Console.WriteLine("╔══════════════╗");
            Console.WriteLine("║  How to Win  ║");
            Console.WriteLine("╚══════════════╝");
            Console.WriteLine("The player who first puts down 15 points in a line(sum of a horizontal, vertical, or diagonal row of three numbers) wins the game.");
            Console.WriteLine();
            Console.WriteLine("╔════════════╗");
            Console.WriteLine("║  Example   ║");
            Console.WriteLine("╚════════════╝");
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║Monte_Carlo_Computer_Player chose position 2 with number 8    ║");
            Console.WriteLine("║**Square Number**	    **GameBoard**                      ║");
            Console.WriteLine("║┌─────┬─────┬─────┐        ┌─────┬─────┬─────┐                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  1  │  2  │  3  │        │  3  │  8  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  4  │  5  │  6  │        │  7  │  2  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  7  │  8  │  9  │        │  5  │  -  │  -  │                ║");
            Console.WriteLine("║└─────┴─────┴─────┘        └─────┴─────┴─────┘                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("In this scenario, if the first player puts 5 in position 7 the sum of the first column will equal 15(3 + 7 + 5).");
            Console.WriteLine();
            Console.WriteLine("╔════════════╗");
            Console.WriteLine("║  Example   ║");
            Console.WriteLine("╚════════════╝");
            Console.WriteLine();
            Console.WriteLine("Human_Player: (Position Piece) >>> 7 5");
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║Human_Player chose position 7  with number 5                  ║");
            Console.WriteLine("║**Square Number**	    **GameBoard**                      ║");
            Console.WriteLine("║┌─────┬─────┬─────┐        ┌─────┬─────┬─────┐                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  1  │  2  │  3  │        │  3  │  8  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  4  │  5  │  6  │        │  7  │  2  │  -  │                ║");
            Console.WriteLine("║├─────┼─────┼─────┤        ├─────┼─────┼─────┤                ║");
            Console.WriteLine("║│     │     │     │        │     │     │     │                ║");
            Console.WriteLine("║│  7  │  8  │  9  │        │  5  │  -  │  -  │                ║");
            Console.WriteLine("║└─────┴─────┴─────┘        └─────┴─────┴─────┘                ║");
            Console.WriteLine("║Winner is Human Player                                        ║");
            Console.WriteLine("║Game Finished                                                 ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("╔════════════╗");
            Console.WriteLine("║    Tips    ║");
            Console.WriteLine("╚════════════╝");
            Console.WriteLine();
            Console.WriteLine("At any point in the game you can undo and redo moves if you aren't happy with the decision you made.");
            Console.WriteLine("You may also save a game and reload it if you need to quit the game if something comes up.");
            Console.WriteLine("*Warning* The game will only save the state the game was in, so undo and redo won't be available when you load.");
        }

        public void ShowHelp()
        {
            Console.Clear();
            Console.WriteLine("ShowHelp 1");
            Console.WriteLine("ShowHelp 2");
            Console.WriteLine("ShowHelp 3");
        }
    }
}

