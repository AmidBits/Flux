namespace Flux
{
  public static partial class SystemDataEm
  {
    public static string GetDefaultTsqlDefinition(this System.Data.IDataRecord source, int index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var name = source.GetName(index);
      var dataTypeName = Data.TsqlDataType.NameFromType(source.GetFieldType(index));
      var magnitude = Data.TsqlDataType.GetDefaultArgument(dataTypeName, true);

      return $"[{name}] [{dataTypeName}]{magnitude} NULL";
    }
  }
}