﻿using System;
using System.Reflection;

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


        private void LoadHelpFiles(string name)
        {
            string resourcePath = "TicTacToe.Help." + name;

            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);


            StreamReader reader = new StreamReader(resourceStream);

            string outputPath = name;
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.Write(reader.ReadToEnd());
            }

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
            string[] helpContext = FileManager.Instance.LoadTXT(fileName + ".txt");
            Console.Clear();
            helpContext.All(a =>
            {
                Console.WriteLine(a);
                return true;
            });
        }
    }
}
