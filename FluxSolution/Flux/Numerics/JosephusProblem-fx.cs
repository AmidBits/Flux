namespace Flux
{
  public static partial class IBinaryIntegers
  {
    /// <summary>
    /// <para>Calculates the last longest surviving position (it's not a 0-based index) of the Flavius Josephus problem where <paramref name="n"/> people stand in a circle and every <paramref name="k"/> person commits suicide.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Josephus_problem"/></para>
    /// </summary>
    /// <remarks>This is about counting positions, so it is 1-based position that is computed.</remarks>
    /// <param name="n">The number of people in the initial circle.</param>
    /// <param name="k">The count of each step. I.e. k-1 people are skipped and the k-th is executed.</param>
    /// <returns>The 1-indexed position that the survivor occupies.</returns>
    public static TInteger JosephusProblem<TInteger>(this TInteger n, TInteger k)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);
      System.ArgumentOutOfRangeException.ThrowIfGreaterThan(k, n);

      var survivingPosition = TInteger.Zero;

      for (var positionCounter = TInteger.One; positionCounter <= n; positionCounter++)
        survivingPosition = (survivingPosition + k) % positionCounter;

      return survivingPosition + TInteger.One;
    }
  }
}
