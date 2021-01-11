using System.Linq;

namespace Flux.Data
{
  /// <summary>An implementation of a System.Data.IDataReader over a System.Collection.Generic.IEnumerable<T>.</summary>
  public class EnumerableDataReader
    : TabularDataReader
  {
    private System.Collections.Generic.IEnumerator<System.Collections.Generic.IList<object>> m_enumerator;

    public EnumerableDataReader(System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<object>> source, System.Collections.Generic.IList<string> fieldNames, System.Collections.Generic.IList<System.Type> fieldTypes)
      : base(fieldNames, fieldTypes)
      => m_enumerator = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

    public override bool Read()
    {
      FieldValues.Clear();

      if (m_enumerator?.MoveNext() ?? false)
      {
        FieldValues.AddRange(m_enumerator.Current);

        if (FieldValues.Count != FieldCount) throw new System.Exception($"Mismatch between the count of field values and FieldCount.");

        return true;
      }
      else return false;
    }

    protected override void DisposeManaged()
    {
      m_enumerator.Dispose();

      base.DisposeManaged();
    }

    public static EnumerableDataReader Create<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Collections.Generic.IList<string> fieldNames, System.Collections.Generic.IList<System.Type> fieldTypes, System.Func<TSource, System.Collections.Generic.IList<object>> valueSelector)
      => new EnumerableDataReader(collection.Select(ts => valueSelector(ts)).ToList(), fieldNames, fieldTypes);
  }
}
