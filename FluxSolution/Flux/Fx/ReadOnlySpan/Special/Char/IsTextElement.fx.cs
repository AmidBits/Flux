namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> represents a single text-element not many, i.e. a grapheme not a cluster.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsTextElement(this System.ReadOnlySpan<char> source)
      => System.Globalization.StringInfo.GetNextTextElementLength(source) == source.Length;
  }
}
