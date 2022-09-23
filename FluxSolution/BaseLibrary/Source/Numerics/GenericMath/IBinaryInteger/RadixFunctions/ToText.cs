#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>PREVIEW! Determines if the number is a power of the specified radix. The sign is ignored so the function can be used on negative numbers as well.</summary>
    public static System.Text.StringBuilder ToText<TSelf>(this TSelf number, System.ReadOnlySpan<System.Text.Rune> symbols)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var sb = new System.Text.StringBuilder(128);

      if (number == TSelf.Zero)
      {
        sb.Append('0');
      }
      else if (number < TSelf.Zero) // Needs a REAL solution for negative numbers.
      {
        for (var bit = number.GetShortestBitLength() - 1; bit >= 0; bit--)
          sb.Append(((TSelf.One << bit) & number) != TSelf.Zero ? '1' : '0');
      }

      while (number > TSelf.Zero)
      {
        number = Number.DivRem(number, TSelf.CreateChecked(symbols.Length), out var remainder);

        sb.Insert(0, symbols[TSelf.Abs(remainder).ConvertTo<TSelf, int>(out var abs)]);
      }

      return sb;
    }

    public static System.Text.StringBuilder ToTextBin<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToText(number, Flux.Text.RuneSequences.Base62[..2]);
    public static System.Text.StringBuilder ToTextOct<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToText(number, Flux.Text.RuneSequences.Base62[..8]);
    public static System.Text.StringBuilder ToTextDec<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToText(number, Flux.Text.RuneSequences.Base62[..10]);
    public static System.Text.StringBuilder ToTextHex<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToText(number, Flux.Text.RuneSequences.Base62[..16]);
  }
}
#endif
