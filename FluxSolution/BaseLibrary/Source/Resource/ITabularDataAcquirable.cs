namespace Flux
{
  public interface ITabularDataAcquirable
  {
    /// <summary>Acquire tabular data from the URI. The first array should be field names.</summary>
    System.Collections.Generic.IEnumerable<object[]> AcquireTabularData();
  }

  public abstract class ATabularDataAcquirer
    : ITabularDataAcquirable
  {
    public abstract System.Collections.Generic.IEnumerable<object[]> AcquireTabularData();

    public virtual System.Data.IDataReader AcquireDataReader()
      => new Data.EnumerableTabularDataReader(AcquireTabularData(), -1);

    public virtual System.Data.DataTable AcquireDataTable()
      => AcquireDataReader().ToDataTable(GetType().Name);
  }
}
