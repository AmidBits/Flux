namespace Flux
{
  public static partial class Convert
  {
    public static string[] SingleDigitSubscripts => new string[] { "\u2080", "\u0081", "\u0082", "\u0083", "\u2084", "\u2085", "\u2086", "\u2087", "\u2088", "\u2089" };

    /// <summary>Returns a char with the numeric subscript.</summary>
    public static string SingleDigitToSubscript(int offset) => offset >= 0 && offset <= 9 ? SingleDigitSubscripts[offset] : throw new System.ArgumentOutOfRangeException(nameof(offset));

    public static string[] SingleDigitSuperscripts => new string[] { "\u2070", "\u00B9", "\u00B2", "\u00B3", "\u2074", "\u2075", "\u2076", "\u2077", "\u2078", "\u2079" };

    /// <summary>Returns a char with the numeric superscript.</summary>
    public static string SingleDigitToSuperscript(int offset) => offset >= 0 && offset <= 9 ? SingleDigitSuperscripts[offset] : throw new System.ArgumentOutOfRangeException(nameof(offset));
  }
}
