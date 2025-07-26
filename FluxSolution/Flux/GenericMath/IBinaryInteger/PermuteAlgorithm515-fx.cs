namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Find the permutation for a <paramref name="lexiographicalIndex"/> (1-based index, L) of a subset with <paramref name="combinationLength"/> (P) elements, in a set with <paramref name="alphabetLength"/> (N) elements.</para>
    /// <para><seealso href="https://github.com/sleeepyjack/alg515"/></para>
    /// <para><seealso href="https://stackoverflow.com/questions/561/how-to-use-combinations-of-sets-as-test-data#794"/></para>
    /// <para><seealso href="https://people.math.sc.edu/Burkardt/f_src/toms515/toms515.html"/></para>
    /// <code><![CDATA[
    /// var(n, p) = (5, 3);
    /// 
    /// var nCp = n.BinomialCoefficient(p);
    /// 
    /// System.Console.WriteLine($"# of distinct combinations: {nCp}");
    /// 
    /// foreach (var l in System.Linq.Enumerable.Range(0, nCp))
    ///   System.Console.WriteLine($"{l} : [{string.Join(", ", n.PermuteAlgorithm515b(p, l))}]");
    ///   
    /// # of distinct combinations: 10
    /// [0, 1, 2]
    /// [0, 1, 3]
    /// [0, 1, 4]
    /// [0, 2, 3]
    /// [0, 2, 4]
    /// [0, 3, 4]
    /// [1, 2, 3]
    /// [1, 2, 4]
    /// [1, 3, 4]
    /// [2, 3, 4]
    /// ]]></code>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="alphabetLength">The size (N) of the entire set.</param>
    /// <param name="combinationLength">The subset size (K).</param>
    /// <param name="lexiographicalIndex">The 1-based index of the subset (or index array) to generate.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>Note that <paramref name="lexiographicalIndex"/> is 1-based.</para>
    /// <para>The <paramref name="alphabetLength"/> is the <see langword="this"/> argument for the extension method.</para>
    /// </remarks>
    public static TInteger[] PermuteAlgorithm515<TInteger>(this TInteger alphabetLength, TInteger combinationLength, TInteger lexiographicalIndex)
       where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var c = new TInteger[int.CreateChecked(combinationLength)];
      var x = TInteger.One;
      var r = (alphabetLength - x).BinomialCoefficient(combinationLength - TInteger.One);
      var k = r;

      while (k <= lexiographicalIndex)
      {
        x += TInteger.One;
        r = (alphabetLength - x).BinomialCoefficient(combinationLength - TInteger.One);
        k += r;
      }
      k -= r;
      c[0] = x - TInteger.One;

      for (var i = TInteger.CreateChecked(2); i < combinationLength; i++)
      {
        x += TInteger.One;
        r = (alphabetLength - x).BinomialCoefficient(combinationLength - i);
        k += r;

        while (k <= lexiographicalIndex)
        {
          x += TInteger.One;
          r = (alphabetLength - x).BinomialCoefficient(combinationLength - i);
          k += r;
        }

        k -= r;

        c[int.CreateChecked(i - TInteger.One)] = x - TInteger.One;
      }

      c[int.CreateChecked(combinationLength - TInteger.One)] = x + lexiographicalIndex - k;

      return c;
    }
  }
}
