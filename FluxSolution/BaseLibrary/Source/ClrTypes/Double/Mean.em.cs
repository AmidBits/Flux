namespace Flux
{
  public static partial class XtensionsDouble
  {
    /// <summary>Calculate the mean of a sequence.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mean"/>
    public static double Mean(this System.Collections.Generic.IEnumerable<double> source, out int count, out double sum)
    {
      count = 0;
      sum = 0.0;

      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        count++;

        sum += item;
      }

      return count >= 1 ? sum / count : throw new System.ArgumentException(@"The sequence must contain at least one element.");
    }

    /// <summary>Calculate the mean of a sequence.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mean"/>
    public static double Mean(this System.Collections.Generic.IEnumerable<double> source)
      => source.Mean(out var _, out var _);
  }
}
