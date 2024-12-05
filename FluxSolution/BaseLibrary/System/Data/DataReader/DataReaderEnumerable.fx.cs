namespace Flux.Data
{
  /// <summary>Abstract class for custom implementation of a <see cref="System.Data.IDataReader"/>.</summary>
  public abstract class DataReaderEnumerable
    : Disposable, System.Data.IDataReader, System.Collections.Generic.IEnumerable<System.Data.IDataRecord>
  {
    /// <summary>Create a new <see cref="System.Data.DataTable"/> representing the custom <see cref="DataReaderEnumerable"/>.</summary>
    /// <remarks>This is a virtual method so it can be overridden for further enhancements.</remarks>
    public virtual System.Data.DataTable CreateSchemaTable()
    {
      var dt = new System.Data.DataTable(@"SchemaTable");
      dt.Columns.Add("ColumnOrdinal", typeof(int));
      dt.Columns.Add("ColumnName", typeof(string));
      dt.Columns.Add("DataType", typeof(System.Type));
      dt.Columns.Add("ColumnSize", typeof(int));
      return dt;
    }

    // IDataReader
    public virtual int Depth { get; protected set; }
    public virtual bool IsClosed { get; protected set; } = false; // False by default. Must change during operations in a derived class, in order to reflect open/close status of the data reader.
    public virtual int RecordsAffected { get; protected set; } = -1;
    public virtual void Close() => IsClosed = true;
    public virtual System.Data.DataTable GetSchemaTable()
    {
      var dt = CreateSchemaTable();

      for (var index = 0; index < FieldCount; index++)
        dt.Rows.Add(new object[] { index, GetName(index), GetFieldType(index), -1 });

      return dt;
    }
    public virtual bool NextResult() => false;
    public abstract bool Read();

    // IDataRecord
    public virtual int FieldCount { get; protected set; }
    public virtual object this[int index] => GetValue(index);
    public virtual object this[string name] => GetValue(GetOrdinal(name));
    public virtual bool GetBoolean(int index) => (bool)GetValue(index);
    public virtual byte GetByte(int index) => (byte)GetValue(index);
    public virtual long GetBytes(int index, long fieldIndex, byte[]? buffer, int bufferIndex, int length)
    {
      var field = (byte[])GetValue(index);

      if (buffer is null)
      {
        return field.Length; // MS docs: If you pass a buffer that is null, GetBytes returns the length of the row in bytes.
      }

      var count = System.Math.Min(System.Math.Min(length, buffer.Length - bufferIndex), field.Length - fieldIndex);

      if (count <= int.MaxValue)
      {
        System.Buffer.BlockCopy(field, (int)fieldIndex, buffer, bufferIndex, (int)count);
      }
      else // If System.Buffer.BlockCopy cannot be used, revert to a good old loop.
      {
        for (var countDown = count; countDown > 0; bufferIndex++, fieldIndex++, countDown--)
        {
          buffer[bufferIndex] = field[fieldIndex];
        }
      }

      return count;
    }
    public virtual char GetChar(int index) => (char)GetValue(index);
    public virtual long GetChars(int index, long fieldIndex, char[]? buffer, int bufferIndex, int length)
    {
      var field = (char[])GetValue(index);

      if (buffer is null)
      {
        return field.Length; // MS docs: If you pass a buffer that is null, GetChars returns the length of the field in characters.
      }

      var count = System.Math.Min(System.Math.Min(length, buffer.Length - bufferIndex), field.Length - fieldIndex);

      if (count <= int.MaxValue)
      {
        System.Array.Copy(field, fieldIndex, buffer, bufferIndex, count);
      }
      else // If System.Array.Copy cannot be used, revert to a good old loop.
      {
        for (var countDown = count; countDown > 0; bufferIndex++, fieldIndex++, countDown--)
        {
          buffer[bufferIndex] = field[fieldIndex];
        }
      }

      return count;
    }
    public virtual System.Data.IDataReader GetData(int index) => throw new System.NotImplementedException();
    /// <summary>Gets the data type information for the specified field. Returns the name of the System.Type exposed by GetFieldType(index) by default. Override to change this behavior.</summary>
    /// <remarks>The data type information can differ from the type information returned by GetFieldType, especially where the underlying data types do not map one for one to the runtime types supported by the language. 
    /// (For example, GetDataTypeName(..) may be "integer" while GetFieldType(..) may be "Int32".)</remarks>
    public virtual string GetDataTypeName(int index) => GetFieldType(index).Name; // By default, return the name of the System.Type returned by GetFieldType(index).
    public virtual System.DateTime GetDateTime(int index) => (System.DateTime)GetValue(index);
    public virtual decimal GetDecimal(int index) => (decimal)GetValue(index);
    public virtual double GetDouble(int index) => (double)GetValue(index);
    /// <summary>Gets the Type information corresponding to the type of Object that would be returned from GetValue(Int32).</summary>
    /// <remarks>This information can be used to increase performance by indicating the strongly-typed accessor to call. 
    /// (For example, using GetInt32 is roughly ten times faster than using GetValue.)</remarks>
    public abstract System.Type GetFieldType(int index); //=> typeof(object);
    public virtual float GetFloat(int index) => (float)GetValue(index);
    public virtual System.Guid GetGuid(int index) => (System.Guid)GetValue(index);
    public virtual short GetInt16(int index) => (short)GetValue(index);
    public virtual int GetInt32(int index) => (int)GetValue(index);
    public virtual long GetInt64(int index) => (long)GetValue(index);
    public abstract string GetName(int index); //=> throw new System.NotImplementedException();
    public abstract int GetOrdinal(string name); //=> throw new System.NotImplementedException();
    public virtual string GetString(int index) => (string)GetValue(index);
    public abstract object GetValue(int index); //=> throw new System.NotImplementedException();
    public virtual int GetValues(object[] values)
    {
      System.ArgumentNullException.ThrowIfNull(values);

      var count = System.Math.Min(FieldCount, values.Length);

      for (var index = count - 1; index >= 0; index--)
      {
        values[index] = GetValue(index);
      }

      return count;
    }
    public virtual bool IsDBNull(int index) => (GetValue(index) ?? System.DBNull.Value) is System.DBNull;

    // IEnumerable
    public System.Collections.Generic.IEnumerator<System.Data.IDataRecord> GetEnumerator() => new DataReaderEnumerator(this);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Creates an enumerator for an <see cref="System.Data.IDataReader"/>.</summary>
    private sealed class DataReaderEnumerator
      : Disposable, System.Collections.Generic.IEnumerator<System.Data.IDataRecord>
    {
      private readonly System.Data.IDataReader m_dataReader;

      public DataReaderEnumerator(System.Data.IDataReader dataReader)
      {
        System.ArgumentNullException.ThrowIfNull(dataReader);

        m_dataReader = dataReader;
      }

      // IEnumerator
      public System.Data.IDataRecord Current => m_dataReader;
      object System.Collections.IEnumerator.Current => m_dataReader;
      public bool MoveNext() => m_dataReader.Read();
      public void Reset() => throw new System.NotSupportedException($"Implementations of System.Data.IDataReader are forward-only constructs.");
    }
  }
}
