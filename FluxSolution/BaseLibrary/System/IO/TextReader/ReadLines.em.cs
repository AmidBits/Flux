namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<TResult> ReadLines<TResult>(this System.IO.TextReader source, System.Func<string, bool> predicate, System.Func<string, TResult> resultSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      while (source.ReadLine() is var line && line is not null)
        if (predicate(line))
          yield return resultSelector(line);
    }

    //public static System.Collections.Generic.IEnumerable<string> ReadLines(this System.IO.TextReader source, bool keepEmptyLines)
    //  => source.ReadLines(s => s.Length > 0 || keepEmptyLines, s => s);

    public static System.Data.DataTable ToDataTable(this System.IO.TextReader source, System.Func<string, bool> predicate, System.Func<string, string[]> resultSelector, string tableName)
    {
      var dataTable = new System.Data.DataTable(tableName);

      foreach (var array in source.ReadLines(predicate, resultSelector))
      {
        if (dataTable.Columns.Count == 0)
          for (var i = 1; i <= array.Length; i++)
            dataTable.Columns.Add($"Column_{i}");

        dataTable.Rows.Add(array);
      }

      return dataTable;
    }
  }
}
