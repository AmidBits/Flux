namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Data.IDataReader AcquireDataReader(this ITabularDataAcquirable source)
         => new Data.EnumerableTabularDataReader(source.GetFieldValues(), source.FieldNames, source.FieldTypes);
    public static System.Data.DataTable AcquireDataTable(this ITabularDataAcquirable source, string tableName)
      => source.AcquireDataReader().ToDataTable(tableName);
  }

  public interface ITabularDataAcquirable
  {
    string[] FieldNames { get; }
    System.Type[] FieldTypes { get; }

    System.Collections.Generic.IEnumerable<object[]> GetFieldValues();
  }
}
