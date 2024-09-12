using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace WordCounter

{
    class Program

    {
        static void Main(string[] args)

        {
            if (args.Length != 1)
            {
                Console.WriteLine("C:\\Users\\Morio\\Desktop\\AplikacjaN1Praktykii\\wejsciowy.txt");
                return;


            }
            string inputFile = args[0];
                                                                        //<3
            string OutFile = "wynikWejsciowegoPliku.txt";

            try
            {
                string text = File.ReadAllText(inputFile).ToLower();                //Loading file

                text = Regex.Replace(text, @"[^a-z0-9\s]", "");             //remove punctuation marks
                string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var wordCounts = new Dictionary<string, int>();             //counting words and numbers
                var numberCounts = new Dictionary<string, int>();

                int totalNumbers = 0;

                foreach (string word in words)
                {
                    if (int.TryParse(word, out int number))
                    {
                        numberCounts.TryAdd(number.ToString(), 1);
                        totalNumbers++;


                    }
                    else
                    {
                        wordCounts.TryAdd(word, 1);
                    }



                }

                var sortedWords = new List<KeyValuePair<string, int>>(wordCounts);   //sorting alphabetically
                sortedWords.Sort((x, y) => x.Key.CompareTo(y.Key));

                var sortedNumbers = new List<KeyValuePair<string, int>>(numberCounts);
                sortedNumbers.Sort((x, y) => int.Parse(x.Key).CompareTo(int.Parse(y.Key)));

                using (StreamWriter writer = new StreamWriter(OutFile))
                {
                    {
                        foreach (var pair in sortedWords)
                        {
                            writer.WriteLine($"{pair.Key}: {pair.Value}");
                        }
                        Console.WriteLine();
                        foreach (var pair in sortedNumbers)
                        {
                            writer.WriteLine(($"{pair.Key}:  {pair.Value}"));

                            totalNumbers += pair.Value;
                        }
                    }
                }



            }
            catch  (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }

        }




    }
}