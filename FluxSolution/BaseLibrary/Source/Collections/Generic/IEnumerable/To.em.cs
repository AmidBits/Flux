using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a new data table from the sequence using the value selector and the column names.</summary>
    public static System.Data.DataTable ToDataTable<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, object[]> valuesSelector, params string[] columnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (valuesSelector is null) throw new System.ArgumentNullException(nameof(valuesSelector));

      var dt = new System.Data.DataTable();

      foreach (var columnName in columnNames)
        dt.Columns.Add(columnName);

      var index = 0;

      foreach (var element in source)
      {
        var dr = dt.NewRow();
        dr.ItemArray = valuesSelector(element, index++);
        dt.Rows.Add(dr);
      }

      return dt;
    }

    /// <summary>Concatenates strings with a delimiter from the sequence based on the return from the valueSelector</summary>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<T, string> selector)
      => string.Join(delimiter, source.Select(t => selector(t)));

    /// <summary>Concatenates strings with a delimiter from the sequence.</summary>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter)
      => ToDelimitedString(source, delimiter, (sb, e) => sb.Append(e));

    private static string ToDelimitedString<T>(System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<System.Text.StringBuilder, T, System.Text.StringBuilder> append)
    {
      var sb = new System.Text.StringBuilder();

      var index = 0;

      foreach (var value in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        if (index++ > 0) sb.Append(delimiter);

        append(sb, value);
      }

      return sb.ToString();
    }

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
