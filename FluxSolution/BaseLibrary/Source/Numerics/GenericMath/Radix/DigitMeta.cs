namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns all single digits in <paramref name="number"/>, as well as the count and sum of them, using base <paramref name="radix"/>.</summary>
    public static void DigitMeta<TSelf>(this TSelf number, TSelf radix, out TSelf count, out System.Collections.Generic.List<TSelf> digits, out TSelf sum)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      count = TSelf.Zero;
      digits = new System.Collections.Generic.List<TSelf>();
      sum = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        count++;
        var digit = number % radix;
        digits.Add(digit);
        sum += digit;

        number /= radix;
      }
    }

#else

    /// <summary>Returns the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static void DigitMeta(this System.Numerics.BigInteger number, System.Numerics.BigInteger radix, out System.Numerics.BigInteger count, out System.Collections.Generic.List<System.Numerics.BigInteger> digits, out System.Numerics.BigInteger sum)
    {
      AssertRadix(radix);

      count = System.Numerics.BigInteger.Zero;
      digits = new System.Collections.Generic.List<System.Numerics.BigInteger>();
      sum = System.Numerics.BigInteger.Zero;

      while (!number.IsZero)
      {
        count++;
        var digit = number % radix;
        digits.Add(digit);
        sum += digit;

        number /= radix;
      }
    }

#endif
  }
}
