using System.Linq;

namespace Flux
{
	public static partial class GlobalizationEnUsEm
	{
		public static System.Text.Rune NextProbabilityRuneEnUs(this System.Random source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var value = source.NextDouble();

			var spectrum = 0.0;

			for (var index = 0; index < Globalization.EnUs.Language.RelativeFrequencyOfLetters.Count; index++)
			{
				var kvp = Globalization.EnUs.Language.RelativeFrequencyOfLetters[index];

				spectrum += kvp.Value;

				if (value < spectrum)
					return kvp.Key;
			}

			return Globalization.EnUs.Language.RelativeFrequencyOfLetters[0].Key;
		}
		public static System.Text.Rune NextProbabilityRuneEnUsConsonant(this System.Random source, bool includeY)
		{
			var rune = NextProbabilityRuneEnUs(source);
			while (!Globalization.EnUs.Language.IsEnglishConsonant(rune, includeY))
				rune = NextProbabilityRuneEnUs(source);
			return rune;
		}
		public static System.Text.Rune NextProbabilityRuneEnUsVowel(this System.Random source, bool includeY)
		{
			var rune = NextProbabilityRuneEnUs(source);
			while (!Globalization.EnUs.Language.IsEnglishVowel(rune, includeY))
				rune = NextProbabilityRuneEnUs(source);
			return rune;
		}

		///// <summary>Indicates whether the char is an English letter.</summary>
		//public static bool IsEnglishLetter(this char source)
		//  => IsEnglishLetterLower(source) || IsEnglishLetterUpper(source);
		///// <summary>Indicates whether the char is an English lower case letter.</summary>
		//public static bool IsEnglishLetterLower(this char source)
		//  => source >= 'a' && source <= 'z';
		///// <summary>Indicates whether the char is an English upper case letter.</summary>
		//public static bool IsEnglishLetterUpper(this char source)
		//  => source >= 'A' && source <= 'Z';

		///// <summary>Indicates whether the char is an English vowel, where y|Y is optional.</summary>
		//public static bool IsEnglishVowel(this char source, bool includeY)
		//{
		//  switch (source)
		//  {
		//    case 'a':
		//    case 'e':
		//    case 'i':
		//    case 'o':
		//    case 'u':
		//    case 'A':
		//    case 'E':
		//    case 'I':
		//    case 'O':
		//    case 'U':
		//      return true;
		//    case 'y':
		//    case 'Y':
		//      return includeY;
		//    default:
		//      return false;
		//  }
		//}
	}
}
