namespace Flux
{
  public static partial class CombinatoricsBasicsHelper
  {
    /// <summary>
    /// <para>Gets a <paramref name="permutation"/> by the specified <paramref name="indices"/> in an <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="permutation"></param>
    /// <param name="indices"></param>
    /// <param name="alphabet"></param>
    public static void GetPermutationByIndices<T>(this System.Span<T> permutation, System.ReadOnlySpan<int> indices, System.ReadOnlySpan<T> alphabet)
    {
      for (var i = 0; i < indices.Length; i++)
        permutation[i] = alphabet[indices[i]];
    }

    extension<TInteger>(TInteger total)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Combinations with repetition, also known as combinations with replacement, are a way to select items from a set where the order does not matter, and items can be chosen more than once.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="totalCount">The total number of items in the set.</param>
      /// <param name="choose">The number of items to choose.</param>
      /// <returns></returns>
      public TInteger CountCombinationsWithRepetition(TInteger choose)
      {
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(choose, total);

        if (total <= TInteger.Zero || TInteger.IsNegative(choose))
          return TInteger.Zero;

        TInteger numerator = TInteger.One;
        for (var i = TInteger.Zero; i < choose; i++)
          numerator *= total + choose - TInteger.One - i;

        var denominator = choose.Factorial();

        return numerator / denominator;

        //return checked((choose + total - TInteger.One).Factorial() / (choose.Factorial() * (total - TInteger.One).Factorial()));
      }

      /// <summary>
      /// <para>Combinations without repetition refer to the selection of items from a larger set, where the order of selection does not matter and each item can only be chosen once.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="totalCount">The total number of items in the set.</param>
      /// <param name="choose">The number of items to choose.</param>
      /// <returns></returns>
      public TInteger CountCombinationsWithoutRepetition(TInteger choose)
      {
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(choose, total);

        return total.BinomialCoefficient(choose);
      }

      /// <summary>
      /// <para>Permutations with repetition involve arranging a set of objects where some objects are identical. This concept is useful in various practical scenarios, such as arranging students of different grades or cars of certain colors without distinguishing between identical items.</para>
      /// <para>A permutation is an ordered combination.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="totalCount">The total number of items in the set.</param>
      /// <param name="choose">The number of items to choose.</param>
      /// <returns></returns>
      public TInteger CountPermutationsWithRepetition(TInteger choose)
      {
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(choose, total);

        return TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(total), int.CreateChecked(choose)));
      }

      /// <summary>
      /// <para>Permutations without repetition refer to different groups of elements that can be done, so that two groups differ from each other only in the order the elements are placed. This situation frequently occurs when you’re working with unique physical objects that can occur only once in a permutation.</para>
      /// <para>A permutation is an ordered combination.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="totalCount">The total number of items in the set.</param>
      /// <param name="choose">The number of items to choose.</param>
      /// <returns></returns>
      public TInteger CountPermutationsWithoutRepetition(TInteger choose)
      {
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(choose, total);

        return total.FallingFactorial(choose);
      }
    }
  }
}
