namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Permutations with repetition involve arranging a set of objects where some objects are identical. This concept is useful in various practical scenarios, such as arranging students of different grades or cars of certain colors without distinguishing between identical items.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="totalCount">The total number of items in the set.</param>
    /// <param name="permutationCount">The number of items to choose.</param>
    /// <returns></returns>
    public static TInteger CountPermutationsWithRepetition<TInteger>(this TInteger totalCount, TInteger permutationCount)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(totalCount), int.CreateChecked(permutationCount)));

    /// <summary>
    /// <para>Permutations without repetition refer to different groups of elements that can be done, so that two groups differ from each other only in the order the elements are placed. This situation frequently occurs when you’re working with unique physical objects that can occur only once in a permutation.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="totalCount">The total number of items in the set.</param>
    /// <param name="permutationCount">The number of items to choose.</param>
    /// <returns></returns>
    public static TInteger CountPermutationsWithoutRepetition<TInteger>(this TInteger totalCount, TInteger permutationCount)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    => totalCount.Factorial(totalCount - permutationCount);
  }
}
