namespace Flux
{
  public interface ITabularDataAcquirable
  {
    string[] FieldNames { get; }
    System.Type[] FieldTypes { get; }
    System.Collections.Generic.IEnumerable<object[]> GetFieldValues();

    /// <summary>Acquire tabular data from the URI. The first array should be field names.</summary>
    //System.Collections.Generic.IEnumerable<object[]> AcquireTabularData();
  }

  public abstract class ATabularDataAcquirable
    : ITabularDataAcquirable
  {
    public abstract string[] FieldNames { get; }
    public abstract System.Type[] FieldTypes { get; }
    public abstract System.Collections.Generic.IEnumerable<object[]> GetFieldValues();

    public System.Data.IDataReader AcquireDataReader()
      => new Data.EnumerableTabularDataReader(GetFieldValues(), FieldNames, FieldTypes);
    public System.Data.DataTable AcquireDataTable()
      => AcquireDataReader().ToDataTable(GetType().Name);
  }
}
