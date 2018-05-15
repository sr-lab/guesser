# Guesser
Sometimes you just need a super-simple list-based password guesser that will take a plaintext list of guesses, a plaintext list of attacks and tell you how many guesses you got right.

## Overview
Takes two lists, one of guesses and one to be guessed, and tells you how many matches turn up. Does some other small stuff like deduplicating guesses. Wrote this in C# fast because I want to use it from Windows.

## Usage
Use the utility like this:

```
Guesser <guess_db> <password_db>
```

Output will look like this, with the number of times the password matched in brackets:

```
Guessed dragon successfully (2476)!
Guessed matrix successfully (1039)!
...
```

A file called `graph.csv` will be placed into the current directory too. This will allow you to graph correct guesses over time using whichever spreadsheet software you like. The file is of the format:

```
1, 10
2, 14
3, 900
4, 980
...
```

The first column is the number of guesses made, the second is the cumulative number of correct guesses. In the example above, guess 3 was a really good guess.
