
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Creates a new sequence with Fibonacci numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fibonacci_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
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
    public static bool IsFibonacciNumber(System.Numerics.BigInteger number)
    {
      var fiver = 5 * number * number;
      var fourp = fiver + 4;
      var fourps = fourp.Sqrt();
      var fourn = fiver - 4;
      var fourns = fourn.Sqrt();

      return fourps * fourps == fourp || fourns * fourns == fourn;
    }
  }
}
