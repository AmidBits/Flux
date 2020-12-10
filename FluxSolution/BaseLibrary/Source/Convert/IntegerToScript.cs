using System.Linq;

namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Returns a char with the numeric subscript.</summary>
    public static char SingleDigitToSubscript(int number)
      => number >= 0 && number <= 9 ? Text.Sequences.Subscript0Through9[number][0] : throw new System.ArgumentOutOfRangeException(nameof(number));
    /// <summary>Returns a char with the numeric subscript.</summary>
    public static string SingleDigitToSubscript(string text)
      => string.Concat(text.Select(c => char.IsDigit(c) ? Text.Sequences.Subscript0Through9[c - '0'][0] : c));

    /// <summary>Returns a char with the numeric superscript.</summary>
    public static char SingleDigitToSuperscript(int number)
      => number >= 0 && number <= 9 ? Text.Sequences.Superscript0Through9[number][0] : throw new System.ArgumentOutOfRangeException(nameof(number));
    /// <summary>Returns a char with the numeric superscript.</summary>
    public static string SingleDigitsToSuperscript(string text)
      => string.Concat(text.Select(c => char.IsDigit(c) ? Text.Sequences.Superscript0Through9[c - '0'][0] : c));
  }
}
