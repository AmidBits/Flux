namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the individual digits (as numbers) of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigits<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var reversed = GetDigitsReversed(number, radix);
      reversed.Reverse();
      return reversed;
    }

    /// <summary>Returns the place value digits (as numbers) of <paramref name="number"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigitsReversed<TSelf>(this TSelf number, TSelf radix, int count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      if (TSelf.IsNegative(number))
        number = -number;

      var list = new System.Collections.Generic.List<TSelf>();

      if (TSelf.IsZero(number))
        list.Add(TSelf.Zero);
      else
        while (!TSelf.IsZero(number) && list.Count < count)
        {
          list.Add(number % radix);
          number /= radix;
        }

      return list;
    }

    /// <summary>Returns the place value digits (as numbers) of <paramref name="number"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigitsReversed<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetDigitsReversed(number, radix, int.MaxValue);

#else

    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Collections.Generic.List<System.Numerics.BigInteger> GetDigits(this System.Numerics.BigInteger value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }

    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Collections.Generic.List<int> GetDigits(this int value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }

    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Collections.Generic.List<long> GetDigits(this long value, int radix)
    {
      var span = GetDigitsReversed(value, radix);
      span.Reverse();
      return span;
    }

    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Collections.Generic.List<System.Numerics.BigInteger> GetDigitsReversed(this System.Numerics.BigInteger value, int radix)
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      while (value != 0)
      {
        list.Add(value % radix);
        value /= radix;
      }

      return list;
    }

    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Collections.Generic.List<int> GetDigitsReversed(this int value, int radix)
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<int>();

      while (value != 0)
      {
        list.Add(value % radix);
        value /= radix;
      }

      return list;
    }

    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Collections.Generic.List<long> GetDigitsReversed(this long value, int radix)
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<long>();

      while (value != 0)
      {
        list.Add(value % radix);
        value /= radix;
      }

      return list;
    }

#endif
  }
}
