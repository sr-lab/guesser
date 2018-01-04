using System;
using System.IO;
using System.Linq;

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
                .Select(x => x.Trim())
                .ToArray();
        }

        static void Main(string[] args)
        {
            // Check number of arguments.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: Guesser <guess_db> <password_db>");
            }

            // Check files exist.
            if (!File.Exists(args[0]) || !File.Exists(args[1]))
            {
                Console.WriteLine("Could not read one of the files specified.");
            }

            // Read guess file and deduplicate.
            var guesses = ReadFileAsLines(args[0]).Distinct();

            // Read password database.
            var db = ReadFileAsLines(args[1]);

            // Guess passwords, keep track of total.
            var totalMatchCount = 0;
            foreach (var guess in guesses)
            {
                var guessMatchCount = db.Count(x => x == guess);
                if (guessMatchCount > 0)
                {
                    Console.WriteLine("Guessed " + guess + " successfully!");
                }
                totalMatchCount += guessMatchCount;
            }

            // Relay result to user.
            Console.WriteLine($"{totalMatchCount}/{db.Count()}");
        }
    }
}
