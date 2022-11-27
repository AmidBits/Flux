namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Converts the number to text using the specified symbols. The count of symbols represents the radix of conversion.</summary>
    public static System.Text.StringBuilder ToRadixString<TSelf>(this TSelf number, System.ReadOnlySpan<System.Text.Rune> symbols)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (!IsRadix(symbols.Length)) throw new System.ArgumentOutOfRangeException(nameof(symbols), "The count of symbols represents the radix for conversion, and radix must be 2 or greater.");

      var sb = new System.Text.StringBuilder();

      if (TSelf.IsZero(number))
        sb.Append(symbols[0]);
      else if (number < TSelf.Zero) // Needs a REAL solution for negative numbers.
      {
        sb = ToRadixString(TSelf.Abs(number), symbols);
        sb.Insert(0, '-');
      }
      else
        while (number > TSelf.Zero)
        {
          number = DivMod(number, TSelf.CreateChecked(symbols.Length), out var remainder);

          sb.Insert(0, symbols[int.CreateChecked(TSelf.Abs(remainder))]);
        }

      return sb;
    }

    public static string ToRadixString<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => ToRadixString(number, Flux.Text.RuneSequences.Base62[..AssertRadix(radix, out int _)]).ToString();

    /// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString<TSelf, TRadix>(this TSelf number, TRadix radix, int minimumLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var sb = ToRadixString(number, Flux.Text.RuneSequences.Base62[..AssertRadix(radix, out int _)]);

      var negative = sb[0] == '-' ? 1 : 0;

      if (minimumLength > (sb.Length - negative))
        sb.Insert(negative, "0", minimumLength - (sb.Length - negative));

      return sb.ToString();
    }
  }
}
