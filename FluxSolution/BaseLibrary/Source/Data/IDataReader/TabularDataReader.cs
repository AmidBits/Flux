using System.Linq;

namespace Flux.Data
{
  /// <summary>Base class for implementing System.Data.IDataReader over tabular data.</summary>
  public abstract class TabularDataReader
    : DataReader
  {
    /// <summary>An array of the field names for the result.</summary>
    public System.Collections.Generic.List<string> FieldNames { get; } = new System.Collections.Generic.List<string>();
    /// <summary>An array of whether the fields FieldNulls is an optional functionality and each field will default to true (as in, this field allows null values).</summary>
    public System.Collections.Generic.List<bool> FieldsAllowNull { get; } = new System.Collections.Generic.List<bool>();
    ///// <summary>FieldTypes is an optional functionality and each field will default to typeof(object).</summary>
    public System.Collections.Generic.List<System.Type> FieldTypes { get; } = new System.Collections.Generic.List<System.Type>();
    /// <summary>An array of field values for the result.</summary>
    public System.Collections.Generic.List<object> FieldValues { get; } = new System.Collections.Generic.List<object>();

    public TabularDataReader(System.Collections.Generic.IEnumerable<string> fieldNames)
    {
      FieldNames.AddRange(fieldNames ?? throw new System.ArgumentNullException(nameof(fieldNames)));

      FieldsAllowNull.AddRange(System.Linq.Enumerable.Repeat(true, FieldNames.Count));
      FieldTypes.AddRange(System.Linq.Enumerable.Repeat(typeof(object), FieldNames.Count));

      FieldValues.Clear();
    }
    /// <summary>This will create the tabular data reader with the specified number of field names preset to "Column_N" where N is the ordinal index.</summary>
    public TabularDataReader(int fieldCount)
      : this(System.Linq.Enumerable.Range(1, fieldCount).Select(i => $"Column_{i}").ToArray())
    {
    }
    /// <summary>This will create the tabular data reader with everything being null.</summary>
    public TabularDataReader()
    {
    }

    // IDataReader
    public override bool IsClosed
      => !FieldValues.Any();
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
      FieldNames.Clear();
      FieldsAllowNull.Clear();
      FieldTypes.Clear();
      FieldValues.Clear();
    }
  }
}
