using System.Linq;

namespace Flux
{
  // Miscellaneous extension methods
  static class ExtensionMethodsProbability
  {
    public static string Histogram(this System.Collections.Generic.IEnumerable<double> d, double low, double high)
    {
      const int width = 40;
      const int height = 20;
      const int sampleCount = 100000;
      int[] buckets = new int[width];
      foreach (double c in d.Take(sampleCount))
      {
        int bucket = (int)(buckets.Length * (c - low) / (high - low));
        if (0 <= bucket && bucket < buckets.Length)
          buckets[bucket] += 1;
      }
      int max = buckets.Max();
      double scale = max < height ? 1.0 : ((double)height) / max;
      return System.Linq.Enumerable.Range(0, height).Select(r => buckets.Select(b => b * scale > (height - r) ? '*' : ' ').Concatenated() + "\n").Concatenated() + new string('-', width) + "\n";
    }

    public static string DiscreteHistogram<TKey>(this System.Collections.Generic.IEnumerable<TKey> d)
      where TKey : notnull
    {
      const int sampleCount = 100000;
      const int width = 40;
      var dict = d.Take(sampleCount).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
      int labelMax = dict.Keys.Select(x => x?.ToString()?.Length ?? 0).Max();
      var sup = dict.Keys.OrderBy(ToLabel).ToList();
      int max = dict.Values.Max();
      double scale = max < width ? 1.0 : ((double)width) / max;
      return sup.Select(s => $"{ToLabel(s)}|{Bar(s)}").NewlineSeparated();
      string ToLabel(TKey t) => t?.ToString()?.PadLeft(labelMax) ?? throw new System.NullReferenceException();
      string Bar(TKey t) => new string('*', (int)((dict ?? throw new System.NullReferenceException())[t] * scale));
    }

    public static string Separated<T>(this System.Collections.Generic.IEnumerable<T> items, string s)
      => string.Join(s, items);

    public static string Concatenated<T>(this System.Collections.Generic.IEnumerable<T> items)
      => string.Join("", items);

    public static string CommaSeparated<T>(this System.Collections.Generic.IEnumerable<T> items)
      => items.Separated(",");

    public static string NewlineSeparated<T>(this System.Collections.Generic.IEnumerable<T> items)
      => items.Separated("\n");
  }

}
