namespace Flux.Data
{
  /// <summary>An implementation of a System.Data.IDataReader over a System.Collection.Generic.IEnumerable<T>.</summary>
  public class EnumerableDataReader<T>
    : TabularDataReader
  {
    private System.Collections.Generic.IEnumerator<T>? m_enumerator;
    private readonly System.Func<T, System.Collections.Generic.IList<object>> m_valuesSelector;

    public EnumerableDataReader(System.Collections.Generic.IEnumerable<T> source, System.Func<T, System.Collections.Generic.IList<object>> valueSelector, System.Collections.Generic.IEnumerable<string>? fieldNames = null)
      : base(fieldNames)
    {
      m_enumerator = source.GetEnumerator();
      m_valuesSelector = valueSelector;
    }
    public EnumerableDataReader(System.Collections.Generic.IEnumerable<T> source, System.Func<T, System.Collections.Generic.IList<object>> valueSelector, int fieldCount)
      : base(fieldCount)
    {
      m_enumerator = source.GetEnumerator();
      m_valuesSelector = valueSelector;
    }

    public override bool Read()
    {
      if (m_enumerator?.MoveNext() ?? false)
      {
        m_fieldValues = m_valuesSelector(m_enumerator.Current) ?? throw new System.NullReferenceException($"Unexpected null from 'valuesSelector'.");

        if (m_fieldValues.Count != FieldCount)
        {
          throw new System.Exception($"Mismatch between the count of field values and FieldCount.");
        }

        return true;
      }
      else
      {
        m_fieldValues = null;

        return false;
      }
    }

    protected override void DisposeManaged()
    {
      m_enumerator?.Dispose();
      m_enumerator = null;

      base.DisposeManaged();
    }
  }
}
