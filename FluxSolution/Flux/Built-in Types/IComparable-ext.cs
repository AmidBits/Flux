namespace Flux
{
  public static class IComparableExtensions
  {
    extension<TComparable>(TComparable source)
      where TComparable : System.IComparable<TComparable>
    {
      /// <summary>
      /// <para>Clamp any <see cref="System.IComparable{T}"/>.</para>
      /// </summary>
      /// <param name="min"></param>
      /// <param name="max"></param>
      /// <returns></returns>
      public TComparable Clamp(TComparable min, TComparable max)
        => source.CompareTo(min) < 0 ? min : source.CompareTo(max) > 0 ? max : source;

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <returns></returns>
      public int CompareToDefault()
        => Number.Sign(source.CompareTo(default));

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns>
      /// <list type="bullet">
      /// <item><c>-1</c> when source is less-than the <paramref name="minValue"/> of the interval</item>
      /// <item><c>0</c> when source is a member of the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>]</item>
      /// <item><c>+1</c> when source is greater-than the <paramref name="maxValue"/> of the interval</item>
      /// </list>
      /// </returns>
      public int CompareToInterval(TComparable minValue, TComparable maxValue) => IntervalNotation.Closed.Compare(source, minValue, maxValue);

      /// <summary>
      /// <para>Returns whether a value is a member of the interval [<paramref name="minValue"/>..<paramref name="maxValue"/>].</para>
      /// </summary>
      public bool IsMember(TComparable minValue, TComparable maxValue) => IntervalNotation.Closed.IsMember(source, minValue, maxValue);
    }
  }
}
