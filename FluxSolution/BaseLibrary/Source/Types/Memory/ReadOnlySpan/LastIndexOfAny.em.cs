namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the specified comparer.</summary>
    public static int LastIndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] values)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (equalityComparer.Equals(source[sourceIndex], values[valueIndex]))
            return sourceIndex;

      return -1;
    }
    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the default comparer</summary>
    public static int LastIndexOfAny<T>(this System.ReadOnlySpan<T> source, params T[] values)
      => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
