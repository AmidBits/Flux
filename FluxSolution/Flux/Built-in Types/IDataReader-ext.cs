namespace Flux
{
  public static partial class IDataReaderExtensions
  {
    const string CsAllowDBNull = @"AllowDBNull";
    const string CsColumnName = @"ColumnName";
    const string CsColumnOrdinal = @"ColumnOrdinal";
    const string CsColumnSize = @"ColumnSize";
    const string CsDataType = @"DataType";
    const string CsDataTypeName = @"DataTypeName";
    const string CsNumericPrecision = @"NumericPrecision";
    const string CsNumericScale = @"NumericScale";

    const string CsTsqlDefinitionAdapted = @"TsqlDefinitionAdapted";
    const string CsTsqlDefinitionCurrent = @"TsqlDefinitionCurrent";
    const string CsTsqlDefinitionDefault = @"TsqlDefinitionDefault";

    extension(System.Data.IDataReader source)
    {
      public System.Data.DataTable GetSchemaTableEx()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var schemaTable = source.GetSchemaTable();

        if (schemaTable is null)
          return new System.Data.DataTable();

        // Append current definitions.
        if (schemaTable.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToHashSet().All(e => new string[] { CsAllowDBNull, CsColumnName, CsColumnSize, CsDataTypeName, CsNumericPrecision, CsNumericScale }.Contains(e)))
        {
          if (!schemaTable.Columns.Contains(CsTsqlDefinitionCurrent)) schemaTable.Columns.Add(CsTsqlDefinitionCurrent, typeof(string));

          foreach (var row in schemaTable.Rows.Cast<System.Data.DataRow>())
          {
            var allowDBNullString = (bool)row[CsAllowDBNull] ? @"NULL" : @"NOT NULL";
            var columnName = (string)row[CsColumnName] ?? ((int)row[CsColumnOrdinal]).ToSingleOrdinalColumnName();
            var columnSize = (int)row[CsColumnSize];
            var dataTypeName = (string)row[CsDataTypeName];

            var dataTypeArgument = string.Empty;

            switch (dataTypeName)
            {
              case Data.TsqlDataType.Timestamp:
                break;
              case Data.TsqlDataType.Binary:
              case Data.TsqlDataType.Varbinary:
              case Data.TsqlDataType.Char:
              case Data.TsqlDataType.Nchar:
              case Data.TsqlDataType.Nvarchar:
              case Data.TsqlDataType.Varchar:
                dataTypeArgument = (columnSize == -1 || columnSize == 2147483647 || columnSize == 1073741823 ? @"(MAX)" : $"({columnSize})"); // 2147483647 from SqlClient and 1073741823 from OleDb/Odbc.
                break;
              case Data.TsqlDataType.Decimal:
              case Data.TsqlDataType.Numeric:
                dataTypeArgument = $"({row[@"NumericPrecision"]},{row[@"NumericScale"]})";
                break;
              case Data.TsqlDataType.Datetime2:
              case Data.TsqlDataType.Datetimeoffset:
              case Data.TsqlDataType.Time:
                dataTypeArgument = $"({row[@"NumericScale"]})";
                break;
              case string s when s.LastIndexOf('.') is var index && index > -1:
                dataTypeName = dataTypeName[(index + 1)..];
                break;
            }

            row[CsTsqlDefinitionCurrent] = $"[{columnName}] [{dataTypeName}]{dataTypeArgument} {allowDBNullString}";
          }
        }

        // Append default definitions.
        if (schemaTable.Columns.Contains(CsTsqlDefinitionCurrent))
        {
          if (!schemaTable.Columns.Contains(CsTsqlDefinitionAdapted)) schemaTable.Columns.Add(CsTsqlDefinitionAdapted, typeof(string));

          foreach (var row in schemaTable.Rows.Cast<System.Data.DataRow>())
          {
            if (row.IsNull(CsTsqlDefinitionAdapted))
            {
              if (Data.TsqlColumnDefinition.Parse((string)row[CsTsqlDefinitionCurrent]) is var definition && definition.DataTypeName.Equals(Data.TsqlDataType.Rowversion, System.StringComparison.Ordinal) || definition.DataTypeName.Equals(Data.TsqlDataType.Timestamp, System.StringComparison.Ordinal))
              {
                row[CsTsqlDefinitionAdapted] = $"[{definition.ColumnName}] [{(definition.Nullability.Equals(@"NULL") ? Data.TsqlDataType.Varbinary : Data.TsqlDataType.Binary)}](8) {definition.Nullability}";
              }
              else row[CsTsqlDefinitionAdapted] = row[CsTsqlDefinitionCurrent];
            }
          }
        }

        // Append adapted definitions.
        if (schemaTable.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToHashSet().All(e => new string[] { CsColumnName, CsColumnOrdinal, CsDataType }.Contains(e)))
        {
          if (!schemaTable.Columns.Contains(CsTsqlDefinitionDefault)) schemaTable.Columns.Add(CsTsqlDefinitionDefault, typeof(string));

          foreach (var row in schemaTable.Rows.Cast<System.Data.DataRow>())
          {
            if (row.IsNull(CsTsqlDefinitionDefault))
            {
              var columnName = (string)row[CsColumnName] is var s && string.IsNullOrWhiteSpace(s) ? ((int)row[CsColumnOrdinal]).ToSingleOrdinalColumnName() : s;
              var dataType = row[CsDataType] == System.DBNull.Value ? typeof(object) : (System.Type)row[CsDataType];
              var dataTypeName = Data.TsqlDataType.NameFromType(dataType);
              var dataTypeArgument = Data.TsqlDataType.GetDefaultArgument(dataTypeName, true);

              row[CsTsqlDefinitionDefault] = $"[{columnName}] [{dataTypeName}]{dataTypeArgument} NULL";
            }
          }
        }

        return schemaTable;
      }

      public System.Data.DataTable GetSchemaTableEx(out string[]? tsqlAdaptedDefinitions, out string[]? tsqlCurrentDefinitions, out string[]? tsqlDefaultDefinitions)
      {
        var schemaTable = source.GetSchemaTableEx();

        try { tsqlCurrentDefinitions = schemaTable.Rows.Cast<System.Data.DataRow>().Select(row => (string)row[CsTsqlDefinitionCurrent]).ToArray(); } catch { tsqlCurrentDefinitions = null; }
        try { tsqlDefaultDefinitions = schemaTable.Rows.Cast<System.Data.DataRow>().Select(row => (string)row[CsTsqlDefinitionDefault]).ToArray(); } catch { tsqlDefaultDefinitions = null; }
        try { tsqlAdaptedDefinitions = schemaTable.Rows.Cast<System.Data.DataRow>().Select(row => (string)row[CsTsqlDefinitionAdapted]).ToArray(); } catch { tsqlAdaptedDefinitions = null; }

        return schemaTable;
      }

      /// <summary>Creates a new data table with values from the IDataReader. The specified table name is used in constructing the data table.</summary>
      public object[][] ToArray()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var length = 0;

        var array = new object[2][];

        while (source.Read())
        {
          if (length == array.Length)
            System.Array.Resize(ref array, array.Length * 2);

          array[length++] = source.GetValues().ToArray();
        }

        if (length < array.Length)
          System.Array.Resize(ref array, length);

        return array;
      }

      /// <summary>Creates a new data table with values from the IDataReader. The specified table name is used in constructing the data table.</summary>
      public System.Data.DataTable ToDataTable(string tableName)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var dt = new System.Data.DataTable(tableName);

        for (var index = 0; index < source.FieldCount; index++)
          dt.Columns.Add(source.GetNameEx(index));

        dt.Load(source);

        return dt;
      }
    }
  }
}
