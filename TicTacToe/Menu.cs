using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public abstract class Menu
    {

        public abstract void SplashScreen(string fileName);
        public abstract void StartGameMenu(bool noticed);
        public abstract void GameModeMenu();
        public abstract void GameTypeMenu();
        public abstract void HelpMenu(string className);
        public abstract bool LoadMenu();

        List<string> listMenu = new List<string>();
        private string question = "";
        private List<string> questions = new();
        public Menu(string question)
        {
            this.question = question;
        }

        public Menu()
        {

        }

        public void SetQuestion(string question) { this.question += question + "\n"; }
        public void SetQuestions(string question) { this.questions.Add(question); }
        public void SetQuestionEnum(Enum question) { this.questions.Add(question.ToStringExt()); }
        public void AddMenu(string menu)
        {
            listMenu.Add(menu);
        }
        public void AddMenuEnum(Enum value)
        {
            listMenu.Add(value.ToStringExt());
        }

        private void ShowMenu()
        {
            foreach (string i in questions)
            {
                //i.PrintCenter(10);
                Extensions.WriteLineCenter(i);
            }
            Console.WriteLine(Environment.NewLine);
            for (int i = 0; i < listMenu.Count; i++)
            {
                ("[" + (i + 1) + "] " + listMenu[i]).PrintCenter(10);
            }
        }
        public int GetUserAnswer(bool clear = true)
        {
            int nSelection = 0;
            
            if (clear)
                Console.Clear();
            
            ShowMenu();
            Console.Write("\n>>> ");
            Int32.TryParse(Console.ReadLine(), out nSelection);

            while (nSelection < 1 || nSelection > listMenu.Count)
            {
                ("Select a valid number\n").PrintCenter();
                ShowMenu();
                Console.Write("\n>>> ");
                Int32.TryParse(Console.ReadLine(), out nSelection);
            }
            Console.Clear();

            return nSelection;
        }

        public void GetUserNotice()
        {
            ShowMenu();
            "Press Any Key To Continue...".PrintCenter();
            Console.ReadKey();
        }

        /// <summary>
        /// Resets Menu
        /// </summary>
        public void ResetMenu()
        {
            this.listMenu.Clear();
            this.questions.Clear();
        }
    }
}
