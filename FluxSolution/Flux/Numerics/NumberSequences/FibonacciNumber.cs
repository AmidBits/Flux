namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Creates a new sequence of <typeparamref name="TSelf"/> with Fibonacci numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
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

    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> is a Fibonacci number.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsFibonacciNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var four = TSelf.CreateChecked(4);

      var fivens = TSelf.CreateChecked(5) * number * number;
      var fp4 = fivens + four;
      var fp4sr = fp4.IntegerSqrt();
      var fm4 = fivens - four;
      var fm4sr = fm4.IntegerSqrt();

      return fp4sr * fp4sr == fp4 || fm4sr * fm4sr == fm4;
    }
  }
}
