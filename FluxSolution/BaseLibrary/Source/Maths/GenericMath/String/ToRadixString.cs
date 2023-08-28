using System.Net.Http.Headers;

namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates a base <paramref name="radix"/> text string from <paramref name="value"/>, with an optional <paramref name="minLength"/> of digits in the resulting string (padded with zeroes if needed).</summary>
    /// <remarks>By default, this function returns the shortest possible string length.</remarks>
    public static string ToRadixString<TSelf>(this TSelf value, int radix, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var sb = new System.Text.StringBuilder();

      if (!TSelf.IsZero(value) && value is var number)
        if (radix == 2)
        {
          for (var bitIndex = number.GetBitCount() - 1; bitIndex >= 0; bitIndex--)
            if ((int.CreateChecked((number >> bitIndex) & TSelf.One) is var position && position > 0) || sb.Length > 0)
              sb.Append(Text.PositionalNotation.Base64[position]);
        }
        else
        {
          while (number != TSelf.Zero)
          {
            (number, var remainder) = TSelf.DivRem(number, TSelf.CreateChecked(radix));

            sb.Insert(0, Flux.Text.PositionalNotation.Base64[int.CreateChecked(TSelf.Abs(remainder))]);
          }
        }

      if (sb.Length < minLength)
        sb.Insert(0, "0", minLength - sb.Length);

      if (radix == 10 && TSelf.IsNegative(value))
        sb.Insert(0, (char)Flux.UnicodeCodepoint.HyphenMinus);

      return sb.ToString();
    }

#else

    /// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString(this System.Numerics.BigInteger number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();
    /// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString(this int number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();
    /// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString(this long number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();

#endif
  }
}
