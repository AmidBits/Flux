#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Converts the number to text using the specified symbols. The count of symbols represents the radix of conversion.</summary>
    public static System.Text.StringBuilder ToString<TSelf>(this TSelf number, System.ReadOnlySpan<System.Text.Rune> symbols)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (!GenericMath.IsRadix(symbols.Length)) throw new System.ArgumentOutOfRangeException(nameof(symbols), "The count of symbols represents the radix for conversion, and radix must be 2 or greater.");

      var sb = new System.Text.StringBuilder();

      if (TSelf.IsZero(number))
        sb.Append('0');
      else if (number < TSelf.Zero) // Needs a REAL solution for negative numbers.
      {
        sb = ToString(TSelf.Abs(number), symbols);
        sb.Append('-');
      }
      else
        while (number > TSelf.Zero)
        {
          number = GenericMath.DivRem(number, TSelf.CreateChecked(symbols.Length), out var remainder);

          sb.Insert(0, symbols[TSelf.Abs(remainder).ConvertTo<TSelf, int>(out var abs)]);
        }

      return sb;
    }
  }
}
#endif
