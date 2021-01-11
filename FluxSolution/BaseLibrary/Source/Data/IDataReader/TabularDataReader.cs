namespace Flux.Data
{
	/// <summary>Base class for implementing System.Data.IDataReader over tabular data.</summary>
	public abstract class TabularDataReader
		: Disposable, System.Data.IDataReader
	{
		public const string ColumnOrdinal = nameof(ColumnOrdinal);
		public const string ColumnName = nameof(ColumnName);
		public const string DataType = nameof(DataType);
		public const string ColumnSize = nameof(ColumnSize);
		public const string AllowDBNull = nameof(AllowDBNull);
		public const string TsqlDataTypeDefinition = nameof(TsqlDataTypeDefinition);

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
			=> FieldNames.AddRange(fieldNames ?? System.Linq.Enumerable.Empty<string>());
		public TabularDataReader(System.Collections.Generic.IEnumerable<string> fieldNames, System.Collections.Generic.IEnumerable<System.Type> fieldTypes)
			: this(fieldNames)
			=> FieldTypes.AddRange(fieldTypes ?? System.Linq.Enumerable.Empty<System.Type>());
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
		public bool GetSchemaTableAllowDBNull(int index)
			=> index >= 0 && index < FieldAllowDBNulls.Count ? FieldAllowDBNulls[index] : true;
		public string GetSchemaTableTsqlDataTypeDefinition(int index)
			=> index >= 0 && index < FieldTsqlDataTypes.Count ? FieldTsqlDataTypes[index] : (TsqlDataType.NameFromType(GetFieldType(index)) is var tsqlTypeName ? tsqlTypeName + TsqlDataType.GetDefaultArgument(tsqlTypeName) : throw new System.Exception(@"Could not construct T-SQL data type definition."));

		// IDataReader
		public int Depth
			=> 0;
		public bool IsClosed
			=> FieldValues.Count == 0;
		public int RecordsAffected
			=> -1;
		public void Close()
			=> DisposeManaged();
		public System.Data.DataTable GetSchemaTable()
		{
			var dt = new System.Data.DataTable(@"SchemaTable")
			{
				Columns = {
						{ ColumnOrdinal, typeof(int) },
						{ ColumnName, typeof(string) },
						{ DataType, typeof(System.Type) },
						{ ColumnSize, typeof(int) },
						{ AllowDBNull, typeof(bool) },
						{ TsqlDataTypeDefinition, typeof(string) }
					}
			};

			for (var index = 0; index < FieldCount; index++)
			{
				dt.Rows.Add(new object[] { index, GetName(index), GetFieldType(index), -1, GetSchemaTableAllowDBNull(index), GetSchemaTableTsqlDataTypeDefinition(index) });
			}

			return dt;
		}
		public bool NextResult()
			=> false;
		public abstract bool Read();

		// IDataRecord
		public int FieldCount
			=> FieldNames.Count;
		public object this[int index]
			=> GetValue(index);
		public object this[string name]
			=> GetValue(GetOrdinal(name));
		public bool GetBoolean(int index)
			=> (bool)GetValue(index);
		public byte GetByte(int index)
			=> (byte)GetValue(index);
		public long GetBytes(int index, long fieldIndex, byte[] buffer, int bufferIndex, int length)
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
		public char GetChar(int index)
			=> (char)GetValue(index);
		public long GetChars(int index, long fieldIndex, char[] buffer, int bufferIndex, int length)
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
		public System.Data.IDataReader GetData(int index)
			=> throw new System.NotImplementedException();
		/// <summary>Gets the data type information for the specified field. Return the name of the System.Type exposed by GetFieldType(index) by default. Override to change this behavior.</summary>
		/// <remarks>The data type information can differ from the type information returned by GetFieldType, especially where the underlying data types do not map one for one to the runtime types supported by the language. 
		/// (For example, DataTypeName may be "integer", while Type.Name may be "Int32".)</remarks>
		public string GetDataTypeName(int index)
			=> GetFieldType(index).Name; // By default, return the name of the System.Type returned by GetFieldType(index).
		public System.DateTime GetDateTime(int index)
			=> (System.DateTime)GetValue(index);
		public decimal GetDecimal(int index)
			=> (decimal)GetValue(index);
		public double GetDouble(int index)
			=> (double)GetValue(index);
		public System.Type GetFieldType(int index)
			=> index >= 0 && index < FieldTypes.Count ? FieldTypes[index] : typeof(object);
		public float GetFloat(int index)
			=> (float)GetValue(index);
		public System.Guid GetGuid(int index)
			=> (System.Guid)GetValue(index);
		public short GetInt16(int index)
			=> (short)GetValue(index);
		public int GetInt32(int index)
			=> (int)GetValue(index);
		public long GetInt64(int index)
			=> (long)GetValue(index);
		public string GetName(int index)
			=> FieldNames[index];
		public int GetOrdinal(string name)
			=> FieldNames.IndexOf(name);
		public string GetString(int index)
			=> (string)GetValue(index);
		public object GetValue(int index)
			=> FieldValues[index];
		public virtual int GetValues(object[] values)
		{
			if (values is null) throw new System.ArgumentNullException(nameof(values));

			var count = System.Math.Min(FieldCount, values.Length);

			for (var index = count - 1; index >= 0; index--)
			{
				values[index] = GetValue(index);
			}

			return count;
		}
		public bool IsDBNull(int index)
			=> (GetValue(index) ?? System.DBNull.Value) is System.DBNull;

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
