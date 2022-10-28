#if NET7_0_OR_GREATER
namespace Flux.NumberSequences
{
  public sealed class CompositeNumber
    : INumericSequence<System.Numerics.BigInteger>
  {
    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static bool IsComposite(System.Numerics.BigInteger value)
    {
      if (value <= long.MaxValue)
        return IsComposite((long)value);

      if (value <= 3)
        return false;

      if (value % 2 == 0 || value % 3 == 0)
        return true;

      var limit = Maths.ISqrt(value);

      for (var k = 5; k <= limit; k += 6)
        if ((value % k) == 0 || (value % (k + 2)) == 0)
          return true;

      return false;
    }

    [System.Diagnostics.Contracts.Pure]
    public static bool IsComposite(int value)
    {
      if (value <= 3)
        return false;

      if (value % 2 == 0 || value % 3 == 0)
        return true;

      var limit = System.Math.Sqrt(value);

      for (var k = 5; k <= limit; k += 6)
        if ((value % k) == 0 || (value % (k + 2)) == 0)
          return true;

      return false;
    }
    [System.Diagnostics.Contracts.Pure]
    public static bool IsComposite(long value)
    {
      if (value <= int.MaxValue)
        return IsComposite((int)value);

      if (value <= 3)
        return false;

      if (value % 2 == 0 || value % 3 == 0)
        return true;

      var limit = System.Math.Sqrt(value);

      for (var k = 5; k <= limit; k += 6)
        if ((value % k) == 0 || (value % (k + 2)) == 0)
          return true;

      return false;
    }

    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetCompositeNumbers()
    {
      for (var k = System.Numerics.BigInteger.One; ; k++)
        if (IsComposite(k))
          yield return k;
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Highly_composite_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>> GetHighlyCompositeNumbers()
    {
      var largestCountOfDivisors = System.Numerics.BigInteger.Zero;
      for (var index = System.Numerics.BigInteger.One; ; index++)
        if (Factors.GetCountOfDivisors(index) is var countOfDivisors && countOfDivisors > largestCountOfDivisors)
        {
          yield return new System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>(index, countOfDivisors);
          largestCountOfDivisors = countOfDivisors;
        }
    }
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => GetCompositeNumbers();

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
#endif
