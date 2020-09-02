using System.Linq;

namespace Flux.Data
{
  /// <summary>Base class for implementing System.Data.IDataReader over tabular data.</summary>
  public abstract class TabularDataReader
    : DataReader
  {
    protected System.Collections.Generic.IList<string>? m_fieldNames;
    /// <summary>An array of the field names for the result.</summary>
    public System.Collections.Generic.IList<string> FieldNames
    {
      get => m_fieldNames ?? throw new System.Exception($"Missing {nameof(FieldNames)}.");
      set => m_fieldNames = value;
    }

    protected System.Collections.Generic.IList<bool>? m_fieldsAllowNull;
    /// <summary>An array of whether the fields FieldNulls is an optional functionality and each field will default to true (as in, this field allows null values).</summary>
    public System.Collections.Generic.IList<bool> FieldsAllowNull
    {
      get => m_fieldsAllowNull ??= System.Linq.Enumerable.Repeat(true, FieldNames.Count).ToList();
      set => m_fieldsAllowNull = value;
    }

    protected System.Collections.Generic.IList<System.Type>? m_fieldTypes;
    /// <summary>FieldTypes is an optional functionality and each field will default to typeof(object).</summary>
    public System.Collections.Generic.IList<System.Type> FieldTypes
    {
      get => m_fieldTypes ??= System.Linq.Enumerable.Repeat(typeof(object), FieldNames.Count).ToList();
      set => m_fieldTypes = value;
    }

    protected System.Collections.Generic.IList<object>? m_fieldValues;
    /// <summary>An array of field values for the result.</summary>
    public System.Collections.Generic.IList<object> FieldValues
      => m_fieldValues ?? throw new System.Exception($"Missing {nameof(FieldValues)}.");

    public TabularDataReader(System.Collections.Generic.IEnumerable<string>? fieldNames)
    {
      m_fieldNames = fieldNames?.ToList();
      m_fieldsAllowNull = null;
      m_fieldTypes = null;

      m_fieldValues = null;
    }
    /// <summary>This will create the tabular data reader with the specified number of field names preset to "Column_N" where N is the ordinal index.</summary>
    public TabularDataReader(int fieldCount)
      : this(System.Linq.Enumerable.Range(1, fieldCount).Select(i => $"Column{i}").ToArray())
    {
    }
    /// <summary>This will create the tabular data reader with everything being null.</summary>
    public TabularDataReader()
    {
    }

    // IDataReader
    public override bool IsClosed
      => m_fieldValues is null || m_fieldValues.Count == 0;
    public override System.Data.DataTable GetSchemaTable()
    {
      var dt = base.GetSchemaTable();

      for (var index = 0; index < FieldCount; index++)
      {
        dt.Rows[index][@"AllowDBNull"] = FieldsAllowNull[index];
      }

      return dt;
    }

    // IDataRecord
    public override int FieldCount
      => FieldNames.Count;
    public override System.Type GetFieldType(int index)
      => FieldTypes[index];
    public override string GetName(int index)
      => FieldNames[index];
    public override int GetOrdinal(string name)
      => FieldNames.IndexOf(name);
    public override object GetValue(int index)
      => FieldValues[index];
    //public override int GetValues(object[] values)
    //{
    //  var count = System.Math.Min(FieldValues.Count, values.Length);

    //  for (var index = count - 1; index >= 0; index--)
    //    values[index] = FieldValues[index];

    //  return count;
    //}

    // IDisposable
    protected override void DisposeManaged()
    {
      m_fieldNames = null;
      m_fieldsAllowNull = null;
      m_fieldTypes = null;

      m_fieldValues = null;
    }
  }
}
