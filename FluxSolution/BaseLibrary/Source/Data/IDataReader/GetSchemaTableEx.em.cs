using System.Linq;

namespace Flux
{
  public static partial class IDataReaderEm
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

    public static System.Data.DataTable GetSchemaTableEx(this System.Data.IDataReader source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var schemaTable = source.GetSchemaTable();

      if (schemaTable is null)
        return new System.Data.DataTable();

      AppendCurrentDefinitions();
      AppendDefaultDefinitions();
      AppendAdaptedDefinitions();

      return schemaTable;

      void AppendCurrentDefinitions()
      {
        if (schemaTable.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ContainsAll(new string[] { CsAllowDBNull, CsColumnName, CsColumnSize, CsDataTypeName, CsNumericPrecision, CsNumericScale }))
        {
          if (!schemaTable.Columns.Contains(CsTsqlDefinitionCurrent)) schemaTable.Columns.Add(CsTsqlDefinitionCurrent, typeof(string));

          foreach (var row in schemaTable.Rows.Cast<System.Data.DataRow>())
          {
            var allowDBNullString = (bool)row[CsAllowDBNull] ? @"NULL" : @"NOT NULL";
            var columnName = (string)row[CsColumnName] ?? $"Column{row[CsColumnOrdinal]}";
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
      }

      void AppendAdaptedDefinitions()
      {
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
      }

      void AppendDefaultDefinitions()
      {
        if (schemaTable.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ContainsAll(new string[] { CsColumnName, CsColumnOrdinal, CsDataType }))
        {
          if (!schemaTable.Columns.Contains(CsTsqlDefinitionDefault)) schemaTable.Columns.Add(CsTsqlDefinitionDefault, typeof(string));

          foreach (var row in schemaTable.Rows.Cast<System.Data.DataRow>())
          {
            if (row.IsNull(CsTsqlDefinitionDefault))
            {
              var columnName = (string)row[CsColumnName] is var s && string.IsNullOrWhiteSpace(s) ? $"Column{(int)row[CsColumnOrdinal] + 1}" : s;
              var dataType = row[CsDataType] == System.DBNull.Value ? typeof(object) : (System.Type)row[CsDataType];
              var dataTypeName = Data.TsqlDataType.NameFromType(dataType);
              var dataTypeArgument = Data.TsqlDataType.GetDefaultArgument(dataTypeName, true);

              row[CsTsqlDefinitionDefault] = $"[{columnName}] [{dataTypeName}]{dataTypeArgument} NULL";
            }
          }
        }
      }
    }

    public static System.Data.DataTable GetSchemaTableEx(this System.Data.IDataReader source, out string[]? tsqlAdaptedDefinitions, out string[]? tsqlCurrentDefinitions, out string[]? tsqlDefaultDefinitions)
    {
      var schemaTable = source.GetSchemaTableEx();

      try { tsqlCurrentDefinitions = schemaTable.Rows.Cast<System.Data.DataRow>().Select(row => (string)row[CsTsqlDefinitionCurrent]).ToArray(); } catch { tsqlCurrentDefinitions = null; }
      try { tsqlDefaultDefinitions = schemaTable.Rows.Cast<System.Data.DataRow>().Select(row => (string)row[CsTsqlDefinitionDefault]).ToArray(); } catch { tsqlDefaultDefinitions = null; }
      try { tsqlAdaptedDefinitions = schemaTable.Rows.Cast<System.Data.DataRow>().Select(row => (string)row[CsTsqlDefinitionAdapted]).ToArray(); } catch { tsqlAdaptedDefinitions = null; }

      return schemaTable;
    }
  }
}
