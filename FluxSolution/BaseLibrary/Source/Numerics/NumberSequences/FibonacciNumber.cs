namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Creates a new sequence with Fibonacci numbers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Fibonacci_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<TSelf> GetFibonacciSequence<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var n1 = TSelf.Zero;
      var n2 = TSelf.One;

      while (true)
      {
        yield return n1;
        n1 += n2;

        yield return n2;
        n2 += n1;
      }
    }

    /// <summary>Determines whether the number is a Fibonacci number.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Fibonacci_number"/>
    public static bool IsFibonacciNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var four = TSelf.CreateChecked(4);

      var fivens = TSelf.CreateChecked(5) * number * number;
      var fp4 = fivens + four;
      var fp4sr = Maths.IntegerSqrt(fp4);
      var fm4 = fivens - four;
      var fm4sr = Maths.IntegerSqrt(fm4);

      return fp4sr * fp4sr == fp4 || fm4sr * fm4sr == fm4;
    }
  }
}
