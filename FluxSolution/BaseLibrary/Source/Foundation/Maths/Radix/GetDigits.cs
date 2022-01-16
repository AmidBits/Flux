using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Span<System.Numerics.BigInteger> GetDigits(System.Numerics.BigInteger value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }

    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Span<int> GetDigits(int value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }
    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Span<long> GetDigits(long value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }

    /// <summary>Returns the digits (as numbers) of a value.</summary>
    [System.CLSCompliant(false)]
    public static System.Span<uint> GetDigits(uint value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }
    /// <summary>Returns the digits (as numbers) of a value.</summary>
    [System.CLSCompliant(false)]
    public static System.Span<ulong> GetDigits(ulong value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }
  }
}
