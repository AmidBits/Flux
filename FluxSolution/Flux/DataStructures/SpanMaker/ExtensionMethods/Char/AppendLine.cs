namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Appends a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source)
      => source.Append(System.Environment.NewLine);

#if XNET9_0_OR_GREATER

    /// <summary>
    /// <para>Appends <paramref name="count"/> <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source, int count, params System.ReadOnlySpan<char> value)
    {
      source = source.Append(count, value);
      source = source.AppendLine();
      return source;
    }

    /// <summary>
    /// <para>Appends <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source, params System.ReadOnlySpan<char> value)
    {
      source = source.Append(value);
      source = source.AppendLine();
      return source;
    }

#else

    /// <summary>
    /// <para>Appends <paramref name="count"/> <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source, int count, System.ReadOnlySpan<char> value)
    {
      source = source.Append(count, value);
      source = source.AppendLine();
      return source;
    }

    /// <summary>
    /// <para>Appends <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<char> AppendLine(this SpanMaker<char> source, System.ReadOnlySpan<char> value)
    {
      source = source.Append(value);
      source = source.AppendLine();
      return source;
    }

#endif
  }
}
