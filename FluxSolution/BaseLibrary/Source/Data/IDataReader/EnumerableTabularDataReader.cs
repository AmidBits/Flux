using System.Linq;

namespace Flux.Data
{
  /// <summary>An implementation of a <see cref="TabularDataReader"/> over a System.Collection.Generic.IEnumerable<T>.</summary>
  public class EnumerableTabularDataReader
    : TabularDataReader
  {
    internal readonly System.Collections.Generic.IEnumerator<System.Collections.Generic.IEnumerable<object>> m_enumerator;

    public EnumerableTabularDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>> source)
      => m_enumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

    /// <summary>Initializes the FieldNames into strings of the default "Column_#" format.</summary>
    /// <param name="fieldCount">The number of fields to create.</param>
    public void CreateDefaultFieldNames(int fieldCount)
    {
      m_fieldNames.Clear();

      for (var index = 1; index <= fieldCount; index++)
        m_fieldNames.Add($"Column_{index}");
    }
    /// <summary>Initializes the property FieldNames into strings by reading a row from the source.</summary>
    public bool ReadFieldNames()
    {
      m_fieldNames.Clear();

      IsClosed = m_enumerator is null || !m_enumerator.MoveNext();

      if (!IsClosed)
        m_fieldNames.AddRange(m_enumerator!.Current.Select(v => $"{v}"));

      return !IsClosed;
    }

    /// <summary>Attempts to read the next result as field values into the EnumerableTabularDataReader.FieldValues object.</summary>
    private bool ReadFieldValues()
    {
      m_fieldValues.Clear();

      IsClosed = m_enumerator is null || !m_enumerator.MoveNext();

      if (!IsClosed)
        m_fieldValues.AddRange(m_enumerator!.Current);

      return !IsClosed;
    }

    // IDataReader
    public override bool Read()
    {
      if (ReadFieldValues())
        if (m_fieldValues.Count != FieldCount)
          throw new System.Exception($"Mismatch between the count of FieldValues={m_fieldValues.Count} and FieldCount={FieldCount}.");

      return !IsClosed;
    }

    // IDisposable
    protected override void DisposeManaged()
    {
      m_enumerator.Dispose();

      base.DisposeManaged();
    }
  }
}
