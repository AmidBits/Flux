#if NET7_0_OR_GREATER
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

        if (lasts.ContainsKey(last))
        {
          next = index - lasts[last];
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
#endif

//#if NET7_0_OR_GREATER
//namespace Flux.NumberSequences
//{
//  /// <summary>Creates a Van Eck's sequence, starting with the specified number (where 0 yields the original sequence).</summary>
//  /// <see href="https://wiki.formulae.org/Van_Eck_sequence"/>
//  /// <seealso cref="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/>
//  /// <remarks>This function runs indefinitely, if allowed.</remarks>
//  public record class VanEckSequence
//    : INumericSequence<System.Numerics.BigInteger>
//  {
//    public System.Numerics.BigInteger StartWith { get; set; }

//    public VanEckSequence(System.Numerics.BigInteger startsWith)
//      => StartWith = startsWith;

//    #region Static methods

//    /// <summary>Creates a Van Eck's sequence, starting with the specified number (where 0 yields the original sequence).</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/>
//    /// <remarks>This function runs indefinitely, if allowed.</remarks>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetVanEckSequence<TSelf>(TSelf startWith)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      if (startWith < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(startWith));

//      var lasts = new System.Collections.Generic.Dictionary<TSelf, TSelf>();
//      var last = startWith;

//      for (var index = TSelf.Zero; ; index++)
//      {
//        yield return last;

//        TSelf next;

//        if (lasts.ContainsKey(last))
//        {
//          next = index - lasts[last];
//          lasts[last] = index;
//        }
//        else // The last was new.
//        {
//          next = TSelf.Zero;
//          lasts.Add(last, index);
//        }

//        last = next;
//      }
//    }

//    #endregion Static methods

//    #region Implemented interfaces
//    // INumberSequence

//    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
//      => GetVanEckSequence(StartWith);


//    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
//      => GetSequence().GetEnumerator();

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => GetEnumerator();
//    #endregion Implemented interfaces
//  }
//}
//#endif
