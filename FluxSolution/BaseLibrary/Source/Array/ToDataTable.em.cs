using System.Linq;

namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class XtensionsArray
  {
    /// <summary>Create a new System.Data.DataTable from the two dimensional array.</summary>
    public static System.Data.DataTable ToDataTable<T>(this T[,] source, bool hasColumnNames, params string[] customColumnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var dt = new System.Data.DataTable();

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      for (var d1 = 0; d1 < sourceLength1; d1++)
      {
        dt.Columns.Add(customColumnNames.Length > d1 ? customColumnNames[d1] : hasColumnNames ? source[0, d1]?.ToString() ?? $"Column{d1}" : $"Column{d1}");
      }

      for (var d0 = hasColumnNames ? 1 : 0; d0 < sourceLength0; d0++)
      {
        dt.Rows.Add(source.GetElements(0, d0).Select(v => (object?)v.item).ToArray());
      }

      return dt.Copy();
    }
  }
}
