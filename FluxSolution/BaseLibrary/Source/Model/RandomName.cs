using System.Collections.Generic;

namespace Flux.Random
{
	public class RandomName
	{
		private readonly System.Random m_rng;

		private string[] m_consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
		private string[] m_vowels = { "a", "e", "i", "o", "u", "ae", "y" };

		public RandomName(System.Random rng)
			=> m_rng = rng;

		public string GetConsonant(double probabilityOfSkipping = 0.25, double chanceOfDouble = 0.10)
			=> m_rng.NextDouble() < probabilityOfSkipping ? string.Empty : m_rng.NextProbabilityRuneEnUsConsonant(true).ToString() is var consonant && m_rng.NextDouble() < chanceOfDouble ? consonant + consonant : consonant;

		public string GetVowel(double probabilityOfSkipping = 0.15, double chanceOfDouble = 0.0)
			=> m_rng.NextDouble() < probabilityOfSkipping ? string.Empty : m_rng.NextProbabilityRuneEnUsVowel(false).ToString() is var vowel && m_rng.NextDouble() < chanceOfDouble ? vowel + vowel : vowel;

		public string GenerateName(int len)
		{
			var name = new System.Text.StringBuilder();

			while (name.Length < len)
			{
				name.Append(GetConsonant());
				name.Append(GetVowel());
			}

			name.NormalizeAdjacent('a', 'e', 'i', 'o', 'u', 'y');

			name.ToUpperCaseInvariant(0, 1);

			return name.LeftMost(len);
		}
	}
}
