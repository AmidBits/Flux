namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reads the file content from that of a <see cref="System.IO.FileInfo"/> into a <see cref="System.Data.DataTable"/>.</para>
    /// </summary>
    public static System.Data.DataTable ReadIntoDataTable(this System.IO.FileInfo source, System.Func<string, string[]>? itemArraySelector = null, System.Text.Encoding? encoding = null)
    {
      itemArraySelector ??= s => new string[] { s };
      encoding ??= System.Text.Encoding.UTF8;

      using var fileStream = source.OpenRead();
      using var streamReader = new System.IO.StreamReader(fileStream);

      var dataTable = new System.Data.DataTable(System.IO.Path.GetFileNameWithoutExtension(source.Name));

      while (streamReader.ReadLine() is var line && line is not null)
      {
        if (line.Length > 0)
        {
          var itemArray = itemArraySelector(line);

          if (dataTable.Columns.Count == 0)
            for (var index = 1; index <= itemArray.Length; index++)
              dataTable.Columns.Add($"Column_{index}");

          dataTable.Rows.Add(itemArray);
        }
      }

      return dataTable;
    }
  }
}
