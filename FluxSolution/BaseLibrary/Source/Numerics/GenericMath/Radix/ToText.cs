#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Converts the number to text using the specified symbols. The count of symbols represents the radix of conversion.</summary>
    public static System.Text.StringBuilder ToText<TSelf>(this TSelf number, System.ReadOnlySpan<System.Text.Rune> symbols)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (!GenericMath.IsRadix(symbols.Length)) throw new System.ArgumentOutOfRangeException(nameof(symbols), "The count of symbols represents the radix for conversion, and radix must be 2 or greater.");

      var sb = new System.Text.StringBuilder();

      if (TSelf.IsZero(number))
        sb.Append('0');
      else if (number < TSelf.Zero) // Needs a REAL solution for negative numbers.
      {
        sb = ToText(TSelf.Abs(number), symbols);
        sb.Append('-');
      }
      //for (var bit = number.GetShortestBitLength() - 1; bit >= 0; bit--)
      //  sb.Append(((TSelf.One << bit) & number) != TSelf.Zero ? (System.Text.Rune)'1' : (System.Text.Rune)'0');
      else
        while (number > TSelf.Zero)
        {
          number = GenericMath.DivRem(number, TSelf.CreateChecked(symbols.Length), out var remainder);

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
