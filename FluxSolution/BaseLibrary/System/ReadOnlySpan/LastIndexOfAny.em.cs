namespace Flux
{
  public static partial class Fx
  {
    public static int LastIndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params System.Func<T, bool>[] predicates)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var predicatesLength = predicates.Length;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var predicateIndex = 0; predicateIndex < predicatesLength; predicateIndex++)
          if (predicates[predicateIndex](source[sourceIndex]))
            return sourceIndex;

      return -1;
    }

    /// <summary>
    /// <para>Reports the last index of any of the specified <paramref name="values"/> in the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static int LastIndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var valuesLength = values.Length;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valuesIndex = 0; valuesIndex < valuesLength; valuesIndex++)
          if (equalityComparer.Equals(source[sourceIndex], values[valuesIndex]))
            return sourceIndex;

      return -1;
    }
  }
}
