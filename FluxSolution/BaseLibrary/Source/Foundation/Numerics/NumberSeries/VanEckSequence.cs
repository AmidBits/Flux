namespace Flux.Numerics
{
  public sealed class VanEckSequence
    : ANumberSequenceable<System.Numerics.BigInteger>
  {
    public System.Numerics.BigInteger StartWith { get; set; }

    public VanEckSequence(System.Numerics.BigInteger startsWith)
      => StartWith = startsWith;

    // INumberSequence
    /// <summary>Creates a Van Eck's sequence, starting with the specified number (where 0 yields the original sequence).</summary>
    /// <see cref="https://wiki.formulae.org/Van_Eck_sequence"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetVanEckSequence(StartWith);

    #region Static methods
    /// <summary>Creates a Van Eck's sequence, starting with the specified number (where 0 yields the original sequence).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetVanEckSequence(System.Numerics.BigInteger startWith)
    {
      if (startWith < 0) throw new System.ArgumentOutOfRangeException(nameof(startWith));

      var lasts = new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>();
      var last = startWith;

      for (var index = System.Numerics.BigInteger.Zero; ; index++)
      {
        yield return last;

        System.Numerics.BigInteger next;

        if (lasts.ContainsKey(last))
        {
          next = index - lasts[last];
          lasts[last] = index;
        }
        else // The last was new.
        {
          next = 0;
          lasts.Add(last, index);
        }

        last = next;
      }
    }
    #endregion Static methods
  }
}
