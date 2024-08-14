namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Creates a sequence of powers-of-radix values.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPowersOfRadixSequence<TSelf>(TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var por = TSelf.One; ; por *= radix)
        yield return por;
    }
  }
}
