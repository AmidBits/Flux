namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reports the first index of any of the <paramref name="values"/> in <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
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
