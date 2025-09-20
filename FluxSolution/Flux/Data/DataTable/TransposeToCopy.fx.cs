namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Data.DataTable"/> containing the source data transposed (pivoted).</para>
    /// </summary>
    /// <param name="targetColumnNames">If less target column names than needed are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable TransposeToCopy(this System.Data.DataTable source, params string[] targetColumnNames)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var rowCount = source.Rows.Count;
      var columnCount = source.Columns.Count;

      var target = new System.Data.DataTable(source.TableName);

      for (var index = 0; index < rowCount; index++)
        target.Columns.Add(targetColumnNames.EnsureColumnName(index));

      for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
      {
        var values = new object[rowCount];
        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
          values[rowIndex] = source.Rows[rowIndex][columnIndex];
        target.Rows.Add(values);
      }

      return target;
    }
  }
}
