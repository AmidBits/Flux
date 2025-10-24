namespace Flux
{
  /// <summary>
  /// <para>A linear time permutation algorithm, non-lexicographically ordered, without repeats, by rank or permutation.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><see href="https://stackoverflow.com/a/26197620"/></para>
  /// <para><see href="https://webhome.cs.uvic.ca/~ruskey/Publications/RankPerm/MyrvoldRuskey.pdf"/></para>
  /// </summary>
  public sealed class MyrvoldRuskey
  {
    //private int m_count;
    //private int[] m_inverse;
    //private int[] m_permutation;
    //private int m_rank;

    //public MyrvoldRuskeyPermutations(int length)
    //{
    //  m_count = length.CountPermutationsWithoutRepetition(length);
    //  m_inverse = new int[length];
    //  m_permutation = new int[length];
    //  m_rank = -1;
    //}

    ///// <summary>
    ///// <para>The number of permutations.</para>
    ///// </summary>
    //public int Count => m_count;

    ///// <summary>
    ///// <para>The permutation of <see cref="Rank"/>.</para>
    ///// </summary>
    //public System.ReadOnlySpan<int> Permutation => m_permutation;

    ///// <summary>
    ///// <para>The rank of <see cref="Permutation"/></para>
    ///// </summary>
    //public int Rank => m_rank;

    //public void GetRank(System.Span<int> permutation)
    //{
    //  System.ArgumentOutOfRangeException.ThrowIfNotEqual(permutation.Length, m_permutation.Length);

    //  permutation.CopyTo(m_permutation);

    //  CreateInversePermutation(m_permutation, m_inverse, m_permutation.Length);

    //  Rank(m_permutation, m_inverse, m_permutation.Length);
    //}

    //public void GetPermutation(int rank)
    //{
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(rank);
    //  System.ArgumentOutOfRangeException.ThrowIfGreaterThan(rank, m_count);

    //  m_rank = rank;

    //  CreateIdentityPermutation(m_permutation, m_permutation.Length);

    //  Unrank(m_rank, m_permutation, m_permutation.Length);
    //}

    public static void CreateIdentityPermutation(System.Span<int> permutation)
    {
      for (var i = 0; i < permutation.Length; i++)
        permutation[i] = i;
    }

    /// <summary>
    /// <para>Creates an inverse of a permutation.</para>
    /// <para>This is the inverse parameter for <see cref="Rank(Span{int}, Span{int}, int)"/>.</para>
    /// </summary>
    /// <param name="permutation">The input permutation.</param>
    /// <param name="inverse">The inverse input permutation (in linear time, <c><![CDATA[q[p[i]] = i for 0 <= i < n]]></c>).</param>
    /// <param name="k">The length(permutation).</param>
    public static void CreateInversePermutation(System.ReadOnlySpan<int> permutation, System.Span<int> inverse)
    {
      System.ArgumentOutOfRangeException.ThrowIfNotEqual(inverse.Length, permutation.Length);

      for (var i = 0; i < permutation.Length; i++)
        inverse[permutation[i]] = i;
    }

    /// <summary>
    /// <para>Get a rank from a <paramref name="permutation"/> in O(n) time.</para>
    /// <para><see href="https://stackoverflow.com/a/26197620"/></para>
    /// <para><see href="https://webhome.cs.uvic.ca/~ruskey/Publications/RankPerm/MyrvoldRuskey.pdf"/></para>
    /// </summary>
    /// <remarks>
    /// <para>Not lexiographically ordered.</para>
    /// </remarks>
    /// <param name="permutation">The input permutation.</param>
    /// <param name="permutationInverse">The inverse input permutation (in linear time, <c><![CDATA[q[p[i]] = i for 0 <= i < n]]></c>).</param>
    /// <param name="k">length(permutation)</param>
    /// <returns>The rank of the <paramref name="permutation"/>.</returns>
    public static int Rank(System.Span<int> permutation, System.Span<int> permutationInverse, int k)
    {
      /*
Initialize:
p = input permutation
q = inverse input permutation (in linear time, q[p[i]] = i for 0 <= i < n)
n = length(p)

rank(n, p, q)
  if n=1 then return 0 fi
  s = p[n-1]
  swap(p[n-1], p[q[n-1]])
  swap(q[s], q[n-1])
  return s + n * rank(n-1, p, q)
end
      */

      if (k == 1)
        return 0;

      var plM1 = k - 1;

      var s = permutation[plM1];

      permutation.Swap(plM1, permutationInverse[plM1]);
      permutationInverse.Swap(s, plM1);

      return s + k * Rank(permutation, permutationInverse, plM1);
    }

    /// <summary>
    /// <para>Get a rank from a <paramref name="permutation"/> in O(n) time.</para>
    /// <para><see href="https://stackoverflow.com/a/26197620"/></para>
    /// <para><see href="https://webhome.cs.uvic.ca/~ruskey/Publications/RankPerm/MyrvoldRuskey.pdf"/></para>
    /// </summary>
    /// <remarks>
    /// <para>Not lexiographically ordered.</para>
    /// </remarks>
    /// <param name="permutation"></param>
    /// <returns>The rank of the <paramref name="permutation"/>.</returns>
    public static int Rank(System.Span<int> permutation)
    {
      var k = permutation.Length;

      var inverse = System.Buffers.ArrayPool<int>.Shared.Rent(k);

      CreateInversePermutation(permutation, inverse);

      var rank = Rank(permutation, inverse, k);

      System.Buffers.ArrayPool<int>.Shared.Return(inverse);

      return rank;
    }

    /// <summary>
    /// <para>Generate a <paramref name="permutation"/> of <paramref name="k"/> from a <paramref name="rank"/> in O(n) time.</para>
    /// <para><see href="https://stackoverflow.com/a/26197620"/></para>
    /// <para><see href="https://webhome.cs.uvic.ca/~ruskey/Publications/RankPerm/MyrvoldRuskey.pdf"/></para>
    /// </summary>
    /// <remarks>
    /// <para>Not lexiographically ordered.</para>
    /// </remarks>
    /// <param name="permutation">identity permutation of n elements [0, 1, ..., n]</param>
    /// <param name="rank">desired rank</param>
    /// <param name="k">length(permutation)</param>
    public static void Unrank(int rank, System.Span<int> permutation, int k)
    {
      /*
Initialize:
n = length(permutation)
r = desired rank
p = identity permutation of n elements [0, 1, ..., n]

unrank(n, r, p)
  if n > 0 then
    swap(p[n-1], p[r mod n])
    unrank(n-1, floor(r/n), p)
  fi
end
      */

      if (k > 0)
      {
        var plM1 = k - 1;

        permutation.Swap(plM1, rank % k);

        Unrank(rank / k, permutation, plM1);
      }
    }

    /// <summary>
    /// <para>Generate a <paramref name="permutation"/> from a <paramref name="rank"/> in O(n) time.</para>
    /// <para><see href="https://stackoverflow.com/a/26197620"/></para>
    /// <para><see href="https://webhome.cs.uvic.ca/~ruskey/Publications/RankPerm/MyrvoldRuskey.pdf"/></para>
    /// </summary>
    /// <remarks>
    /// <para>Not lexiographically ordered.</para>
    /// </remarks>
    /// <param name="permutation">identity permutation of n elements [0, 1, ..., n]</param>
    /// <param name="rank">desired rank</param>
    public static void Unrank(int rank, System.Span<int> permutation)
    {
      var k = permutation.Length;

      CreateIdentityPermutation(permutation);

      Unrank(rank, permutation, k);
    }
  }
}
