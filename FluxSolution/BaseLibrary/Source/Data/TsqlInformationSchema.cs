using System.Linq;

namespace Flux.Data
{
  /// <summary></summary>
  /// <seealso cref="https://stackoverflow.com/questions/7419225/build-column-definition-from-information-schema-columns-table" />
  public static class TsqlInformationSchema
  {
    private static readonly string[] m_columns = new string[] { @"TABLE_CATALOG", @"TABLE_SCHEMA", @"TABLE_NAME", @"COLUMN_NAME", @"ORDINAL_POSITION", @"COLUMN_DEFAULT", @"IS_NULLABLE", @"DATA_TYPE", @"CHARACTER_MAXIMUM_LENGTH", @"CHARACTER_OCTET_LENGTH", @"NUMERIC_PRECISION", @"NUMERIC_SCALE", @"DATETIME_PRECISION" };

    //public static TsqlColumnDefinition SelectColumnDefinitions(this System.Data.IDataRecord idr)
    //{
    //  var columnName = idr.GetString(idr.GetOrdinal(CsColumnName));

    //  //var ordinalPosition = int.Parse(idr.GetString(idr.GetOrdinal(CsOrdinalPosition)), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture);

    //  var dataType = idr.GetString(idr.GetOrdinal(CsDataType));

    //  var arguments = System.Array.Empty<string>();

    //  if (dataType.EndsWith(TsqlDataTypeName.Text, System.StringComparison.OrdinalIgnoreCase) || dataType.Equals(TsqlDataTypeName.Image, System.StringComparison.OrdinalIgnoreCase) || dataType.Equals(TsqlDataTypeName.SqlVariant, System.StringComparison.OrdinalIgnoreCase) || dataType.Equals(TsqlDataTypeName.Xml, System.StringComparison.OrdinalIgnoreCase))
    //  {
    //    arguments = System.Array.Empty<string>();
    //  }
    //  else if (dataType.Equals(TsqlDataTypeName.Datetime2, System.StringComparison.OrdinalIgnoreCase) || dataType.Equals(TsqlDataTypeName.Datetimeoffset, System.StringComparison.OrdinalIgnoreCase) || dataType.Equals(TsqlDataTypeName.Time, System.StringComparison.OrdinalIgnoreCase))
    //  {
    //    var dateTimePrecision = idr.GetString(idr.GetOrdinal(CsDatetimePrecision));

    //    arguments = new string[] { dateTimePrecision };
    //  }
    //  else if (dataType.Equals(TsqlDataTypeName.Decimal, System.StringComparison.OrdinalIgnoreCase) || dataType.Equals(TsqlDataTypeName.Numeric, System.StringComparison.OrdinalIgnoreCase))
    //  {
    //    var numericPrecision = idr.GetString(idr.GetOrdinal(@"NUMERIC_PRECISION"));
    //    var numericScale = idr.GetString(idr.GetOrdinal(@"NUMERIC_SCALE"));

    //    arguments = new string[] { numericPrecision, numericScale };
    //  }
    //  else if (idr.GetString(idr.GetOrdinal(@"CHARACTER_MAXIMUM_LENGTH")) is var characterMaximumLength && characterMaximumLength == "-1")
    //  {
    //    arguments = new string[] { @"MAX" };
    //  }
    //  else if ((dataType.EndsWith(TsqlDataTypeName.Binary, System.StringComparison.OrdinalIgnoreCase) || dataType.EndsWith(TsqlDataTypeName.Char, System.StringComparison.OrdinalIgnoreCase)) && characterMaximumLength != "-1")
    //  {
    //    arguments = new string[] { characterMaximumLength };
    //  }

    //  var isNullable = idr.GetString(idr.GetOrdinal(CsIsNullable)) switch { CsNo => false, CsYes => true, _ => throw new System.Exception(CsIsNullable) };

    //  return new TsqlColumnDefinition(columnName, dataType, arguments, new TsqlNullability(isNullable));
    //}

    //public static string SelectColumnTsqlDefinitions(TsqlName table3)
    //  => $"{SelectColumns(table3, "'[' + column_name +'] ['+ data_type +']'+ CASE WHEN DATA_TYPE IN ('datetime2', 'datetimeoffset', 'time') THEN '(' + CONVERT([varchar](10), COALESCE(DATETIME_PRECISION, 7)) + ')' WHEN DATA_TYPE IN ('decimal', 'numeric') THEN '(' + CONVERT([varchar](10), COALESCE(NUMERIC_PRECISION, 18)) + ',' + CONVERT([varchar](10), COALESCE(NUMERIC_SCALE, 0)) + ')' WHEN (DATA_TYPE LIKE '%binary' OR DATA_TYPE LIKE '%char') THEN CASE WHEN CHARACTER_MAXIMUM_LENGTH = -1 THEN '(MAX)' ELSE '(' + CONVERT([nvarchar](10), CHARACTER_MAXIMUM_LENGTH) + ')' END ELSE '' END +' '+ (CASE WHEN IS_NULLABLE = 'No' then 'NOT NULL' ELSE 'NULL' END) AS [TSqlDefinition] INTO #Columns")}; {Tsql.Select(Tsql.Stuff("(SELECT N', ' + [TSqlDefinition] FROM #Columns FOR XML PATH(''))", 1, 2))}";

    /// <summary>Select rows from [INFORMATION_SCHEMA].[COLUMNS] for the specified table, optionally with specified columns.</summary>
    //public static string SelectColumns(TsqlName tableName3, params string[] columnNames)
    //  => $"{Tsql.Select(columnNames)} FROM {tableName3.DatabaseNameQuoted}.[INFORMATION_SCHEMA].[COLUMNS] (NOLOCK) WHERE TABLE_CATALOG = '{tableName3.DatabaseName}' AND TABLE_SCHEMA = '{tableName3.SchemaName}' AND TABLE_NAME = '{tableName3.ObjectName}' ORDER BY ORDINAL_POSITION";
    /// <summary>Select the record from [INFORMATION_SCHEMA].[TABLES] for the specifified table, optionally with specified columns.</summary>
    //public static string SelectTables(TsqlName tableName3, params string[] columnNames)
    //  => $"{Tsql.Select(columnNames)} FROM {tableName3.DatabaseNameQuoted}.[INFORMATION_SCHEMA].[TABLES] (NOLOCK) WHERE TABLE_CATALOG = '{tableName3.DatabaseName}' AND TABLE_SCHEMA = '{tableName3.SchemaName}' AND TABLE_NAME = '{tableName3.ObjectName}'";

    public static string JoinColumnNames(System.Collections.Generic.IEnumerable<string> columnNames, bool quoted = true, string? alias = null)
      => string.Join(@", ", columnNames.Any() ? columnNames.Select(cn => (!(alias is null) ? alias : string.Empty) + (quoted ? '[' + cn + ']' : cn)) : (!(alias is null) ? alias : string.Empty) + @"*");
    public static string JoinColumnNames(params string[] columnNames)
      => JoinColumnNames(columnNames, true, null);


    /// <summary>Select all rows with column information from INFORMATION_SCHEMA.COLUMNS for the specified schema and table ordered by ordinal position.</summary>
    public static string SelectColumns(string schemaName, string tableName)
      => $"SELECT {string.Join(@", ", m_columns)} FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{schemaName}' AND TABLE_NAME = '{tableName}' ORDER BY ORDINAL_POSITION";
    /// <summary>Select rows from [sys].[columns] for the specified table, optionally with specified columns.</summary>
    public static string SelectColumns(TsqlName tableName3, params string[] columnNames)
      => $"SELECT {(columnNames.Any() ? JoinColumnNames(columnNames) : @"C.*")} FROM sys.schemas S (NOLOCK) INNER JOIN sys.tables AS T (NOLOCK) ON (S.[schema_id] = T.[schema_id]) INNER JOIN sys.columns AS C (NOLOCK) ON (T.[object_id] = C.[object_id]) INNER JOIN sys.types CT (NOLOCK) ON (C.system_type_id = CT.system_type_id AND C.user_type_id = CT.user_type_id) WHERE DB_NAME() = '{tableName3.DatabaseName}' AND S.[name] = '{tableName3.SchemaName}' AND T.[name] = '{tableName3.ObjectName}' ORDER BY [column_id]";

    /// <summary>Select the record from [INFORMATION_SCHEMA].[TABLES] for the specifified table, optionally with specified columns.</summary>
    public static string SelectTable(TsqlName tableName3, params string[] columnNames)
      => $"{Tsql.Select(columnNames)} FROM {tableName3.DatabaseNameQuoted}.[INFORMATION_SCHEMA].[TABLES] (NOLOCK) WHERE TABLE_CATALOG = '{tableName3.DatabaseName}' AND TABLE_SCHEMA = '{tableName3.SchemaName}' AND TABLE_NAME = '{tableName3.ObjectName}'";
  }
}
