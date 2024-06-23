namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> ends with <paramref name="count"/> elements that satisfy the <paramref name="predicate"/>.</para>
    /// </summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, int count, System.Func<T, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      if (source.Length < count) return false;

      for (var index = source.Length - 1; count > 0; count--, index--)
        if (!predicate(source[index]))
          return false;

      return true;
    }

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> ends with the <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      if (sourceIndex < targetIndex)
        return false;

      while (--sourceIndex >= 0 && --targetIndex >= 0)
        if (!equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
          return false;

      return true;
    }
  }
}
