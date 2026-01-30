namespace Flux.Permutations
{
  /// <summary>
  /// <para>Factoradic permutations.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Factorial_number_system"/></para>
  /// </summary>
  public static partial class Factoradic
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

    public static int RankPermutationWithoutRepetition(System.ReadOnlySpan<int> perm, int n, int k)
    {
      var available = new System.Collections.Generic.List<int>(Enumerable.Range(0, n));

      int rank = 0;

      for (int i = 0; i < k; i++)
      {
        int idx = available.IndexOf(perm[i]);
        rank += idx * IBinaryInteger.Factorial(n - i - 1) / IBinaryInteger.Factorial(n - k);
        available.RemoveAt(idx);
      }

      return rank;
    }

    public static int[] UnrankPermutationWithoutRepetition(int rank, int n, int k)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(rank, IBinaryInteger.CountPermutationsWithoutRepetition(n, k));

      var available = new System.Collections.Generic.List<int>(Enumerable.Range(0, n));

      var perm = new int[k];

      for (int i = 0; i < k; i++)
      {
        int blockSize = IBinaryInteger.Factorial(n - i - 1) / IBinaryInteger.Factorial(n - k);
        int idx = rank / blockSize;
        rank %= blockSize;
        perm[i] = available[idx];
        available.RemoveAt(idx);
      }

      return perm;
    }

    #endregion

    #region Permutations with repetition

    public static int RankPermutationWithRepetition(System.ReadOnlySpan<int> perm, int n, int k)
    {
      var rank = 0;

      for (var i = 0; i < k; i++)
        rank = rank * n + perm[i];

      return rank;
    }

    public static int[] UnrankPermutationWithRepetition(int rank, int n, int k)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(rank, IBinaryInteger.CountPermutationsWithRepetition(n, k));

      var perm = new int[k];

      for (int i = k - 1; i >= 0; i--)
      {
        perm[i] = rank % n;

        rank /= n;
      }

      return perm;
    }

    #endregion
  }
}
