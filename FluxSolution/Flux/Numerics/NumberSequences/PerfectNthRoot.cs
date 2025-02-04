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
    public static System.Collections.Generic.IEnumerable<TSelf> GetPerfectNthRootSequence<TSelf>(int nth)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var root = System.Numerics.BigInteger.Zero; ; root++)
        yield return TSelf.CreateChecked(System.Numerics.BigInteger.Pow(root, nth));
    }
  }
}
