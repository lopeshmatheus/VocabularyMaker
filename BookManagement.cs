using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections;

namespace DictionaryMaker
{
    public static class BookManagement
    {
        public static string TxtToString()
        {
            string book = File.ReadAllText(@"D:\Projects\Books\birdbox.txt");
            return book;
        }
        
        public static string RemoveSpecialChars(string book)
        {
            book = new string((from c in book
                              where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                              select c
       ).ToArray());
            book = Regex.Replace(book, @"\s+", " ");


            return book;
        }
        public static List<string> stringToList(string book)
        {
            List<string> words = new List<string>(book.Split(' '));
            return words;
        }
        public static List<string> RankItems(List<string> wordList)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();

            for (int i = 0; i < wordList.Count; i++)
            {
                if (!dic.ContainsKey(wordList[i]))
                {
                    dic.Add(wordList[i], 1);
                }
                else if (dic.ContainsKey(wordList[i]))
                {
                    dic[wordList[i]]++;
                }
            }

            var sortedDict = from entry in dic orderby entry.Value descending select entry;
            var newDict = sortedDict.ToDictionary<KeyValuePair<string, int>, string, int>(pair => pair.Key, pair => pair.Value);
            var sortedList = newDict.Select(kvp => kvp.Key).ToList();
            return sortedList;

        }
        public static List<string> ReturnSortedItemsList()
        {
            var book = BookManagement.TxtToString();
            book = BookManagement.RemoveSpecialChars(book);
            var bookList = BookManagement.stringToList(book);
            var sortedList = BookManagement.RankItems(bookList);          
            return sortedList;
        }

        
        static public string MergeWithComma(List<string> book)
        {
            string combinedString = string.Join(",", book.ToArray());
            return combinedString;
        }
        public static IEnumerable<T> TakePercent<T>(this ICollection<T> source, double percent)
        {
            int count = (int)(source.Count * percent / 100);
            return source.Take(count);
        }
        public static List<string> DeletePercent(List<string> list)
        {
            var topPercentList = TakePercent(list, 0.5);
            List<string> newList = new List<string>();
            list.RemoveRange(0, topPercentList.Count());
            return list;
        }
        public static List<string> ReturnMinusOnePercent()
        {
            var list = ReturnSortedItemsList();
            var newList = DeletePercent(list);                   
            return newList;
        }
        public static string AddNewLine(string str)
        {
            var newStr = str.Replace(",", System.Environment.NewLine);
            return newStr;
        }
        public static Dictionary<string, string> ReturnFinalDictionary()
        {
            var sortedLst = BookManagement.ReturnMinusOnePercent();          
            var filterList = BookManagement.TakePercent(sortedLst, 10).ToList();
            var originalWords = BookManagement.MergeWithComma(filterList);          
            originalWords = BookManagement.AddNewLine(originalWords);
            string translatedWords = Translation.TranslateText(originalWords);           

            var eachOriginalWord = originalWords.Split(System.Environment.NewLine);            
            var eachTranslatedWord = translatedWords.Split(System.Environment.NewLine);
            for ( int i = 0; i < eachTranslatedWord.Length; i++ )
            {
                eachTranslatedWord[i] = eachTranslatedWord[i].Trim();
            }
            
            var dic = eachOriginalWord.Zip(eachTranslatedWord, (k, v) => new { k, v })
                          .ToDictionary(x => x.k, x => x.v);           
            return dic;


        }
        static public void RemoveWhiteSpace()
        {

        }

        public static List<Word> AsignWords()
        {
            var dictionary = BookManagement.ReturnFinalDictionary();
           
            List<Word> wordsList = new List<Word>();
            foreach (var word in dictionary)
            {               
                wordsList.Add(new Word(word.Value, word.Key));
            }
            return wordsList;
        }



    }
}
