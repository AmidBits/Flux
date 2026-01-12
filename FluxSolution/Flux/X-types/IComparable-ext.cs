namespace Flux
{
  public static class IComparableExtensions
  {
    public static bool EqualsDefault<TEqualityComparer>(this TEqualityComparer source)
      where TEqualityComparer : System.IComparable<TEqualityComparer>
      => System.Collections.Generic.EqualityComparer<TEqualityComparer>.Default.Equals(source, default);

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
        => Numbers.Sign(source.CompareTo(default));

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns>0 when source is a member of the interval, -1 when source is less than the interval, +1 when source is greater than the interval.</returns>
      public int CompareToInterval(TComparable minValue, TComparable maxValue)
        => source.CompareTo(minValue) < 0 ? -1 : source.CompareTo(maxValue) > 0 ? +1 : 0;

      /// <summary>
      /// <para>Returns whether a value is a member of the interval [<paramref name="minValue"/>..<paramref name="maxValue"/>].</para>
      /// </summary>
      public bool IsMember(TComparable minValue, TComparable maxValue)
      {
        if (minValue.CompareTo(maxValue) > 0)
          (minValue, maxValue) = (maxValue, minValue);

        return source.CompareTo(minValue) >= 0 && source.CompareTo(maxValue) <= 0;
      }
    }
  }
}
