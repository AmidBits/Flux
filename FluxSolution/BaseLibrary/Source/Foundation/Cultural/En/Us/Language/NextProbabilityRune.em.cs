namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the next probable rune based on the rng <paramref name="source"/>.</summary>
    public static System.Text.Rune NextProbabilityRuneEnUs(this System.Random source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      double accumulatedValues = 0.0, probabilityThreshold = source.NextDouble();

      for (var index = 0; index < Cultural.EnUs.Language.RelativeFrequencyOfLetters.Count; index++)
      {
        var kvp = Cultural.EnUs.Language.RelativeFrequencyOfLetters[index];

        accumulatedValues += kvp.Value;

        if (probabilityThreshold <= accumulatedValues)
          return kvp.Key;
      }

      return Cultural.EnUs.Language.RelativeFrequencyOfLetters[0].Key;
    }

    /// <summary>Returns the next probable consonant based on <paramref name="source"/> and whether Y will be considered.</summary>
    public static System.Text.Rune NextProbabilityRuneEnUsConsonant(this System.Random source, bool includeY)
    {
      var rune = NextProbabilityRuneEnUs(source);
      while (!GlobalizationEnUs.IsEnglishConsonant(rune, includeY))
        rune = NextProbabilityRuneEnUs(source);
      return rune;
    }
    /// <summary>Returns the next probable vowel based on <paramref name="source"/> and whether Y will be considered.</summary>
    public static System.Text.Rune NextProbabilityRuneEnUsVowel(this System.Random source, bool includeY)
    {
      var rune = NextProbabilityRuneEnUs(source);
      while (!GlobalizationEnUs.IsEnglishVowel(rune, includeY))
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
