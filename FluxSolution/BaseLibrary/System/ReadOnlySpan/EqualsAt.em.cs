namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns whether <paramref name="maxLength"/> (or the actual length if less) elements of <paramref name="value"/> are found at the <paramref name="sourceIndex"/> in the <paramref name="source"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="maxLength"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool EqualsAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, int maxLength, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      maxLength = int.Min(maxLength, value.Length);

      var sourceLength = source.Length;

      if (sourceIndex < 0 || maxLength < 0 || (sourceIndex + maxLength) >= sourceLength || (sourceLength - sourceIndex) < maxLength) return false;

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var valueIndex = 0; valueIndex < maxLength; valueIndex++)
        if (!equalityComparer.Equals(source[sourceIndex++], value[valueIndex]))
          return false;

      return true;
    }

    /// <summary>
    /// <para>Returns whether the <paramref name="value"/> is found at the <paramref name="sourceIndex"/> in the <paramref name="source"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool EqualsAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => source.EqualsAt(sourceIndex, value.Length, value, equalityComparer);
  }
}
