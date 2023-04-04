using System;

namespace TicTacToe
{
    public class OnlineHelp
    {
        const string DEF_HELPNAME = "help.txt";

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
        /// </summary>
        public void ShowHelp(Enum? helpType)
        {
            string fileName = String.Empty;

            if (helpType is not null)
            {
                fileName = helpType.ToString();
            }
            else
            {
                fileName = DEF_HELPNAME;
            }

            this.Display(fileName);
        }

        private void Display(string fileName)
        {
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
