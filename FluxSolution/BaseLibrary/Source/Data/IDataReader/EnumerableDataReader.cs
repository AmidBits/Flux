namespace Flux.Data
{
  /// <summary>An implementation of a <see cref="DataReader"/> over a System.Collection.Generic.IEnumerable<T>.</summary>
  public class EnumerableDataReader<TSource>
    : DataReader
  {
    internal readonly System.Collections.Generic.IEnumerator<TSource> m_enumerator;

    System.Func<TSource, int, string> m_nameSelector;
    System.Func<TSource, int, object> m_valueSelector;
    System.Func<TSource, string, int> m_ordinalSelector;
    System.Func<TSource, int, System.Type> m_fieldTypeSelector;

    public EnumerableDataReader(System.Collections.Generic.IEnumerator<TSource> source, int fieldCount, System.Func<TSource, int, string> nameSelector, System.Func<TSource, int, object> valueSelector, System.Func<TSource, string, int> ordinalSelector, System.Func<TSource, int, System.Type> fieldTypeSelector)
    {
      m_enumerator = source ?? throw new System.ArgumentNullException(nameof(source));

      FieldCount = fieldCount;

      m_nameSelector = nameSelector;
      m_valueSelector = valueSelector;
      m_ordinalSelector = ordinalSelector;
      m_fieldTypeSelector = fieldTypeSelector;

      m_fieldNames = System.Array.Empty<string>();
    }
    public EnumerableDataReader(System.Collections.Generic.IEnumerable<TSource> source, int fieldCount, System.Func<TSource, int, string> nameSelector, System.Func<TSource, int, object> valueSelector, System.Func<TSource, string, int> ordinalSelector, System.Func<TSource, int, System.Type> fieldTypeSelector)
      : this(source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source)), fieldCount, nameSelector, valueSelector, ordinalSelector, fieldTypeSelector)
    { }

    private string[] m_fieldNames;

    public EnumerableDataReader(System.Collections.Generic.IEnumerable<TSource> source, int fieldCount, bool isFirstRowFieldNames, System.Func<TSource, int, object> valueSelector, System.Func<TSource, int, System.Type> fieldTypeSelector)
    {
      m_enumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

      FieldCount = fieldCount;

      m_fieldNames = new string[fieldCount];

      if (isFirstRowFieldNames)
      {
        if (IsClosed = !m_enumerator.MoveNext())
          throw new System.InvalidOperationException();

        for (var index = 0; index < fieldCount; index++)
          m_fieldNames[index] = $"{valueSelector(m_enumerator.Current, index)}";
      }
      else
        for (var index = 0; index < fieldCount; index++)
          m_fieldNames[index] = $"Column_{index}";

      m_nameSelector = (e, i) => m_fieldNames[i];
      m_valueSelector = valueSelector;
      m_ordinalSelector = (e, s) => System.Array.IndexOf(m_fieldNames, s);
      m_fieldTypeSelector = fieldTypeSelector;
    }

    public override System.Type GetFieldType(int index)
      => IsClosed ? throw new System.InvalidOperationException() : m_fieldTypeSelector(m_enumerator.Current, index);

    public override string GetName(int index)
      => IsClosed ? throw new System.InvalidOperationException() : m_nameSelector(m_enumerator.Current, index);

    public override int GetOrdinal(string name)
      => IsClosed ? throw new System.InvalidOperationException() : m_ordinalSelector(m_enumerator.Current, name);

    public override object GetValue(int index)
      => IsClosed ? throw new System.InvalidOperationException() : m_valueSelector(m_enumerator.Current, index);

    // IDataReader
    public override bool Read()
    {
      IsClosed = m_enumerator is null || !m_enumerator.MoveNext();

      return !IsClosed;
    }

    // IDisposable
    protected override void DisposeManaged()
    {
      m_enumerator.Dispose();

      base.DisposeManaged();
    }
  }
}
