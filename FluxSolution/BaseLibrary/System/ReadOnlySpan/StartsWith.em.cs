namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="count"/> elements that satisfies the <paramref name="predicate"/>.</para>
    /// </summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, int count, System.Func<T, int, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      if (source.Length < count)
        return false;

      for (var index = 0; count > 0; count--, index++)
        if (!predicate(source[index], index))
          return false;

      return true;
    }

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with the <paramref name="target"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var targetIndex = target.Length;

      if (source.Length < targetIndex)
        return false;

      while (--targetIndex >= 0)
        if (!equalityComparer.Equals(source[targetIndex], target[targetIndex]))
          return false;

      return true;
    }
  }
}
