#if NET7_0_OR_GREATER

namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Creates a sequence of powers-of-radix values.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPerfectNthRootSequence<TSelf>(int nth)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var root = System.Numerics.BigInteger.Zero; ; root++)
        yield return TSelf.CreateChecked(System.Numerics.BigInteger.Pow(root, nth));
    }
  }
}

//namespace Flux.NumberSequences
//{
//  /// <summary>Creates a new sequence with perfect-square (or simply just square number) values.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Square_number"/>
//  public record class PerfectNthRoot
//    : INumericSequence<System.Numerics.BigInteger>
//  {
//    public int Nth { get; set; }

//    public PerfectNthRoot(int nth) => Nth = nth;

//    #region Static methods

//    /// <summary>Creates a sequence of powers-of-radix values.</summary>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetPerfectNthRootSequence<TSelf>(int nth)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      for (var root = System.Numerics.BigInteger.Zero; ; root++)
//        yield return TSelf.CreateChecked(System.Numerics.BigInteger.Pow(root, nth));
//    }

//    #endregion Static methods

//    #region Implemented interfaces

//    // INumberSequence
//    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence() => GetPerfectNthRootSequence<System.Numerics.BigInteger>(Nth);

//    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

//    #endregion Implemented interfaces
//  }
//}
#endif
