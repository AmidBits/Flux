using System;

namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer (null for default).</summary>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = source.Length - 1; index >= 0; index--)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }

    /// <summary>Returns the last index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = source.Length - value.Length; index >= 0; index--)
        if (EqualsAt(source, index, value, 0, value.Length, equalityComparer))
          return index;

      return -1;
    }
  }
}
