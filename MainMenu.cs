using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryMaker
{
    public class MainMenu
    {
        public static void Start()
        {
            Console.WriteLine("Would you like to start the app?");
            Console.ReadLine();
            var wordList = BookManagement.AsignWords();
            foreach (var word in wordList)
            {
                Console.WriteLine(word.OriginalWord);
                Console.WriteLine(word.PortugueseWord);
            }
            
        }
    }
}
