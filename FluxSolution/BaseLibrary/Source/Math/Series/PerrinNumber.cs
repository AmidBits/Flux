using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Yields a Perrin number of the specified value number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    public static System.Numerics.BigInteger GetPerrinNumber(System.Numerics.BigInteger value)
    {
      var a = (System.Numerics.BigInteger)3;
      var b = System.Numerics.BigInteger.Zero;
      var c = (System.Numerics.BigInteger)2;

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
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPerrinSequence()
    {
      for (var number = 0; number <= int.MaxValue; number++)
      {
        yield return GetPerrinNumber(number);
      }
    }
  }
}
