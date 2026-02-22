namespace Flux
{
  public static partial class IDataRecordExtensions
  {
    extension(System.Data.IDataRecord source)
    {
      /// <summary>
      /// <para>Creates a string with the default T-SQL definition for the field at <paramref name="index"/> in a <see cref="System.Data.IDataRecord"/>.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="nullable"></param>
      /// <returns></returns>
      public string CreateDefaultTsqlDefinition(int index, bool nullable = true)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var name = source.GetName(index);
        var dataTypeName = Data.TsqlDataType.NameFromType(source.GetFieldType(index));
        var magnitude = Data.TsqlDataType.GetDefaultArgument(dataTypeName, true);

        return $"[{name}] [{dataTypeName}]{magnitude} {(nullable ? "NULL" : "NOT NULL")}";
      }

      /// <summary>
      /// <para>Generates a hash for a recordset using field names and types for the generation.</para>
      /// </summary>
      /// <returns></returns>
      public int GenerateRecordSetHash()
        => System.HashCode.Combine(source.GetNames().SequenceHashCode(), source.GetFieldTypes().SequenceHashCode());

      /// <summary>
      /// <para>Generates a hash for a record using <see cref="GenerateRecordSetHash(System.Data.IDataRecord)"/> and the values in a <see cref="System.Data.IDataRecord"/>.</para>
      /// </summary>
      /// <returns></returns>
      public int GenerateRecordHash()
        => System.HashCode.Combine(source.GenerateRecordSetHash(), source.GetValues().SequenceHashCode());

      /// <summary>
      /// <para>Returns a list of <see cref="System.Type"/> representing the type of each field in a <see cref="System.Data.IDataRecord"/>.</para>
      /// </summary>
      /// <returns></returns>
      public System.Collections.Generic.List<System.Type> GetFieldTypes()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var types = new System.Collections.Generic.List<System.Type>(source.FieldCount);

        for (var i = 0; i < source.FieldCount; i++)
          types.Add(source.GetFieldType(i));

        return types;
      }

      /// <summary>
      /// <para>Creates a new string with the column name (header) of the specified <paramref name="index"/> in a <see cref="System.Data.IDataRecord"/>.</para>
      /// <para>This version will replace null or blank names with "Column_N" where N is the column index + 1.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <returns></returns>
      public string GetNameEx(int index)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.GetName(index) is var name && string.IsNullOrWhiteSpace(name) ? index.ToSingleOrdinalColumnName() : name;
      }

      /// <summary>
      /// <para>Creates a list with all column names in a <see cref="System.Data.IDataRecord"/>.</para>
      /// </summary>
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
      /// <para>Creates a new string with the field value of <paramref name="index"/> in a <see cref="System.Data.IDataRecord"/>.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="nullString"></param>
      /// <returns></returns>
      public string GetString(int index, string nullString)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.IsDBNull(index) ? nullString : source.GetValue(index).ToString() ?? nullString;
      }

      /// <summary>
      /// <para>Creates a list of <see cref="System.String"/> with all field values in a <see cref="System.Data.IDataRecord"/>.</para>
      /// </summary>
      /// <param name="nullString"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<string> GetStrings(string nullString)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var strings = new System.Collections.Generic.List<string>(source.FieldCount);

        for (var i = 0; i < source.FieldCount; i++)
          strings.Add(source.GetString(i, nullString));

        return strings;
      }

      public string GetUrgf(string nullString)
        => string.Join('\u001F', GetStrings(source, nullString));

      /// <summary>
      /// <para>Creates a list of <see cref="object"/> with all field values in a <see cref="System.Data.IDataRecord"/>.</para>
      /// </summary>
      /// <returns></returns>
      public System.Collections.Generic.List<object> GetValues()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var values = new System.Collections.Generic.List<object>(source.FieldCount);

        for (var i = 0; i < source.FieldCount; i++)
          values.Add(source.GetValue(i));

        return values;
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> with all field names and values in a <see cref="System.Data.IDataRecord"/>.</para>
      /// </summary>
      /// <returns></returns>
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
