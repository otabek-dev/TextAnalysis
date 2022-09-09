using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
                return true;
            }

            return false;
        }

        public static void TryAdd(this Dictionary<string, Dictionary<string, int>> dictionary,
            string key,
            string value)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (dictionary.ContainsKey(key))
            {

                if (dictionary[key].ContainsKey(value))
                {
                    dictionary[key][value]++;
                }
                else
                {
                    dictionary[key].Add(value, 1);
                }
            }
            else
            {
                dictionary.Add(key, new Dictionary<string, int>() { { value, 1 } });
            }
        }

        public static void CreateResultMap(Dictionary<string, string> result,
            Dictionary<string, Dictionary<string, int>> frequencyMap)
        {
            foreach (var e in frequencyMap.Keys)
            {
                var tempResultFreqMap = new Dictionary<string, int>() 
                { 
                    { frequencyMap[e].Keys.Min(), frequencyMap[e][frequencyMap[e].Keys.Min()] } 
                };

                foreach (var ee in frequencyMap[e])
                {
                    if (ee.Value > tempResultFreqMap.Values.First())
                    {
                        tempResultFreqMap.Clear();
                        tempResultFreqMap.Add(ee.Key, ee.Value);
                    }
                    else if (ee.Value == tempResultFreqMap.Values.First())
                    {
                        if (string.CompareOrdinal(ee.Key, tempResultFreqMap.Keys.First()) < 0)
                        {
                            tempResultFreqMap.Clear();
                            tempResultFreqMap.Add(ee.Key, ee.Value);
                        }
                    }
                }
                result.TryAdd(e, tempResultFreqMap.Keys.First());
            }
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var frequencyMap = new Dictionary<string, Dictionary<string, int>>();

            foreach (var word in text)
            {
                for (int i = 0; i < word.Count - 1; i++)
                {
                    if (word.Count >= 2)
                        TryAdd(frequencyMap, word[i], word[i + 1]);
                    else
                        break;
                }

                for (int i = 0; i < word.Count - 2; i++)
                {
                    if (word.Count >= 3)
                        TryAdd(frequencyMap, $"{word[i]} {word[i + 1]}", word[i + 2]);
                    else
                        break;
                }
            }
            CreateResultMap(result, frequencyMap);
            return result;
        }
        /* //////// Визуализация и быстрый поиск по частотному словарю
        private static async void WriteInTXT(Dictionary<string, Dictionary<string, int>> frequencyMap)
        {
            var lineBuil = new StringBuilder();
            foreach (var e in frequencyMap.Keys)
            {

                lineBuil.AppendLine($"{e}");

                foreach (var ee in frequencyMap[e])
                {
                    lineBuil.AppendLine($"\t{ee.Key}, {ee.Value}");
                }
                lineBuil.AppendLine(new string('_', 30));
            }
            string path = "note1.txt";

            // полная перезапись файла 
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync(lineBuil.ToString());
            }
        }
        */
    }
}