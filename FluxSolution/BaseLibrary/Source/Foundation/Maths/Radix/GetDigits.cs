using System;
using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Span<System.Numerics.BigInteger> GetDigits(this System.Numerics.BigInteger value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }

    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Span<int> GetDigits(this int value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }
    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Span<long> GetDigits(this long value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }
  }
}
