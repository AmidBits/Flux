using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Calculate the mean of a sequence, also return the count and the sum of values in the sequence as output parameters.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mean"/>
    public static double Mean(this System.Collections.Generic.IEnumerable<double> source, out int count, out double sum)
    {
      count = 0;
      sum = 0;

      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        count++;
        sum += item;
      }

      return count >= 1 ? sum / count : 0;
    }
    /// <summary>Calculate the mean of a sequence, also return the count and the sum of values in the sequence as output parameters.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mean"/>
    public static double Mean(this System.Collections.Generic.IEnumerable<double> source)
      => Mean(source, out int _, out double _);

    /// <summary>Calculate the mean of a sequence, also return the sum and a list of the values in the sequence as output parameters.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mean"/>
    public static double Mean(this System.Collections.Generic.IEnumerable<double> source, out System.Collections.Generic.List<double> values, out double sum)
    {
      sum = 0;
      values = new System.Collections.Generic.List<double>();

      foreach (var element in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        sum += element;
        values.Add(element);
      }

      return values.Any() ? sum / values.Count : 0;
    }
    /// <summary>Calculate the mean of a sequence, also return the sum and a list of the values in the sequence as output parameters.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mean"/>
    public static double Mean(this System.Collections.Generic.IEnumerable<double> source, out System.Collections.Generic.List<double> values)
      => Mean(source, out values, out double _);
  }
}
