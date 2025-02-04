namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Appends any object as a string to a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> Append(this SpanMaker<System.Text.Rune> source, object value)
      => source.Append(1, (value?.ToString() ?? string.Empty).ToSpanMakerOfRune());

    public static SpanMaker<System.Text.Rune> Append(this SpanMaker<System.Text.Rune> source, params object[] values)
      => source.Append(1, string.Concat(values.SelectMany(v => v.ToString() ?? string.Empty)).ToSpanMakerOfRune());
  }
}
