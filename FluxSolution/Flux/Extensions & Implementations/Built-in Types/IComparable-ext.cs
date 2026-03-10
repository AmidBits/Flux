namespace Flux
{
  public static class IComparableExtensions
  {
    extension<TComparable>(System.IComparable<TComparable>)
      where TComparable : System.IComparable<TComparable>
    {
      /// <summary>
      /// <para>Clamp the current instance of a <see cref="System.IComparable{T}"/> to the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public static TComparable Clamp(TComparable source, TComparable minValue, TComparable maxValue)
        => source.CompareTo(minValue) < 0 ? minValue : source.CompareTo(maxValue) > 0 ? maxValue : source;
    }

    extension<TComparable>(TComparable source)
      where TComparable : System.IComparable<TComparable>
    {
      /// <summary>
      /// <para>Compares the current instance with <see langword="default"/> and returns an integer that indicates whether the current instance preceeds, follows, or occurs in the same position in the sort order as <see langword="default"/>.</para>
      /// </summary>
      /// <returns>
      /// <list type="bullet">
      /// <item><c>-1</c> when this instance preceeds <see langword="default"/></item>
      /// <item><c>0</c> when this instance occurs in the same sort order as <see langword="default"/></item>
      /// <item><c>+1</c> when this instance follows <see langword="default"/></item>
      /// </list>
      /// </returns>
      public int CompareToDefault()
        => Number.Sign(source.CompareTo(default));
    }
  }
}
