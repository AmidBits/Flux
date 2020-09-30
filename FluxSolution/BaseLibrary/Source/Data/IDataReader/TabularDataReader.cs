namespace Flux.Data
{
  /// <summary>Base class for implementing System.Data.IDataReader over tabular data.</summary>
  public abstract class TabularDataReader
    : DataReader
  {
    /// <summary>An array of whether the fields FieldNulls is an optional functionality and each field will default to true (as in, this field allows null values).</summary>
    public System.Collections.Generic.List<bool> FieldAllowDBNulls { get; private set; } = new System.Collections.Generic.List<bool>();
    /// <summary>An array of the T-SQL data types for the result.</summary>
    public System.Collections.Generic.List<string> FieldTsqlDataTypes { get; private set; } = new System.Collections.Generic.List<string>();

    /// <summary>An array of the field names for the result.</summary>
    public System.Collections.Generic.List<string> FieldNames { get; private set; } = new System.Collections.Generic.List<string>();
    ///// <summary>FieldTypes is an optional functionality and each field will default to typeof(object).</summary>
    public System.Collections.Generic.List<System.Type> FieldTypes { get; private set; } = new System.Collections.Generic.List<System.Type>();
    /// <summary>An array of field values for the result.</summary>
    public System.Collections.Generic.List<object> FieldValues { get; private set; } = new System.Collections.Generic.List<object>();

    public TabularDataReader(System.Collections.Generic.IEnumerable<string> fieldNames)
      => FieldNames.AddRange(fieldNames.EmptyOnNull());
    public TabularDataReader(System.Collections.Generic.IEnumerable<string> fieldNames, System.Collections.Generic.IEnumerable<System.Type>? fieldTypes)
      : this(fieldNames)
      => FieldTypes.AddRange(fieldTypes.EmptyOnNull());
    /// <summary>This will create the tabular data reader with the specified number of field names preset to "Column_N" where N is the ordinal index.</summary>
    public TabularDataReader(int fieldCount)
    {
      for (var index = 0; index < fieldCount; index++)
        FieldNames.Add($"Column_{index}");
    }
    /// <summary>This will create the tabular data reader with everything being null.</summary>
    public TabularDataReader()
    {
    }

    // DataReader
    public override bool GetAllowDBNull(int index)
      => index >= 0 && index < FieldAllowDBNulls.Count ? FieldAllowDBNulls[index] : base.GetAllowDBNull(index);
    public override string GetTsqlDataType(int index)
      => index >= 0 && index < FieldTsqlDataTypes.Count ? FieldTsqlDataTypes[index] : base.GetTsqlDataType(index);

    // IDataReader
    public override bool IsClosed
      => FieldValues.Count == 0;

    // IDataRecord
    public override int FieldCount
      => FieldNames.Count;
    public override System.Type GetFieldType(int index)
      => index >= 0 && index < FieldTypes.Count ? FieldTypes[index] : typeof(object);
    public override string GetName(int index)
      => FieldNames[index];
    public override int GetOrdinal(string name)
      => FieldNames.IndexOf(name);
    public override object GetValue(int index)
      => FieldValues[index];

    // IDisposable
    protected override void DisposeManaged()
    {
      FieldAllowDBNulls.Clear();
      FieldTsqlDataTypes.Clear();

      FieldNames.Clear();
      FieldTypes.Clear();
      FieldValues.Clear();
    }
  }
}
