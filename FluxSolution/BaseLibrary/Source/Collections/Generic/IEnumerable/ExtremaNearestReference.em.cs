namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Locate the max element that is less than and the min element that is greater than the specified reference value identified by the <see cref="keySelector"/>. Uses the specified comparer.</summary>
    public static (TSource elementMaxLt, int indexMaxLt, TSource elementMinGt, int indexMinGt) ExtremaNearestReference<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, TKey referenceKey, System.Collections.Generic.IComparer<TKey> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var elementMaxLt = default(TSource)!;
        var indexMaxLt = -1;

        var keyMaxLt = referenceKey;
        var elementMinGt = default(TSource)!;
        var indexMinGt = -1;
        var keyMinGt = referenceKey;

        var index = 0;

        do
        {
          var keyCurrent = keySelector(e.Current);

          switch (comparer.Compare(keyCurrent, referenceKey))
          {
            case int lt when lt < 0:
              if (comparer.Compare(keyCurrent, keyMaxLt) > 0 || indexMaxLt == -1)
              {
                elementMaxLt = e.Current;
                indexMaxLt = index;
                keyMaxLt = keyCurrent;
              }
              break;
            case int gt when gt > 0:
              if (comparer.Compare(keyCurrent, keyMinGt) < 0 || indexMinGt == -1)
              {
                elementMinGt = e.Current;
                indexMinGt = index;
                keyMinGt = keyCurrent;
              }
              break;
            default:
              break;
          }

          index++;
        }
        while (e.MoveNext());

        return (elementMaxLt, indexMaxLt, elementMinGt, indexMinGt);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
    /// <summary>Locate the max element that is less than and the min element that is greater than the specified reference value identified by the <see cref="keySelector"/>. Uses the specified comparer.</summary>
    public static (TSource elementMaxLt, int indexMaxLt, TSource elementMinGt, int indexMinGt) ExtremaNearestReference<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue referenceValue)
      => ExtremaNearestReference(source, valueSelector, referenceValue, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
