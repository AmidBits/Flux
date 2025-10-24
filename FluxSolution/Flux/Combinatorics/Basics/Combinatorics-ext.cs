namespace Flux
{
  public static partial class Combinatorics
  {
    extension<TInteger>(TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Combinations with repetition, also known as combinations with replacement, are a way to select items from a set where the order does not matter, and items can be chosen more than once.</para>
      /// </summary>
      /// <param name="k">The number of items to choose.</param>
      /// <returns></returns>
      public TInteger CountCombinationsWithRepetition(TInteger k)
        => (n + k - TInteger.One).BinomialCoefficient(k);

      /// <summary>
      /// <para>Combinations without repetition refer to the selection of items from a larger set, where the order of selection does not matter and each item can only be chosen once.</para>
      /// </summary>
      /// <param name="k">The number of items to choose.</param>
      /// <returns></returns>
      public TInteger CountCombinationsWithoutRepetition(TInteger k)
        => n.BinomialCoefficient(k);

      /// <summary>
      /// <para>Permutations with repetition involve arranging a set of objects where some objects are identical. This concept is useful in various practical scenarios, such as arranging students of different grades or cars of certain colors without distinguishing between identical items.</para>
      /// <para>A permutation is an ordered combination.</para>
      /// </summary>
      /// <param name="k">The number of items to choose.</param>
      /// <returns></returns>
      public TInteger CountPermutationsWithRepetition(TInteger k)
        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(n), int.CreateChecked(k)));

      /// <summary>
      /// <para>Permutations without repetition refer to different groups of elements that can be done, so that two groups differ from each other only in the order the elements are placed. This situation frequently occurs when you’re working with unique physical objects that can occur only once in a permutation.</para>
      /// <para>A permutation is an ordered combination.</para>
      /// </summary>
      /// <param name="k">The number of items to choose.</param>
      /// <returns></returns>
      public TInteger CountPermutationsWithoutRepetition(TInteger k)
        => n.FallingFactorial(k);
    }
  }
}
