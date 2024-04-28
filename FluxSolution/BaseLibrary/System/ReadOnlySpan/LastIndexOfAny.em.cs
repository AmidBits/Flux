namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the specified comparer (null for default).</summary>
    public static int LastIndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
          if (equalityComparer.Equals(source[sourceIndex], values[valuesIndex]))
            return sourceIndex;

      return -1;
    }
  }
}
