namespace Flux
{
  public static partial class StirlingNumbers
  {
    /// <summary>
    /// <para>Stirling numbers of the first kind arise in the study of permutations. In particular, the unsigned Stirling numbers of the first kind count permutations according to their number of cycles (counting fixed points as cycles of length one).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Stirling_number"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="dp"></param>
    /// <returns></returns>
    public static TInteger StirlingNumber1st<TInteger>(TInteger n, TInteger k, out TInteger[,]? dp)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TInteger.IsNegative(n) || TInteger.IsNegative(k) || k > n)
      {
        dp = null;

        return TInteger.Zero;
      }

      var ni = int.CreateChecked(n);
      var ki = int.CreateChecked(k);

      dp = new TInteger[ni + 1, ki + 1];

      dp[0, 0] = TInteger.One; // c(0, 0) = 1

      for (var i = 1; i <= ni; i++)
        dp[i, 0] = TInteger.Zero; // c(n, 0) = 0 for n > 0

      for (var j = 1; j <= ki; j++)
        dp[0, j] = TInteger.Zero; // c(0, k) = 0 for k > 0

      for (var i = 1; i <= ni; i++)
        for (var j = 1; j <= ki; j++)
          dp[i, j] = dp[i - 1, j - 1] + TInteger.CreateChecked(i - 1) * dp[i - 1, j]; // Fill the table using the recurrence relation.

      return dp[ni, ki];
    }

    /// <summary>
    /// <para>a Stirling number of the second kind (or Stirling partition number) is the number of ways to partition a set of n objects into k non-empty subsets.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Stirling_number"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static TInteger StirlingNumber2nd<TInteger>(TInteger n, TInteger k)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var sum = TInteger.Zero;
      var neg = TInteger.One;

      if ((TInteger.IsZero(n) ^ TInteger.IsZero(k)) || (k > n)) return sum;
      if (n == k) return neg;

      checked
      {
        for (var i = sum; i <= k; i++)
        {
          sum += neg * BinomialTheorem.BinomialCoefficient(k, i) * (k - i).IntegerPow(n);
          neg = -neg;
        }
      }

      sum /= k.Factorial();

      return sum;
    }
  }
}
