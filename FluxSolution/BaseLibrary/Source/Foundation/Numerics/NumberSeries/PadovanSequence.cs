namespace Flux.Numerics
{
  public sealed class PadovanSequence
  : ANumberSequenceable<System.Numerics.BigInteger>
  {
    // INumberSequence
    /// <summary>Creates a new sequence with padovan numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Padovan_sequence"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetPadovanSequence();

    #region Static methods
    /// <summary>Creates a new sequence with padovan numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Padovan_sequence"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPadovanSequence()
    {
      int p1 = 1, p2 = 1, p3 = 1;

      yield return p1;
      yield return p2;
      yield return p3;

      while (true)
      {
        var pn = p2 + p3;

        yield return pn;

        p3 = p2;
        p2 = p1;
        p1 = pn;
      }
    }
    #endregion Static methods
  }
}
