namespace Flux
{
  public interface ITabularDataAcquirable
  {
    string[] FieldNames { get; }
    System.Type[] FieldTypes { get; }

    System.Collections.Generic.IEnumerable<object[]> GetFieldValues();

    System.Data.DataTable AcquireDataTable(string tableName)
      => AcquireDataReader().ToDataTable(string.IsNullOrEmpty(tableName) ? GetType().Name : tableName);

    System.Data.IDataReader AcquireDataReader()
      => new Data.EnumerableTabularDataReader(GetFieldValues(), FieldNames, FieldTypes);
  }
}
