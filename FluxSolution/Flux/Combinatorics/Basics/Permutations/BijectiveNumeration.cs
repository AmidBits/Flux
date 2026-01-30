namespace Flux.Permutations
{
  /// <summary>
  /// <para>Flexible lexiographically ordered permutation algorithm with repetition by index.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><seealso href="https://stackoverflow.com/a/20446640/3178666"/></para>
  /// </summary>
  public static partial class BijectiveNumeration
  {
    /// <summary>
    /// <para>Yields the number of permutations with repetition that a bijective numeration permutations with <paramref name="n"/>C<paramref name="k"/> (c choose k) will produce.</para>
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int CountPermutationsWithRepetition(int n, int k)
    {
      var range = GetPermutationInterval(n, k);

      return range.MaxValue - range.MinValue + 1;
    }

    /// <summary>
    /// <para>Gets the interval of indices for an <paramref name="n"/> choose <paramref name="k"/>.</para>
    /// <para>Generates all possible permutations (with repeats) of length <paramref name="k"/> and <paramref name="n"/> size alphabet in lexiographical order</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static Interval<TInteger> GetPermutationInterval<TInteger>(TInteger n, TInteger k)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var minIndex = TInteger.Zero;
      var maxIndex = TInteger.One;

      for (var index = TInteger.One; index <= k; index++)
      {
        minIndex = minIndex + maxIndex;
        maxIndex = TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(n), int.CreateChecked(index)));
      }

      return new(minIndex, maxIndex + minIndex - TInteger.One);
    }

    public static TInteger Rank<TSymbol, TInteger>(System.ReadOnlySpan<TSymbol> permutation, System.ReadOnlySpan<TSymbol> alphabet)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var rank = TInteger.Zero;

      var k = TInteger.One;

      for (var i = permutation.Length - 1; i >= 0; i--)
      {
        rank += TInteger.CreateChecked(alphabet.IndexOf(permutation[i]) + 1) * k;

        k *= TInteger.CreateChecked(alphabet.Length);
      }

      return rank;
    }

    /// <summary>
    /// <para><see href="https://stackoverflow.com/a/20446640/3178666"/></para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="alphabet"></param>
    /// <param name="rank"></param>
    /// <returns>A span representing the portion of the permutationBuffer used for the unranked permutation.</returns>
    public static System.ReadOnlySpan<TSymbol> Unrank<TSymbol, TInteger>(System.Span<TSymbol> permutationBuffer, TInteger rank, System.ReadOnlySpan<TSymbol> alphabet)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /*
      def get_bijective_val(n, alphabet):
          base = len(alphabet)
          digits = []
          while n:
              remainder = math.ceil(n/base)-1
              digits.append(n - remainder*base)
              n = remainder
          digits.reverse()
          return "".join(alphabet[digit-1] for digit in digits)
       */

      var n = TInteger.CreateChecked(alphabet.Length);
      var k = permutationBuffer.Length;

      while (rank > TInteger.Zero)
      {
        var (q, r) = TInteger.DivRem(rank, n);

        r = (TInteger.IsZero(r) ? q : q + TInteger.One) - TInteger.One;

        permutationBuffer[--k] = alphabet[int.CreateChecked(rank - r * n - TInteger.One)];

        rank = r;
      }

      return permutationBuffer.Slice(k);
    }
  }
}
