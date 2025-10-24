namespace Flux
{
  public static partial class XtensionIDataRecord
  {
    extension(System.Data.IDataRecord source)
    {
      public string GetDefaultTsqlDefinition(int index, bool nullable = true)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var name = source.GetName(index);
        var dataTypeName = Data.TsqlDataType.NameFromType(source.GetFieldType(index));
        var magnitude = Data.TsqlDataType.GetDefaultArgument(dataTypeName, true);

        return $"[{name}] [{dataTypeName}]{magnitude} {(nullable ? "NULL" : "NOT NULL")}";
      }

      public System.Collections.Generic.IEnumerable<System.Type> GetFieldTypes()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (var index = 0; index < source.FieldCount; index++)
          yield return source.GetFieldType(index);
      }

      /// <summary>
      /// <para>Returns a string with the column name (header) for the specified index in the current row. This version will replace null or blank names with "Column_N" where N is the column index + 1.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="index"></param>
      /// <returns></returns>
      public string GetNameEx(int index)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.GetName(index) is var name && string.IsNullOrWhiteSpace(name) ? index.ToSingleOrdinalColumnName() : name;
      }

      /// <summary>
      /// <para>Results in a string array of all column names.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<string> GetNames()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (var index = 0; index < source.FieldCount; index++)
          yield return source.GetNameEx(index);
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public int GetRecordSetHash()
        => System.HashCode.Combine(source.GetNames().SequenceHashCode(), source.GetFieldTypes().SequenceHashCode());

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public int GetRecordHash()
        => System.HashCode.Combine(source.GetRecordSetHash(), source.GetValues().SequenceHashCode());

      /// <summary>Results in a string array of all column values in the current row.</summary>
      public string GetString(int index, string nullString)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.IsDBNull(index) ? nullString : source.GetValue(index).ToString() ?? nullString;
      }

      /// <summary>Results in a sequence of strings of all column values in the current row.</summary>
      public System.Collections.Generic.IEnumerable<string> GetStrings(string nullString)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (var index = source.FieldCount - 1; index >= 0; index--)
          yield return source.GetString(index, nullString);
      }

      public string GetUrgf(string nullString)
        => string.Join((char)UnicodeInformationSeparator.UnitSeparator, GetStrings(source, nullString));

      /// <summary>Results in an object array of all column values in the current row.</summary>
      public System.Collections.Generic.IEnumerable<object> GetValues()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        for (var index = source.FieldCount - 1; index >= 0; index--)
          yield return source.GetValue(index);
      }

      public System.Collections.Generic.IDictionary<string, object> ToDictionary()
      {
        var od = new Flux.DataStructures.OrderedDictionary<string, object>();

        for (var index = 0; index < source.FieldCount; index++)
          od.Add(source.GetNameEx(index), source.GetValue(index));

        return od;
      }
    }
  }
}
