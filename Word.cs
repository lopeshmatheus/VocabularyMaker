using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Translation.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections;
using Newtonsoft.Json;


namespace DictionaryMaker
{
    public class Word
    {
        public string OriginalWord { get; set; }
        public string PortugueseWord { get; set; }
        //public List<string> Sentences { get; set; }

        public Word(string originalWord, string translatedWord)
        {
            OriginalWord = originalWord;
            PortugueseWord = translatedWord;
        }
    }

}
