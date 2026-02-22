namespace Flux.Permutations
{
  /// <summary>
  /// <para>Factoradic permutations.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Factorial_number_system"/></para>
  /// </summary>
  public class Factoradic
  {
    /// <summary>
    /// <para>Readjust values to obtain the permutation. Start from the end and check if preceding values are lower.</para>
    /// <para><see href="https://stackoverflow.com/a/7919887"/></para>
    /// </summary>
    /// <param name="factorialRepresentation"></param>
    public static void ConvertFactorialRepresentationToIndices(System.Span<int> factorialRepresentation)
    {
      for (var k = factorialRepresentation.Length - 1; k > 0; --k)
        for (var j = k - 1; j >= 0; --j)
          if (factorialRepresentation[j] <= factorialRepresentation[k])
            factorialRepresentation[k]++;
    }

    /// <summary>
    /// <para></para>
    /// <para>No checks are made.</para>
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="factoradicRepresentation"></param>
    public static void GetFactoradicRepresentation(System.Span<int> factoradicRepresentation, int rank)
    {
      var divisor = 1;

      for (var place = 1; place <= factoradicRepresentation.Length; place++)
      {
        if (rank / divisor is var quotient && quotient == 0)
          break; // All the remaining indices will be zero.

        factoradicRepresentation[^place] = quotient % place; // Compute the index at that place.

        divisor *= place;
      }
    }

    #region Permutations without repetition

    /// <summary>
    /// <para>Rank permutation without repetition using the factorial number system.</para>
    /// </summary>
    /// <param name="permutation"></param>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int RankPermutationWithoutRepetition(System.ReadOnlySpan<int> permutation, int n, int k)
    {
      var available = new System.Collections.Generic.List<int>(Enumerable.Range(0, n));

      int rank = 0;

      for (int i = 0; i < k; i++)
      {
        int idx = available.IndexOf(permutation[i]);
        rank += idx * BinaryInteger.Factorial(n - i - 1) / BinaryInteger.Factorial(n - k);
        available.RemoveAt(idx);
      }

      return rank;
    }

    /// <summary>
    /// <para>Unrank permutation without repetition using the factorial number system.</para>
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="permutation"></param>
    public static void UnrankPermutationWithoutRepetition(int rank, int n, int k, System.Span<int> permutation)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(rank, BinaryInteger.CountPermutationsWithoutRepetition(n, k));

      var available = new System.Collections.Generic.List<int>(Enumerable.Range(0, n));

      for (int i = 0; i < k; i++)
      {
        int blockSize = BinaryInteger.Factorial(n - i - 1) / BinaryInteger.Factorial(n - k);
        int idx = rank / blockSize;
        rank %= blockSize;
        permutation[i] = available[idx];
        available.RemoveAt(idx);
      }
    }

    #endregion

    #region Permutations with repetition

    /// <summary>
    /// <para>Rank permutation with repetition using the factorial number system.</para>
    /// </summary>
    /// <param name="permutation"></param>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int RankPermutationWithRepetition(System.ReadOnlySpan<int> permutation, int n, int k)
    {
      var rank = 0;

      for (var i = 0; i < k; i++)
        rank = rank * n + permutation[i];

      return rank;
    }

    /// <summary>
    /// <para>Unrank permutation with repetition using the factorial number system.</para>
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="permutation"></param>
    public static void UnrankPermutationWithRepetition(int rank, int n, int k, System.Span<int> permutation)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(rank, BinaryInteger.CountPermutationsWithRepetition(n, k));

      for (int i = k - 1; i >= 0; i--)
      {
        permutation[i] = rank % n;

        rank /= n;
      }
    }

    #endregion
  }
}
