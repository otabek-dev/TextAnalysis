using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            List<string> resultWords = phraseBeginning.Split(' ').ToList();

            for (int i = 0; i < wordsCount; i++)
            {
                if (!AddNextWord(nextWords, resultWords))
                    break;
            }

            return string.Join(" ", resultWords).Trim();
        }

        private static bool AddNextWord(Dictionary<string, string> nextWords, List<string> words, string key)
        {
            if (nextWords.ContainsKey(key))
            {
                words.Add(nextWords[key]);

                return true;
            }

            return false;
        }

        private static bool AddNextWord(Dictionary<string, string> nextWords, List<string> words)
        {
            string key1 = words[words.Count - 1];

            if (words.Count >= 2)
            {
                string key2 = words[words.Count - 2] + " " + words[words.Count - 1];

                if (AddNextWord(nextWords, words, key2))
                {
                    return true;
                }
            }

            if (AddNextWord(nextWords, words, key1))
            {
                return true;
            }

            return false;
        }
    }
}