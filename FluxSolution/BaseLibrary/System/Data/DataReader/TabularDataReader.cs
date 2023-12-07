namespace Flux.Data
{
  /// <summary>Abstract class for implementing a <see cref="DataReaderEnumerable"/> over tabular data.</summary>
  public abstract class TabularDataReader
    : DataReaderEnumerable
  {
    protected System.Collections.Generic.List<string> m_fieldNames = new();
    /// <summary>A collection of field names for the <see cref="DataReaderEnumerable"/>.</summary>
    public System.Collections.Generic.IReadOnlyList<string> FieldNames { get => m_fieldNames; init => m_fieldNames.AddRange(value); }

    protected System.Collections.Generic.List<System.Type> m_fieldTypes = new();
    ///// <summary>FieldTypes is an optional functionality and each field will default to typeof(object).</summary>
    public System.Collections.Generic.IReadOnlyList<System.Type> FieldTypes { get => m_fieldTypes; init => m_fieldTypes.AddRange(value); }

    protected System.Collections.Generic.List<object> m_fieldValues = new();
    /// <summary>An array of field values for the result.</summary>
    public System.Collections.Generic.IReadOnlyList<object> FieldValues => m_fieldValues;

    // IDataRecord
    /// <summary>Gets the data type information for the specified field. Return the name of the System.Type exposed by GetFieldType(index) by default. Override to change this behavior.</summary>
    /// <remarks>The data type information can differ from the type information returned by GetFieldType, especially where the underlying data types do not map one for one to the runtime types supported by the language. 
    /// (For example, DataTypeName may be "integer", while Type.Name may be "Int32".)</remarks>
    public override int FieldCount => m_fieldNames.Count;
    public override System.Type GetFieldType(int index)
    {
      if (index < 0 && index >= FieldCount) throw new System.ArgumentOutOfRangeException(nameof(index));

      return index < m_fieldTypes.Count ? m_fieldTypes[index] : typeof(object);
    }
    public override string GetName(int index)
    {
      if (index < 0 && index >= FieldCount) throw new System.ArgumentOutOfRangeException(nameof(index));

      return index < m_fieldNames.Count ? m_fieldNames[index] : $"Column_{index}";
    }
    public override int GetOrdinal(string name)
    {
      for (var index = FieldCount - 1; index >= 0; index--)
        if (GetName(index) == name)
          return index;

      throw new System.ArgumentOutOfRangeException(nameof(name));
    }
    public override object GetValue(int index)
    {
      if (index < 0 && index >= FieldCount) throw new System.ArgumentOutOfRangeException(nameof(index));

      return m_fieldValues[index];
    }

    // IDisposable
    protected override void DisposeManaged()
    {
      m_fieldNames.Clear();
      m_fieldTypes.Clear();
      m_fieldValues.Clear();

      base.DisposeManaged();
    }
  }
}
