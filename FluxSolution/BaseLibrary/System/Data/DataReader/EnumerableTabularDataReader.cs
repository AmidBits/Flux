namespace Flux.Data
{
  /// <summary>An implementation of a <see cref="TabularDataReader"/> over a System.Collection.Generic.IEnumerable<T>.</summary>
  public sealed class EnumerableTabularDataReader
    : TabularDataReader
  {
    internal readonly System.Collections.Generic.IEnumerator<System.Collections.Generic.IEnumerable<object>> m_enumerator;

    /// <summary>Creates a enumerable tabular data reader using the specified sequence.</summary>
    public EnumerableTabularDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> source)
    {
      System.ArgumentNullException.ThrowIfNull(nameof(source));

      m_enumerator = source.GetEnumerator();
    }
    /// <summary>Creates a enumerable tabular data reader using the specified sequence and number of field names. Zero or less will force the reader to use the values from the first element as field names.</summary>
    /// <param name="fieldCount">If field count is less or equal to zero, then the enumerable tabular data reader will attempt to read from the enumerator.</param>
    public EnumerableTabularDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> source, int fieldCount)
      : this(source)
    {
      m_fieldNames.Clear();

      if (fieldCount > 0) // If fieldCount is one or more, generate column names "Column_n..".
      {
        for (var index = 0; index < fieldCount; index++)
          m_fieldNames.Add(index.ToColumnName());
      }
      else // Otherwise, try to move to the first element and use the field values for field names.
      {
        IsClosed = !m_enumerator.MoveNext();

        if (!IsClosed)
          foreach (var fieldName in m_enumerator.Current)
            m_fieldNames.Add($"{fieldName}");
      }

      FieldCount = m_fieldNames.Count;
    }
    /// <summary>Creates a enumerable tabular data reader using the specified sequence and field names.</summary>
    public EnumerableTabularDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> source, System.Collections.Generic.IEnumerable<string> fieldNames)
      : this(source)
    {
      m_fieldNames.AddRange(fieldNames); // Override the field names 

      FieldCount = m_fieldNames.Count;
    }
    /// <summary>Creates a enumerable tabular data reader using the specified sequence, field names and types.</summary>
    public EnumerableTabularDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> source, System.Collections.Generic.IEnumerable<string> fieldNames, System.Collections.Generic.IEnumerable<System.Type> fieldTypes)
      : this(source, fieldNames)
    {
      m_fieldTypes.AddRange(fieldTypes);
    }

    /// <summary>Attempts to read the next result as field values into the EnumerableTabularDataReader.FieldValues object.</summary>
    private bool ReadFieldValues()
    {
      m_fieldValues.Clear();

      IsClosed = !m_enumerator.MoveNext();

      if (!IsClosed)
        m_fieldValues.AddRange(m_enumerator!.Current);

      return !IsClosed;
    }

    // IDataReader
    public override bool Read()
    {
      if (ReadFieldValues())
        if (m_fieldValues.Count != FieldCount)
          throw new System.Exception($"Mismatch between the count of FieldValues={m_fieldValues.Count} and FieldCount={FieldCount}.");

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
