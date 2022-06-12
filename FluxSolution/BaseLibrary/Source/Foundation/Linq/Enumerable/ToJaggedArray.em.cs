using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a jagged array from the sequence, using the specified selector and column names.</summary>
    public static object[][] ToJaggedArray<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, object[]> arraySelector, params string[] columnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (arraySelector is null) throw new System.ArgumentNullException(nameof(arraySelector));

      var list = new System.Collections.Generic.List<object[]>();

      if (columnNames.Length > 0)
        list.Add(columnNames);

      var index = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
        list.Add(arraySelector(e.Current, index++));

      return list.ToArray();
    }
  }
}
