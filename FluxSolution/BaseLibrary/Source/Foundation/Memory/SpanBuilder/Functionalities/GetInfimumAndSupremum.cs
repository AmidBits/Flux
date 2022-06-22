namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Locate the index of the max element that is less than and the index of the min element that is greater than, the specified reference value identified by the <paramref name="valueSelector"/>. Uses the specified comparer.</summary>
    public (int indexLessThan, int indexGreaterThan) GetInfimumAndSupremum<TValue>(TValue referenceValue, System.Func<T, TValue> valueSelector, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var indexLt = -1;
      var indexGt = -1;

      for (var index = m_bufferPosition - 1; index >= 0; index--)
      {
        var value = valueSelector(m_buffer[index]);

        var cmp = comparer.Compare(value, referenceValue);

        if (cmp < 0 && (indexLt < 0 || comparer.Compare(value, valueSelector(m_buffer[indexLt])) > 0))
          indexLt = index;
        if (cmp > 0 && (indexGt < 0 || comparer.Compare(value, valueSelector(m_buffer[indexGt])) < 0))
          indexGt = index;
      }

      return (indexLt, indexGt);
    }
    /// <summary>Locate the index of the max element that is less than and the index of the min element that is greater than, the specified reference value identified by the <paramref name="valueSelector"/>. Uses the default comparer.</summary>
    public (int indexLessThan, int indexGreaterThan) GetInfimumAndSupremum<TValue>(TValue referenceValue, System.Func<T, TValue> valueSelector)
      => GetInfimumAndSupremum(referenceValue, valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
