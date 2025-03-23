//using System.Linq;

//namespace Flux.Data
//{
//  public static class Tsql // T-SQL static class (within the Sql static class) for organization.
//  {
//    #region Column mappings
//    public static System.Data.Common.DataColumnMappingCollection CreateColumnMappings(System.Collections.Generic.IList<string> sourceNames, System.Collections.Generic.IList<string> targetNames)
//    {
//      if (sourceNames is null) throw new System.ArgumentNullException(nameof(sourceNames));
//      if (targetNames is null) throw new System.ArgumentNullException(nameof(targetNames));

//      var dcmc = new System.Data.Common.DataColumnMappingCollection();
//      for (var index = 0; index < sourceNames.Count && index < targetNames.Count; index++)
//        dcmc.Add(sourceNames[index], targetNames[index]);
//      return dcmc;
//    }

//    //public static System.Collections.Generic.IEnumerable<(TResultLeft, TResultRight)> CreateColumnMappings<TResultLeft, TResultRight>(System.Collections.Generic.IDictionary<int, string> source, System.Collections.Generic.IDictionary<int, string> target, System.Func<System.Collections.Generic.KeyValuePair<int, string>, System.Collections.Generic.KeyValuePair<int, string>, (TResultLeft, TResultRight)> selector)
//    //{
//    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
//    //  if (target is null) throw new System.ArgumentNullException(nameof(target));
//    //  if (selector is null) throw new System.ArgumentNullException(nameof(selector));

//    //  using var sourceEnumerator = source.GetEnumerator();
//    //  using var targetEnumerator = target.GetEnumerator();

//    //  while (sourceEnumerator.MoveNext() && targetEnumerator.MoveNext())
//    //  {
//    //    yield return selector(sourceEnumerator.Current, targetEnumerator.Current);
//    //  }
//    //}
//    #endregion Column mappings

//    //public static string FormatColumnDefinition(string columnName, string dataTypeName, string dataTypeArgument, string nullString)
//    //{
//    //  ValidateColumnDefinition(columnName, dataTypeName, dataTypeArgument, nullString);

//    //  return $"{columnName.QuoteTsql()} {dataTypeName.QuoteTsql()}{dataTypeArgument.Wrap('(', ')')} {nullString}";
//    //}
//    //public static (string columnName, string dataTypeName, string dataTypeArguments, string nullString) ParseColumnDefinition(string tsql_column_definition)
//    //{
//    //  var match = System.Text.RegularExpressions.Regex.Match(tsql_column_definition, @"(?<ColumnName>\[.+\])\s*?(?<DataTypeName>\[.+\])\s*?(?<Arguments>\(.+\))?\s*?(?<Nullability>NULL|NOT NULL)");

//    //  if (match.Success && match.Groups["ColumnName"] is var g1 && g1.Success && match.Groups["DataTypeName"] is var g2 && g2.Success && match.Groups["Arguments"] is var g3 && match.Groups["Nullability"] is var g4 && g4.Success)
//    //  {
//    //    var columnName = g1.Value.UnquoteTsql();
//    //    var dataTypeName = g2.Value.UnquoteTsql();
//    //    var dataTypeArguments = g3.Success ? g3?.Value ?? string.Empty : string.Empty;
//    //    var nullString = g4.Value;

//    //    ValidateColumnDefinition(columnName, dataTypeName, dataTypeArguments, nullString);

//    //    return (columnName, dataTypeName, dataTypeArguments, nullString);
//    //  }

//    //  throw new System.ArgumentOutOfRangeException(nameof(tsql_column_definition));
//    //}
//    //public static bool TryFormatColumnDefinition(string columnName, string dataTypeName, string argument, string nullability, out string? result)
//    //{
//    //  try
//    //  {
//    //    result = FormatColumnDefinition(columnName, dataTypeName, argument, nullability);
//    //    return true;
//    //  }
//    //  catch { }

//    //  result = default;
//    //  return false;
//    //}
//    //public static bool TryParseColumnDefinition(string fullyQualifiedName, out (string columnName, string dataTypeName, string arguments, string nullability) result)
//    //{
//    //  try
//    //  {
//    //    result = ParseColumnDefinition(fullyQualifiedName);
//    //    return true;
//    //  }
//    //  catch { }

//    //  result = default;
//    //  return false;
//    //}
//    //public static void ValidateColumnDefinition(string columnName, string dataTypeName, string dataTypeArguments, string nullString)
//    //{
//    //  dataTypeName = dataTypeName.UnquoteTsql();

//    //  if (string.IsNullOrWhiteSpace(dataTypeName) || !SqlType.GetTypeNames().Contains(dataTypeName))
//    //  {
//    //    throw new System.ArgumentOutOfRangeException($"Unrecognized data type name '{dataTypeName}'.");
//    //  }

//    //  dataTypeArguments = dataTypeArguments.Unwrap('(', ')');

//    //  if (dataTypeArguments is null || (dataTypeArguments.Length > 0 && !System.Text.RegularExpressions.Regex.IsMatch(dataTypeArguments, @"^\s*(\d+(\s*,\s*\d+)?|MAX)\s*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)))
//    //  {
//    //    throw new System.ArgumentException($"Invalid data type arguments '{dataTypeArguments}'.", nameof(dataTypeArguments));
//    //  }

//    //  if (string.IsNullOrWhiteSpace(nullString) || !NullForms.Any((nf => nf.Equals(nullString))))
//    //  {
//    //    throw new System.ArgumentOutOfRangeException($"Invalid null string '{nullString}' (must be 'NULL' or 'NOT NULL').");
//    //  }
//    //}

//    public static string All(string subquery)
//      => $"ALL ( {subquery} )";
//    public static string And(params string[] booleanExpressions)
//      => $"({string.Join(" AND ", booleanExpressions)})";
//    public static string Any(string subquery)
//      => $"ANY ( {subquery} )";
//    public static string BeginEnd(string statementOrBlock)
//      => $"BEGIN {statementOrBlock} END";
//    public static string Between(string leftExpression, string rightExpression)
//      => $"BETWEEN {leftExpression} AND {rightExpression}";
//    public static string Cast(string expression, string dataType)
//      => $"CAST({expression} AS {dataType})";
//    public static string Coalesce(params string[] expressions)
//      => $"COALESCE({string.Join(Static.CommaSpace, expressions)})";
//    public static string Convert(string dataType, string expression)
//      => $"CONVERT({dataType}, {expression})";
//    public static string Convert(string dataType, string expression, string style)
//      => $"CONVERT({dataType}, {expression}, {style})";
//    public static string Equal(string leftExpression, string rightExpression)
//      => $"{leftExpression} = {rightExpression}";
//    public static string Exists(string subquery)
//      => $"EXISTS ( {subquery} )";
//    public static System.Collections.Generic.IEnumerable<string> Exists(params string[] subqueries)
//      => subqueries.Select(subquery => Exists(subquery));
//    public static string If(string booleanExpression)
//      => $"IF {booleanExpression}";
//    public static string If(string booleanExpression, string statementOrBlock)
//      => $"{If(booleanExpression)} {statementOrBlock}";
//    public static string In(string subqueryOrExpression)
//      => $"IN ( {subqueryOrExpression} )";
//    public static string In(params string[] expressions)
//      => In(string.Join(Static.CommaSpace, expressions));
//    public static string IsNotNull(string expression)
//      => $"{expression} IS NOT NULL";
//    public static System.Collections.Generic.IEnumerable<string> IsNotNull(params string[] expressions)
//      => expressions.Select(expression => IsNotNull(expression));
//    public static string IsNull(string expression)
//      => $"{expression} IS NULL";
//    public static System.Collections.Generic.IEnumerable<string> IsNull(params string[] expressions)
//      => expressions.Select(expression => IsNull(expression));
//    public static string N(string expression)
//      => $"N'{expression}'";
//    public static string Not(string booleanExpression)
//      => $"NOT {booleanExpression}";
//    public static System.Collections.Generic.IEnumerable<string> Not(params string[] booleanExpressions)
//      => booleanExpressions.Select(boolean_expression => Not(boolean_expression));
//    public static string NotEqual(string leftExpression, string rightExpression)
//      => $"{leftExpression} != {rightExpression}";
//    public static string NullIf(string ifExpression, string replacementExpression)
//      => $"NULLIF({ifExpression}, {replacementExpression})";
//    public static string Or(params string[] booleanExpression)
//      => $"({string.Join(" OR ", booleanExpression)})";
//    public static string Raiserror(string msgStrOrLocalVariable, int severity = -1, int state = -1, params string[] args)
//      => $"RAISERROR( {msgStrOrLocalVariable}, {severity}, {state}{(args.Length > 0 ? $", {string.Join(Static.CommaSpace, args)}" : string.Empty)} )";
//    public static string Some(string subquery)
//      => $"SOME ( {subquery} )";
//    public static string Stuff(string characterExpression, int start, int length, string replacementExpression = "")
//      => $"STUFF({characterExpression}, {start}, {length}, '{replacementExpression}')";
//    public static string Throw(int errorNumber, string messageOrLocalVariable, byte state = 1)
//      => $"THROW {errorNumber}, {messageOrLocalVariable}, {state}";
//    public static string TryCatch(string tryStatementOrBlock, string catchStatementOrBlock)
//      => $"BEGIN TRY {tryStatementOrBlock} END TRY BEGIN CATCH {catchStatementOrBlock} END CATCH";
//    public static string WaitforDelay(System.TimeSpan timeToPass)
//      => $"WAITFOR DELAY '{timeToPass:hh:mm}'";
//    public static string WaitforTime(System.DateTime timeToExecute)
//      => $"WAITFOR TIME '{timeToExecute:HH:mm}'";
//    public static string While(string booleanExpression)
//      => $"WHILE {booleanExpression}";
//    public static string While(string booleanExpression, string statementOrBlock)
//      => $"{While(booleanExpression)} {statementOrBlock}";

//    #region Merge statements
//    public static string DeclareGetDate()
//      => @"DECLARE @DateTime [datetime] = GETDATE();DECLARE @Date [date] = CAST(@DateTime AS [date]);";
//    public static string DeclareEffectiveDate(bool highFrequency)
//      => $"DECLARE @iStartDate [{(highFrequency ? TsqlDataType.Bigint : TsqlDataType.Int)}] = CAST(FORMAT(GETDATE(), '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}') AS [{(highFrequency ? TsqlDataType.Bigint : TsqlDataType.Int)}])";
//    public static string DeclareEndDate(bool highFrequency)
//      => $"DECLARE @iEndDate [{(highFrequency ? TsqlDataType.Bigint : TsqlDataType.Int)}] = {(highFrequency ? @"29990101010101" : @"29990101")}";

//    public static string CreateMergeTable(TsqlName mergeName3, string[] providerTypes, bool mergeDense)
//      => $"CREATE TABLE {mergeName3.QualifiedNameQuoted(3)} ({string.Join(Static.CommaSpace, providerTypes)}, [EffectiveStartDate] {(mergeDense ? "[bigint]" : "[int]")} NOT NULL, [EffectiveEndDate] {(mergeDense ? "[bigint]" : "[int]")} NOT NULL)";
//    public static string CreateTable(TsqlName tableName3, string[] providerTypes)
//      => $"CREATE TABLE {tableName3.QualifiedNameQuoted(3)} ({string.Join(Static.CommaSpace, providerTypes)})";
//    public static string MergeTable(bool highFrequency, TsqlName mergeName3, TsqlName sourceName3, string[] fieldNames, string[] fieldProviderTypes)
//    {
//      var sb = new System.Text.StringBuilder();
//      sb.AppendLine($"DECLARE @GetDate [datetime] = GETDATE(); DECLARE @ProcessDate {(highFrequency ? "[bigint]" : "[int]")} = CONVERT({(highFrequency ? "[bigint]" : "[int]")}, FORMAT(@GetDate, '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}')); DECLARE @PriorProcessDate {(highFrequency ? "[bigint]" : "[int]")} = CONVERT({(highFrequency ? "[bigint]" : "[int]")}, FORMAT(DATEADD({(highFrequency ? "second" : "day")}, -1, @GetDate), '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}')); DECLARE @EndDate {(highFrequency ? "[bigint]" : "[int]")} = {(highFrequency ? "29990101010101" : "29990101")} ");
//      sb.AppendLine($";WITH T AS (SELECT * FROM {mergeName3.QualifiedNameQuoted(3)} WHERE [EffectiveEndDate] = @EndDate) MERGE INTO T ");
//      sb.AppendLine($"USING (SELECT * FROM {sourceName3.QualifiedNameQuoted(3)}) AS S ");
//      sb.AppendLine($"ON ({string.Join(" AND ", fieldNames.Select((name, index) => TsqlMerge.OnCompareEquality(name, fieldProviderTypes[index][..fieldProviderTypes[index].IndexOf(']', System.StringComparison.Ordinal)][(fieldProviderTypes[index].IndexOf('[', System.StringComparison.Ordinal) + 1)..], !fieldProviderTypes[index].EndsWith("NOT NULL", System.StringComparison.OrdinalIgnoreCase))))}) ");
//      sb.AppendLine($"WHEN NOT MATCHED BY SOURCE THEN UPDATE SET T.[EffectiveEndDate] = @PriorProcessDate ");
//      sb.Append($"WHEN NOT MATCHED BY TARGET THEN INSERT ({string.Join(Static.CommaSpace, fieldNames.Select(fieldName => $"[{fieldName}]"))}, [EffectiveStartDate], [EffectiveEndDate]) VALUES ({string.Join(Static.CommaSpace, fieldNames.Select(fieldName => $"[{fieldName}]"))}, @ProcessDate, @EndDate)");
//      sb.Append($"OUTPUT $action INTO @actions; SELECT 'INSERT=' + (SELECT CAST(COUNT(1) AS [nvarchar](10)) FROM @actions WHERE ActionType = 'INSERT') + '; UPDATE=' + (SELECT CAST(COUNT(1) AS [nvarchar](10)) FROM @actions WHERE ActionType = 'UPDATE') + '; DELETE=' + (SELECT CAST(COUNT(1) AS [nvarchar](10)) FROM @actions WHERE ActionType = 'DELETE')");
//      //sb.Append($"OUTPUT $action, inserted.*, deleted.*");
//      sb.Append(';');
//      return sb.ToString();
//    }
//    public static string SelectIntoMergeTable(TsqlName mergeName3, TsqlName sourceName3, bool mergeDense)
//      => $"SELECT *, CAST(0 AS {(mergeDense ? "[bigint]" : "[int]")}) AS [EffectiveStartDate], CAST(0 AS {(mergeDense ? "[bigint]" : "[int]")}) AS [EffectiveEndDate] \nINTO {mergeName3.QualifiedNameQuoted(3)} \nFROM {sourceName3.QualifiedNameQuoted(3)} (NOLOCK) WHERE 1 = 0; ";
//    public static string TableExists(TsqlName tableName3)
//      => Exists($"SELECT 1 FROM sys.schemas S INNER JOIN sys.tables T ON (S.[schema_id] = T.[schema_id]) WHERE DB_NAME() = '{tableName3.DatabaseName}' AND S.[name] = '{tableName3.SchemaName}' AND T.[name] = '{tableName3.ObjectName}'");
//    #endregion Merge statements

//    //public static string CompareCastNvarcharMax(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareCastNvarcharMax(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(Cast(columnLeft, DataTypeNvarcharMax), Cast(columnRight, DataTypeNvarcharMax));
//    //public static string CompareCastVarbinaryMax(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareCastNvarcharMax(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(Cast(columnLeft, DataTypeVarbinaryMax), Cast(columnRight, DataTypeVarbinaryMax));
//    //public static string CompareCastVarcharMax(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareCastNvarcharMax(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(Cast(columnLeft, DataTypeVarcharMax), Cast(columnRight, DataTypeVarcharMax));
//    //public static string CompareGeneric(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareGeneric(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(columnLeft, columnRight);
//    //public static string CompareGeo(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareGeo(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : $"({columnLeft}.STEquals({columnRight}) = 1)";

//    //public static string TsqlComparisonEquality(string columnName, string dataTypeName, bool allowDBNull)
//    //{
//    //  switch (dataTypeName)
//    //  {
//    //    case DataTypeGeography:
//    //    case DataTypeGeometry:
//    //      return CompareGeo($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//    //    case DataTypeNtext:
//    //    case DataTypeXml:
//    //      return CompareCastNvarcharMax($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//    //    case DataTypeImage:
//    //    case DataTypeTimestamp:
//    //      return CompareCastVarbinaryMax($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//    //    case DataTypeText:
//    //      return CompareCastVarcharMax($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//    //    default:
//    //      return CompareGeneric($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//    //  }
//    //}

//    /// <summary>Concatenates a single column expression SELECT statement over multiple rows using the delimiter into a single string result.</summary>
//    public static string ConcatColumnOverRows(string statement, char delimiter = ',')
//      => (statement ?? throw new System.ArgumentNullException(nameof(statement))).StartsWith("SELECT ", System.StringComparison.OrdinalIgnoreCase) ? $"{statement.Insert(7, $"'{delimiter}' + ")} FOR XML PATH('')" : throw new System.ArgumentException("Must be a SELECT stament with a single resulting column.");
//    /// <summary>Creates a variable and concatenates a single column expression SELECT statement over multiple rows using the delimiter into the variable.</summary>
//    public static string ConcatColumnOverRows(string variableName, string statement, char delimiter = ',')
//      => (statement ?? throw new System.ArgumentNullException(nameof(statement))).StartsWith("SELECT ", System.StringComparison.OrdinalIgnoreCase) ? $"DECLARE @{variableName} [nvarchar](MAX) {statement.Insert(7, $"@{variableName} = COALESCE(@{variableName} + '{delimiter}', '') + ")}" : throw new System.ArgumentException("Must be a SELECT stament with a single resulting column.");

//    #region Composite statements
//    public static string CombineColumnNames(System.Collections.Generic.IEnumerable<string> columnNames, string? tableAlias = null)
//      => string.Join(Static.CommaSpace, columnNames.SubstituteOnNullOrEmpty(@"*").Select(cn => (tableAlias?.Length ?? 0) > 0 ? $"{tableAlias}.{cn}" : cn));

//    public static string DropTable(TsqlName table3)
//      => $"DROP TABLE {table3.QualifiedNameQuoted(3)}";
//    public static string DropTableIfExists(TsqlName table3)
//      => $"DROP TABLE IF EXISTS {table3.QualifiedNameQuoted(3)}";
//    public static string From(TsqlName table3)
//      => $"FROM {table3.QualifiedNameQuoted(3)} (NOLOCK)";
//    public static string FromSubquery(string subquery, string alias = @"SQ")
//      => $"FROM ( {subquery} ) AS {alias}";
//    public static string InsertInto(TsqlName table3, params string[] columnNames)
//      => $"INSERT INTO {table3.QualifiedNameQuoted(3)} ( {CombineColumnNames(columnNames)} )";
//    public static string Select(params string[] columnNames)
//      => $"SELECT {CombineColumnNames(columnNames)}";
//    public static string Select(System.Collections.Generic.IEnumerable<string> columnNames)
//      => $"SELECT {CombineColumnNames(columnNames)}";
//    public static string SelectCount() 
//      => Select("COUNT(1)");
//    public static string SelectTop(double percent, params string[] columnNames)
//      => $"SELECT TOP ( {percent} ) PERCENT {CombineColumnNames(columnNames)}";
//    public static string SelectTop(int rowCount, params string[] columnNames)
//      => $"SELECT TOP ( {rowCount} ) {CombineColumnNames(columnNames)}";
//    public static string TruncateTable(TsqlName table3)
//      => $"TRUNCATE TABLE {table3.QualifiedNameQuoted(3)}";
//    public static string Values(params string[] columnValues)
//      => $"VALUES ( {string.Join(Static.CommaSpace, columnValues)} )";
//    #endregion Composite statements
//  }

//  //public static class TSql
//  //{
//  //  public static string CreateMergeTable(Sql.QName mergeName3, string[] providerTypes, bool mergeDense) => $"CREATE TABLE {mergeName3.QualifiedNameQuoted(3)} ({string.Join(Static.CommaSpace, providerTypes)}, [EffectiveStartDate] {(mergeDense ? "[bigint]" : "[int]")} NOT NULL, [EffectiveEndDate] {(mergeDense ? "[bigint]" : "[int]")} NOT NULL)";
//  //  public static string CreateTable(Sql.QName tableName3, string[] providerTypes) => $"CREATE TABLE {tableName3.QualifiedNameQuoted(3)} ({string.Join(Static.CommaSpace, providerTypes)})";
//  //  public static string MergeTable(bool highFrequency, Sql.QName mergeName3, Sql.QName sourceName3, string[] fieldNames, string[] fieldProviderTypes)
//  //  {
//  //    var sb = new System.Text.StringBuilder();
//  //    sb.AppendLine($"DECLARE @GetDate [datetime] = GETDATE(); DECLARE @ProcessDate {(highFrequency ? "[bigint]" : "[int]")} = CONVERT({(highFrequency ? "[bigint]" : "[int]")}, FORMAT(@GetDate, '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}')); DECLARE @PriorProcessDate {(highFrequency ? "[bigint]" : "[int]")} = CONVERT({(highFrequency ? "[bigint]" : "[int]")}, FORMAT(DATEADD({(highFrequency ? "second" : "day")}, -1, @GetDate), '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}')); DECLARE @EndDate {(highFrequency ? "[bigint]" : "[int]")} = {(highFrequency ? "29990101010101" : "29990101")} ");
//  //    sb.AppendLine($";WITH T AS (SELECT * FROM {mergeName3.QualifiedNameQuoted(3)} WHERE [EffectiveEndDate] = @EndDate) MERGE INTO T ");
//  //    sb.AppendLine($"USING (SELECT * FROM {sourceName3.QualifiedNameQuoted(3)}) AS S ");
//  //    sb.AppendLine($"ON ({string.Join(" AND ", fieldNames.Select((name, index) => Merge.OnCompareEquality(name, fieldProviderTypes[index].Substring(0, fieldProviderTypes[index].IndexOf(']')).Substring(fieldProviderTypes[index].IndexOf('[') + 1), fieldProviderTypes[index].EndsWith("NOT NULL") ? false : true)))}) ");
//  //    sb.AppendLine($"WHEN NOT MATCHED BY SOURCE THEN UPDATE SET T.[EffectiveEndDate] = @PriorProcessDate ");
//  //    sb.Append($"WHEN NOT MATCHED BY TARGET THEN INSERT ({string.Join(Static.CommaSpace, fieldNames.Select(fieldName => $"[{fieldName}]"))}, [EffectiveStartDate], [EffectiveEndDate]) VALUES ({string.Join(Static.CommaSpace, fieldNames.Select(fieldName => $"[{fieldName}]"))}, @ProcessDate, @EndDate)");
//  //    sb.Append($"OUTPUT $action INTO @actions; SELECT 'INSERT=' + (SELECT CAST(COUNT(1) AS [nvarchar](10)) FROM @actions WHERE ActionType = 'INSERT') + '; UPDATE=' + (SELECT CAST(COUNT(1) AS [nvarchar](10)) FROM @actions WHERE ActionType = 'UPDATE') + '; DELETE=' + (SELECT CAST(COUNT(1) AS [nvarchar](10)) FROM @actions WHERE ActionType = 'DELETE')");
//  //    //sb.Append($"OUTPUT $action, inserted.*, deleted.*");
//  //    sb.Append(';');
//  //    return sb.ToString();
//  //  }
//  //  public static string SelectIntoMergeTable(Sql.QName mergeName3, Sql.QName sourceName3, bool mergeDense) => $"SELECT *, CAST(0 AS {(mergeDense ? "[bigint]" : "[int]")}) AS [EffectiveStartDate], CAST(0 AS {(mergeDense ? "[bigint]" : "[int]")}) AS [EffectiveEndDate] \nINTO {mergeName3.QualifiedNameQuoted(3)} \nFROM {sourceName3.QualifiedNameQuoted(3)} (NOLOCK) WHERE 1 = 0; ";
//  //  public static string TableExists(Sql.QName tableName3) => Exists($"SELECT 1 FROM sys.schemas S INNER JOIN sys.tables T ON (S.[schema_id] = T.[schema_id]) WHERE DB_NAME() = '{tableName3.DatabaseName}' AND S.[name] = '{tableName3.SchemaName}' AND T.[name] = '{tableName3.ObjectName}'");

//  //  //public static string CompareCastNvarcharMax(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareCastNvarcharMax(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(Cast(columnLeft, DataTypeNvarcharMax), Cast(columnRight, DataTypeNvarcharMax));
//  //  //public static string CompareCastVarbinaryMax(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareCastNvarcharMax(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(Cast(columnLeft, DataTypeVarbinaryMax), Cast(columnRight, DataTypeVarbinaryMax));
//  //  //public static string CompareCastVarcharMax(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareCastNvarcharMax(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(Cast(columnLeft, DataTypeVarcharMax), Cast(columnRight, DataTypeVarcharMax));
//  //  //public static string CompareGeneric(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareGeneric(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(columnLeft, columnRight);
//  //  //public static string CompareGeo(string columnLeft, string columnRight, bool allowDBNull) => allowDBNull ? $"(({CompareGeo(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : $"({columnLeft}.STEquals({columnRight}) = 1)";

//  //  //public static string TsqlComparisonEquality(string columnName, string dataTypeName, bool allowDBNull)
//  //  //{
//  //  //  switch (dataTypeName)
//  //  //  {
//  //  //    case DataTypeGeography:
//  //  //    case DataTypeGeometry:
//  //  //      return CompareGeo($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//  //  //    case DataTypeNtext:
//  //  //    case DataTypeXml:
//  //  //      return CompareCastNvarcharMax($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//  //  //    case DataTypeImage:
//  //  //    case DataTypeTimestamp:
//  //  //      return CompareCastVarbinaryMax($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//  //  //    case DataTypeText:
//  //  //      return CompareCastVarcharMax($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//  //  //    default:
//  //  //      return CompareGeneric($"T.[{columnName}]", $"S.[{columnName}]", allowDBNull);
//  //  //  }
//  //  //}

//  //  /// <summary>Concatenates a single column expression SELECT statement over multiple rows using the delimiter into a single string result.</summary>
//  //  public static string ConcatColumnOverRows(string tsql_statement, char delimiter = ',') => tsql_statement.StartsWith("SELECT ") ? $"{tsql_statement.Insert(7, $"'{delimiter}' + ")} FOR XML PATH('')" : throw new System.ArgumentException("Must be a SELECT stament with a single resulting column.");
//  //  /// <summary>Creates a variable and concatenates a single column expression SELECT statement over multiple rows using the delimiter into the variable.</summary>
//  //  public static string ConcatColumnOverRows(string variable_name, string tsql_statement, char delimiter = ',') => tsql_statement.StartsWith("SELECT ") ? $"DECLARE @{variable_name} [nvarchar](MAX) {tsql_statement.Insert(7, $"@{variable_name} = COALESCE(@{variable_name} + '{delimiter}', '') + ")}" : throw new System.ArgumentException("Must be a SELECT stament with a single resulting column.");

//  //  public static string DropIfExistsTable(Sql.QName table3) => $"DROP TABLE IF EXISTS {table3.QualifiedNameQuoted(3)}";
//  //  public static string DropTable(Sql.QName table3) => $"DROP TABLE {table3.QualifiedNameQuoted(3)}";
//  //  public static string From(Sql.QName table3) => $"FROM {table3.QualifiedNameQuoted(3)} (NOLOCK)";
//  //  public static string FromSubquery(string tsql_subquery) => $"FROM ( {tsql_subquery} ) AS SQ";
//  //  public static string InsertInto(Sql.QName table3, params string[] columnNames) => $"INSERT INTO {table3.QualifiedNameQuoted(3)} ( {string.Join(Static.CommaSpace, columnNames)} )";
//  //  public static string JoinColumnNames(System.Collections.Generic.IEnumerable<string> columnNames, string tableNameOrAlias = "") => string.Join(Static.CommaSpace, (columnNames.Any() ? columnNames : columnNames.Append(@"*")).Select(cn => string.IsNullOrWhiteSpace(tableNameOrAlias) ? cn : $"{tableNameOrAlias}.{cn}"));
//  //  public static string Select(params string[] columnNames) => $"SELECT {JoinColumnNames(columnNames)}";
//  //  public static string Select(System.Collections.Generic.IEnumerable<string> columnNames) => $"SELECT {JoinColumnNames(columnNames)}";
//  //  public static string SelectCount() => Select("COUNT(1)");
//  //  public static string SelectTop(int topCount, params string[] columnNames) => $"SELECT TOP {topCount} {JoinColumnNames(columnNames)}";
//  //  public static string TruncateTable(Sql.QName table3) => $"TRUNCATE TABLE {table3.QualifiedNameQuoted(3)}";
//  //  public static string Values(params string[] columnValues) => $"VALUES ( {string.Join(Static.CommaSpace, columnValues)} )";

//  //  public static class InformationSchema
//  //  {
//  //    public static string SelectColumnTSqlDefinitions(Sql.QName table3) => $"{SelectColumns(table3, "'[' + column_name +'] ['+ data_type +']'+ CASE WHEN DATA_TYPE IN ('datetime2', 'datetimeoffset', 'time') THEN '(' + CONVERT([varchar](10), COALESCE(DATETIME_PRECISION, 7)) + ')' WHEN DATA_TYPE IN ('decimal', 'numeric') THEN '(' + CONVERT([varchar](10), COALESCE(NUMERIC_PRECISION, 18)) + ',' + CONVERT([varchar](10), COALESCE(NUMERIC_SCALE, 0)) + ')' WHEN (DATA_TYPE LIKE '%binary' OR DATA_TYPE LIKE '%char') THEN CASE WHEN CHARACTER_MAXIMUM_LENGTH = -1 THEN '(MAX)' ELSE '(' + CONVERT([nvarchar](10), CHARACTER_MAXIMUM_LENGTH) + ')' END ELSE '' END +' '+ (CASE WHEN IS_NULLABLE = 'No' then 'NOT NULL' ELSE 'NULL' END) AS [TSqlDefinition] INTO #Columns")}; {Select(Stuff("(SELECT N', ' + [TSqlDefinition] FROM #Columns FOR XML PATH(''))", 1, 2))}";
//  //    /// <summary>Select rows from [INFORMATION_SCHEMA].[COLUMNS] for the specified table, optionally with specified columns.</summary>
//  //    public static string SelectColumns(Sql.QName tableName3, params string[] columnNames) => $"{Select(columnNames)} FROM {tableName3.DatabaseNameQuoted}.[INFORMATION_SCHEMA].[COLUMNS] (NOLOCK) WHERE TABLE_CATALOG = '{tableName3.DatabaseName}' AND TABLE_SCHEMA = '{tableName3.SchemaName}' AND TABLE_NAME = '{tableName3.ObjectName}' ORDER BY ORDINAL_POSITION";
//  //    /// <summary>Select the record from [INFORMATION_SCHEMA].[TABLES] for the specifified table, optionally with specified columns.</summary>
//  //    public static string SelectTable(Sql.QName tableName3, params string[] columnNames) => $"{Select(columnNames)} FROM {tableName3.DatabaseNameQuoted}.[INFORMATION_SCHEMA].[TABLES] (NOLOCK) WHERE TABLE_CATALOG = '{tableName3.DatabaseName}' AND TABLE_SCHEMA = '{tableName3.SchemaName}' AND TABLE_NAME = '{tableName3.ObjectName}'";
//  //  }

//  //  public static class Merge
//  //  {
//  //    public static string BootStrap(bool highFrequency) => $"DECLARE @GetDate [datetime] = GETDATE(); DECLARE @ProcessDate {(highFrequency ? "[bigint]" : "[int]")} = CONVERT({(highFrequency ? "[bigint]" : "[int]")}, FORMAT(@GetDate, '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}')); DECLARE @PriorProcessDate {(highFrequency ? "[bigint]" : "[int]")} = CONVERT({(highFrequency ? "[bigint]" : "[int]")}, FORMAT(DATEADD({(highFrequency ? "second" : "day")}, -1, @GetDate), '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}')); DECLARE @EndDate {(highFrequency ? "[bigint]" : "[int]")} = {(highFrequency ? "29990101010101" : "29990101")} ";
//  //    public static string IntoTarget(Sql.QName targetTable3, params string[] targetColumnNames) => $";WITH T AS (SELECT {string.Join(",", targetColumnNames.Select(name => $"[{name}]"))} FROM {targetTable3.QualifiedNameQuoted(3)} WHERE[EffectiveEndDate] = @EndDate) MERGE INTO T ";
//  //    public static string IntoTarget(Sql.QName targetTable3) => IntoTarget(targetTable3, @"*");
//  //    public static string UsingSource(Sql.QName sourceTable3, params string[] sourceColumnNames) => $"USING ( SELECT {string.Join(",", sourceColumnNames.Select(name => $"[{name}]"))} FROM {sourceTable3.QualifiedNameQuoted(3)} ) AS S ";
//  //    public static string UsingSource(Sql.QName sourceTable3) => UsingSource(sourceTable3, @"*");
//  //    public static string On(string[] symmetricColumnNames, params string[] targetColumnDefinitions) => On(symmetricColumnNames, symmetricColumnNames, targetColumnDefinitions);
//  //    public static string On(string[] sourceColumnNames, string[] targetColumnNames, string[] targetColumnDefinitions) => $"ON ({string.Join(" AND ", targetColumnNames.Select((name, index) => OnCompareEquality(sourceColumnNames[index], targetColumnNames[index], targetColumnDefinitions[index].Substring(0, targetColumnDefinitions[index].LastIndexOf(']')).Substring(targetColumnDefinitions[index].LastIndexOf('[') + 1), targetColumnDefinitions[index].EndsWith("NOT NULL") ? false : true)))}) ";
//  //    public static string WhenNotMatchedBySource() => $"WHEN NOT MATCHED BY SOURCE THEN UPDATE SET T.[EffectiveEndDate] = @PriorProcessDate ";
//  //    public static string WhenNotMatchedByTarget(string[] sourceColumnNames, string[] targetColumnNames) => $"WHEN NOT MATCHED BY TARGET THEN INSERT ({string.Join(Static.CommaSpace, targetColumnNames.Select(name => $"[{name}]"))}, [EffectiveStartDate], [EffectiveEndDate]) VALUES ({string.Join(Static.CommaSpace, sourceColumnNames.Select(name => $"S.[{name}]"))}, @ProcessDate, @EndDate)";

//  //    public static string OnCompareEquality(string columnName, string dataTypeName, bool allowDBNull) => OnCompareEquality(columnName, columnName, dataTypeName, allowDBNull);
//  //    public static string OnCompareEquality(string sourceColumnName, string targetColumnName, string dataTypeName, bool allowDBNull)
//  //    {
//  //      var columnTarget = $"T.[{targetColumnName}]";
//  //      var columnSource = $"S.[{sourceColumnName}]";

//  //      string OnCompareCast(string toType, string columnLeft, string columnRight, bool isNullable) => isNullable ? $"(({OnCompareCast(toType, columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(Cast(columnLeft, toType), Cast(columnRight, toType));
//  //      string OnCompareGeneric(string columnLeft, string columnRight, bool isNullable) => isNullable ? $"(({OnCompareGeneric(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : Equal(columnLeft, columnRight);
//  //      string OnCompareGeo(string columnLeft, string columnRight, bool isNullable) => isNullable ? $"(({OnCompareGeo(columnLeft, columnRight, false)}) OR ({And(IsNull(columnLeft), IsNull(columnRight))}))" : $"({columnLeft}.STEquals({columnRight}) = 1)";

//  //      switch (dataTypeName)
//  //      {
//  //        case DataTypeName.Geography:
//  //        case DataTypeName.Geometry:
//  //          return OnCompareGeo(columnTarget, columnSource, allowDBNull);
//  //        case DataTypeName.Ntext:
//  //        case DataTypeName.Xml:
//  //          return OnCompareCast($"{DataTypeName.Nvarchar}(MAX)", columnTarget, columnSource, allowDBNull);
//  //        case DataTypeName.Image:
//  //        case DataTypeName.Timestamp:
//  //          return OnCompareCast($"{DataTypeName.Varbinary}(MAX)", columnTarget, columnSource, allowDBNull);
//  //        case DataTypeName.Text:
//  //          return OnCompareCast($"{DataTypeName.Varchar}(MAX)", columnTarget, columnSource, allowDBNull);
//  //        default:
//  //          return OnCompareGeneric(columnTarget, columnSource, allowDBNull);
//  //      }

//  //    }
//  //  }

//  //  public static class Sys
//  //  {
//  //    public static string SelectColumnTSqlDefinitions(Sql.QName table3) => $"{SelectColumns(table3, "'[' + C.[name] +'] ['+ CT.[name] +']'+ CASE WHEN CT.[name] IN ('datetime2', 'datetimeoffset', 'time') THEN '(' + CONVERT([varchar](10), COALESCE(C.[scale], 7)) + ')' WHEN CT.[name] IN ('decimal', 'numeric') THEN '(' + CONVERT([varchar](10), COALESCE(C.[precision], 18)) + ',' + CONVERT([varchar](10), COALESCE(C.[scale], 0)) + ')' WHEN (CT.[name] LIKE '%binary' OR CT.[name] LIKE '%char') THEN CASE WHEN C.[max_length] = -1 THEN '(MAX)' WHEN CT.[name] LIKE 'n%' THEN '(' + CONVERT([nvarchar](10), C.[max_length] / 2) + ')' ELSE '(' + CONVERT([nvarchar](10), C.[max_length]) + ')' END ELSE '' END +' '+ (CASE WHEN C.[is_nullable] = 0 then 'NOT NULL' ELSE 'NULL' END) AS [TSqlDefinition] INTO #Columns")}; {Select(Stuff("(SELECT N', ' + [TSqlDefinition] FROM #Columns FOR XML PATH(''))", 1, 2))}";
//  //    /// <summary>Select rows from [sys].[columns] for the specified table, optionally with specified columns.</summary>
//  //    public static string SelectColumns(Sql.QName tableName3, params string[] columnNames) => $"SELECT {(columnNames.Any() ? JoinColumnNames(columnNames) : @"C.*")} FROM sys.schemas S (NOLOCK) INNER JOIN sys.tables AS T (NOLOCK) ON (S.[schema_id] = T.[schema_id]) INNER JOIN sys.columns AS C (NOLOCK) ON (T.[object_id] = C.[object_id]) INNER JOIN sys.types CT (NOLOCK) ON (C.system_type_id = CT.system_type_id AND C.user_type_id = CT.user_type_id) WHERE DB_NAME() = '{tableName3.DatabaseName}' AND S.[name] = '{tableName3.SchemaName}' AND T.[name] = '{tableName3.ObjectName}' ORDER BY [column_id]";
//  //    /// <summary>Select the record from [sys].[tables] for the specifified table, optionally with specified columns.</summary>
//  //    public static string SelectTable(Sql.QName tableName3, params string[] columnNames) => $"SELECT {(columnNames.Any() ? JoinColumnNames(columnNames) : @"T.*")} FROM sys.schemas S (NOLOCK) INNER JOIN sys.tables AS T (NOLOCK) ON (S.[schema_id] = T.[schema_id]) WHERE DB_NAME() = '{tableName3.DatabaseName}' AND S.[name] = '{tableName3.SchemaName}' AND T.[name] = '{tableName3.ObjectName}'";
//  //  }
//  //}
//}
