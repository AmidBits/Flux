using System.Linq;

namespace Flux
{
  public static partial class Convert
  {
    public const string SubscriptsDigits = "\u2080\u0081\u0082\u0083\u2084\u2085\u2086\u2087\u2088\u2089"; // "0123456789"

    /// <summary>Returns a char with the numeric subscript.</summary>
    public static char SingleDigitToSubscript(int number)
      => number >= 0 && number <= 9 ? SubscriptsDigits[number] : throw new System.ArgumentOutOfRangeException(nameof(number));
    /// <summary>Returns a char with the numeric subscript.</summary>
    public static string SingleDigitToSubscript(string text)
      => string.Concat(text.Select(c => char.IsDigit(c) ? SubscriptsDigits[c - '0'] : c));

    public const string SuperscriptsDigits = "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079"; // "0123456789"

    /// <summary>Returns a char with the numeric superscript.</summary>
    public static char SingleDigitToSuperscript(int number)
      => number >= 0 && number <= 9 ? SuperscriptsDigits[number] : throw new System.ArgumentOutOfRangeException(nameof(number));
    /// <summary>Returns a char with the numeric superscript.</summary>
    public static string SingleDigitsToSuperscript(string text)
      => string.Concat(text.Select(c => char.IsDigit(c) ? SuperscriptsDigits[c - '0'] : c));
  }
}
