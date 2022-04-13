namespace Flux.Numerics
{
  public sealed class BellTriangleAugmented
    : ANumberSequenceable<System.Numerics.BigInteger[]>
  {
    // INumberSequence
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetNumberSequence()
      => GetBellTriangleAugmented();

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
  }
}
