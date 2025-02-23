namespace Flux
{
  public static partial class Fx
  {
    public static string GetDefaultTsqlDefinition(this System.Data.IDataRecord source, int index, bool nullable = true)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var name = source.GetName(index);
      var dataTypeName = Data.TsqlDataType.NameFromType(source.GetFieldType(index));
      var magnitude = Data.TsqlDataType.GetDefaultArgument(dataTypeName, true);

      return $"[{name}] [{dataTypeName}]{magnitude} {(nullable ? "NULL" : "NOT NULL")}";
    }
  }
}
