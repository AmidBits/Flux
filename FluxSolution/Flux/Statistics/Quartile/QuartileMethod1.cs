//namespace Flux.Statistics.Quartile
//{
//  /// <summary>
//  /// <para>This rule is employed by the TI-83 calculator boxplot and "1-Var Stats" functions.</para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_1"/></para>
//  /// </summary>
//  public record class Method1
//    : IQuartileComputable
//  {
//    public static IQuartileComputable Default => new Method1();

//    public (double q1, double q2, double q3) ComputeQuartiles(System.Collections.Generic.IEnumerable<double> sample)
//      => Compute(sample);

//    public static (double q1, double q2, double q3) Compute(System.Collections.Generic.IEnumerable<double> sample)
//    {
//      var sampleCount = sample.Count();

//      var m2 = sampleCount / 2;
//      var q2 = (sampleCount & 1) == 0 ? (sample.ElementAt(m2 - 1) + sample.ElementAt(m2)) / 2 : sample.ElementAt(m2);

//      var o2 = (m2 & 1) == 1;

//      var m1 = m2 / 2;
//      var q1 = o2 ? sample.ElementAt(m1) : (sample.ElementAt(m1) + sample.ElementAt(m1 + 1)) / 2;

//      var m3 = sampleCount - (m2 - m1);
//      var q3 = o2 ? sample.ElementAt(m3) : (sample.ElementAt(m3 - 1) + sample.ElementAt(m3)) / 2;

//      return (q1, q2, q3);
//    }
//  }
//}
