using Rekcja2;
using System;
using System.Linq;

namespace Rekcja
{
    internal class WordData
    {
        public WordData()
        {
        }

        public void Start()
        {
            char choose;
            Console.Clear();
            Console.WriteLine("Co chcesz zrobić?\n" +
                "1. Edytować bazę danych\n" +
                "2. Dodać nowe słowo\n" +
                "3. Wyświetlić zawartość bazy");
            do
            {
                choose = Convert.ToChar(Console.Read());
            } while (choose != '1' && choose != '2' && choose != '3');
            Console.ReadLine();
            switch (choose)
            {
                case '1':
                    EditMode();
                    break;
                case '2':
                    AddMode();
                    break;
                case '3':
                    Show();
                    break;
                default:
                    throw new Exception("Bad Choice");
            }

        }

        private void AddMode()
        {
            char choice;
            do
            {
                Console.Clear();
                bool result;
                string wordCase;
                Console.WriteLine("Podaj nowy czasownik bez przyimka:");
                string GermangermanWord = Console.ReadLine();
                Console.WriteLine("Podaj przyimek:");
                string prep = Console.ReadLine();
                do
                {
                    Console.WriteLine("Podaj przypadek A - Akkusativ lub D - Dativ:");
                    wordCase = Console.ReadLine();
                    if (wordCase[0] == 'D' || wordCase[0] == 'A')
                        result = true;
                    else
                        result = false;
                } while (!result);
                Console.WriteLine("Podaj tłumaczenie:");
                string translation = Console.ReadLine();

                result = Add(GermangermanWord, prep, wordCase, translation);

                if (result)
                    Console.WriteLine("Pomyślnie dodano nowe słowo!");
                else
                    Console.WriteLine("Dodanie nowego słowa nie powiodło się!");
                
                do
                {
                    Console.WriteLine("Czy chcesz dodać nowe słowo? T/N");
                    choice = char.ToUpper(Convert.ToChar(Console.Read()));
                    Console.ReadLine();
                } while (choice != 'T' && choice != 'N');
            } while (choice == 'T');
        }

        private bool Add(string germangermanWord, string prep, string wordCase, string translation)
        {
            var context = new WordsEntities1();
            int id = context.Table.Count() + 1;
            var newWord = new Table();
            newWord.Id = id;
            newWord.GermanWord = germangermanWord;
            newWord.Preposition = prep;
            newWord.wordCase = wordCase;
            newWord.translation = translation;
            context.Table.Add(newWord);
            var result = context.SaveChanges();
            Console.WriteLine(result.ToString());
            Console.ReadLine();
            if (result > 0)
                return true;
            else
                return false;
        }

        private void EditMode()
        {
            Console.Clear();
            bool result;
            int noWord;
            do
            {
                Console.WriteLine("Podaj id słowa do edycji:");
                result = int.TryParse(Console.ReadLine(), out noWord);
            } while (!result);

            Edit(noWord);
        }

        private void Edit(int noWord)
        {
            Console.Clear();
            var context = new WordsEntities1();
            int wordCount = context.Table.Count();
            if (noWord > wordCount)
            {
                Console.WriteLine("Nie ma w bazie słowa o podanym indeksie!");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Co chcesz edytować?\n" +
                "1. Słowo niemieckie\n" +
                "2. Przyimek\n" +
                "3. Przypadek\n" +
                "4. Tłumaczenie");
            char choice = Convert.ToChar(Console.Read());
            Console.ReadLine();
            var oldWord = context.Table.First(x => x.Id == noWord);
            string newWord;
            switch (choice)
            {
                case '1':
                    Console.WriteLine("Poprzednie słowo było {0}\n Podaj nowe brzmienie:", oldWord.GermanWord);
                    newWord = Console.ReadLine();
                    oldWord.GermanWord = newWord;
                    int num = context.SaveChanges();
                    break;
                case '2':
                    Console.WriteLine("Poprzednie słowo było {0}\n Podaj nowe brzmienie:", oldWord.Preposition);
                    newWord = Console.ReadLine();
                    oldWord.Preposition = newWord;
                    context.SaveChanges();
                    break;
                case '3':
                    Console.WriteLine("Poprzednie słowo było {0}\n Podaj nowe brzmienie:", oldWord.wordCase);
                    newWord = Console.ReadLine();
                    oldWord.wordCase = newWord;
                    context.SaveChanges();
                    break;
                case '4':
                    Console.WriteLine("Poprzednie słowo było {0}\n Podaj nowe brzmienie:", oldWord.translation);
                    newWord = Console.ReadLine();
                    oldWord.translation = newWord;
                    context.SaveChanges();
                    break;
                default:
                    Console.WriteLine("Nie ma takiej opcji");
                    Console.ReadLine();
                    break;
            }
        }

        void Show()
        {
            var context = new WordsEntities1();
            int pages = context.Table.Count()/10 +1;
            for (int i = 0; i < pages; i++)
            {
                Console.Clear();
                var word = context.Table.Where(x => x.Id <= 10*(i+1) && x.Id > 10*i);
                foreach (var item in word)
                {
                    Console.WriteLine("{0}. {1} {2} {3} - {4}", item.Id, item.GermanWord, item.Preposition, item.wordCase, item.translation);
                }
                Console.WriteLine("Strona {0} z {1}, Enter aby kontynuować ...", i+1, pages);
                Console.ReadLine();
            }
        }
    }
}