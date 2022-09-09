using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
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

        public static void TryAdd(this Dictionary<string, Dictionary<string, int>> dictionary, string key, string value)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (dictionary.ContainsKey(key))
            {

                if(dictionary[key].ContainsKey(value))
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
                dictionary.Add(key, new Dictionary<string, int>() { {value, 1 } });
            }
          
        }

        private static int GetValueInMap(Dictionary<(string, string), int> frequencyMap, (string, string) key)
        {
            int result;
            frequencyMap.TryGetValue(key, out result);
            return result;
        }

        public static void CreateResultMap(Dictionary<string, string> result, Dictionary<(string, string), int> frequencyMap)
        {
            foreach(var e in frequencyMap.Keys)
            {
                var ee = e;

                foreach(var e2 in frequencyMap.Keys)
                {
                    if (e.Item1 == e2.Item1)
                    {
                        if (frequencyMap[e] > frequencyMap[e2])
                        {
                            ee = e;
                        }
                        else if (frequencyMap[e] == frequencyMap[e2])
                        {
                            var item1Compare = String.CompareOrdinal(e.Item1, e2.Item1);
                            var item2Compare = String.CompareOrdinal(e.Item2, e2.Item2);

                            if ((item1Compare + item2Compare) < 0)
                            {
                                ee = e;
                            }else
                            {
                                ee = e2;
                            }
                        } 
                        else
                        {
                            ee = e2;
                        }
                    }
                }

                result.TryAdd(ee.Item1, ee.Item2);
            }
        }

        //----------------------------------------------------------------------------------------
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            var frequencyMap = new Dictionary<string, Dictionary<string, int>>();



            foreach (var word in text)
            {
                for (int i = 0; i < word.Count - 1; i++)
                {
                    if (word.Count >= 2)
                        TryAdd(frequencyMap, word[i], word[i+1]);
                    else
                        break;
                }

                for (int i = 0; i < word.Count - 2; i++)
                {
                    if (word.Count >= 3)
                        TryAdd(frequencyMap, $"{word[i]} {word[i + 1]}", word[i+2] );
                    else
                        break;
                }
            }

            //CreateResultMap(result, frequencyMap);



            return result;
        }
    }
}