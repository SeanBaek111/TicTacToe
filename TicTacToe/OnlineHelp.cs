using System;
using System.Reflection;

namespace TicTacToe
{
    public class OnlineHelp
    {
        const string DEF_HELPNAME = "help.txt";
        private const string DEF_HELP_PREFIX = "Help.";

        public static OnlineHelp _instance = new OnlineHelp();

        public static OnlineHelp GetInstance()
        {
            return _instance;
        }


        private void LoadHelpFiles(string name)
        {
            string resourcePath = DEF_HELP_PREFIX + name;

            FileManager.Instance.LoadFile(resourcePath);
        }

        private OnlineHelp()
        {
            LoadHelpFiles("Numeric_Tic_Tac_Toe.txt");
            LoadHelpFiles("Wild_Tic_Tac_Toe.txt");
        }

        /// <summary>
        /// Show game mode help, if no parametes is inputted, show general help
        /// </summary>
        public void ShowHelp(Enum? helpType)
        {
            string fileName = String.Empty;

            if (helpType is not null)
            {
                fileName = DEF_HELP_PREFIX + helpType.ToString();
            }
            else
            {
                fileName = DEF_HELPNAME;
            }

            this.Display(fileName);
        }

        private void Display(string fileName)
        {
            Console.Clear();
            string[] lines = FileManager.Instance.LoadFileContent(fileName);

            foreach (string i in lines)
            {
                i.PrintCenter();
            }
            "Press Any Key To Continue...".PrintCenter(10);
            Console.ReadKey();
        }
    }
}
