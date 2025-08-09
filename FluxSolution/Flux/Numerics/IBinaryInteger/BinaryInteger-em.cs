
//namespace Flux
//{
//  public static partial class BinaryInteger
//  {
//    /// <summary>
//    /// </summary>
//    /// <param name="value">The total number of items in the set. Greater than or equal to <paramref name="k"/>.</param>
//    /// <returns></returns>
//    extension<TInteger>(TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//      public TInteger CountCombinationsWithRepetition(TInteger totalCount, TInteger combinationCount)
//        => (combinationCount + totalCount - TInteger.One).Factorial() / (combinationCount.Factorial() * (totalCount - TInteger.One).Factorial());

//      [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//      public TInteger CountCombinationsWithoutRepetition(TInteger totalCount, TInteger combinationCount)
//        => totalCount.BinomialCoefficient(combinationCount);

//      [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//      public TInteger CountPermutationsWithRepetition(TInteger totalCount, TInteger permutationCount)
//        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(totalCount), int.CreateChecked(permutationCount)));

//      [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//      public TInteger CountPermutationsWithoutRepetition(TInteger totalCount, TInteger permutationCount)
//        => totalCount.Factorial(totalCount - permutationCount);
//    }
//  }
//}
