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
    public static SpanMaker<char> Append(this SpanMaker<char> source, object value) => source.Append(1, value.ToString());

    public static SpanMaker<char> Append(this SpanMaker<char> source, params object[] values)
      => source.Append(1, values.SelectMany(v => v.ToString() ?? string.Empty).ToList());
  }
}
