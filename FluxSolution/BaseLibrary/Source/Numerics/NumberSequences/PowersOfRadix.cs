#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Creates a sequence of powers-of-radix values.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPowersOfRadixSequence<TSelf>(TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var por = TSelf.One; ; por *= radix)
        yield return por;
    }
  }
}
#endif

//#if NET7_0_OR_GREATER
//namespace Flux.NumberSequences
//{
//  /// <summary>Creates a new sequence with powers-of-radix values.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/PowersOfRadix"/>
//  public record class PowersOfRadix
//    : INumericSequence<System.Numerics.BigInteger>
//  {
//    public int Radix { get; set; }

//    public PowersOfRadix(int radix) => Radix = radix;

//    #region Static methods

//    /// <summary>Creates a sequence of powers-of-radix values.</summary>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetPowersOfRadixSequence<TSelf>(TSelf radix)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      for (var por = TSelf.One; ; por *= radix)
//        yield return por;
//    }

//    #endregion Static methods

//    #region Implemented interfaces

//    // INumberSequence
//    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence() => GetPowersOfRadixSequence(Radix.ToBigInteger());

//    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

//    #endregion Implemented interfaces
//  }
//}
//#endif
