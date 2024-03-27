namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Creates a Van Eck's sequence, starting with the specified number (where 0 yields the original sequence).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<TSelf> GetVanEckSequence<TSelf>(TSelf startWith)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (startWith < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(startWith));

      var lasts = new System.Collections.Generic.Dictionary<TSelf, TSelf>();
      var last = startWith;

      for (var index = TSelf.Zero; ; index++)
      {
        yield return last;

        TSelf next;

        if (lasts.TryAdd(last, index))
        {
          next = TSelf.Zero;
        }
        else
        {
          next = index - lasts[last];
          lasts[last] = index;
        }

        if (lasts.TryGetValue(last, out var value))
        {
          next = index - value;
          lasts[last] = index;
        }
        else // The last was new.
        {
          next = TSelf.Zero;
          lasts.Add(last, index);
        }

        last = next;
      }
    }
  }
}
