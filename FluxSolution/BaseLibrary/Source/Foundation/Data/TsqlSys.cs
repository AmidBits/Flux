using System.Linq;

namespace Flux.Data
{
  public static class TsqlSys
  {
    public static string SelectColumnTSqlDefinitions(TsqlName table3)
      => $"{SelectColumns(table3, "'[' + C.[name] +'] ['+ CT.[name] +']'+ CASE WHEN CT.[name] IN ('datetime2', 'datetimeoffset', 'time') THEN '(' + CONVERT([varchar](10), COALESCE(C.[scale], 7)) + ')' WHEN CT.[name] IN ('decimal', 'numeric') THEN '(' + CONVERT([varchar](10), COALESCE(C.[precision], 18)) + ',' + CONVERT([varchar](10), COALESCE(C.[scale], 0)) + ')' WHEN (CT.[name] LIKE '%binary' OR CT.[name] LIKE '%char') THEN CASE WHEN C.[max_length] = -1 THEN '(MAX)' WHEN CT.[name] LIKE 'n%' THEN '(' + CONVERT([nvarchar](10), C.[max_length] / 2) + ')' ELSE '(' + CONVERT([nvarchar](10), C.[max_length]) + ')' END ELSE '' END +' '+ (CASE WHEN C.[is_nullable] = 0 then 'NOT NULL' ELSE 'NULL' END) AS [TSqlDefinition] INTO #Columns")}; {Tsql.Select(Tsql.Stuff("(SELECT N', ' + [TSqlDefinition] FROM #Columns FOR XML PATH(''))", 1, 2))}";
    /// <summary>Select rows from [sys].[columns] for the specified table, optionally with specified columns.</summary>
    public static string SelectColumns(TsqlName tableName3, params string[] columnNames)
      => $"SELECT {(columnNames.Any() ? Tsql.CombineColumnNames(columnNames) : @"C.*")} FROM sys.schemas S (NOLOCK) INNER JOIN sys.tables AS T (NOLOCK) ON (S.[schema_id] = T.[schema_id]) INNER JOIN sys.columns AS C (NOLOCK) ON (T.[object_id] = C.[object_id]) INNER JOIN sys.types CT (NOLOCK) ON (C.system_type_id = CT.system_type_id AND C.user_type_id = CT.user_type_id) WHERE DB_NAME() = '{tableName3.DatabaseName}' AND S.[name] = '{tableName3.SchemaName}' AND T.[name] = '{tableName3.ObjectName}' ORDER BY [column_id]";
    /// <summary>Select the record from [sys].[tables] for the specifified table, optionally with specified columns.</summary>
    public static string SelectTables(TsqlName tableName3, params string[] columnNames)
      => $"SELECT {(columnNames.Any() ? Tsql.CombineColumnNames(columnNames) : @"T.*")} FROM sys.schemas S (NOLOCK) INNER JOIN sys.tables AS T (NOLOCK) ON (S.[schema_id] = T.[schema_id]) WHERE DB_NAME() = '{tableName3.DatabaseName}' AND S.[name] = '{tableName3.SchemaName}' AND T.[name] = '{tableName3.ObjectName}'";
  }
}
