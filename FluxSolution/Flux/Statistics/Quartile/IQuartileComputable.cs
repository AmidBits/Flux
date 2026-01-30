//namespace Flux.Statistics.Quartile
//{
//  /// <summary>
//  /// A quartile is a type of quantile which divides the number of data points into four parts, or quarters, of more-or-less equal size. The data must be ordered from smallest to largest to compute quartiles; as such, quartiles are a form of order statistic. The three main quartiles are as follows:
//  /// <list type="bullet">
//  /// <item>The first quartile (Q1) is defined as the middle number between the smallest number (minimum) and the median of the data set. It is also known as the lower or 25th empirical quartile, as 25% of the data is below this point.</item>
//  /// <item>The second quartile (Q2) is the median of a data set; thus 50% of the data lies below this point.</item>
//  /// <item>The third quartile (Q3) is the middle value between the median and the highest value (maximum) of the data set. It is known as the upper or 75th empirical quartile, as 75% of the data lies below this point.</item>
//  /// </list>
//  /// <para><see href="https://en.wikipedia.org/wiki/Quartile"/></para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Discrete_distributions"/></para>
//  /// </summary>
//  public interface IQuartileComputable
//  {
//    /// <summary>Computes the three main quartiles Q1, Q2 and Q3 according to the implementation.</summary>
//    /// <returns>A ValueTuple with Q1, Q2 and Q3.</returns>
//    (double q1, double q2, double q3) ComputeQuartiles(System.Collections.Generic.IEnumerable<double> sample);
//  }
//}
