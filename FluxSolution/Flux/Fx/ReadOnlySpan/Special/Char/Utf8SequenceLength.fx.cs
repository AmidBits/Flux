namespace Flux
{
  public static partial class Fx
  {
    public static int Utf8SequenceLength(this char source)
    {
      if (source <= 0x7F)
        return 1;
      else if (source <= 0x7FF || char.IsLowSurrogate(source) || char.IsHighSurrogate(source))
        return 2;
      else
        return 3;
    }

    /// <summary>
    /// <para><see cref="https://www.rfc-editor.org/rfc/rfc3629#section-3"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int Utf8SequenceLength(this System.Span<char> source)
    {
      var count = 0;
      for (var i = source.Length - 1; i >= 0; i--)
        count += source[i].Utf8SequenceLength();
      return count;
    }
  }
}
