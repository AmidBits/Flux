namespace Flux
{
  public static class IComparableExtensions
  {
    extension<TComparable>(TComparable source)
      where TComparable : System.IComparable<TComparable>
    {
      /// <summary>
      /// <para>Clamp the current instance of a <see cref="System.IComparable{T}"/> to the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public TComparable Clamp(TComparable minValue, TComparable maxValue)
        => source.CompareTo(minValue) < 0 ? minValue : source.CompareTo(maxValue) > 0 ? maxValue : source;

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

      ///// <summary>
      ///// <para>Compares the current instance with an <see cref="Interval{T}"/> and returns an integer that indicates whether the current instance preceeds <see cref="Interval{T}.MinValue"/>, follows <see cref="Interval{T}.MaxValue"/>, or is a member of the closed <see cref="Interval{T}"/>.</para>
      ///// </summary>
      ///// <param name="minValue"></param>
      ///// <param name="maxValue"></param>
      ///// <returns>
      ///// <list type="bullet">
      ///// <item><c>-1</c> when this instance preceeds <see cref="Interval{T}.MinValue"/></item>
      ///// <item><c>0</c> when this instance is a member of the closed <see cref="Interval{T}"/></item>
      ///// <item><c>+1</c> when this instance follows <see cref="Interval{T}.MaxValue"/></item>
      ///// </list>
      ///// </returns>
      //public int CompareToInterval(TComparable minValue, TComparable maxValue)
      //  => IntervalNotation.Closed.Compare(source, minValue, maxValue);

      ///// <summary>
      ///// <para>Indicates whether the current instance is a member of the closed <see cref="Interval{T}"/> [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
      ///// </summary>
      ///// <param name="minValue"></param>
      ///// <param name="maxValue"></param>
      ///// <returns></returns>
      //public bool IsMember(TComparable minValue, TComparable maxValue)
      //  => IntervalNotation.Closed.IsMember(source, minValue, maxValue);
    }
  }
}
