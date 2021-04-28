namespace Flux.Numerics
{
  public class PerrinNumber
    : INumberSequence<System.Numerics.BigInteger>
  {
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    /// <summary>Yields a Perrin number of the specified value number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    public static System.Numerics.BigInteger GetNumber(System.Numerics.BigInteger value)
    {
      System.Numerics.BigInteger a = 3, b = 0, c = 2;

      if (value == 0) return a;
      if (value == 1) return b;
      if (value == 2) return c;

      var p = System.Numerics.BigInteger.Zero;

      while (value > 2)
      {
        p = a + b;

        a = b;
        b = c;
        c = p;

        value--;
      }

      return p;
    }

    /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
    {
      for (var number = System.Numerics.BigInteger.Zero; ; number++)
        GetNumber(number);
    }
  }

  //public static partial class Maths
  //{
  //  /// <summary>Yields a Perrin number of the specified value number.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
  //  public static System.Numerics.BigInteger GetPerrinNumber(System.Numerics.BigInteger value)
  //  {
  //    System.Numerics.BigInteger a = 3, b = 0, c = 2;

  //    if (value == 0) return a;
  //    if (value == 1) return b;
  //    if (value == 2) return c;

  //    var p = System.Numerics.BigInteger.Zero;

  //    while (value > 2)
  //    {
  //      p = a + b;

  //      a = b;
  //      b = c;
  //      c = p;

  //      value--;
  //    }

  //    return p;
  //  }

  //  /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
  //  /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
  //  [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
  //  public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPerrinSequence()
  //    => Flux.LinqX.Range(System.Numerics.BigInteger.Zero, long.MaxValue, 1).Select(GetPerrinNumber);
  //}
}
