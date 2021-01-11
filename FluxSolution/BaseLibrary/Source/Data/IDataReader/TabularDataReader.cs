namespace Flux.Data
{
	/// <summary>Abstract class for implementing a <see cref="DataReader"/> over tabular data.</summary>
	public abstract class TabularDataReader
		: DataReader
	{
		/// <summary>An array of the field names for the result.</summary>
		public System.Collections.Generic.List<string> FieldNames { get; } = new System.Collections.Generic.List<string>();
		///// <summary>FieldTypes is an optional functionality and each field will default to typeof(object).</summary>
		public System.Collections.Generic.List<System.Type> FieldTypes { get; } = new System.Collections.Generic.List<System.Type>();
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
		/// <summary>This will create the tabular data reader with everything being empty.</summary>
		public TabularDataReader()
		{
		}

		// IDataRecord
		public override int FieldCount
			=> FieldNames.Count;
		/// <summary>Gets the data type information for the specified field. Return the name of the System.Type exposed by GetFieldType(index) by default. Override to change this behavior.</summary>
		/// <remarks>The data type information can differ from the type information returned by GetFieldType, especially where the underlying data types do not map one for one to the runtime types supported by the language. 
		/// (For example, DataTypeName may be "integer", while Type.Name may be "Int32".)</remarks>
		public override System.Type GetFieldType(int index)
			=> index >= 0 && index < FieldTypes.Count ? FieldTypes[index] : typeof(object); //  Returns the corresponding System.Type in the FieldTypes array, if present, otherwise typeof(object) is returned.
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
