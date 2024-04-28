namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the specified comparer (null for default).</summary>
    public static int IndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
        for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
          if (equalityComparer.Equals(source[sourceIndex], values[valuesIndex]))
            return sourceIndex;

      return -1;
    }
  }
}
