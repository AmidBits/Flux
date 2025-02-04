namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Appends ordinal extensions (e.g. rd, th, etc.) to any sequence of digits (e.g. 3, 12, etc.) in a <see cref="SpanMaker{T}"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Ordinal_indicator"/></para>
    /// </summary>
    public static SpanMaker<char> InsertOrdinalIndicatorSuffix(this SpanMaker<char> source)
      => source.ReplaceRegex(@"\d+", d => int.Parse(d, System.Globalization.NumberStyles.Integer).ToOrdinalIndicatorString());
  }
}
