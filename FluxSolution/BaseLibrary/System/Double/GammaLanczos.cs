namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Coefficients for Lanczos approximation when (g=7, n=9).</para>
    /// </summary>
    private static double[] p = {
      0.99999999999980993,
      676.5203681218851,
      -1259.1392167224028,
      771.32342877765313,
      -176.61502916214059,
      12.507343278686905,
      -0.13857109526572012,
      9.9843695780195716e-6,
      1.5056327351493116e-7
    };

    /// <summary>
    /// <para>The Gamma function. The (g, n) values are currently hardcoded for (g=7, n=9).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Lanczos_approximation"/></para>
    /// </summary>
    /// <param name="z"></param>
    /// <returns></returns>
    public static double GammaLanczos(this double z)
    {
      var y = 0.0;

      if (z < 0.5)
        y = double.Pi / (double.SinPi(z) * GammaLanczos(1 - z)); // Reflection formula.
      else // Lanczos approximation (invalid for above condition).
      {
        z -= 1;

        var x = p[0];

        for (var i = 1; i < 9; i++) // Changed to: 9 from (g + 2)
          x += p[i] / (z + i);

        var t = z + 7 + 0.5; // Changed to: 7 from (g)

        y = double.Sqrt(2 * double.Pi) * double.Pow(t, z + 0.5) * double.Exp(-t) * x;
      }

      return y;
    }

    public static TSelf GammaLanczos<TSelf>(this TSelf z)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IFloatingPointConstants<TSelf>, System.Numerics.IExponentialFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var y = TSelf.Zero;

      var halfOf1 = TSelf.CreateChecked(0.5);

      if (z < halfOf1)
        y = TSelf.Pi / (TSelf.SinPi(z) * GammaLanczos(TSelf.One - z)); // Reflection formula.
      else // Lanczos approximation (invalid for above condition).
      {
        z -= TSelf.One;

        var x = TSelf.CreateChecked(p[0]);

        for (var i = 1; i < 9; i++) // Changed to: 9 from (g + 2)
          x += TSelf.CreateChecked(p[i]) / (z + TSelf.CreateChecked(i));

        var t = z + TSelf.CreateChecked(7) + halfOf1; // Changed to: 7 from (g)

        y = TSelf.Sqrt(TSelf.Tau) * TSelf.Pow(t, z + halfOf1) * TSelf.Exp(-t) * x;
      }

      return y;
    }
  }
}
