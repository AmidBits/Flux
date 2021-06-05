namespace Flux
{
  public static partial class SystemCollectionsGenericIEnumerableEm
  {
    /// <summary>Compute both the minimum and maximum element in a single pass, and return them as a 2-tuple. Uses the specified comparer.</summary>
    public static bool GetNearestGreaterAndLessOrEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> keySelector, TValue referenceValue, out int indexOfLtMax, out int indexOfEq, out int indexOfGtMin)
      where TValue : struct, System.IComparable<TValue>
    {
      using var e = source.GetEnumerator();

      var ltCurrent = new TValue?();
      var gtCurrent = new TValue?();

      indexOfLtMax = -1;
      indexOfEq = -1;
      indexOfGtMin = -1;

      var index = 0;

      while (e.MoveNext())
      {
        var currentValue = keySelector(e.Current);

        switch (currentValue.CompareTo(referenceValue))
        {
          case int lo when lo < 0:
            if (!ltCurrent.HasValue || currentValue.CompareTo(ltCurrent.Value) > 0)
            {
              indexOfLtMax = index;
              ltCurrent = currentValue;
            }
            break;
          case int hi when hi > 0:
            if (!gtCurrent.HasValue || currentValue.CompareTo(gtCurrent.Value) < 0)
            {
              indexOfGtMin = index;
              gtCurrent = currentValue;
            }
            break;
          default:
            indexOfEq = index;
            break;
        }

        index++;
      }

      return indexOfLtMax != -1 || indexOfEq != -1 || indexOfGtMin != -1;
    }
    public static (TSource lessThan, TSource equal, TSource greaterThan) GetNearestGreaterAndLessOrEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> keySelector, TValue referenceValue, System.Collections.Generic.IComparer<TValue> comparer)
       where TValue : struct
    {
      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var cValue = keySelector(e.Current);

        var cmp = comparer.Compare(cValue, referenceValue);

        var ltSource = cmp < 0 ? e.Current : default!;
        var eqSource = cmp == 0 ? e.Current : default!;
        var gtSource = cmp > 0 ? e.Current : default!;

        var ltValue = cmp < 0 ? cValue : referenceValue;
        var eqValue = cmp == 0 ? cValue : referenceValue;
        var gtValue = cmp > 0 ? cValue : referenceValue;

        while (e.MoveNext())
        {
          cValue = keySelector(e.Current);

          switch (comparer.Compare(cValue, referenceValue))
          {
            case int lt when lt < 0:
              if (comparer.Compare(cValue, ltValue) > 0)
              {
                ltSource = e.Current;
                ltValue = cValue;
              }
              break;
            case int gt when gt > 0:
              if (comparer.Compare(cValue, gtValue) < 0)
              {
                gtSource = e.Current;
                gtValue = cValue;
              }
              break;
            default:
              eqSource = e.Current;
              eqValue = cValue;
              break;
          }
        }

        return (ltSource, eqSource, gtSource);
      }

      return (default!, default!, default!);
    }
  }
}
