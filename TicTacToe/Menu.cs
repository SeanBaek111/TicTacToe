using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Menu
    {
        List<string> listMenu = new List<string>();
        private string question = "";
        public Menu(string question)
        {
            this.question = question;
        }

        public Menu()
        {

        }

        public void SetQuestion(string question) { this.question += question + "\n"; }
        public void SetQuestionEnum(Enum question) { this.question += question.ToStringExt() + "\n"; }
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
            Console.WriteLine(question);
            Console.WriteLine(Environment.NewLine);
            for (int i = 0; i < listMenu.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + listMenu[i]);
            }
        }

        public int GetUserAnswer()
        {
            int nSelection = 0;

            ShowMenu();
            Console.Write("\n>>> ");
            Int32.TryParse(Console.ReadLine(), out nSelection);

            while (nSelection < 1 || nSelection > listMenu.Count)
            {
                Console.WriteLine("Select a valid number\n");
                ShowMenu();
                Console.Write("\n>>> ");
                Int32.TryParse(Console.ReadLine(), out nSelection);
            }
            Console.Clear();

            return nSelection;
        }
    }
}
