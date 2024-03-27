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
