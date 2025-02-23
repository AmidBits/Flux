namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static int IndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if (source.IndexOf(c => values.Contains(c, equalityComparer)) is var index && index > -1)
        return index;

      return -1;
    }
  }
}
