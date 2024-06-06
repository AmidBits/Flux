//namespace Flux
//{
//  public static partial class RandomEm
//  {
//    /// <summary>Returns the next probable rune based on the rng <paramref name="source"/>.</summary>
//    public static System.Text.Rune NextProbabilityRuneEnUs(this System.Random source)
//    {
//      if (source is null) throw new System.ArgumentNullException(nameof(source));

//      double accumulatedValues = 0.0, probabilityThreshold = source.NextDouble();

//      for (var index = 0; index < Flux.Globalization.EnUs.Language.AlphabetRelativeFrequencyOfLetters.Length; index++)
//      {
//        var relativeFrequency = Globalization.EnUs.Language.AlphabetRelativeFrequencyOfLetters[index];

//        accumulatedValues += relativeFrequency;

//        if (probabilityThreshold <= accumulatedValues)
//          return (System.Text.Rune)(char)('A' + index);
//      }

//      return (System.Text.Rune)'A';
//    }

//    /// <summary>Returns the next probable consonant based on <paramref name="source"/>.</summary>
//    public static System.Text.Rune NextProbabilityRuneEnUsConsonant(this System.Random source, System.Globalization.CultureInfo? culture = null)
//    {
//      source ??= System.Random.Shared;
//      culture ??= System.Globalization.CultureInfo.CurrentCulture;

//      var rune = NextProbabilityRuneEnUs(source);
//      while (!culture.IsConsonantOf(rune))
//        rune = NextProbabilityRuneEnUs(source);
//      return rune;
//    }

//    /// <summary>Returns the next probable vowel based on <paramref name="source"/> and whether Y will be considered.</summary>
//    public static System.Text.Rune NextProbabilityRuneEnUsVowel(this System.Random source, System.Globalization.CultureInfo? culture = null)
//    {
//      source ??= System.Random.Shared;
//      culture ??= System.Globalization.CultureInfo.CurrentCulture;

//      var rune = NextProbabilityRuneEnUs(source);
//      while (!culture.IsVowelOf(rune))
//        rune = NextProbabilityRuneEnUs(source);
//      return rune;
//    }

//  }
//}
