namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Make any consecutive sequence of digits (any number) into <paramref name="fixedLength"/> sequences using <paramref name="padding"/> in a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fixedLength"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public static SpanMaker<char> MakeNumbersFixedLength(this SpanMaker<char> source, int fixedLength, char padding = '0')
      => source.ReplaceRegex(@"\d+", d => new SpanMaker<char>(d).PadLeft(fixedLength, padding).ToString());
  }
}
