namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny<T>(this System.Span<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var index = 0; index < source.Length; index++)
      {
        var character = source[index];

        if (System.Array.Exists(values, c => comparer.Equals(c, character)))
          return index;
      }

      return -1;
    }
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the default comparer.</summary>
    public static int IndexOfAny<T>(this System.Span<T> source, params T[] values)
      => IndexOfAny(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
