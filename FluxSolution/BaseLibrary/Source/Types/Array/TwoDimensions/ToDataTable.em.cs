namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Create a new System.Data.DataTable from the two dimensional array.</summary>
    public static System.Data.DataTable ToDataTable<T>(this T[,] source, bool hasColumnNames, params string[] customColumnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var dt = new System.Data.DataTable();

      for (var i1 = 0; i1 < sourceLength1; i1++)
        dt.Columns.Add((customColumnNames.Length > i1 ? customColumnNames[i1] : null) ?? (hasColumnNames ? source[0, i1]?.ToString() : null) ?? $"Column{i1}");

      for (var i0 = (hasColumnNames ? 1 : 0); i0 < sourceLength0; i0++)
      {
        var array = new object[sourceLength1];
        for (var i1 = 0; i1 < sourceLength1; i1++)
          array[i1] = source[i0, i1]!;
        dt.Rows.Add(array);
      }

      return dt;
    }
  }
}
