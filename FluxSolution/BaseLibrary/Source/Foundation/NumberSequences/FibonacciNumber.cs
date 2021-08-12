namespace Flux.Numerics
{
  public class FibonacciNumber
    : ASequencedNumbers<System.Numerics.BigInteger>
  {
    // INumberSequence
    /// <summary>Creates a new sequence with Fibonacci numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fibonacci_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
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

    #region Statics

    /// <summary>Determines whether the number is a Fibonacci number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fibonacci_number"/>
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
  }

  //public static partial class Maths
  //{
  //  /// <summary>Creates a new sequence with Fibonacci numbers.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/Fibonacci_number"/>
  //  /// <remarks>This function runs indefinitely, if allowed.</remarks>
  //  public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetFibonacciSequence()
  //  {
  //    var n1 = System.Numerics.BigInteger.Zero;
  //    var n2 = System.Numerics.BigInteger.One;

  //    while (true)
  //    {
  //      yield return n1;
  //      n1 += n2;

  //      yield return n2;
  //      n2 += n1;
  //    }
  //  }

  //  /// <summary>Determines whether the number is a Fibonacci number.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/Fibonacci_number"/>
  //  public static bool IsFibonacciNumber(System.Numerics.BigInteger number)
  //  {
  //    var fiver = 5 * number * number;
  //    var fp4 = fiver + 4;
  //    var fp4sr = ISqrt(fp4);
  //    var fm4 = fiver - 4;
  //    var fm4sr = ISqrt(fm4);

  //    return fp4sr * fp4sr == fp4 || fm4sr * fm4sr == fm4;
  //  }
  //}
}
