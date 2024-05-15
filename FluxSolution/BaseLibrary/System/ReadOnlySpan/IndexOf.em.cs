namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reports the first index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
    /// </summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
          return index;

      return -1;
    }

    /// <summary>
    /// <para>Reports the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return IndexOf(source, t => equalityComparer.Equals(t, value));
    }

    /// <summary>
    /// <para>Returns the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var maxIndex = source.Length - value.Length;

      for (var index = 0; index < maxIndex; index++)
        if (EqualsAt(source, index, value, 0, value.Length, equalityComparer))
          return index;

      return -1;
    }
  }
}
