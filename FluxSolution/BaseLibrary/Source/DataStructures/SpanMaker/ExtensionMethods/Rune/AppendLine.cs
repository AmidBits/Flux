namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Appends a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source)
      => source.Append(System.Environment.NewLine.ToSpanMakerOfRune());

#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Appends <paramref name="count"/> <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source, int count, params System.ReadOnlySpan<System.Text.Rune> value)
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
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source, params System.ReadOnlySpan<System.Text.Rune> value)
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
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source, int count, System.ReadOnlySpan<System.Text.Rune> value)
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
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source, System.ReadOnlySpan<System.Text.Rune> value)
    {
      source = source.Append(value);
      source = source.AppendLine();
      return source;
    }

#endif
  }
}
