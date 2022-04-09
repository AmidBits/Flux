namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static double EmpiricalQuartile(this System.Collections.Generic.IList<double> source, double p)
    {
      var a = p * (source.Count + 1);
      var k = System.Convert.ToInt32(System.Math.Truncate(a));

      a -= k;

      var c = source[k - 1];

      return c + a * (source[k] - c);
    }

    public static (double q1, double q2, double q3) QuartileMethod1(this System.Collections.Generic.IList<double> source)
    {
      var m2 = source.Count / 2;

      var e2 = (source.Count & 1) == 0;
      var q2 = e2 ? (source[m2 - 1] + source[m2]) / 2 : source[m2];

      var m2Even = (m2 & 1) == 1;

      var m1 = m2 / 2;
      var m3 = m2 + m1 ;

      if (m2Even) // If m2 is odd:
        return (source[m1], q2, source[m3]);
      else // Else is even:
        return ((source[m1] + source[m1 + 1]) / 2, q2, (source[m3] + source[m3 + 1]) / 2);
    }

    public static (double q1, double q2, double q3) QuartileMethod2(this System.Collections.Generic.IList<double> source)
    {
      var m2 = source.Count / 2;

      var odd = (source.Count & 1) == 1;

      var q2 = odd ? source[m2] : (source[m2] + source[m2 + 1]) / 2;

      var m1 = m2 / 2;
      var m3 = m2 + m1;

      if (odd)
      {
        // m1 -= 1;
        m2 += 1;
      }

      if ((m2 & 1) == 1) // If m2 is odd:
        return (source[m1], q2, source[m3 + 1]);
      else // Else is even:
        return ((source[m1] + source[m1 + 1]) / 2, q2, (source[m3] + source[m3 + 1]) / 2);
    }

    /// <summary>General function to compute empirical quantiles.</summary>
    public static (double q1, double q2, double q3) QuartileMethod4(this System.Collections.Generic.IList<double> source)
      => (EmpiricalQuartile(source, 0.25), EmpiricalQuartile(source, 0.50), EmpiricalQuartile(source, 0.75));

    /// <summary>Calculate the quartiles of the ordered values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quartile"/>
    public static (double q1, double q2, double q3) Quartiles(this System.Collections.Generic.IList<double> source)
    {
      var count = source.Count;
      var indexQ2 = count / 2;

      var q1 = 0.0;
      var q2 = 0.0;
      var q3 = 0.0;

      if (count % 2 == 0) // Even count.
      {
        q2 = (source[indexQ2 - 1] + source[indexQ2]) / 2;

        var indexQ13 = indexQ2 / 2;

        if (indexQ2 % 2 == 0) // Even split.
        {
          q1 = (source[indexQ13 - 1] + source[indexQ13]) / 2;
          q3 = (source[indexQ2 + indexQ13 - 1] + source[indexQ2 + indexQ13]) / 2;
        }
        else // Odd split.
        {
          q1 = source[indexQ13];
          q3 = source[indexQ13 + indexQ2];
        }
      }
      else if (count == 1) // Special case.
      {
        q1 = q2 = q3 = source[0];
      }
      else // Odd count.
      {
        q2 = source[indexQ2];

        if ((count - 1) % 4 == 0) // Count = (4n-1).
        {
          var n = (count - 1) / 4;
          q1 = (source[n - 1] * 0.25) + (source[n] * 0.75);
          q3 = (source[3 * n] * 0.75) + (source[3 * n + 1] * 0.25);
        }
        else if ((count - 3) % 4 == 0) // Count = (4n-3).
        {
          var n = (count - 3) / 4;
          q1 = (source[n] * 0.75) + (source[n + 1] * 0.25);
          q3 = (source[3 * n + 1] * 0.25) + (source[3 * n + 2] * 0.75);
        }
      }

      return (q1, q2, q3);
    }
    public static (double q1, double q2, double q3) Quartiles(this System.Collections.Generic.IEnumerable<double> source)
      => Quartiles(System.Linq.Enumerable.ToList(System.Linq.Enumerable.OrderBy(source, k => k)));
  }
}
