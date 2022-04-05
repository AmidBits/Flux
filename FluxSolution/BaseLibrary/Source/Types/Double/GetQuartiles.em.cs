namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Calculate the quartiles of the ordered values.</summary>
    public static (double q1, double q2, double q3) GetQuartiles(this System.Collections.Generic.IList<double> source)
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
    public static (double q1, double q2, double q3) GetQuartiles(this System.Collections.Generic.IEnumerable<double> source)
      => GetQuartiles(System.Linq.Enumerable.ToList(System.Linq.Enumerable.OrderBy(source, k => k)));
  }
}
