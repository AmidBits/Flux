using System.Linq;

namespace Flux
{
  public static partial class IDbConnectionEm
  {
    /// <summary>Returns a sequence of objects (as an array) from all results, rows and columns.</summary>
    /// <param name="includeNames">Whether to the column header names for each result set.</param>
    public static System.Collections.Generic.IEnumerable<object[]> ExecuteArray(this System.Data.IDbConnection source, string commandText, int commandTimeout, bool includeNames)
    {
      var index = 0;

      foreach (var idr in ExecuteRecords(source, commandText, commandTimeout))
      {
        if (index == 0 && includeNames)
        {
          yield return IDataRecordEm.GetNames(idr).ToArray();
        }

        yield return IDataRecordEm.GetValues(idr);
      }
    }
  }
}
