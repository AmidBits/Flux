namespace Flux
{
  public static partial class IDataRecordExtensions
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

      public System.Collections.Generic.List<System.Type> GetFieldTypes()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var types = new System.Collections.Generic.List<System.Type>(source.FieldCount);

        for (var i = 0; i < source.FieldCount; i++)
          types.Add(source.GetFieldType(i));

        return types;
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
      public System.Collections.Generic.List<string> GetNames()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var names = new System.Collections.Generic.List<string>(source.FieldCount);

        for (var i = 0; i < source.FieldCount; i++)
          names.Add(source.GetNameEx(i));

        return names;
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
      public System.Collections.Generic.List<string> GetStrings(string nullString)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var strings = new System.Collections.Generic.List<string>(source.FieldCount);

        for (var i = 0; i < source.FieldCount; i++)
          strings.Add(source.GetString(i, nullString));

        return strings;
      }

      public string GetUrgf(string nullString)
        => string.Join((char)UnicodeInformationSeparator.UnitSeparator, GetStrings(source, nullString));

      /// <summary>Results in an object array of all column values in the current row.</summary>
      public System.Collections.Generic.List<object> GetValues()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var values = new System.Collections.Generic.List<object>(source.FieldCount);

        for (var i = 0; i < source.FieldCount; i++)
          values.Add(source.GetValue(i));

        return values;
      }

      public System.Collections.Generic.IDictionary<string, object> ToDictionary()
      {
        var od = new Flux.DataStructures.OrderedDictionary<string, object>();

        for (var i = 0; i < source.FieldCount; i++)
          od.Add(source.GetNameEx(i), source.GetValue(i));

        return od;
      }
    }
  }
}
