namespace Flux
{
  public static partial class XtensionTextReader
  {
    extension(System.IO.TextReader source)
    {
      /// <summary>
      /// <para>Creates a sequence of fields derived using <see cref="Microsoft.VisualBasic.FileIO.TextFieldParser"/>.</para>
      /// </summary>
      /// <param name="reader">The <see cref="System.IO.TextReader"/> to read from.</param>
      /// <param name="delimiter">The delimiter to use for the parser.</param>
      /// <param name="enclosedInQuotes">Whether fields are enclosed in double quotes.</param>
      /// <param name="trimWhiteSpace">Indicates whether leading and trailing white space should be trimmed from field values.</param>
      /// <returns></returns>
      /// <exception cref="System.InvalidOperationException"></exception>
      public System.Collections.Generic.IEnumerable<string[]> ReadCsv(string delimiter = ",", bool enclosedInQuotes = true, bool trimWhiteSpace = false)
      {
        using var tfp = new Microsoft.VisualBasic.FileIO.TextFieldParser(source);

        tfp.SetDelimiters(delimiter);
        tfp.HasFieldsEnclosedInQuotes = enclosedInQuotes;
        tfp.TrimWhiteSpace = trimWhiteSpace;

        while (!tfp.EndOfData)
          yield return tfp.ReadFields() ?? throw new System.InvalidOperationException();
      }

      public System.Collections.Generic.IEnumerable<TResult> ReadLines<TResult>(System.Func<string, bool> predicate, System.Func<string, TResult> resultSelector)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(predicate);
        System.ArgumentNullException.ThrowIfNull(resultSelector);

        while (source.ReadLine() is var line && line is not null)
          if (predicate(line))
            yield return resultSelector(line);
      }

      //public System.Collections.Generic.IEnumerable<string> ReadLines(bool keepEmptyLines)
      //  => source.ReadLines(s => s.Length > 0 || keepEmptyLines, s => s);

      /// <summary>
      /// <para>Creates a new <see cref="System.Data.DataTable"/> from the content in the <paramref name="source"/> <see cref="System.IO.TextReader"/> using <paramref name="predicate"/> and <paramref name="resultSelector"/>. The <paramref name="tableName"/> (defaults to "DataTable") and <paramref name="columnNames"/> (defaults to "Column_n") are optional.</para>
      /// </summary>
      /// <param name="source">The <see cref="System.IO.TextReader"/>.</param>
      /// <param name="predicate">Determines whether a line from the reader will be used.</param>
      /// <param name="resultSelector">Converts a line into an array of strings.</param>
      /// <param name="tableName">The name of the table.</param>
      /// <param name="columnNames">Zero or more column names. If there aren't enough column names, or none, column names in the format of "Column_n" will be added.</param>
      /// <returns></returns>
      public System.Data.DataTable ToDataTable(System.Func<string, bool> predicate, System.Func<string, string[]> resultSelector, string tableName, params string[] columnNames)
      {
        var dataTable = new System.Data.DataTable(tableName ?? $"Table");

        if (columnNames is not null && columnNames.Length > 0)
          for (var i = 0; i < columnNames.Length; i++)
            dataTable.Columns.Add(columnNames[i]);

        foreach (var array in source.ReadLines(predicate, resultSelector))
        {
          if (dataTable.Columns.Count < array.Length)
            for (var i = dataTable.Columns.Count; i < array.Length; i++)
              dataTable.Columns.Add(i.ToSingleOrdinalColumnName());

          dataTable.Rows.Add(array);
        }

        return dataTable;
      }
    }
  }
}
