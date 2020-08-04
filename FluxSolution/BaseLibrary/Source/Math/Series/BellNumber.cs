using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Yields a Bell number of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bell_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetBellNumbers()
    {
      var current = new System.Numerics.BigInteger[1] { 1 };

      while (true)
      {
        yield return current[0];

        var previous = current;

        current = new System.Numerics.BigInteger[previous.Length + 1];
        current[0] = previous[previous.Length - 1];

        for (var i = 1; i <= previous.Length; i++)
        {
          current[i] = previous[i - 1] + current[i - 1];
        }
      }
    }

    /// <summary>Creates a new sequence with each element being an array (i.e. row) of Bell numbers in a Bell triangle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bell_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetBellTriangle()
    {
      var current = new System.Numerics.BigInteger[] { 1 };

      while (true)
      {
        yield return current;

        var previous = current;

        current = new System.Numerics.BigInteger[previous.Length + 1];
        current[0] = previous[previous.Length - 1];

        for (var i = 1; i <= previous.Length; i++)
        {
          current[i] = previous[i - 1] + current[i - 1];
        }
      }
    }

    /// <summary>Creates a new sequence with each element being an array (i.e. row) of Bell numbers in an augmented Bell triangle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bell_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetBellTriangleAugmented()
    {
      var current = new System.Numerics.BigInteger[] { 1 };

      while (true)
      {
        yield return current;

        var previous = current;

        current = new System.Numerics.BigInteger[previous.Length + 1];
        current[0] = (current[1] = previous[previous.Length - 1]) - previous[0];

        for (var i = 2; i <= previous.Length; i++)
        {
          current[i] = previous[i - 1] + current[i - 1];
        }
      }
    }
  }
}
