namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Creates a Van Eck's sequence, starting with the specified number (where 0 yields the original sequence).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
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

    /// <summary>Creates a Van Eck's sequence, starting with the specified number (where 0 yields the original sequence).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/>
    public static System.Collections.Generic.IEnumerable<int> GetVanEckSequence(int startWith, int count)
    {
      if (startWith < 0) throw new System.ArgumentOutOfRangeException(nameof(startWith));
      else if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var lasts = new System.Collections.Generic.Dictionary<int, int>();
      var last = startWith;

      for (var index = 0; index < count; index++)
      {
        yield return last;

        int next;

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
    /// <summary>Creates a Van Eck's sequence, starting with the specified number (where 0 yields the original sequence).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/>
    public static System.Collections.Generic.IEnumerable<long> GetVanEckSequence(long startWith, long count)
    {
      if (startWith < 0) throw new System.ArgumentOutOfRangeException(nameof(startWith));
      else if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var lasts = new System.Collections.Generic.Dictionary<long, long>();
      var last = startWith;

      for (var index = 0L; index <= count; index++)
      {
        yield return last;

        long next;

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
  }
}
