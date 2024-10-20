namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reports the index of any of the specified <paramref name="values"/> within the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static int IndexOfAny(this System.Text.StringBuilder source, int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] values)
    {
      if (source.IndexOf(offset, c => values.Contains(c, equalityComparer)) is var index && index > -1)
        return index;

      return -1;
    }

    /// <summary>
    /// <para>Reports the index of any of the specified <paramref name="values"/> within the <paramref name="source"/>, or -1 if none were found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static int IndexOfAny(this System.Text.StringBuilder source, int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
    {
      for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        if (source.IndexOf(offset, values[valueIndex], equalityComparer) is var index && index > -1)
          return index;

      return -1;
    }
  }
}
