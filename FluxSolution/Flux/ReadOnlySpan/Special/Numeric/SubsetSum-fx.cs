namespace Flux
{
  public static class SubsetSum
  {
    extension<TInteger>(System.ReadOnlySpan<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Build a boolean table matrix using dynamic programming.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Subset_sum_problem"/></para>
      /// <para><seealso href="https://www.geeksforgeeks.org/subset-sum-problem-dp-25/"/></para>
      /// <para><remarks>The subset sum problem (SSP) is a decision problem in computer science. Given a set of non-negative integers, and a value sum, determine if there is a subset of the given set with sum equal to given sum.</remarks></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="source"></param>
      /// <param name="targetSum"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public bool[,] SubsetSumMatrix(int targetSum)
      {
        // The state matrix[i][j] will be true if there exists a subset of elements from A[0….i] with sum value = 'j'.
        var dp = new bool[targetSum + 1, source.Length + 1]; // The value of subset[i][j] will be true if there is a subset of set[0..j-1] with sum equal to i.

        for (var i = source.Length - 1; i >= 0; i--) // If sum is 0, then answer is true.
          dp[0, i] = true;

        for (var i = 1; i <= targetSum; i++)
        {
          dp[i, 0] = false;

          for (var j = 1; j <= source.Length; j++)
          {
            var b = dp[i, j - 1];

            dp[i, j] = b;

            if (source[j - 1] is var jm1 && TInteger.CreateChecked(i) is var it && it >= jm1)
            {
              if (TInteger.IsNegative(jm1)) throw new System.ArgumentOutOfRangeException(nameof(source), "All values must be non-negative."); // Instead of checking all values prior, we simply throw when a value is found non-negative.

              dp[i, j] = b || dp[int.CreateChecked(it - jm1), j - 1];
            }
          }
        }

        // System.Diagnostics.Debug.WriteLine($"{nameof(SubsetSum)}.{nameof(GetDpMatrix)}():{System.Environment.NewLine}{dp.TransposeToCopy().ToConsole()}");

        return dp;
      }

      /// <summary>
      /// <para>Given a <paramref name="source"/> set of non-negative integers, and a value <paramref name="targetSum"/> sum, determine if there is a subset of the given <paramref name="source"/> set with sum equal to given <paramref name="targetSum"/> sum.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Subset_sum_problem"/></para>
      /// <para><seealso href="https://www.geeksforgeeks.org/subset-sum-problem-dp-25/"/></para>
      /// <para><remarks>The subset sum problem (SSP) is a decision problem in computer science. Given a set of non-negative integers, and a value sum, determine if there is a subset of the given set with sum equal to given sum.</remarks></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="source"></param>
      /// <param name="targetSum"></param>
      /// <returns>Whether there is a subset of the given set with sum equal to given sum.</returns>
      public bool IsSubsetSum(int targetSum)
        => SubsetSumMatrix(source, targetSum)[targetSum, source.Length];
    }
  }
}
