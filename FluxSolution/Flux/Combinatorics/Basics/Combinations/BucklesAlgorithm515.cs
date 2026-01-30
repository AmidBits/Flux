namespace Flux.Combinations
{
  /// <summary>
  /// <para>A combination algorithm, in lexiograpic order, without repeats, by index.</para>
  /// <para>Finds the combination of a subset of size (P) elements, in a set with size (N) elements.</para>
  /// <para><seealso href="https://github.com/sleeepyjack/alg515"/></para>
  /// <para><seealso href="https://stackoverflow.com/questions/561/how-to-use-combinations-of-sets-as-test-data#794"/></para>
  /// <para><seealso href="https://people.math.sc.edu/Burkardt/f_src/toms515/toms515.html"/></para>
  /// <code><![CDATA[
  /// var(n, p) = (5, 3);
  /// 
  /// var nCp = n.BinomialCoefficient(p); // BucklesAlgorithm515.Count(n, p);
  /// 
  /// System.Console.WriteLine($"# of distinct combinations: {nCp}");
  /// 
  /// foreach (var l in System.Linq.Enumerable.Range(0, nCp))
  ///   System.Console.WriteLine($"{l} : [{string.Join(", ", n.PermuteAlgorithm515b(p, l))}]"); // BucklesAlgorithm515.Create(n, p, l, ref arrayOf3);
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
  public static partial class BucklesAlgorithm515
  {
    /// <summary>
    /// <para>Calculates the number of combinations of <paramref name="n"/> (N) things taken <paramref name="k"/> (K) at a time.</para>
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int CountCombinationsWithoutRepetition(int n, int k)
      => (int)IBinaryInteger.CountCombinationsWithoutRepetition(System.Numerics.BigInteger.CreateChecked(n), k);

    /// <summary>
    /// <para>Gets a single combination corresponding to the <paramref name="rank"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="alphabet"></param>
    /// <param name="indicesResult">Does not need to be initialized.</param>
    /// <param name="combination">Does not need to be initialized.</param>
    /// <param name="rank">The lexiographic index of the combination to build.</param>
    public static void GetCombinationByIndices<T>(System.Span<T> combination, System.ReadOnlySpan<int> combinationIndices, System.ReadOnlySpan<T> alphabet)
    {
      for (var i = combination.Length - 1; i >= 0; i--)
        combination[i] = alphabet[combinationIndices[i]];
    }

    ///// <summary>
    ///// <para>Yields a new sequence, in lexiographical order, of subset combinations from <paramref name="source"/> of the <paramref name="source"/> data.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="combinationLength"></param>
    ///// <returns></returns>
    //public static System.Collections.Generic.IEnumerable<T[]> GetCombinations<T>(T[] source, int combinationLength)
    //{
    //  var permutationCount = CountCombinationsWithoutRepetition(source.Length, combinationLength);

    //  var indices = new int[combinationLength];

    //  var combination = new T[combinationLength];

    //  for (var l = 0; l < permutationCount; l++)
    //  {
    //    GetCombination<T>(source, l, indices, combination);

    //    yield return combination;
    //  }
    //}

    /// <summary>
    /// <para>Find the permutation for a <paramref name="rank"/> of a subset with <paramref name="combinationIndices"/> (P) elements, in a set with <paramref name="n"/> (N) elements.</para>
    /// <para>Selects a subset of order <paramref name="combinationIndices"/> (P) from a set of order <paramref name="n"/> (N). This subroutine finds the combination set of <paramref name="n"/> (N) things taken <paramref name="combinationIndices"/> (P) at a time for a given <paramref name="rank"/>.</para>
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
    /// <param name="combinationIndices">An uninitialized array for subset (K-size).</param>
    /// <param name="n">The size (N) of the entire set.</param>
    /// <param name="rank">The 0-based index of the subset (or index array) to generate.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>Note that <paramref name="rank"/> is 1-based.</para>
    /// <para>The <paramref name="n"/> is the <see langword="this"/> argument for the extension method.</para>
    /// </remarks>
    public static void Unrank(System.Span<int> combinationIndices, int n, int rank)
    {
      var combinationLength = combinationIndices.Length;

      var x = 1;
      var r = CountCombinationsWithoutRepetition(n - x, combinationLength - 1);
      var k = r;

      while (k <= rank)
      {
        x++;
        r = CountCombinationsWithoutRepetition(n - x, combinationLength - 1);
        k += r;
      }

      k -= r;

      combinationIndices[0] = x - 1;

      for (var i = 2; i < combinationLength; i++)
      {
        x++;
        r = CountCombinationsWithoutRepetition(n - x, combinationLength - i);
        k += r;

        while (k <= rank)
        {
          x++;
          r = CountCombinationsWithoutRepetition(n - x, combinationLength - i);
          k += r;
        }

        k -= r;

        combinationIndices[i - 1] = x - 1;
      }

      combinationIndices[combinationLength - 1] = x + rank - k;
    }
  }
}

/*
public record class BucklesAlgorithm515
{
    /// <summary>
    /// <para>Find the permutation for a <paramref name="lexiographicIndex"/> of a subset with <paramref name="source"/>.Length (P) elements, in a set with <paramref name="alphabetLength"/> (N) elements.</para>
    /// <para>Selects a subset of order <paramref name="source"/>.Length (P) from a set of order <paramref name="alphabetLength"/> (N). This subroutine finds the combination set of <paramref name="alphabetLength"/> (N) things taken <paramref name="source"/>.Length (P) at a time for a given <paramref name="lexiographicIndex"/>.</para>
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
    /// <param name="source">The combination storage, and therefor the subset size (K).</param>
    /// <param name="alphabetLength">The size (N) of the entire set.</param>
    /// <param name="lexiographicIndex">A 0-based index of the subset (or index array) to generate.</param>
    /// <returns></returns>
    public static void GetCombination(System.Span<int> source, int alphabetLength, int lexiographicIndex)
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

    /// <summary>
    /// <para>Yields a new sequence, in lexiographical order, of subset combinations from <paramref name="source"/> of the <paramref name="source"/> data.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The alphabet.</param>
    /// <param name="combinationLength">The length of each combination.</param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T[]> GetCombinations<T>(T[] source, int combinationLength)
    {
		var indices = new int[combinationLength];
		
		for(var i = 0; i < combinationLength; i++)
			indices[i] = i;
		
      	var count = (source.Length).BinomialCoefficient(combinationLength);

      	var permutation = new T[combinationLength];

		for (var l = 0; l < count; l++)
      	{
        	GetCombination(indices, source.Length, l);

			for(var i = 0; i < combinationLength; i++)
				permutation[i] = source[indices[i]];
			
        	yield return permutation;
      	}
    }
}
*/
