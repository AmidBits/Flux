namespace Flux.Data
{
  /// <summary>Abstract class for implementing a <see cref="DataReader"/> over tabular data.</summary>
  public abstract class TabularDataReaderEx
    : TabularDataReader
  {
    public const string AllowDBNull = nameof(AllowDBNull);
    public const string TsqlDataType = nameof(TsqlDataType);

    protected System.Collections.Generic.List<bool> m_fieldAllowDBNulls = new System.Collections.Generic.List<bool>();
    /// <summary>An array of whether the fields FieldNulls is an optional functionality where each field defaults to true (as in, the field allows null values).</summary>
    public System.Collections.Generic.IReadOnlyList<bool> FieldAllowDBNulls { get => m_fieldAllowDBNulls; set => m_fieldAllowDBNulls.AddRange(value); }

    protected System.Collections.Generic.List<string> m_fieldTsqlDataTypes = new System.Collections.Generic.List<string>();
    /// <summary>An array of the T-SQL data types for the result.</summary>
    public System.Collections.Generic.IReadOnlyList<string> FieldTsqlDataTypes { get => m_fieldTsqlDataTypes; set => m_fieldTsqlDataTypes.AddRange(value); }

    /// <summary>DataReader extension, which indicates whether the field at the specified index allows nulls. By default, true is returned for any index (field).</summary>
    public virtual bool GetFieldAllowDBNull(int index)
    {
      if (index < 0 && index >= FieldCount) throw new System.ArgumentOutOfRangeException(nameof(index));

      return index >= FieldAllowDBNulls.Count || FieldAllowDBNulls[index]; // The default is to allow nulls.
    }

    /// <summary>DataReader extension, which returns a complete T-SQL data type (with type arguments as needed/desired) the field at the specified index corresponds to. By default, the type returned by GetFieldType() is System.Object, which results in "sql_variant" being returned.</summary>
    public virtual string GetFieldTsqlDataType(int index)
    {
      if (index < 0 && index >= FieldCount) throw new System.ArgumentOutOfRangeException(nameof(index));

      if (index < FieldTsqlDataTypes.Count)
        return FieldTsqlDataTypes[index];

      // If there is no designated T-SQL data type stored, then create a default on-the-fly.

      var tsqlTypeName = Data.TsqlDataType.NameFromType(GetFieldType(index));
      var tsqlTypeArguments = Data.TsqlDataType.GetDefaultArgument(tsqlTypeName);

      return tsqlTypeName + tsqlTypeArguments;
    }

    // IDataReader
    public override System.Data.DataTable GetSchemaTable()
    {
      var dt = new System.Data.DataTable(@"SchemaTable")
      {
        Columns = {
                { ColumnOrdinal, typeof(int) },
                { ColumnName, typeof(string) },
                { DataType, typeof(System.Type) },
                { ColumnSize, typeof(int) },
                { AllowDBNull, typeof(bool) },
                { TsqlDataType, typeof(string) }
              }
      };

      for (var index = 0; index < FieldCount; index++)
        dt.Rows.Add(new object[] { index, GetName(index), GetFieldType(index), -1, GetFieldAllowDBNull(index), GetFieldTsqlDataType(index) });

      return dt;
    }

    // IDisposable
    protected override void DisposeManaged()
    {
      m_fieldAllowDBNulls.Clear();
      m_fieldTsqlDataTypes.Clear();

      base.DisposeManaged();
    }
  }
}
