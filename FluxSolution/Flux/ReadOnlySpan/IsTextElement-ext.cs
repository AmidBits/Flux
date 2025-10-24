namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension(System.ReadOnlySpan<char> source)
    {
      #region IsTextElement

      /// <summary>
      /// <para>Determines whether the <paramref name="source"/> represents a single text-element not many, i.e. a grapheme not a cluster.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public bool IsTextElement()
        => System.Globalization.StringInfo.GetNextTextElementLength(source) == source.Length;

      #endregion
    }
  }
}
