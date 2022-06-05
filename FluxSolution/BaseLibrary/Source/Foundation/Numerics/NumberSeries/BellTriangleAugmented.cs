namespace Flux.Numerics
{
  public record class BellTriangleAugmented
    : INumberSequenceable<System.Numerics.BigInteger[]>
  {
    #region Static methods
    /// <summary>Creates a new sequence with each element being an array (i.e. row) of Bell numbers in an augmented Bell triangle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bell_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
    [System.Diagnostics.Contracts.Pure]
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
          current[i] = previous[i - 1] + current[i - 1];
      }
    }
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetNumberSequence()
      => GetBellTriangleAugmented();

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger[]> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
