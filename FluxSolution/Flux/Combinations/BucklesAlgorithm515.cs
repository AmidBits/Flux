namespace Flux.Combinations
{
  public static partial class BucklesAlgorithm515
  {
    #region Extension methods

    /// <summary>
    /// <para>Yields a new sequence, in lexiographical order, of subset combinations from <paramref name="source"/> of the <paramref name="source"/> data.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="combinationLength"></param>
    /// <returns></returns>
    //public static System.Collections.Generic.IEnumerable<T[]> CombinationsAlgorithm515<T>(this T[] source, int combinationLength)
    //{
    //  var permutationCount = CombinationCount(source.Length, combinationLength);

    //  var permutation = new T[combinationLength];

    //  for (var l = 0; l < permutationCount; l++)
    //  {
    //    (source.Length).CombinationFillAlgorithm515(combinationLength, l, ref permutation);

    //    yield return permutation;
    //  }
    //}

    #endregion // Extension methods

    /// <summary>
    /// <para>Calculates the number of combinations of <paramref name="alphabetLength"/> (N) things taken <paramref name="combinationLength"/> (K) at a time.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="alphabetLength"></param>
    /// <param name="combinationLength"></param>
    /// <returns></returns>
    //public static TNumber CombinationCount<TNumber>(this TNumber alphabetLength, TNumber combinationLength)
    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>
    //  => alphabetLength.BinomialCoefficient(combinationLength);

    /// <summary>
    /// <para>Find the permutation for a <paramref name="lexiographicIndex"/> of a subset with <paramref name="combinationLength"/> (P) elements, in a set with <paramref name="alphabetLength"/> (N) elements.</para>
    /// <para>Selects a subset of order <paramref name="combinationLength"/> (P) from a set of order <paramref name="alphabetLength"/> (N). This subroutine finds the combination set of <paramref name="alphabetLength"/> (N) things taken <paramref name="combinationLength"/> (P) at a time for a given <paramref name="lexiographicIndex"/>.</para>
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
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="alphabetLength">The size (N) of the entire set.</param>
    /// <param name="combinationLength">The subset size (K).</param>
    /// <param name="lexiographicIndex">The 1-based index of the subset (or index array) to generate.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>Note that <paramref name="lexiographicIndex"/> is 1-based.</para>
    /// <para>The <paramref name="alphabetLength"/> is the <see langword="this"/> argument for the extension method.</para>
    /// </remarks>
    //public static TNumber[] CombinationAlgorithm515Get<TNumber>(this TNumber alphabetLength, TNumber combinationLength, TNumber lexiographicIndex)
    //   where TNumber : System.Numerics.IBinaryInteger<TNumber>
    //{
    //  var c = new TNumber[int.CreateChecked(combinationLength)];

    //  var x = TNumber.One;
    //  var r = (alphabetLength - x).BinomialCoefficient(combinationLength - TNumber.One);
    //  var k = r;

    //  while (k <= lexiographicIndex)
    //  {
    //    x += TNumber.One;
    //    r = (alphabetLength - x).BinomialCoefficient(combinationLength - TNumber.One);
    //    k += r;
    //  }

    //  k -= r;

    //  c[0] = x - TNumber.One;

    //  for (var i = TNumber.CreateChecked(2); i < combinationLength; i++)
    //  {
    //    x += TNumber.One;
    //    r = (alphabetLength - x).BinomialCoefficient(combinationLength - i);
    //    k += r;

    //    while (k <= lexiographicIndex)
    //    {
    //      x += TNumber.One;
    //      r = (alphabetLength - x).BinomialCoefficient(combinationLength - i);
    //      k += r;
    //    }

    //    k -= r;

    //    c[int.CreateChecked(i - TNumber.One)] = x - TNumber.One;
    //  }

    //  c[int.CreateChecked(combinationLength - TNumber.One)] = x + lexiographicIndex - k;

    //  return c;
    //}

    public static void CombinationCountAlgorithm515(this System.Span<int> source, int alphabetLength)
      => alphabetLength.BinomialCoefficient(source.Length);

    public static void CombinationNextAlgorithm515(this System.Span<int> source, int alphabetLength, int lexiographicIndex)
    {
      var x = 1;
      var r = (alphabetLength - x).BinomialCoefficient(source.Length - 1);
      var k = r;
      
      while (k <= lexiographicIndex)
      {
        x += 1;
        r = (alphabetLength - x).BinomialCoefficient(source.Length - 1);
        k += r;
      }

      k -= r;

      source[0] = x - 1;

      for (var i = 2; i < source.Length; i++)
      {
        x += 1;
        r = (alphabetLength - x).BinomialCoefficient(source.Length - i);
        k += r;

        while (k <= lexiographicIndex)
        {
          x += 1;
          r = (alphabetLength - x).BinomialCoefficient(source.Length - i);
          k += r;
        }

        k -= r;

        source[i - 1] = x - 1;
      }

      source[source.Length - 1] = x + lexiographicIndex - k;
    }

    public static void CombinationResetAlgorithm515(this System.Span<int> source)
    {
      for(var index = source.Length - 1; index >= 0; index--)
        source[index] = index;
    }

    public static void CombinationFillAlgorithm515<TNumber>(this TNumber alphabetLength, TNumber combinationLength, TNumber lexiographicIndex, ref TNumber[] combinationArray)
       where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var x = TNumber.One;
      var r = (alphabetLength - x).BinomialCoefficient(combinationLength - TNumber.One);
      var k = r;

      while (k <= lexiographicIndex)
      {
        x += TNumber.One;
        r = (alphabetLength - x).BinomialCoefficient(combinationLength - TNumber.One);
        k += r;
      }

      k -= r;

      combinationArray[0] = x - TNumber.One;

      for (var i = TNumber.CreateChecked(2); i < combinationLength; i++)
      {
        x += TNumber.One;
        r = (alphabetLength - x).BinomialCoefficient(combinationLength - i);
        k += r;

        while (k <= lexiographicIndex)
        {
          x += TNumber.One;
          r = (alphabetLength - x).BinomialCoefficient(combinationLength - i);
          k += r;
        }

        k -= r;

        combinationArray[int.CreateChecked(i - TNumber.One)] = x - TNumber.One;
      }

      combinationArray[int.CreateChecked(combinationLength - TNumber.One)] = x + lexiographicIndex - k;
    }
  }
}
