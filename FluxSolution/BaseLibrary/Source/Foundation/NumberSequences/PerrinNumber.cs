using System.Linq;

namespace Flux.Numerics
{
  public class PerrinNumber
    : ASequencedNumbers<System.Numerics.BigInteger>
  {
    // INumberSequence
    /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetPerrinNumbers();

    #region Statics

    /// <summary>Yields a Perrin number of the specified value number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    public static System.Numerics.BigInteger GetPerrinNumber(System.Numerics.BigInteger index)
      => GetPerrinNumbers().Where((e, i) => i == index).First();

    /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPerrinNumbers()
    {
      System.Numerics.BigInteger a = 3, b = 0, c = 2;

      yield return a;
      yield return b;
      yield return c;

      while (true)
      {
        var p = a + b;

        a = b;
        b = c;
        c = p;

        yield return p;
      }
    }

    #endregion Statics
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
