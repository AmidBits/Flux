namespace Flux
{
  public static partial class ExtensionMethods
  {
    public enum QuartileMethod
    {
      /// <summary>This rule is employed by the TI-83 calculator boxplot and "1-Var Stats" functions.</summary>
      Method1,
      /// <summary>The values found by this method are also known as "Tukey's hinges".</summary>
      Method2,
      /// <summary>This method is not implemented.</summary>
      //Method3,
      /// <summary>This interpolates between data points to find the pth empirical quantile</summary>
      Method4,
    }

    public static double EmpiricalQuartile(this System.Collections.Generic.IList<double> source, double p)
    {
      var a = p * (source.Count + 1);
      var k = System.Convert.ToInt32(System.Math.Truncate(a));

      a -= k;

      var c = source[k - 1];

      return c + a * (source[k] - c);
    }

    //public static double Quartile(this System.Collections.Generic.List<double> source, double rthQuartile, double frequencyCount, double cumulativeFrequency)
    //  => source[0] + ((rthQuartile * (source.Count / 4) - cumulativeFrequency) / frequencyCount) * (source[^1] - source[0]);

    ///// <summary>Calculate the quartiles of the ordered values.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Quartile"/>
    //public static (double q1, double q2, double q3) Quartiles(this System.Collections.Generic.IList<double> source)
    //{
    //  var count = source.Count;
    //  var indexQ2 = count / 2;

    //  var q1 = 0.0;
    //  var q2 = 0.0;
    //  var q3 = 0.0;

    //  if (count % 2 == 0) // Even count.
    //  {
    //    q2 = (source[indexQ2 - 1] + source[indexQ2]) / 2;

    //    var indexQ13 = indexQ2 / 2;

    //    if (indexQ2 % 2 == 0) // Even split.
    //    {
    //      q1 = (source[indexQ13 - 1] + source[indexQ13]) / 2;
    //      q3 = (source[indexQ2 + indexQ13 - 1] + source[indexQ2 + indexQ13]) / 2;
    //    }
    //    else // Odd split.
    //    {
    //      q1 = source[indexQ13];
    //      q3 = source[indexQ13 + indexQ2];
    //    }
    //  }
    //  else if (count == 1) // Special case.
    //  {
    //    q1 = q2 = q3 = source[0];
    //  }
    //  else // Odd count.
    //  {
    //    q2 = source[indexQ2];

    //    if ((count - 1) % 4 == 0) // Count = (4n-1).
    //    {
    //      var n = (count - 1) / 4;
    //      q1 = (source[n - 1] * 0.25) + (source[n] * 0.75);
    //      q3 = (source[3 * n] * 0.75) + (source[3 * n + 1] * 0.25);
    //    }
    //    else if ((count - 3) % 4 == 0) // Count = (4n-3).
    //    {
    //      var n = (count - 3) / 4;
    //      q1 = (source[n] * 0.75) + (source[n + 1] * 0.25);
    //      q3 = (source[3 * n + 1] * 0.25) + (source[3 * n + 2] * 0.75);
    //    }
    //  }

    //  return (q1, q2, q3);
    //}
    //public static (double q1, double q2, double q3) Quartiles(this System.Collections.Generic.IEnumerable<double> source)
    //  => Quartiles(System.Linq.Enumerable.ToList(System.Linq.Enumerable.OrderBy(source, k => k)));

    public static (double q1, double q2, double q3) Quartiles(this System.Collections.Generic.IList<double> source, QuartileMethod method)
      => method switch
      {
        QuartileMethod.Method1 => QuartilesMethod1(source),
        QuartileMethod.Method2 => QuartilesMethod2(source),
        //QuartileMethod.Method3 => QuartilesMethod3(source),
        QuartileMethod.Method4 => QuartilesMethod4(source),
        _ => throw new System.ArgumentOutOfRangeException(nameof(method)),
      };
    public static (double q1, double q2, double q3) Quartiles(this System.Collections.Generic.IList<int> source, QuartileMethod method)
      => Quartiles(System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(source, int32 => (double)int32)), method);

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quartile"/>
    public static (double q1, double q2, double q3) QuartilesMethod1(this System.Collections.Generic.IList<double> source)
    {
      var e2 = (source.Count & 1) == 0;

      var m2 = source.Count / 2;
      var q2 = e2 ? (source[m2 - 1] + source[m2]) / 2 : source[m2];

      var o2 = (m2 & 1) == 1;

      var m1 = m2 / 2;
      var q1 = o2 ? source[m1] : (source[m1] + source[m1 + 1]) / 2;

      var m3 = source.Count - (m2 - m1);
      var q3 = o2 ? source[m3] : (source[m3 - 1] + source[m3]) / 2;

      return (q1, q2, q3);
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quartile"/>
    public static (double q1, double q2, double q3) QuartilesMethod2(this System.Collections.Generic.IList<double> source)
    {
      var o2 = (source.Count & 1) == 1;

      var m2 = source.Count / 2;
      var q2 = o2 ? source[m2] : (source[m2 - 1] + source[m2]) / 2;

      if (o2) m2 += 1; // If odd counts, include median in both halfs.

      o2 = (m2 & 1) == 1;

      var m1 = m2 / 2;
      var q1 = o2 ? source[m1] : (source[m1 - 1] + source[m1]) / 2;

      var m3 = source.Count - (m2 - m1);
      var q3 = o2 ? source[m3] : (source[m3 - 1] + source[m3]) / 2;

      return (q1, q2, q3);
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quartile"/>
    public static (double q1, double q2, double q3) QuartilesMethod3(this System.Collections.Generic.IList<double> source)
      => throw new System.NotImplementedException();

    /// <summary>General function to compute empirical quartiles.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quartile"/>
    public static (double q1, double q2, double q3) QuartilesMethod4(this System.Collections.Generic.IList<double> source)
    => (EmpiricalQuartile(source, 0.25), EmpiricalQuartile(source, 0.50), EmpiricalQuartile(source, 0.75));
  }
}
