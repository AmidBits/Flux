using System.Security.Cryptography;

namespace Flux
{
  public static partial class Permutation
  {
    /// <summary>
    /// <para>Find the <paramref name="index"/>th (1-based) lexicographically ordered subset of <paramref name="subsetSize"/> (K) elements in set <paramref name="setSize"/> (N).</para>
    /// <para><seealso href="https://github.com/sleeepyjack/alg515"/></para>
    /// <para><seealso href="https://stackoverflow.com/questions/561/how-to-use-combinations-of-sets-as-test-data#794"/></para>
    /// <para><seealso href="https://people.math.sc.edu/Burkardt/f_src/toms515/toms515.html"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="setSize">The size (N) of the entire set.</param>
    /// <param name="subsetSize">The subset size (K).</param>
    /// <param name="index">The 1-based index of the subset (or index array) to generate.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>Note that <paramref name="index"/> is 1-based.</para>
    /// <para>The <paramref name="setSize"/> is the <see langword="this"/> argument for the extension method.</para>
    /// </remarks>
    public static TNumber[] PermuteAlgorithm515<TNumber>(this TNumber setSize, TNumber subsetSize, TNumber index)
       where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var p = int.CreateChecked(subsetSize);

      var c = new TNumber[p];

      TNumber r;

      var k = TNumber.Zero;

      for (var i = 0; i < p - 1; i++)
      {
        c[i] = (i != 0) ? c[i - 1] : TNumber.Zero;

        do
        {
          c[i]++;
          r = (setSize - c[i]).BinomialCoefficient(subsetSize - TNumber.CreateChecked(i + 1));
          k = k + r;
        }
        while (k < index);

        k = k - r;
      }

      c[p - 1] = c[p - 2] + index - k;

      return c;
    }
  }
}
