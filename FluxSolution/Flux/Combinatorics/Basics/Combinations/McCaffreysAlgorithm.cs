
namespace Flux.Combinatorics.Basics.Combinations
{
  /// <summary>
  /// <para><see href="https://www.redperegrine.net/2021/04/10/software-algorithms-for-k-combinations/"/></para>
  /// </summary>
  public static partial class McCaffreysAlgorithm
  {
    /// <summary>Calculates zero-based array of c such that maxRank = (c1 choose k-1) + (c2 choose k-2) + ... (c[of k-1] choose 1) </summary>
    private static void CalculateCombinadic(System.Span<int> combinadic, int n, int k, int maxRank)
    {
      var diminishingRank = maxRank;
      var reducingK = k;

      for (int i = 0; i < k; i++)
      {
        combinadic[i] = CalculateLargestRankBelowThreshold(n, reducingK, diminishingRank);

        diminishingRank -= combinadic[i].BinomialCoefficient(reducingK);
        reducingK--;
      }
    }

    /// <summary>
    /// <para>Returns the highest rank of n2 choose k2 that is less than the threshold</para>
    /// </summary>
    /// <param name="n2"></param>
    /// <param name="k2"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    private static int CalculateLargestRankBelowThreshold(int n2, int k2, int threshold)
    {
      var i = n2 - 1;

      while (i.BinomialCoefficient(k2) > threshold)
        i--;

      return i;
    }

    public static int CountCombinationsWithoutRepetition(int n, int k)
      => (int)System.Numerics.BigInteger.CreateChecked(n).CountCombinationsWithoutRepetition(k);

    public static int Rank(System.ReadOnlySpan<int> combination, int n, int k, System.Span<int> workspace)
    {
      var dualOfZero = n - 1;

      combination.CopyTo(workspace);

      var dualOfCombinadic = 0;

      var reducingK = k;

      for (int i = 0; i < k; i++)
      {
        workspace[i] = dualOfZero - workspace[i]; // Map to combinadic.

        dualOfCombinadic += workspace[i].BinomialCoefficient(reducingK); // Calculate dual of combinadic (by accumulation).

        reducingK--;
      }

      var dual = n.BinomialCoefficient(k) - 1 - dualOfCombinadic;

      return dual;
    }

    public static void Unrank(System.Span<int> combination, int n, int k, int rank)
    {
      var dualOfZero = n - 1;

      var dual = n.BinomialCoefficient(k) - 1 - rank;

      CalculateCombinadic(combination, n, k, dual);

      for (int i = 0; i < k; i++)
        combination[i] = dualOfZero - combination[i];
    }
  }
}
