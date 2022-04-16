namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Returns the min and max T from the source as output parameters.</summary>
    public static void CreateMinMax<T>(this System.ReadOnlySpan<T> source, out T min, out T max)
      where T : System.IComparable<T>
    {
      if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

      min = max = source[0];

      UpdateMinMax(source, ref min, ref max);
    }

    /// <summary>Returns the min and max T from the source, taking into account the already existing values in min and max. The reference fields min and max must be preset.</summary>
    public static void UpdateMinMax<T>(this System.ReadOnlySpan<T> source, ref T min, ref T max)
      where T : System.IComparable<T>
    {
      if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

      for (var i = source.Length - 1; i >= 0; i--)
      {
        var t = source[i];

        if (t.CompareTo(min) < 0)
          min = t;
        if (t.CompareTo(max) > 0)
          max = t;
      }
    }
  }
}
