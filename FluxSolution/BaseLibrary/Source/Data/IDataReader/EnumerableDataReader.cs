namespace Flux.Data
{
  /// <summary>An implementation of a System.Data.IDataReader over a System.Collection.Generic.IEnumerable<T>.</summary>
  public class EnumerableDataReader<T>
    : TabularDataReader
  {
    private System.Collections.Generic.IEnumerator<T>? m_enumerator;
    private readonly System.Func<T, System.Collections.Generic.IList<object>> m_valuesSelector;

    public EnumerableDataReader(System.Collections.Generic.IEnumerable<T> source, System.Func<T, System.Collections.Generic.IList<object>> valueSelector, System.Collections.Generic.IEnumerable<string> fieldNames, System.Collections.Generic.IEnumerable<System.Type>? fieldTypes)
      : base(fieldNames, fieldTypes)
    {
      m_enumerator = source is null ? throw new System.ArgumentNullException(nameof(source)) : source.GetEnumerator();
      m_valuesSelector = valueSelector is null ? throw new System.ArgumentNullException(nameof(valueSelector)) : valueSelector;
    }
    public EnumerableDataReader(System.Collections.Generic.IEnumerable<T> source, System.Func<T, System.Collections.Generic.IList<object>> valueSelector, System.Collections.Generic.IEnumerable<string> fieldNames)
      : this(source, valueSelector, fieldNames, null)
    {
    }
    public EnumerableDataReader(System.Collections.Generic.IEnumerable<T> source, System.Func<T, System.Collections.Generic.IList<object>> valueSelector, int fieldCount)
      : base(fieldCount)
    {
      m_enumerator = source is null ? throw new System.ArgumentNullException(nameof(source)) : source.GetEnumerator();
      m_valuesSelector = valueSelector is null ? throw new System.ArgumentNullException(nameof(valueSelector)) : valueSelector;
    }

    public override bool Read()
    {
      FieldValues.Clear();

      if (m_enumerator?.MoveNext() ?? false)
      {
        FieldValues.AddRange(m_valuesSelector(m_enumerator.Current) ?? throw new System.NullReferenceException($"Unexpected null from 'valuesSelector'."));

        if (FieldValues.Count != FieldCount) throw new System.Exception($"Mismatch between the count of field values and FieldCount.");

        return true;
      }
      else return false;
    }

    protected override void DisposeManaged()
    {
      m_enumerator?.Dispose();
      m_enumerator = null;

      base.DisposeManaged();
    }
  }
}
