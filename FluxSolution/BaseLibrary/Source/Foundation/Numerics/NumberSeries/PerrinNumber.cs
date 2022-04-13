namespace Flux.Numerics
{
  public sealed class PerrinNumber
    : ANumberSequenceable<System.Numerics.BigInteger>
  {
    // INumberSequence
    /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetPerrinNumbers();

    #region Static methods
    /// <summary>Yields a Perrin number of the specified value number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    public static System.Numerics.BigInteger GetPerrinNumber(System.Numerics.BigInteger index)
      => System.Linq.Enumerable.First(System.Linq.Enumerable.Where(GetPerrinNumbers(), (e, i) => i == index));
    /// <summary>Creates an indefinite sequence of Perrin numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perrin_number"/>
    [System.Diagnostics.Contracts.Pure]
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
    #endregion Static methods
  }
}
