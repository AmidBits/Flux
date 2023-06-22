using System.Linq;

namespace Flux.Statistics
{
  /// <summary>
  /// <para>The values found by this method are also known as "Tukey's hinges".</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_2"/></para>
  /// </summary>
  public record class QuartileMethod2
    : IQuartileComputable
  {
    public (double q1, double q2, double q3) ComputeQuartiles(System.Collections.Generic.IEnumerable<double> sample)
      => Compute(sample);

    public static (double q1, double q2, double q3) Compute(System.Collections.Generic.IEnumerable<double> sample)
    {
      var sampleCount = sample.Count();

      var o2 = (sampleCount & 1) == 1;

      var m2 = sampleCount / 2;
      var q2 = o2 ? sample.ElementAt(m2) : (sample.ElementAt(m2 - 1) + sample.ElementAt(m2)) / 2;

      if (o2) m2 += 1; // If odd counts, include median in both halfs.

      o2 = (m2 & 1) == 1;

      var m1 = m2 / 2;
      var q1 = o2 ? sample.ElementAt(m1) : (sample.ElementAt(m1 - 1) + sample.ElementAt(m1)) / 2;

      var m3 = sampleCount - (m2 - m1);
      var q3 = o2 ? sample.ElementAt(m3) : (sample.ElementAt(m3 - 1) + sample.ElementAt(m3)) / 2;

      return (q1, q2, q3);
    }
  }
}
