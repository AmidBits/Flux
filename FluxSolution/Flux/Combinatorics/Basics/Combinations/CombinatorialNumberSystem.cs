namespace Flux.Combinations
{
  public static class CombinatorialNumberSystem
  {
    #region Combinations without repetition

    public static int RankCombinationWithoutRepetition(System.ReadOnlySpan<int> combo, int n, int k)
    {
      var rank = 0;

      for (var i = 0; i < k; i++)
        for (var j = (i == 0 ? 0 : combo[i - 1] + 1); j < combo[i]; j++)
          rank += BinaryInteger.BinomialCoefficient(n - j - 1, k - i - 1);

      return rank;
    }

    public static int[] UnrankCombinationWithoutRepetition(int rank, int n, int k)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(rank, BinaryInteger.CountCombinationsWithoutRepetition(n, k));

      var combo = new int[k];

      var x = 0;

      for (var i = 0; i < k; i++)
      {
        while (BinaryInteger.BinomialCoefficient(n - x - 1, k - i - 1) <= rank)
        {
          rank -= BinaryInteger.BinomialCoefficient(n - x - 1, k - i - 1);

          x++;
        }

        combo[i] = x++;
      }

      return combo;
    }

    #endregion

    #region Combinations with repetition

    public static int RankCombinationWithRepetition(System.ReadOnlySpan<int> combo, int n, int k)
    {
      var rank = 0;

      for (var i = 0; i < k; i++)
        for (var j = (i == 0 ? 0 : combo[i - 1]); j < combo[i]; j++)
          rank += BinaryInteger.BinomialCoefficient(n - j + k - i - 2, k - i - 1);

      return rank;
    }

    public static int[] UnrankCombinationWithRepetition(int rank, int n, int k)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(rank, BinaryInteger.CountCombinationsWithRepetition(n, k));

      var combo = new int[k];

      var x = 0;

      for (var i = 0; i < k; i++)
      {
        while (BinaryInteger.BinomialCoefficient(n - x + k - i - 2, k - i - 1) <= rank)
        {
          rank -= BinaryInteger.BinomialCoefficient(n - x + k - i - 2, k - i - 1);

          x++;
        }

        combo[i] = x;
      }

      return combo;
    }

    #endregion
  }
}
