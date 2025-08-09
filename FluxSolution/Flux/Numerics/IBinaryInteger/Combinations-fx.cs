namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Combinations with repetition, also known as combinations with replacement, are a way to select items from a set where the order does not matter, and items can be chosen more than once.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="totalCount">The total number of items in the set.</param>
    /// <param name="combinationCount">The number of items to choose.</param>
    /// <returns></returns>
    public static TInteger CountCombinationsWithRepetition<TInteger>(this TInteger totalCount, TInteger combinationCount)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    => (combinationCount + totalCount - TInteger.One).Factorial() / (combinationCount.Factorial() * (totalCount - TInteger.One).Factorial());

    /// <summary>
    /// <para>Combinations without repetition refer to the selection of items from a larger set, where the order of selection does not matter and each item can only be chosen once.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="totalCount">The total number of items in the set.</param>
    /// <param name="combinationCount">The number of items to choose.</param>
    /// <returns></returns>
    public static TInteger CountCombinationsWithoutRepetition<TInteger>(this TInteger totalCount, TInteger combinationCount)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    => totalCount.BinomialCoefficient(combinationCount);
  }
}
