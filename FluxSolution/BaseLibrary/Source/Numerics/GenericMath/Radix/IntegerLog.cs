namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Computes the floor integer log of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Logarithm"/></para>
    /// </summary>
    /// <remarks>The ceiling integer log: (<paramref name="value"/> >= 1 ? IntegerLog(<paramref name="value"/> - 1, <paramref name="radix"/>) + 1 : 0).</remarks>
    public static TSelf IntegerLog<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      var ilogr = TSelf.Zero;

      if (!TSelf.IsZero(value))
        while (value >= radix)
        {
          value /= radix;

          ilogr++;
        }

      return ilogr;
    }

    /// <summary>
    /// <para>Computes the integer log <paramref name="ilogTowardZero"/> and <paramref name="ilogAwayFromZero"/> of <paramref name="value"/> in <paramref name="radix"/>. Optionally <paramref name="proper"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Logarithm"/></para>
    /// </summary>
    /// <param name="value">The value for which <paramref name="ilogTowardZero"/> and <paramref name="ilogAwayFromZero"/> will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="ilogTowardZero"></param>
    /// <param name="ilogAwayFromZero"></param>
    public static void IntegerLog<TSelf>(this TSelf value, TSelf radix, out TSelf ilogTowardZero, out TSelf ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      ilogAwayFromZero = ilogTowardZero = IntegerLog(value, radix);

      if (!IsPowOf(value, radix))
        ilogAwayFromZero++;
    }

#else

    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void IntegerLog(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, out System.Numerics.BigInteger ilogTowardZero, out System.Numerics.BigInteger ilogAwayFromZero)
    {
      ilogAwayFromZero = ilogTowardZero = IntegerLog(value, radix);

      if (!IsPowOf(value, radix))
        ilogAwayFromZero++;
    }

    /// <summary>Computes the integer log floor of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static System.Numerics.BigInteger IntegerLog(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix)
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      var ilogTowardsZero = System.Numerics.BigInteger.Zero;

      if (!value.IsZero)
        while (value >= radix)
        {
          value /= radix;

          ilogTowardsZero++;
        }

      return ilogTowardsZero;
    }


    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void IntegerLog(this int value, int radix, out int ilogTowardZero, out int ilogAwayFromZero)
    {
      ilogAwayFromZero = ilogTowardZero = IntegerLog(value, radix);

      if (!IsPowOf(value, radix))
        ilogAwayFromZero++;
    }

    /// <summary>Computes the integer log floor of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static int IntegerLog(this int value, int radix)
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      var ilogTowardsZero = 0;

      if (value != 0)
        while (value >= radix)
        {
          value /= radix;

          ilogTowardsZero++;
        }

      return ilogTowardsZero;
    }

#endif
  }
}
