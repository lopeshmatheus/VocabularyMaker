using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace DictionaryMaker
{
    internal static class CsvGenerator
    {
        public static void AppendToCsv()
        {
            //string path = @"D:\Projects\csvFiles\";


            //using (var writer = new StreamWriter(path + "test.csv"))
            //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            //{
            //    csv.WriteRecords(BookManagement.AsignWords());
            //}
            Console.WriteLine("Whats the name of the file?");
            string fileName = Console.ReadLine();
            var list = BookManagement.AsignWords();
            foreach(var item in list)
            {
                File.AppendAllText(@$"D:\Projects\csvFiles\{fileName}.csv", $"{item.OriginalWord},{item.PortugueseWord}\n");
            }

        }
    }
}
