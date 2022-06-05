namespace Flux.Numerics
{
  /// <summary>Creates a new sequence with Fibonacci numbers.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Fibonacci_number"/>
  /// <remarks>This function runs indefinitely, if allowed.</remarks>
  public record class FibonacciNumber
    : INumberSequenceable<System.Numerics.BigInteger>
  {
    #region Statics
    //  /// <summary>Creates a new sequence with Fibonacci numbers.</summary>
    //  /// <see cref="https://en.wikipedia.org/wiki/Fibonacci_number"/>
    //  /// <remarks>This function runs indefinitely, if allowed.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetFibonacciSequence()
    {
      var n1 = System.Numerics.BigInteger.Zero;
      var n2 = System.Numerics.BigInteger.One;

      while (true)
      {
        yield return n1;
        n1 += n2;

        yield return n2;
        n2 += n1;
      }
    }

    /// <summary>Determines whether the number is a Fibonacci number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fibonacci_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsFibonacciNumber(System.Numerics.BigInteger number)
    {
      var fiver = 5 * number * number;
      var fp4 = fiver + 4;
      var fp4sr = Maths.ISqrt(fp4);
      var fm4 = fiver - 4;
      var fm4sr = Maths.ISqrt(fm4);

      return fp4sr * fp4sr == fp4 || fm4sr * fm4sr == fm4;
    }
    #endregion Statics

    #region Implemented interfaces
    // INumberSequence
    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetFibonacciSequence();

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
