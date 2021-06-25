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

    /// <summary>Reports the first index of any of the specified targets within the source, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.Span<char> source, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        if (IndexOf(source, values[valueIndex], comparer) is var index && index > -1)
          return index;

      return -1;
    }
    /// <summary>Reports the first index of any of the specified targets within the source, or -1 if none were found. Uses the default comparer.</summary>
    public static int IndexOfAny(this System.Span<char> source, params string[] values)
      => IndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);
  }
}
