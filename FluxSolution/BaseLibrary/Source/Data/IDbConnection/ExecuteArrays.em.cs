using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns a sequence of objects (as an array) from all results, rows and columns.</summary>
    /// <param name="includeNames">Whether to the column header names for each result set.</param>
    public static System.Collections.Generic.IEnumerable<object[]> ExecuteArray(this System.Data.IDbConnection source, string commandText, int commandTimeout, bool includeNames)
    {
      using var e = ExecuteRecords(source, commandText, commandTimeout).GetEnumerator();

      if (e.MoveNext())
      {
        if (includeNames)
          yield return GetNames(e.Current).ToArray();

        do
        {
          yield return GetValues(e.Current);
        }
        while (e.MoveNext());
      }
    }
  }
}
