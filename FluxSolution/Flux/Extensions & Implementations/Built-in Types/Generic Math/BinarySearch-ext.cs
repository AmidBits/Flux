namespace Flux
{
  public static class BinarySearch
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region Try/First

      public static TInteger First(TInteger lowerInclusive, TInteger upperExclusive, Func<TInteger, bool> predicate)
      {
        while (lowerInclusive < upperExclusive)
        {
          var middle = lowerInclusive + ((upperExclusive - lowerInclusive) >> 1);

          if (predicate(middle))
            upperExclusive = middle;
          else
            lowerInclusive = middle + TInteger.One;
        }

        return lowerInclusive;
      }

      public static bool TryFirst(TInteger lowerInclusive, TInteger upperExclusive, Func<TInteger, bool> predicate, out TInteger result)
      {
        result = default!;

        if (lowerInclusive >= upperExclusive)
          return false;

        var low = lowerInclusive;
        var high = upperExclusive;

        while (low < high)
        {
          var middle = low + ((high - low) >> 1);

          if (predicate(middle))
            high = middle;
          else
            low = middle + TInteger.One;
        }

        if (low >= upperExclusive || !predicate(low))
          return false;

        result = low;
        return true;
      }

      #endregion

      #region Try/Last

      public static TInteger Last(TInteger lowerInclusive, TInteger upperExclusive, Func<TInteger, bool> predicate)
        => First(lowerInclusive, upperExclusive, x => !predicate(x)) - TInteger.One;

      public static bool TryLast(TInteger lowerInclusive, TInteger upperExclusive, Func<TInteger, bool> predicate, out TInteger result)
      {
        result = default!;

        if (lowerInclusive >= upperExclusive)
          return false;

        var low = lowerInclusive;
        var high = upperExclusive;

        while (low < high)
        {
          var middle = low + ((high - low) >> 1);

          if (predicate(middle))
            low = middle + TInteger.One;
          else
            high = middle;
        }

        var candidate = low - TInteger.One;

        if (candidate < lowerInclusive || !predicate(candidate))
          return false;

        result = candidate;
        return true;
      }

      #endregion
    }

    #region Collection searching

    #region Contains 

    public static bool Contains<TComparable>(System.Collections.Generic.IReadOnlyList<TComparable> values, TComparable value)
      where TComparable : System.IComparable<TComparable>
      => IndexOf(values, value) >= 0;


    #endregion

    #region EqualRange

    public static (int Start, int End) EqualRange<TComparable>(System.Collections.Generic.IReadOnlyList<TComparable> values, TComparable value)
      where TComparable : System.IComparable<TComparable>
    {
      var start = LowerBound(values, value);
      var end = UpperBound(values, value);

      return (start, end);
    }

    #endregion

    #region IndexOf

    public static int IndexOf<TComparable>(System.Collections.Generic.IReadOnlyList<TComparable> values, TComparable value)
      where TComparable : System.IComparable<TComparable>
    {
      var index = LowerBound(values, value);

      return index < values.Count && values[index].CompareTo(value) == 0 ? index : -1;
    }

    #endregion

    #region LowerBound

    public static int LowerBound<TComparable>(System.Collections.Generic.IReadOnlyList<TComparable> values, TComparable value)
      where TComparable : System.IComparable<TComparable>
      => First(0, values.Count, i => values[i].CompareTo(value) >= 0);

    #endregion

    #region UpperBound

    public static int UpperBound<TComparable>(System.Collections.Generic.IReadOnlyList<TComparable> values, TComparable value)
      where TComparable : System.IComparable<TComparable>
      => First(0, values.Count, i => values[i].CompareTo(value) > 0);

    #endregion

    #endregion
  }
}
