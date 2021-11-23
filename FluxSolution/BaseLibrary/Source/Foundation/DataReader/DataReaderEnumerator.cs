namespace Flux.Data
{
  public sealed class DataReaderEnumerator
    : Disposable, System.Collections.Generic.IEnumerator<System.Data.IDataRecord>
  {
    private readonly System.Data.IDataReader m_dataReader;

    public DataReaderEnumerator(System.Data.IDataReader dataReader)
      => m_dataReader = dataReader ?? throw new System.ArgumentNullException(nameof(dataReader));

    // IEnumerator
    public System.Data.IDataRecord Current
      => m_dataReader;
    object System.Collections.IEnumerator.Current
      => m_dataReader;
    public bool MoveNext()
      => m_dataReader.Read();
    public void Reset()
      => throw new System.NotSupportedException($"Implementations of System.Data.IDataReader are forward-only constructs.");
  }
}
