using System;

namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    public static System.Numerics.BigInteger FromRadixString(this string text, int radix)
      => radix switch
      {
        2 => Text.PositionalNotation.TextToNumber(text, Text.PositionalNotation.Base2),
        8 => Text.PositionalNotation.TextToNumber(text, Text.PositionalNotation.Base8),
        10 => Text.PositionalNotation.TextToNumber(text, Text.PositionalNotation.Base10),
        16 => Text.PositionalNotation.TextToNumber(text, Text.PositionalNotation.Base16),
        var r when r >= 2 && r <= 62 => Text.PositionalNotation.TextToNumber(text, Text.PositionalNotation.Base62.ToArray(0, r)),
        _ => throw new ArgumentOutOfRangeException(nameof(radix))
      };
    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    public static string ToRadixString(this System.Numerics.BigInteger number, int radix)
      => radix switch
      {
        2 => Text.PositionalNotation.NumberToText(number, Text.PositionalNotation.Base2),
        8 => Text.PositionalNotation.NumberToText(number, Text.PositionalNotation.Base8),
        10 => Text.PositionalNotation.NumberToText(number, Text.PositionalNotation.Base10),
        16 => Text.PositionalNotation.NumberToText(number, Text.PositionalNotation.Base16),
        var r when r >= 2 && r <= 62 => Text.PositionalNotation.NumberToText(number, Text.PositionalNotation.Base62.ToArray(0, r)),
        _ => throw new ArgumentOutOfRangeException(nameof(radix))
      };

    ///// <summary>Convert from decimal unicode subscript.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Unicode_subscripts_and_superscripts"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    //public static System.Numerics.BigInteger FromUnicodeSubscriptString(this string textUnicodeSubscript) => PositionalNotation.TextToNumber(textUnicodeSubscript, PositionalNotation.Base10UnicodeSuperscript.ToArray());
    ///// <summary>Convert to decimal unicode subscript.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Unicode_subscripts_and_superscripts"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    //public static string ToUnicodeSubscriptString(this System.Numerics.BigInteger number) => PositionalNotation.NumberToText(number, PositionalNotation.Base10UnicodeSubscript.ToArray());

    ///// <summary>Convert from decimal unicode superscript.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Unicode_subscripts_and_superscripts"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    //public static System.Numerics.BigInteger FromUnicodeSuperscriptString(this string textUnicodeSuperscript) => PositionalNotation.TextToNumber(textUnicodeSuperscript, PositionalNotation.Base10UnicodeSuperscript.ToArray());
    ///// <summary>Convert to decimal unicode superscript.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Unicode_subscripts_and_superscripts"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    //public static string ToUnicodeSuperscriptString(this System.Numerics.BigInteger number) => PositionalNotation.NumberToText(number, PositionalNotation.Base10UnicodeSuperscript.ToArray());
  }
}
