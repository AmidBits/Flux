#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the statistics zample.</summary>
    public static void RunStatistics()
    {
      System.Console.WriteLine(nameof(RunStatistics));
      System.Console.WriteLine();

      var a = new System.Collections.Generic.List<double>() { 6, 7, 15, 36, 39, 40, 41, 42, 43, 47, 49 };
      System.Console.WriteLine($"For {{{string.Join(", ", a)}}}:");
      System.Console.WriteLine();

      RunQuantiles(a);
      System.Console.WriteLine();
      RunQuartiles(a);
      System.Console.WriteLine();

      var b = new System.Collections.Generic.List<double>() { 7, 15, 36, 39, 40, 41 };
      System.Console.WriteLine($"For {{{string.Join(", ", b)}}}:");
      System.Console.WriteLine();

      RunQuantiles(b);
      System.Console.WriteLine();
      RunQuartiles(b);
      System.Console.WriteLine();
    }

    static void RunQuartiles(System.Collections.Generic.List<double> x)
    {
      System.Console.WriteLine($"The computed quartiles:");
      System.Console.WriteLine($"Method 1: {new Flux.Numerics.QuartileMethod1().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 2: {new Flux.Numerics.QuartileMethod2().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 3: {new Flux.Numerics.QuartileMethod3().ComputeQuartiles(x)}");
      System.Console.WriteLine($"Method 4: {new Flux.Numerics.QuartileMethod4().ComputeQuartiles(x)}");
    }

    static void RunQuantiles(System.Collections.Generic.List<double> x)
    {
      for (var q = 0.25; q < 1; q += 0.25)
        RunQuantiles(x, q);

      static void RunQuantiles(System.Collections.Generic.List<double> x, double p)
      {
        var values = new double[]
        {
          Numerics.QuantileR1.Default.EstimateQuantileValue(x, p),
          Numerics.QuantileR2.Default.EstimateQuantileValue(x, p),
          Numerics.QuantileR3.Default.EstimateQuantileValue(x, p),
          Numerics.QuantileR4.Default.EstimateQuantileValue(x, p),
          Numerics.QuantileR5.Default.EstimateQuantileValue(x, p),
          Numerics.QuantileR6.Default.EstimateQuantileValue(x, p),
          Numerics.QuantileR7.Default.EstimateQuantileValue(x, p),
          Numerics.QuantileR8.Default.EstimateQuantileValue(x, p),
          Numerics.QuantileR9.Default.EstimateQuantileValue(x, p),
        };

        System.Console.WriteLine($"The estimated quantiles of {p:N2}:");
        System.Console.WriteLine($"{string.Join("\t", System.Linq.Enumerable.Range(0, values.Length).Select((e, i) => $"R{(i + 1)} = {values[i]:N2}"))}");
        System.Console.WriteLine($"(Average: {values.Average()})");
      }
    }

    static void RunStats()
    {
      var a = new int[] { 13, 12, 11, 8, 4, 3, 2, 1, 1, 1 };
      a = a.Reverse().ToArray();
      //a = new int[] { 1, 2, 3, 6, 6, 6, 7, 8, 9 };
      //a = new int[] { 1, 2, 3, 4, 4, 5, 6, 7, 8 };
      //a = new int[] { 2, 4, 5, 6, 6, 7, 9, 12, 14, 15, 18, 19, 22, 24, 26, 28 };
      //a = new int[] { 3, 6, 7, 8, 8, 10, 13, 15, 16, 20 };
      //  a = new int[] { 15, 20, 35, 40, 50 };
      //a = new Flux.Randomization.Xoshiro256SS().GetRandomInt32s(0, 20).Take(200).OrderBy(k => k).ToArray();

      //var hg = new Histogram<int>();
      //foreach (var k in a)
      //  hg[k] = hg.TryGetValue(k, out var v) ? v + 1 : 1;

      var h = a.ToHistogram(e => e, e => 1);
      System.Console.WriteLine("H:");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, h));
      System.Console.WriteLine();

      var pmf = h.ToProbabilityMassFunction(1d);
      System.Console.WriteLine("PMF:");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, pmf));
      System.Console.WriteLine();

      var cdf = h.ToCumulativeDistributionFunction(1d);
      System.Console.WriteLine("CDF:");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, cdf));
      System.Console.WriteLine();

      //var pr = h.PercentRank(sof);
      //System.Console.WriteLine("PR:");
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, pr));
      //System.Console.WriteLine();

      //var pl = h.CumulativeMassFunction(kvp => kvp.Key, kvp => kvp.Value, 100.0);
      //System.Console.WriteLine("PL:");
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, pl));
      //System.Console.WriteLine();
    }
  }
}
#endif
