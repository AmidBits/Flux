namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension(System.ReadOnlySpan<char> source)
    {
      #region SplitByLength

      /// <summary>
      /// <para>Splits a <see cref="System.ReadOnlySpan{T}"/> into smaller ranges by a specified <paramref name="length"/>. If the source.Length is not a multiple of <paramref name="length"/> the last .</para>
      /// </summary>
      /// <param name="length"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<System.Range> SplitByLength(int length)
        => source.Length.CreateSubRanges(length);

      #endregion
    }
  }
}
