namespace Flux.Combinations
{
  /// <summary>
  /// <para>Implementation of ranking and unranking combinations using the combinatorial number system (the Lehmer‑style code for combinations).</para>
  /// </summary>
  public class CombinatorialNumberSystem
  {
    #region Combinations without repetition

    /// <summary>
    /// <para>Combinadics rank combination without repetition using the combinatorial number system.</para>
    /// </summary>
    /// <param name="combination"></param>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int RankCombinationWithoutRepetition(System.ReadOnlySpan<int> combination, int n, int k)
    {
      var rank = 0;

      for (var i = 0; i < k; i++)
        for (var j = (i == 0 ? 0 : combination[i - 1] + 1); j < combination[i]; j++)
          rank += BinaryInteger.BinomialCoefficient(n - j - 1, k - i - 1);

      return rank;
    }

    /// <summary>
    /// <para>Combinadics unrank combination without repetition using the combinatorial number system.</para>
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="combination"></param>
    public static void UnrankCombinationWithoutRepetition(int rank, int n, int k, System.Span<int> combination)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(rank, BinaryInteger.CountCombinationsWithoutRepetition(n, k));

      var x = 0;

      for (var i = 0; i < k; i++)
      {
        while (BinaryInteger.BinomialCoefficient(n - x - 1, k - i - 1) is var bc && bc <= rank)
        {
          rank -= bc;

          x++;
        }

        combination[i] = x++;
      }
    }

    #endregion

    #region Combinations with repetition

    /// <summary>
    /// <para>Combinadics rank combination with repetition using the combinatorial number system.</para>
    /// </summary>
    /// <param name="combination"></param>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int RankCombinationWithRepetition(System.ReadOnlySpan<int> combination, int n, int k)
    {
      var rank = 0;

      for (var i = 0; i < k; i++)
        for (var j = (i == 0 ? 0 : combination[i - 1]); j < combination[i]; j++)
          rank += BinaryInteger.BinomialCoefficient(n - j + k - i - 2, k - i - 1);

      return rank;
    }

    /// <summary>
    /// <para>Combinadics unrank combination with repetition using the combinatorial number system.</para>
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="combination"></param>
    public static void UnrankCombinationWithRepetition(int rank, int n, int k, System.Span<int> combination)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(rank, BinaryInteger.CountCombinationsWithRepetition(n, k));

      var x = 0;

      for (var i = 0; i < k; i++)
      {
        while (BinaryInteger.BinomialCoefficient(n - x + k - i - 2, k - i - 1) is var bc && bc <= rank)
        {
          rank -= bc;

          x++;
        }

        combination[i] = x;
      }
    }

    #endregion
  }
}
