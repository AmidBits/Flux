#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the statistics zample.</summary>
    static void RunStatistics()
    {
      System.Console.WriteLine(nameof(RunStatistics));
      System.Console.WriteLine();

      var b = new System.Collections.Generic.List<double>() { 7, 15, 36, 39, 40, 41 };
      var a = new System.Collections.Generic.List<double>() { 6, 7, 15, 36, 39, 40, 41, 42, 43, 47, 49 };

      RunQuantiles(a);
      System.Console.WriteLine();
      RunQuartiles(a);
      System.Console.WriteLine();

      RunQuantiles(b);
      System.Console.WriteLine();
      RunQuartiles(b);
    }

    static void RunQuartiles(System.Collections.Generic.List<double> x)
    {
      System.Console.WriteLine($"The four quartiles of {{{string.Join(", ", x)}}} are:");
      System.Console.WriteLine($"Method 1: {Flux.Quartiles.GetQuartilesMethod1(x)}");
      System.Console.WriteLine($"Method 2: {Flux.Quartiles.GetQuartilesMethod2(x)}");
      //System.Console.WriteLine($"Method 3: {Flux.Quartiles.GetQuartilesMethod3(x)}");
      System.Console.WriteLine($"Method 4: {Flux.Quartiles.GetQuartilesMethod4(x)}");
    }

    static void RunQuantiles(System.Collections.Generic.List<double> x)
    {
      for (var q = 0.25; q < 1; q += 0.25)
        RunQuantiles(x, q);

      static void RunQuantiles(System.Collections.Generic.List<double> x, double p)
      {
        var values = new double[]
        {
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R1),
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R2),
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R3),
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R4),
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R5),
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R6),
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R7),
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R8),
          Quantiles.Quantile(x, p, Quantiles.QuantileType.R9),
        };

        System.Console.WriteLine($"The estimated quantiles of {p:N2} for {{{string.Join(", ", x)}}} are:");
        System.Console.WriteLine($"{string.Join(", ", System.Linq.Enumerable.Range(0, values.Length).Select((e, i) => $"R{i + 1} = {values[i]}"))}");
        System.Console.WriteLine($"(Average: {values.Average()})");
      }
    }
  }
}
#endif
