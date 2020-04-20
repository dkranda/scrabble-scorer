using System;
using System.Collections.Generic;

namespace ScrabbleScorer
{
	class ScrabbleScorer
	{
		/// <summary>
		/// Character score map
		/// </summary>
		static public Dictionary<char, int> CharacterScoreMap = new Dictionary<char, int>
		{
			{'A', 1},
			{'E', 1},
			{'I', 1},
			{'O', 1},
			{'U', 1},
			{'L', 1},
			{'N', 1},
			{'S', 1},
			{'T', 1},
			{'R', 1},
			{'D', 2},
			{'G', 2},
			{'B', 3},
			{'C', 3},
			{'M', 3},
			{'P', 3},
			{'F', 4},
			{'H', 4},
			{'V', 4},
			{'W', 4},
			{'Y', 4},
			{'K', 5},
			{'J', 8},
			{'X', 8},
			{'Q', 10},
			{'Z', 10},
		};

		/// <summary>
		/// Main entry point to console app
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			string filePath = GetFilePath();
			int wordCount = 0;
			int score = ScoreLines(ReadDocument(filePath), ref wordCount);
			Console.WriteLine(String.Format("Score: \t\t {0}", score));
			Console.WriteLine(String.Format("Words: \t\t {0}", wordCount));
			Console.WriteLine(String.Format("Average: \t {0}", Math.Round(Convert.ToDecimal(score) / wordCount, 2)));
			Console.ReadKey();
		}

		/// <summary>
		/// Retrieve filepath from user - no validation here
		/// </summary>
		/// <returns>The filepath (string) entered by the user</returns>
		static string GetFilePath()
		{
			Console.WriteLine("Enter filepath:");
			string filePath = Console.ReadLine();
			return filePath;
		}

		/// <summary>
		/// Attempts to read the lines from the given filepath
		/// **No validation done to filepath before attempting to read**
		/// </summary>
		/// <param name="filePath">The path from which to read</param>
		/// <returns>The array of lines from the file</returns>
		static string[] ReadDocument(string filePath)
		{
			string[] lines = System.IO.File.ReadAllLines(@filePath);
			return lines;
		}

		/// <summary>
		/// Scrabble scores an array of lines 
		/// </summary>
		/// <param name="lines">The array of lines to score</param>
		/// <param name="wordCount">The total words in the given lines (by ref)</param>
		/// <returns>The total scrabble score of the given lines</returns>
		static int ScoreLines(string[] lines, ref int wordCount)
		{
			int score = 0;
			foreach (string line in lines)
			{
				score += ScoreLine(line, ref wordCount);
			}
			return score;
		}

		/// <summary>
		/// Scrabble scores an individual line of text
		/// </summary>
		/// <param name="line">A line of text</param>
		/// <param name="wordCount">The total words in the given line (by ref)</param>
		/// <returns>The total score for the given line of text</returns>
		static int ScoreLine(string line, ref int wordCount)
		{
			string[] words = line.Split(' ');
			int score = 0;
			foreach (string word in words)
			{
				int wordScore = ScoreWord(word);
				score += wordScore;
				wordCount += (wordScore > 0 ? 1 : 0);
			}
			return score;
		}

		/// <summary>
		/// Scrabble scores an individual word
		/// </summary>
		/// <param name="word">The word to score</param>
		/// <returns>The scrabble score for the given word</returns>
		static int ScoreWord(string word)
		{
			int score = 0;
			for (int i = 0; i < word.Length; i++)
			{
				score += ScoreLetter(word[i]);
			}
			return score;
		}

		/// <summary>
		/// Scrabble scores the given character
		/// </summary>
		/// <param name="c">The character to score</param>
		/// <returns>The scrabble score for the given character; 0 if not scoreable</returns>
		static int ScoreLetter(char c)
		{
			return CharacterScoreMap.ContainsKey(char.ToUpper(c)) ? CharacterScoreMap[char.ToUpper(c)] : 0;
		}
	}
}
