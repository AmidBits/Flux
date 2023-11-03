namespace Flux
{
  public static partial class Permutation
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// Creates a list of the subsets of size K selected from a set of size N.
    /// <para><seealso href="https://github.com/sleeepyjack/alg515"/></para>
    /// <para><seealso href="https://stackoverflow.com/questions/561/how-to-use-combinations-of-sets-as-test-data#794"/></para>
    /// <para><seealso href="https://people.math.sc.edu/Burkardt/f_src/toms515/toms515.html"/></para>
    /// </summary>
    /// <param name="n">The size (N) of the entire set.</param>
    /// <param name="p">The subset size (K).</param>
    /// <param name="l">The index of the subset (or index array) to generate.</param>
    /// <returns></returns>
    public static TSelf[] PermuteAlgorithm515<TSelf>(TSelf n, TSelf p, TSelf l)
       where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var c = new TSelf[int.CreateChecked(p)];

      TSelf x = TSelf.Zero, r, k = TSelf.Zero;

      for (var i = TSelf.One; i < p; i++)
      {
        do
        {
          x++;
          r = Maths.BinomialCoefficient(n - x, p - i);
          k += r;
        }
        while (k <= l);

        k -= r;
        c[int.CreateChecked(i - TSelf.One)] = x - TSelf.One;
      }

      c[int.CreateChecked(p - TSelf.One)] = x + l - k;

      return c;
    }

#else

    /// <summary>
    /// Permutation indices of algorithm 515.
    /// <para><see href="https://github.com/sleeepyjack/alg515"/></para>
    /// <para><seealso href="https://stackoverflow.com/questions/561/how-to-use-combinations-of-sets-as-test-data#794"/></para>
    /// </summary>
    /// <param name="n"></param>
    /// <param name="p"></param>
    /// <param name="l"></param>
    /// <returns></returns>
    public static int[] PermuteAlgorithm515(int n, int p, int l)
    {
      var c = new int[p];

      int x = 0, r, k = 0;

      for (var i = 1; i < p; i++)
      {
        do
        {
          x++;
          r = (int)Maths.BinomialCoefficient(n - x, p - i);
          k += r;
        }
        while (k <= l);

        k -= r;
        c[i - 1] = x - 1;
      }

      c[p - 1] = x + l - k;

      return c;
    }

#endif
  }
}
