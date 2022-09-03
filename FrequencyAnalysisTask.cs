using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            int i = 1;
            foreach (var word in text)
            {
                Console.WriteLine("String " + i);
                foreach (var word2 in word)
                {
                    Console.Write(word2 + " ");
                }
                Console.WriteLine("\n" + new String('-', 20));
                i++;
            }
            return result;
        }
    }
}