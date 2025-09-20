namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Creates a sequence of powers-of-radix values.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="nth"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<(TSelf Root, TSelf Number)> GetSequenceOfPerfectRootN<TSelf>(int nth)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IMinMaxValue<TSelf>
    {
      System.ArgumentOutOfRangeException.ThrowIfLessThan(nth, 2);

      var nthRoot = System.Numerics.BigInteger.CreateChecked(TSelf.MaxValue).IntegerRootN(nth).IRootTz;

      for (var root = System.Numerics.BigInteger.Zero; root <= nthRoot; root++)
        yield return (TSelf.CreateChecked(root), TSelf.CreateChecked(System.Numerics.BigInteger.Pow(root, nth)));
    }
  }
}
