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
