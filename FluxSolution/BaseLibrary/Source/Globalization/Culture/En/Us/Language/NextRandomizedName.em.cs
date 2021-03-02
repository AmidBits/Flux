namespace Flux
{
	public static partial class GlobalizationEnUsEm
	{
		/// <summary></summary>
		public static string NextRandomNameEnUs(this System.Random source, int length)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var sb = new System.Text.StringBuilder();

			while (sb.Length < (2 * length))
			{
				sb.Append(source.NextBoolean() ? GetConsonant() : GetVowel());
			}

			return sb.NormalizeAdjacent('a', 'i', 'u', 'y', 'h', 'j', 'k', 'q', 'v', 'w', 'x', 'z').ToUpperCaseInvariant(0, 1).LeftMost(length);

			string GetConsonant(double probabilityOfSkipping = 0.25, double chanceOfDouble = 0.10)
				=> source.NextDouble() < probabilityOfSkipping ? string.Empty : source.NextProbabilityRuneEnUsConsonant(true).ToString() is var consonant && source.NextDouble() < chanceOfDouble ? consonant + consonant : consonant;

			string GetVowel(double probabilityOfSkipping = 0.15, double chanceOfDouble = 0.0)
			 => source.NextDouble() < probabilityOfSkipping ? string.Empty : source.NextProbabilityRuneEnUsVowel(false).ToString() is var vowel && source.NextDouble() < chanceOfDouble ? vowel + vowel : vowel;
		}
	}
}
