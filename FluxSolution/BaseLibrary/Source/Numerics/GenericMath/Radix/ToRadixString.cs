#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Creates a string with the number converted using the specified radix (base).</summary>
    public static System.ReadOnlySpan<char> ToRadixString<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixString(number, Flux.Text.RuneSequences.Base62[..AssertRadix(int.CreateChecked(radix))]).ToString();

    /// <summary>PREVIEW! Converts the number to text using the specified symbols. The count of symbols represents the radix of conversion.</summary>
    public static System.Text.StringBuilder ToRadixString<TSelf>(this TSelf number, System.ReadOnlySpan<System.Text.Rune> symbols)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (!IsRadix(symbols.Length)) throw new System.ArgumentOutOfRangeException(nameof(symbols), "The count of symbols represents the radix for conversion, and radix must be 2 or greater.");

      var sb = new System.Text.StringBuilder();

      if (TSelf.IsZero(number))
        sb.Append('0');
      else if (number < TSelf.Zero) // Needs a REAL solution for negative numbers.
      {
        sb = ToRadixString(TSelf.Abs(number), symbols);
        sb.Insert(0, '-');
      }
      else
        while (number > TSelf.Zero)
        {
          number = DivRem(number, TSelf.CreateChecked(symbols.Length), out var remainder);

          sb.Insert(0, symbols[int.CreateChecked(TSelf.Abs(remainder))]);
        }

      return sb;
    }
  }
}
#endif
