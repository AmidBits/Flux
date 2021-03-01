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

    public string GetConsonant(double probabilityOfSkipping = 0.10, double chanceOfDouble = 0.10)
      => probabilityOfSkipping < probabilityOfSkipping ? string.Empty : m_consonants[m_rng.Next(m_consonants.Length)] is var consonant && m_rng.NextDouble() < chanceOfDouble ? consonant + consonant : consonant;

    public string GetVowel(double probabilityOfSkipping = 0.05, double chanceOfDouble = 0.05)
      => probabilityOfSkipping < probabilityOfSkipping ? string.Empty : m_vowels[m_rng.Next(m_vowels.Length)] is var vowel && m_rng.NextDouble() < chanceOfDouble ? vowel + vowel : vowel;

    public string GenerateName(int len)
    {

      var Name = string.Empty;

      Name += GetConsonant(0, 0);
      Name += GetVowel(0, 0);

      var b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.

      while (b < len)
      {
        Name += GetConsonant();
        if (Name.Length >= len) break;
        Name += GetVowel();
        if (Name.Length >= len) break;
      }

      return Name.LeftMost(len);
    }
  }
}
