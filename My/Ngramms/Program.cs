using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ngramms
{
    public class Program
    {
        public static void Main()
        {
            var file = @"c:\users\vorou\text.txt";
            // грязновато -- действительно большой файл может не влезть в память
            var text = File.ReadAllText(file).Replace(Environment.NewLine, "");
            var sentences = text.Split(".!?;:()-".ToArray());
            // регулярка
            var wordRegex = new Regex("[a-z']+");
            // тоже хак -- это уже второй проход по строке (первый -- когда делим на предложения)
            // в таких масштабах скорость норм, поэтому можно забить
            var sentencesWords = sentences.Select(s => wordRegex.Matches(s.ToLower()).Cast<Match>().Select(m => m.Value).ToList());

            var wordToCount = new Dictionary<string, int>();
            // список списков становится списком -- flattening
            foreach (var word in sentencesWords.SelectMany(sw => sw))
            {
                if (!wordToCount.ContainsKey(word))
                    wordToCount[word] = 0;
                wordToCount[word]++;
            }

            // LINQ
            // Dictionary<string, int> -> IEnumerable<KeyValuePair<string, int>> (пропадает требование уникальности)
            var topWords = wordToCount.OrderByDescending(kv => kv.Value).Take(50).Select(kv => kv.Key);
            File.WriteAllLines(@"c:\users\vorou\top.txt", topWords);
            // vs ReadAllText:string (оба поднимают весь файл в память)
            var keepWords = File.ReadAllLines(@"c:\users\vorou\keep.txt");
            // hashset
            // можно убрать превращение в хэшсет и ничего не сломается
            // но код будет гораздо медленнее (профайлинг)
            var removeWords = new HashSet<string>(topWords.Except(keepWords));

            // List vs IEnumerable
            var prunedSentencesWords = new List<IEnumerable<string>>();
            foreach (var sentenceAsWords in sentencesWords)
                prunedSentencesWords.Add(sentenceAsWords.Where(w => !removeWords.Contains(w)));

            // сложный ключ
            var biToCount = new Dictionary<Tuple<string, string>, int>();
            foreach (var sentenceAsWords in prunedSentencesWords)
                for (var i = 0; i < sentenceAsWords.Count() - 1; i++)
                {
                    var bi = new Tuple<string, string>(sentenceAsWords.ElementAt(i), sentenceAsWords.ElementAt(i + 1));
                    if (!biToCount.ContainsKey(bi))
                        biToCount[bi] = 0;
                    biToCount[bi]++;
                }

            // коллекция групп (есть ключ, можно перебирать)
            var wordToCompletions = biToCount.GroupBy(kv => kv.Key.Item1);
            var wordToBestCompletion = wordToCompletions.ToDictionary(
                g => g.Key,
                g => g.OrderByDescending(kv => kv.Value).First().Key.Item2);

            Console.Out.Write("word: ");
            var toComplete = Console.ReadLine();
            Console.Out.Write(toComplete + " ");
            for (var i = 0; i < 10; i++)
            {
                toComplete = wordToBestCompletion[toComplete];
                Console.Out.Write(toComplete + " ");
            }
            Console.Out.WriteLine();

            // можно сделать и без LINQ

            // IEnumerable

            // List/[]
            // HashSet
            // Dictionary<TX,TY> / IEnumerable<KeyValuePair<TX,TY>>
        }
    }
}