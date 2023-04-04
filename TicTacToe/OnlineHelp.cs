using System;
using static TicTacToe.Enums;

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

        /// <summary>
        /// Show game mode help, if no parametes is inputted, show general help
        /// text file.
        /// </summary>
        public void ShowHelp(GameModeEnum? gameMode)
        {
            string fileName = String.Empty;

            if (gameMode is not null)
            {
                fileName = gameMode.ToString();
            }
            else
            {
                fileName = "GeneralHelp.txt";
            }

            string[] helpContext = FileManager.Instance.LoadTXT(fileName ??
            throw new FileLoadException());
            Console.Clear();
            helpContext.All(a =>
            {
                Console.WriteLine(a);
                return true;
            });
        }
    }
}
}
