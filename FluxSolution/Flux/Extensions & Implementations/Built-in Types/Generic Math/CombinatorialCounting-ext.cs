namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region BinomialCoefficient

      /// <summary>
      /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
      /// <para>This implementation can easily overflow, use larger storage types when possible.</para>
      /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
      /// <para><seealso href="https://cp-algorithms.com/combinatorics/binomial-coefficients.html"/></para>
      /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</para>
      /// <para>(k &lt; 0 or k > n) = 0</para>
      /// <para>(k = 0 or k = n) = 1</para>
      /// </remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger BinomialCoefficient(TInteger n, TInteger k)
      {
        k = TInteger.Min(k, n - k); // Optimization.

        if (TInteger.IsNegative(k))
          return TInteger.Zero;

        if (TInteger.IsZero(k)) // Because of the optimization above, only half of "(TInteger.IsZero(k) || k == n)" is needed.
          return TInteger.One;

        return FallingFactorial(n, k) / Factorial(k);
      }

      #endregion

      #region CountCombinationsWithRepetition

      /// <summary>
      /// <para>Combinations with repetition are a way to select items from a set where the order does not matter (combination), and items can be chosen more than once (with repeats).</para>
      /// <para>Computes the number of combinations with repeats:</para>
      /// <para><c>"n multichoose k"</c> = <c>Cr(n + k - 1, k)</c> = <c>(n + k - 1)! / (k! * (n - 1)!)</c></para>
      /// <para>A <i>combination</i> is a way of choosing items from a set when the order of choice does not matter. If you rearrange the same chosen items, it still counts as the same combination.</para>
      /// <para><i><b>With</b> repetition</i> means that each item can be chosen more than once.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountCombinationsWithRepetition(TInteger total, TInteger choose)
        => BinomialCoefficient(total + choose - TInteger.One, choose);

      #endregion

      #region CountCombinationsWithoutRepetition

      /// <summary>
      /// <para>Combinations without repetition refer to the selection of items from a larger set, where the order of selection does not matter (combination), and each item can only be chosen once (no repeats).</para>
      /// <para>Counts combinations without repeats:</para>
      /// <para><c>"n choose k"</c> = <c>C(n, k)</c> = <c>n! / (k! * (n - k)!)</c></para>
      /// <para>A <i>combination</i> is a way of choosing items from a set when the order of choice does not matter. If you rearrange the same chosen items, it still counts as the same combination.</para>
      /// <para><i><b>Without</b> repetition</i> means that once an item is chosen, it cannot be selected again.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountCombinationsWithoutRepetition(TInteger total, TInteger choose)
        => BinomialCoefficient(total, choose);

      #endregion

      #region CountPermutationsWithRepetition

      /// <summary>
      /// <para>Permutations with repetition count the number of ordered selections where each selected item may be chosen more than once.</para>
      /// <para>Counts permutations with repeats:</para>
      /// <para><c>P(n, k) = n^k</c></para>
      /// <para>A <i>permutation</i> is an arrangement of items where the order matters. If you change the order, you create a different permutation.</para>
      /// <para><i><b>With</b> repetition</i> means that each item can be chosen more than once.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountPermutationsWithRepetition(TInteger total, TInteger choose)
        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(total), int.CreateChecked(choose)));

      #endregion

      #region CountPermutationsWithoutRepetition

      /// <summary>
      /// <para>Permutations without repetition refer to different groups of elements that can be done, so that two groups differ from each other only in the order the elements are placed. This situation frequently occurs when you’re working with unique physical objects that can occur only once in a permutation.</para>
      /// <para>Counts permutations without repeats:</para>
      /// <para><c>P(n, k)</c> = <c>n! / (n - k)!</c></para>
      /// <para>A <i>permutation</i> is an arrangement of items where the order matters. If you change the order, you create a different permutation.</para>
      /// <para><i><b>Without</b> repetition</i> means that once an item is chosen, it cannot be chosen again.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountPermutationsWithoutRepetition(TInteger total, TInteger choose)
        => FallingFactorial(total, choose);

      #endregion
    }
  }
}
