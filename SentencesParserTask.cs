using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesArray = text.Split(new char[] { '.', '?', '!', ':', ';', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            var sentencesList = new List<List<string>>();
            
            var wordList = new List<string>();

            for (int i = 0; i < sentencesArray.Length; i++)
            {
                wordList.AddRange(sentencesArray[i].ToLower().Split(
                    new char[] { '^', '#', '$', '-', '+', '1', '=', ' ', '\t', '\n', '\r', '—', '\"', ',', '…' },
                    StringSplitOptions.RemoveEmptyEntries
                    ));

                var word = new StringBuilder();

                for (int j = 0; j < wordList.Count; j++)
                {

                    wordList[j] = wordList[j].Trim(' ', ',');

                    foreach (char c in wordList[j])
                    {
                        if (char.IsLetter(c) || c == '\'')
                        {
                            word.Append(c);
                        }
                    }
                    wordList[j] = word.ToString();
                    word.Clear();
                }

                wordList.RemoveAll(s => s == "");

                if (wordList.Count != 0)
                {
                    sentencesList.Add(new List<string>(wordList));

                }
                wordList.Clear();
            }

            return sentencesList;
        }
    }
}