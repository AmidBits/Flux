namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var length = 0;
      for (var index = 0; length < maxLength && predicate(source[index]); index++)
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var length = 0;
      for (var index = 0; length < maxLength && equalityComparer.Equals(source[index], value); index++)
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var minLength = int.Min(source.Length, target.Length);

      var length = 0;
      while (length < minLength && length < maxLength && equalityComparer.Equals(source[length], target[length]))
        length++;
      return length;
    }
  }
}
