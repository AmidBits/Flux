namespace Flux
{
  public static partial class Reflection
  {
    public static bool IsVowelOf_en(System.Text.Rune rune)
      => System.Text.Rune.IsLetter(rune) && (rune.Value is 'a' or 'e' or 'i' or 'o' or 'u' or 'y' or 'A' or 'E' or 'I' or 'O' or 'U' or 'Y');
    public static bool IsVowelOf_se(System.Text.Rune rune)
      => IsVowelOf_en(rune) || System.Text.Rune.IsLetter(rune) && (rune.Value is '\u00E5' or '\u00E4' or '\u00F6' or '\u00C5' or '\u00C4' or '\u00D6');

    /// <summary></summary>
    /// <exception cref="System.NotImplementedException"/>
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, char character)
    {
      source ??= System.Globalization.CultureInfo.CurrentCulture;

      return source.TwoLetterISOLanguageName switch
      {
        "en" => IsVowelOf_en((System.Text.Rune)character),
        "se" => IsVowelOf_se((System.Text.Rune)character),
        _ => throw new System.NotImplementedException(nameof(source))
      };
    }

    /// <summary></summary>
    /// <exception cref="System.OverflowException"/>
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, System.Text.Rune rune)
    {
      source ??= System.Globalization.CultureInfo.CurrentCulture;

      return source.TwoLetterISOLanguageName switch
      {
        "en" => IsVowelOf_en(rune),
        "se" => IsVowelOf_se(rune),
        _ => throw new System.NotImplementedException(nameof(source))
      };
    }

    ///// <summary></summary>
    ///// <exception cref="System.OverflowException"/>
    //public static bool IsVowelOf(this System.Globalization.CultureInfo source, string textElement)
    //  => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
    //  {
    //    "en" => textElement is not null && textElement.Length == 1 && IsVowelOf_en(textElement[0]),
    //    "se" => textElement is not null && textElement.Length == 1 && IsVowelOf_se(textElement[0]),
    //    _ => throw new System.NotImplementedException(nameof(source))
    //  };
  }
}
