namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a sequence of strings from all results, rows, columns and specified delimiters for easy parsing.</summary>
    /// <param name="nameSelector">The string representing the separation between result sets (if more than one).</param>
    /// <param name="valueSelector">The lambda used to convert the value to string.</param>
    /// <param name="nullValue">The lambda used to convert the value to string.</param>
    /// <param name="fieldSeparator">The string separator between columns (if more than one).</param>
    /// <param name="recordSeparator">The string separator between rows (if more than one).</param>
    /// <param name="resultSeparator">The string representing the separation between result sets (if more than one).</param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string> ExecuteStrings(this System.Data.IDbConnection source, string commandText, int commandTimeout, System.Func<string, int, string> nameSelector, System.Func<object, int, string> valueSelector, string nullValue = "\u2400", string fieldSeparator = "\u241F", string recordSeparator = "\u241E", string resultSeparator = "\u241D")
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (nameSelector is null) throw new System.ArgumentNullException(nameof(nameSelector));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      using var c = source.CreateCommand();

      c.CommandText = commandText;
      c.CommandTimeout = commandTimeout;

      using var idr = c.ExecuteReader();

      var resultIndex = 0;

      do
      {
        if (resultIndex++ > 0)
          yield return resultSeparator;

        for (var fieldIndex = 0; fieldIndex < idr.FieldCount; fieldIndex++)
        {
          if (fieldIndex > 0)
            yield return fieldSeparator;

          yield return nameSelector(idr.GetName(fieldIndex), fieldIndex) ?? idr.GetNameEx(fieldIndex);
        }

        yield return recordSeparator;

        var recordIndex = 0;

        while (idr.Read())
        {
          if (recordIndex++ > 0)
            yield return recordSeparator;

          for (var fieldIndex = 0; fieldIndex < idr.FieldCount; fieldIndex++)
          {
            if (fieldIndex > 0)
              yield return fieldSeparator;

            yield return valueSelector(idr.GetValue(fieldIndex), fieldIndex) ?? idr.GetStringEx(fieldIndex, nullValue);
          }
        }
      }
      while (idr.NextResult());
    }
  }
}
