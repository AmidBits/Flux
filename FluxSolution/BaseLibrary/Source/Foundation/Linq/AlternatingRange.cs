namespace Flux.Linq
{
  public static partial class Enumerable
  {
    /// <summary>Produces a new sequence of numbers starting with at the specified mean, how many numbers and step size, with every other number above/below the mean.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> AlternatingRange(System.Numerics.BigInteger mean, int count, System.Numerics.BigInteger step, AlternatingRangeDirection direction)
    {
      switch (direction)
      {
        case AlternatingRangeDirection.AwayFromMean:
          for (var index = 1; index <= count; index++)
          {
            yield return mean;

            mean += step * index;
            step *= -1;
          }
          break;
        case AlternatingRangeDirection.TowardsMean:
          if ((count & 1) == 1) step *= -1;
          mean += step * (count / 2);

          for (var index = count - 1; index >= 0; index--)
          {
            yield return mean;

            mean -= step * index;
            step *= -1;
          }
          break;
      }
    }

    public static System.Collections.Generic.IEnumerable<decimal> AlternatingRange(decimal mean, int count, decimal step, AlternatingRangeDirection direction)
    {
      switch (direction)
      {
        case AlternatingRangeDirection.AwayFromMean:
          for (var index = 1; index <= count; index++)
          {
            yield return mean;

            mean += step * index;
            step *= -1;
          }
          break;
        case AlternatingRangeDirection.TowardsMean:
          if ((count & 1) == 1) step *= -1;
          mean += step * (count / 2);

          for (var index = count - 1; index >= 0; index--)
          {
            yield return mean;

            mean -= step * index;
            step *= -1;
          }
          break;
      }
    }
    public static System.Collections.Generic.IEnumerable<double> AlternatingRange(double mean, int count, double step, AlternatingRangeDirection direction)
    {
      switch (direction)
      {
        case AlternatingRangeDirection.AwayFromMean:
          for (var index = 1; index <= count; index++)
          {
            yield return mean;

            mean += step * index;
            step *= -1;
          }
          break;
        case AlternatingRangeDirection.TowardsMean:
          if ((count & 1) == 1) step *= -1;
          mean += step * (count / 2);

          for (var index = count - 1; index >= 0; index--)
          {
            yield return mean;

            mean -= step * index;
            step *= -1;
          }
          break;
      }
    }
    public static System.Collections.Generic.IEnumerable<float> AlternatingRange(float mean, int count, float step, AlternatingRangeDirection direction)
    {
      switch (direction)
      {
        case AlternatingRangeDirection.AwayFromMean:
          for (var index = 1; index <= count; index++)
          {
            yield return mean;

            mean += step * index;
            step *= -1;
          }
          break;
        case AlternatingRangeDirection.TowardsMean:
          if ((count & 1) == 1) step *= -1;
          mean += step * (count / 2);

          for (var index = count - 1; index >= 0; index--)
          {
            yield return mean;

            mean -= step * index;
            step *= -1;
          }
          break;
      }
    }

    public static System.Collections.Generic.IEnumerable<int> AlternatingRange(int mean, int count, int step, AlternatingRangeDirection direction)
    {
      switch (direction)
      {
        case AlternatingRangeDirection.AwayFromMean:
          for (var index = 1; index <= count; index++)
          {
            yield return mean;

            mean += step * index;
            step *= -1;
          }
          break;
        case AlternatingRangeDirection.TowardsMean:
          if ((count & 1) == 1) step *= -1;
          mean += step * (count / 2);

          for (var index = count - 1; index >= 0; index--)
          {
            yield return mean;

            mean -= step * index;
            step *= -1;
          }
          break;
      }
    }
    public static System.Collections.Generic.IEnumerable<long> AlternatingRange(long mean, int count, long step, AlternatingRangeDirection direction)
    {
      switch (direction)
      {
        case AlternatingRangeDirection.AwayFromMean:
          for (var index = 1; index <= count; index++)
          {
            yield return mean;

            mean += step * index;
            step *= -1;
          }
          break;
        case AlternatingRangeDirection.TowardsMean:
          if ((count & 1) == 1) step *= -1;
          mean += step * (count / 2);

          for (var index = count - 1; index >= 0; index--)
          {
            yield return mean;

            mean -= step * index;
            step *= -1;
          }
          break;
      }
    }
  }
}

/*
  var seq1 = Flux.Linq.AlternatingRange(5, 12, -3, Linq.AlternatingRangeDirection.AwayFromMean).ToArray();
  System.Console.WriteLine(string.Join(", ", seq1.Select(i => i.ToString("D2"))));
  System.Console.WriteLine();

  var seq1r = Flux.Linq.AlternatingRange(5, 12, -3, Linq.AlternatingRangeDirection.TowardsMean).ToArray();
  System.Console.WriteLine(string.Join(", ", seq1r.Select(i => i.ToString("D2"))));
  System.Console.WriteLine();

  var seq2 = Flux.Linq.AlternatingRange(0, 11, 3, Linq.AlternatingRangeDirection.AwayFromMean).ToArray();
  System.Console.WriteLine(string.Join(", ", seq2.Select(i => i.ToString("D2"))));
  System.Console.WriteLine();

  var seq2r = Flux.Linq.AlternatingRange(0, 11, 3, Linq.AlternatingRangeDirection.TowardsMean).ToArray();
  System.Console.WriteLine(string.Join(", ", seq2r.Select(i => i.ToString("D2"))));
  System.Console.WriteLine();
*/
