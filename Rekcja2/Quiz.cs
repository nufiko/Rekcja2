using Rekcja2;
using System;
using System.Linq;

namespace Rekcja
{
    internal class Quiz
    {

        bool restart = false;
        int [] randomWords;
        int score = 0;

        public Quiz()
        {
            
        }

        public void Start()
        {

            Console.WriteLine("Podane zostanie polskie tłumaczenie niemieckiego czasownika, \n" +
                "zadaniem jest podanie tego czasownika wraz z przyimkiem oraz pierwszą literą odpowiedniego przypadku \n" +
                "A - Akkusativ D - Dativ");
            //Console.WriteLine("Na udzielenie odpowiedzi jest 30 sekund");
            do
            {
                Console.WriteLine("Wciśnij dowolny klawisz aby zacząć");
                Console.ReadKey(true);
                Console.ReadLine();
                RandomizeWords();
                Console.Clear();
                for (int i = 0; i < 10; i++)
                {
                    Console.Write("{0}.  ", i+1);
                    ShowWord(randomWords[i]);
                    var answear = Console.ReadLine();
                    bool result = CheckAnswear(answear, randomWords[i]);
                    Console.Clear();
                    if (result)
                    {
                        Console.WriteLine("Dobrze\n");
                        score++;
                    }
                    else
                    {
                        Console.WriteLine("Źle\n");
                    }
                }
                Console.WriteLine("\nTwój wynik to {0}", score);
                Console.WriteLine("Czy chcesz zagrać jeszcze raz? T/N");
                int retry = Console.Read();
                if (char.ToUpper(Convert.ToChar(retry)) == 'T') restart = true;
                else restart = false;
            } while (restart);


        }

        private bool CheckAnswear(string answear, int number)
        {
            var context = new WordsEntities1();
            var word = context.Table.First(x => x.Id == number);
            Console.WriteLine("\nPoprawna odpowiedź to:\n {0} {1} {2}\n", word.GermanWord, word.Preposition, word.wordCase);
            var ans = answear.Split(' ');

            string german, prepo, wordCase;

            if (ans.Count() == 4)
            {
                german = ans[0] + ' ' + ans[1]; //if first part is two words concat
                prepo = ans[2];
                wordCase = ans[3];
            }
            else if (ans.Count() == 3)
            {
                german = ans[0];
                prepo = ans[1];
                wordCase = ans[2];
            }
            else
                return false;  //wrong answear if to few words

            if (german.Equals(word.GermanWord) && prepo.Equals(word.Preposition) && wordCase.ToUpper().Equals(word.wordCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void ShowWord(int number)
        {
            var context = new WordsEntities1();
            var word = context.Table.First(x => x.Id == number);
            Console.WriteLine("{0}", word.translation);
        }

        void RandomizeWords()
        {
            Random rand = new Random();
            var context = new WordsEntities1();
            int noWords = context.Table.Count();
            randomWords = new int[10];

            for(int i = 0; i<10; i++)
            {
                randomWords[i] = rand.Next(noWords)+1;
            }
        }
    }
}