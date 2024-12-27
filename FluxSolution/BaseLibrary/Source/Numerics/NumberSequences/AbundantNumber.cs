namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Creates a new sequence of abundant numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<(TSelf Number, TSelf Sum)> GetAbundantNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.CreateChecked(3).LoopCustom<TSelf>((e, i) => true, (e, i) => e + TSelf.One).AsParallel().AsOrdered().Select(n => (n, sum: n.Factors(false).Sum() - n)).Where(x => x.sum > x.n);
    //=> Enumerable.Loop(() => (System.Numerics.BigInteger)3, e => true, e => e + 1, e => e).AsParallel().AsOrdered().Select(n => (n, sum: NumberSequences.Factors.GetSumOfDivisors(n) - n)).Where(x => x.sum > x.n);

    /// <summary>
    /// <para>Creates a new sequence of highly abundant numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Highly_abundant_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TSelf, TSelf>> GetHighlyAbundantNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var largestSumOfDivisors = TSelf.Zero;
      for (var index = TSelf.One; ; index++)
        if (index.Factors(false).Sum() is var sumOfDivisors && sumOfDivisors > largestSumOfDivisors)
        {
          yield return new System.Collections.Generic.KeyValuePair<TSelf, TSelf>(index, sumOfDivisors);
          largestSumOfDivisors = sumOfDivisors;
        }
    }

    /// <summary>
    /// <para>Creates a new sequence of super-abundant numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Superabundant_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TSelf, TSelf>> GetSuperAbundantNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var largestValue = 0.0;
      foreach (var kvp in GetHighlyAbundantNumbers<TSelf>())
        if ((double.CreateChecked(kvp.Value) / double.CreateChecked(kvp.Key)) is var value && value > largestValue)
        {
          yield return kvp;
          largestValue = value;
        }
    }

    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> is an abundant number.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsAbundantNumber<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number.Factors(false).Sum() - number > number;
  }
}
