namespace Flux.Permutations
{
  /// <summary>
  /// <para>Permutations without repetitions.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Lehmer_code"/></para>
  /// <para><seealso href="https://stackoverflow.com/a/24257996"/></para>
  /// </summary>
  public static class LehmerCode
  {
    public static int GetRadixes(System.Span<int> radices, int n)
    {
      radices[0] = n;
      for (var i = 1; i < radices.Length; i++)
        radices[i] = radices[i - 1] - 1;

      return IBinaryInteger.CountPermutationsWithoutRepetition(n, radices.Length);
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="alphabet"></param>
    /// <param name="rank"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<T> NthPermutation<T>(T[] alphabet, int rank, int k)
    {
      var source = new System.Collections.Generic.List<T>(alphabet);
      var target = new System.Collections.Generic.List<T>();
      while (--k >= 0)
      {
        (rank, var index) = int.DivRem(rank, source.Count);
        target.Add(source[index]);
        source.RemoveAt(index);
      }
      return target;
    }

    /// <summary>
    /// <para>A.k.a. a pack function.</para>
    /// <para><see href="https://stackoverflow.com/a/24257996"/></para>
    /// </summary>
    /// <param name="digits"></param>
    /// <param name="radixes"></param>
    /// <returns></returns>
    public static int Pack(System.ReadOnlySpan<int> digits, System.ReadOnlySpan<int> radixes)
    {
      var n = 0;
      for (var i = 0; i < digits.Length; i++)
        n = n * radixes[i] + digits[i];
      return n;
    }

    /// <summary>
    /// <para>A.k.a. an unpack function.</para>
    /// <para><see href="https://stackoverflow.com/a/24257996"/></para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="radixes"></param>
    /// <param name="digits"></param>
    public static void Unpack(int index, System.ReadOnlySpan<int> radixes, System.Span<int> digits)
    {
      for (var i = radixes.Length - 1; i >= 0; i--)
      {
        digits[i] = index % radixes[i];
        index /= radixes[i];
      }
    }
  }
}
