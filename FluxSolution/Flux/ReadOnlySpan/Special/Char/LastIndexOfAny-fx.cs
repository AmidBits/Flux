namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Reports the index of any of the <paramref name="values"/> in the <paramref name="source"/>. or -1 if none is found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static int LastIndexOfAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, params string[] values)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        if (source.LastIndexOf(values[valueIndex], equalityComparer) is var index && index > -1)
          return index;

      return -1;
    }
  }
}
