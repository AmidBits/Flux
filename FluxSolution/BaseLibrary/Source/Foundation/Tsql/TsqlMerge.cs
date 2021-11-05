using System.Linq;

namespace Flux.Data
{
  public static class TsqlMerge
  {
    public static string BootStrap(bool highFrequency)
      => $"DECLARE @GetDate [datetime] = GETDATE(); DECLARE @ProcessDate {(highFrequency ? "[bigint]" : "[int]")} = CONVERT({(highFrequency ? "[bigint]" : "[int]")}, FORMAT(@GetDate, '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}')); DECLARE @PriorProcessDate {(highFrequency ? "[bigint]" : "[int]")} = CONVERT({(highFrequency ? "[bigint]" : "[int]")}, FORMAT(DATEADD({(highFrequency ? "second" : "day")}, -1, @GetDate), '{(highFrequency ? "yyyyMMddHHmmss" : "yyyyMMdd")}')); DECLARE @EndDate {(highFrequency ? "[bigint]" : "[int]")} = {(highFrequency ? "29990101010101" : "29990101")} ";
    public static string IntoTarget(TsqlName targetTable3, params string[] targetColumnNames)
      => $";WITH T AS (SELECT {string.Join(",", targetColumnNames.Select(name => $"[{name}]"))} FROM {targetTable3.QualifiedNameQuoted(3)} WHERE[EffectiveEndDate] = @EndDate) MERGE INTO T ";
    public static string IntoTarget(TsqlName targetTable3)
      => IntoTarget(targetTable3, @"*");
    public static string UsingSource(TsqlName sourceTable3, params string[] sourceColumnNames)
      => $"USING ( SELECT {string.Join(",", sourceColumnNames.Select(name => $"[{name}]"))} FROM {sourceTable3.QualifiedNameQuoted(3)} ) AS S ";
    public static string UsingSource(TsqlName sourceTable3)
      => UsingSource(sourceTable3, @"*");
    public static string On(params TsqlColumnDefinition[] columnDefinitions)
      => On(columnDefinitions.Select(cd => cd.ColumnName).ToArray(), columnDefinitions.Select(cd => cd.ColumnName).ToArray(), columnDefinitions.Select(cd => cd.ToString()).ToArray());
    public static string On(string[] symmetricColumnNames, params string[] targetColumnDefinitions)
      => On(symmetricColumnNames, symmetricColumnNames, targetColumnDefinitions);
    public static string On(string[] sourceColumnNames, string[] targetColumnNames, string[] targetColumnDefinitions)
      => $"ON ({string.Join(" AND ", targetColumnNames.Select((name, index) => OnCompareEquality(sourceColumnNames[index], targetColumnNames[index], targetColumnDefinitions[index].Substring(0, targetColumnDefinitions[index].LastIndexOf(']')).Substring(targetColumnDefinitions[index].LastIndexOf('[') + 1), !targetColumnDefinitions[index].EndsWith("NOT NULL", System.StringComparison.OrdinalIgnoreCase))))}) ";
    public static string WhenNotMatchedBySource()
      => $"WHEN NOT MATCHED BY SOURCE THEN UPDATE SET T.[EffectiveEndDate] = @PriorProcessDate ";
    public static string WhenNotMatchedByTarget(string[] sourceColumnNames, string[] targetColumnNames)
      => $"WHEN NOT MATCHED BY TARGET THEN INSERT ({string.Join(", ", targetColumnNames.Select(name => $"[{name}]"))}, [EffectiveStartDate], [EffectiveEndDate]) VALUES ({string.Join(", ", sourceColumnNames.Select(name => $"S.[{name}]"))}, @ProcessDate, @EndDate)";

    public static string OnCompareEquality(string columnName, string dataTypeName, bool allowDBNull)
      => OnCompareEquality(columnName, columnName, dataTypeName, allowDBNull);
    public static string OnCompareEquality(string sourceColumnName, string targetColumnName, string dataTypeName, bool allowDBNull)
    {
      var columnTarget = $"T.[{targetColumnName}]";
      var columnSource = $"S.[{sourceColumnName}]";

      static string OnCompareCast(string toType, string columnLeft, string columnRight, bool isNullable)
        => isNullable ? $"(({OnCompareCast(toType, columnLeft, columnRight, false)}) OR ({Tsql.And(Tsql.IsNull(columnLeft), Tsql.IsNull(columnRight))}))" : Tsql.Equal(Tsql.Cast(columnLeft, toType), Tsql.Cast(columnRight, toType));
      static string OnCompareGeneric(string columnLeft, string columnRight, bool isNullable)
        => isNullable ? $"(({OnCompareGeneric(columnLeft, columnRight, false)}) OR ({Tsql.And(Tsql.IsNull(columnLeft), Tsql.IsNull(columnRight))}))" : Tsql.Equal(columnLeft, columnRight);
      static string OnCompareGeo(string columnLeft, string columnRight, bool isNullable)
        => isNullable ? $"(({OnCompareGeo(columnLeft, columnRight, false)}) OR ({Tsql.And(Tsql.IsNull(columnLeft), Tsql.IsNull(columnRight))}))" : $"({columnLeft}.STEquals({columnRight}) = 1)";

      return dataTypeName switch
      {
        TsqlDataType.Geography or TsqlDataType.Geometry => OnCompareGeo(columnTarget, columnSource, allowDBNull),
        TsqlDataType.Ntext or TsqlDataType.Xml => OnCompareCast($"{TsqlDataType.Nvarchar}(MAX)", columnTarget, columnSource, allowDBNull),
        TsqlDataType.Image or TsqlDataType.Timestamp => OnCompareCast($"{TsqlDataType.Varbinary}(MAX)", columnTarget, columnSource, allowDBNull),
        TsqlDataType.Text => OnCompareCast($"{TsqlDataType.Varchar}(MAX)", columnTarget, columnSource, allowDBNull),
        _ => OnCompareGeneric(columnTarget, columnSource, allowDBNull),
      };
    }
  }
}
