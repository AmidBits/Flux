namespace Flux
{
	public static partial class GlobalizationEnUsEm
	{
		/// <summary>Generates a random name of specified length based on simple statistical properties of the English language.</summary>
		public static string NextRandomNameEnUs(this System.Random source, int length)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var sb = new System.Text.StringBuilder();

			var consonantRepeats = 0;
			var vowelRepeats = 0;

			while (sb.Length < (2 * length))
			{
				if (vowelRepeats >= 2 || (consonantRepeats < 2 && source.NextDouble() <= 0.6)) // Typical consonants to vowel ratio is about 3/2.
				{
					if (source.NextDouble() > 0.6)
					{
						consonantRepeats++;
						vowelRepeats = 0;

						sb.Append(source.NextProbabilityRuneEnUsConsonant(source.NextDouble() < 0.6).ToString());
					}
				}

				if (consonantRepeats >= 2 || (vowelRepeats < 2 && source.NextDouble() < 0.4)) // Typical vowel to consonant ratio is about 2/3.
				{
					if (source.NextDouble() > 0.4)
					{
						consonantRepeats = 0;
						vowelRepeats++;

						sb.Append(source.NextProbabilityRuneEnUsVowel(source.NextDouble() < 0.4).ToString());
					}
				}
			}

			return sb.ToUpperCaseInvariant(0, 1).LeftMost(length);
		}
	}
}
