namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a sequence of objects (as an array) from all results, rows and columns.</summary>
    /// <param name="includeNames">Whether to the column header names for each result set.</param>
    public static System.Collections.Generic.IEnumerable<object[]> ExecuteArray(this System.Data.IDbConnection source, string commandText, int commandTimeout, bool includeNames)
    {
      using var e = ExecuteRecords(source, commandText, commandTimeout).GetEnumerator();

      if (e.MoveNext())
      {
        if (includeNames)
          yield return e.Current.GetNames();

        do
        {
          yield return e.Current.GetValues();
        }
        while (e.MoveNext());
      }
    }
  }
}
