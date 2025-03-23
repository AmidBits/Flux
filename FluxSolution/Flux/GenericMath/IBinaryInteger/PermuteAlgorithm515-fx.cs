namespace Flux
{
  public static partial class GenericMath
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
    /// <example>
    ///var(n, p) = (5, 3);
    ///  var nCp = n.BinomialCoefficient(p);

    ///System.Console.WriteLine($"Combinations = {nCp}");

    ///  foreach (var l in System.Linq.Enumerable.Range(0, nCp))
    ///    System.Console.WriteLine($"{l} : [{string.Join(", ", n.PermuteAlgorithm515b(p, l))}]");
    ///    
    /// # of distinct combinations: 10
    /// combinations:
    ///[0, 1, 2]
    ///[0, 1, 3]
    ///[0, 1, 4]
    ///[0, 2, 3]
    ///[0, 2, 4]
    ///[0, 3, 4]
    ///[1, 2, 3]
    ///[1, 2, 4]
    ///[1, 3, 4]
    ///[2, 3, 4]
    /// </example>
    public static TNumber[] PermuteAlgorithm515b<TNumber>(this TNumber N, TNumber P, TNumber L)
       where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var C = new TNumber[int.CreateChecked(P)];
      var X = TNumber.One;
      var R = (N - X).BinomialCoefficient(P - TNumber.One);
      var K = R;

      while (K <= L)
      {
        X += TNumber.One;
        R = (N - X).BinomialCoefficient(P - TNumber.One);
        K += R;
      }
      K -= R;
      C[0] = X - TNumber.One;

      for (var I = TNumber.CreateChecked(2); I < P; I++)
      {
        X += TNumber.One;
        R = (N - X).BinomialCoefficient(P - I);
        K += R;

        while (K <= L)
        {
          X += TNumber.One;
          R = (N - X).BinomialCoefficient(P - I);
          K += R;
        }

        K -= R;

        C[int.CreateChecked(I - TNumber.One)] = X - TNumber.One;
      }

      C[int.CreateChecked(P - TNumber.One)] = X + L - K;

      return C;
    }

    // Not work!
    private static TNumber[] PermuteAlgorithm515<TNumber>(this TNumber setSize, TNumber subsetSize, TNumber index)
       where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var p = int.CreateChecked(subsetSize);

      var c = new TNumber[p];

      TNumber r;

      var k = TNumber.Zero;

      for (var i = 1; i < p - 1; i++)
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
