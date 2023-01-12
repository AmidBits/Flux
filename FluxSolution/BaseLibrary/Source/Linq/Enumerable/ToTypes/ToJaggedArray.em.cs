using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a jagged array from the sequence, using the specified selector and column names.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static object[][] ToJaggedArray<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, object[]> arraySelector, params string[] columnNames)
    {
      if (arraySelector is null) throw new System.ArgumentNullException(nameof(arraySelector));

      var list = new System.Collections.Generic.List<object[]>();

      if (columnNames.Length > 0)
        list.Add(columnNames);

      var index = 0;

      foreach (var item in source)
        list.Add(arraySelector(item, index++));

      return list.ToArray();
    }
  }
}
