using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Creates a jagged array from the sequence, using the specified selector and column names.</summary>
    public static object[][] ToJaggedArray<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, object[]> arraySelector, params string[] columnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (arraySelector is null) throw new System.ArgumentNullException(nameof(arraySelector));

      return ToArrays().ToArray();

      System.Collections.Generic.IEnumerable<object[]> ToArrays()
      {
        if (columnNames.Any())
          yield return columnNames;

        var index = 0;

        foreach (var element in source)
          yield return arraySelector(element, index++);
      }
    }
  }
}
