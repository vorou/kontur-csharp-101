using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ngramms
{
    public class Program
    {
        private const string TextPath = @"c:\users\vorou\text.txt";
        private const string TopPath = @"c:\users\vorou\top.txt";
        private const string TextPrunedPath = @"c:\users\vorou\text-pruned.txt";

        public static void Main()
        {
            Privalov();
        }

        private static void My()
        {
            // грязновато -- действительно большой файл может не влезть в память
            var text = File.ReadAllText(TextPath).Replace(Environment.NewLine, "");
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
            File.WriteAllLines(TopPath, topWords);
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

        public static void Privalov()
        {
            // I.             
            const int outputAmount = 50;

            // кодировку указывать не обязательно, в файле только ASCII-символы
            var str = File.ReadAllText(TextPath, Encoding.UTF8);
            var word = "";

            // переменная долгоживущая, лучше назвать ее осмысленно
            // я словари обычно называю [Key]To[Value]т
            // т.е. здесь это будет wordToCount или wordToFrequency
            var dct = new Dictionary<string, int>();

            foreach (var e in str)
                if (!char.IsLetter(e) && e.ToString() != "'" && word != "")
                {
                    if (!dct.ContainsKey(word.ToLower()))
                        // тут тоже можно воспользоваться индексером: dct[word.ToLower()] = 0;
                        dct.Add(word.ToLower(), 0);
                    dct[word.ToLower()]++;
                    word = "";
                }
                else if (char.IsLetter(e) || e.ToString() == "'")
                    // word точно должен быть StringBuilder-ом (было в какой-то из предыдущих лекций)
                    word += e.ToString();

            // есть еще вариант с использованием Substring:
            // идем по строке, запоминаем, где слово началось
            // когда слово кончилось, делаем str.Substring(start, length)
            // это будет более оптимально
            // но вообще, для парсинга текста самый *читабельный* вариант -- регулярные выражения (см. мое решение)

            // sortDct неудачное название
            // это уже не словарь, а IEnumerable<KeyValuePair>
            var sortDct = dct.OrderByDescending(i => i.Value);
            for (var i = 0; i < outputAmount; i++)
                Console.WriteLine("{0,2}  {1,-10}{2,4}", i + 1, sortDct.ElementAt(i).Key, sortDct.ElementAt(i).Value);

            // LINQ и IEnumerable устроены так, что на каждый .ElementAt здесь будет заново выполняться сортировка (DEMO)
            // для IEnumerable лучше всегда пользоваться foreach, и считать индекс руками, если он нужен:
            //            var count = 0;
            //            foreach (var wordToFreq in sortDct)
            //            {
            //                Console.WriteLine("{0,2}  {1,-10}{2,4}", count + 1, wordToFreq.Key, wordToFreq.Value);
            //                count++;
            //            }

            Console.ReadKey();

            // II.            
            var outputStr = new StringBuilder();
            var excludeWords = new string[] {"a", "an", "the", "to", "in", "on", "at", "for", "from", "into"};
            var j = 0;

            Console.WriteLine();
            Console.WriteLine("Удаление артиклей и наиболее часто встречающихся предлогов ...");
            Console.WriteLine("Не выключайте питание и не выдергивайте из розетки вилку шнура питания вашего компьютера");

            word = "";
            foreach (var e in str)
            {
                if (!char.IsLetter(e) && e.ToString() != "'")
                {
                    if (word != "")
                    {
                        if (!excludeWords.Contains(word.ToLower()))
                            outputStr.Append(word + e); //outputStr += word + e;
                    }
                    else
                        outputStr.Append(e); //outputStr += e; 
                    word = "";
                }
                else
                // для outputStr использовали StringBuilder, а для word все еще нет
                    word += e;

                j++;
                // проценты это круто
                Console.Write("\r" + Convert.ToString(j*100/str.Length) + '%');
            }
            var outputFile = new StreamWriter(TextPrunedPath);
            outputFile.WriteLine(outputStr);
            outputFile.Close();
            Console.Write("\r" + "Создание файла " + TextPrunedPath + " завершено успешно!");
            Console.ReadKey();

            // III.
            // есть вариант: ".!?;:()-".ToArray()
            // его проще модифицировать (меньше печатать)
            var sentenceSeparators = new string[] {".", "!", "?", ";", ":", "(", ")", "-"};
            var wordSeparators = new string[] {",", " "};
            var couplesWords = new Dictionary<string, int>();

            // кажется, что этот блок должен работать с outputStr (которая уже без артиклей)

            str = str.Replace("\n", "").Replace("\r", "");
            str = str.Replace("Mr.", "Mr#").Replace("Mrs.", "Mrs#");

            var sentences = str.Trim().Split(sentenceSeparators, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < sentences.Length; i++)
            {
                // что тут происходит? зачем s2? почему этот риплейс отдельно?
                {
                    sentences[i] = sentences[i].Replace("Mr#", "Mr.").Replace("Mrs#", "Mrs.").Replace("“", "").Replace("”", "");

                    var s1 = sentences[i];
                    var s = @"""";
                    var s2 = s1.Replace(s, "");
                }

                var words = sentences[i].Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 0)
                    // k < words.Length -- проход по всем элементам
                    // нам нужно пройти по всем, кроме последнего
                    // почему k < words.Length - 2?
                    for (int k = 0; k < words.Length - 2; k++)
                        if (!couplesWords.ContainsKey(words[k] + " " + words[k + 1]))
                            couplesWords.Add(words[k] + " " + words[k + 1], 1);
                        else
                            couplesWords[words[k] + " " + words[k + 1]]++;
            }
            var items = from pair in couplesWords
                        orderby pair.Value descending
                        select pair;

            foreach (var keyValuePair in items.Take(10))
                Console.Out.WriteLine(keyValuePair);
        }
    }
}