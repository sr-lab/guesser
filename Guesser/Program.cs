using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Guesser
{
    class Program
    {
        /// <summary>
        /// Reads a file as lines, returning it as an array of strings.
        /// </summary>
        /// <param name="filename">The filename of the file to read.</param>
        /// <returns></returns>
        private static string[] ReadFileAsLines(string filename)
        {
            return File.ReadAllText(filename)
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .ToArray();
        }

        static void Main(string[] args)
        {
            // Check number of arguments.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: Guesser <guess_db> <password_db>");
                return;
            }

            // Check files exist.
            if (!File.Exists(args[0]) || !File.Exists(args[1]))
            {
                Console.WriteLine("Could not read one of the files specified.");
                return;
            }

            // Read guess file and deduplicate.
            var guesses = ReadFileAsLines(args[0]).Distinct();

            // Read password database.
            var db = ReadFileAsLines(args[1]);

            // Guess passwords, keep track of total.
            var totalMatchCount = 0;
            var cumulative = new List<int>();
            foreach (var guess in guesses)
            {
                var guessMatchCount = db.Count(x => x == guess);
                if (guessMatchCount > 0)
                {
                    Console.WriteLine($"Guessed {guess} successfully ({guessMatchCount})!");
                }
                cumulative.Add(cumulative.Count == 0 ? guessMatchCount : cumulative.Last() + guessMatchCount);
                totalMatchCount += guessMatchCount;
            }

            // Relay result to user.
            Console.WriteLine($"{totalMatchCount}/{db.Count()}");

            // Write cumulative file.
            var sb = new StringBuilder();
            for (var i = 0; i < cumulative.Count; i++)
            {
                sb.Append(i)
                    .Append(',')
                    .Append(cumulative[i])
                    .AppendLine();
            }
            File.WriteAllText("graph.csv", sb.ToString());
        }
    }
}
