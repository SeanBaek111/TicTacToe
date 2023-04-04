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
            Console.WriteLine("ShowNumericTTTHelp 1");
            Console.WriteLine("ShowNumericTTTHelp 2");
            Console.WriteLine("ShowNumericTTTHelp 3");
        }

        public void ShowWildTTTHelp()
        {
            Console.Clear();
            Console.WriteLine("ShowWildTTTHelp 1");
            Console.WriteLine("ShowWildTTTHelp 2");
            Console.WriteLine("ShowWildTTTHelp 3");
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

