namespace Flux
{
  public static partial class Fx
  {
    public static int Utf8SequenceLength(this char source)
      => (source <= 0x7F)
      ? 1
      : (source <= 0x7FF || char.IsLowSurrogate(source) || char.IsHighSurrogate(source))
      ? 2
      : 3;

    /// <summary>
    /// <para><see cref="https://www.rfc-editor.org/rfc/rfc3629#section-3"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int Utf8SequenceLength(this System.ReadOnlySpan<char> source)
    {
      var length = 0;
      for (var i = source.Length - 1; i >= 0; i--)
        length += source[i].Utf8SequenceLength();
      return length;
    }
  }
}
