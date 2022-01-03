namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the specified comparer.</summary>
    public static int LastIndexOfAny<T>(this System.Span<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (comparer.Equals(source[sourceIndex], values[valueIndex]))
            return sourceIndex;

      return -1;
    }
    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the default comparer</summary>
    public static int LastIndexOfAny<T>(this System.Span<T> source, params T[] values)
      => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
