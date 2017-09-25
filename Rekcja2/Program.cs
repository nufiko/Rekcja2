using System;

namespace Rekcja
{
    class Program
    {
        static void Main(string[] args)
        {
            var quiz = new Quiz();
            var wordData = new WordData();
            char choice;
            do
            {
                Console.WriteLine("Wybierz co chcesz zrobić \n1. Rozpocznij quiz.\n2. Przegladaj/edytuj baze danych.");
                char button;
                do
                {
                    button = Convert.ToChar(Console.Read());
                }
                while (button != '1' && button != '2');
                if (button == '1')
                    quiz.Start();
                else
                    wordData.Start();
                Console.WriteLine("Czy chcesz zakończyć program? T/N");
                do
                    choice = char.ToUpper(Convert.ToChar(Console.Read()));
                while (choice != 'T' && choice != 'N');
            } while (choice == 'N');
        }
    }
}
