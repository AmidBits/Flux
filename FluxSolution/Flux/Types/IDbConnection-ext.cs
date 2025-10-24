namespace Flux
{
  public static partial class XtensionIDbConnection
  {
    extension(System.Data.IDbConnection source)
    {
      /// <summary>Returns a sequence of objects (as an array) from all results, rows and columns.</summary>
      /// <param name="includeNames">Whether to the column header names for each result set.</param>
      public System.Collections.Generic.IEnumerable<object[]> ExecuteArray(string commandText, int commandTimeout, bool includeNames)
      {
        using var e = ExecuteRecords(source, commandText, commandTimeout).GetEnumerator();

        if (e.MoveNext())
        {
          if (includeNames)
            yield return e.Current.GetNames().ToArray();

          do
          {
            yield return e.Current.GetValues().ToArray();
          }
          while (e.MoveNext());
        }
      }

      /// <summary>
      /// <para>Executes the specified <paramref name="commandText"/> and returns the number of rows affected.</para>
      /// <para><see cref="System.Data.IDbCommand.ExecuteNonQuery"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="commandText"></param>
      /// <param name="commandTimeout"></param>
      /// <returns>The number of rows affected.</returns>
      public int ExecuteNonQuery(string commandText, int commandTimeout)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        using var command = source.CreateCommand();

        command.CommandText = commandText;
        command.CommandTimeout = commandTimeout;

        return command.ExecuteNonQuery();
      }

      /// <summary>
      /// <para>Executes the specified <paramref name="commandText"/> and returns the number of rows affected.</para>
      /// <para><see cref="System.Data.IDbCommand.ExecuteNonQuery"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="commandText"></param>
      /// <param name="commandTimeout"></param>
      /// <param name="result"></param>
      /// <returns>The number of rows affected.</returns>
      public bool TryExecuteNonQuery(string commandText, int commandTimeout, out int result)
      {
        try
        {
          result = ExecuteNonQuery(source, commandText, commandTimeout);
          return true;
        }
        catch { }

        result = default;
        return false;
      }

      /// <summary>
      /// <para>Returns a sequence of <see cref="System.Data.IDataRecord"/> from all rows.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="commandText"></param>
      /// <param name="commandTimeout"></param>
      /// <returns>A sequence of <see cref="System.Data.IDataRecord"/>.</returns>
      public System.Collections.Generic.IEnumerable<System.Data.IDataRecord> ExecuteRecords(string commandText, int commandTimeout)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        using var command = source.CreateCommand();

        command.CommandText = commandText;
        command.CommandTimeout = commandTimeout;

        using var idr = command.ExecuteReader();

        do
        {
          if (idr.Read())
          {
            //var st = idr.GetSchemaTableEx();

            do
            {
              yield return idr;
            }
            while (idr.Read());

            //st.Dispose();
          }
        }
        while (idr.NextResult());

        //do
        //{
        //  while (idr.Read())
        //    yield return idr;
        //}
        //while (idr.NextResult());
      }

      /// <summary>
      /// <para>Returns a sequence of <see cref="System.Data.IDataRecord"/> and provides a SchemaTable from each of the result sets with all records.</para>
      /// </summary>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="source"></param>
      /// <param name="commandText"></param>
      /// <param name="commandTimeout"></param>
      /// <param name="recordSelector"></param>
      /// <returns>A sequence of <typeparamref name="TResult"/>.</returns>
      public System.Collections.Generic.IEnumerable<TResult> ExecuteRecords<TResult>(string commandText, int commandTimeout, System.Func<System.Data.IDataRecord, System.Data.DataTable?, TResult> recordSelector)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(recordSelector);

        using var c = source.CreateCommand();

        c.CommandText = commandText;
        c.CommandTimeout = commandTimeout;

        using var idr = c.ExecuteReader();

        do
        {
          var dt = idr.GetSchemaTableEx();

          while (idr.Read())
            yield return recordSelector(idr, dt);

          dt.Dispose();
        }
        while (idr.NextResult());
      }

      /// <summary>
      /// <para>Executes the specified command text and returns the value in the first column of the first row in the first resultset returned by the query. Additional columns, rows and resultsets are ignored.</para>
      /// <para><see cref="System.Data.IDbCommand.ExecuteScalar"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="commandText"></param>
      /// <param name="commandTimeout"></param>
      /// <returns>The value in the first column of the first row in the first resultset.</returns>
      public object? ExecuteScalar(string commandText, int commandTimeout)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        using var command = source.CreateCommand();

        command.CommandText = commandText;
        command.CommandTimeout = commandTimeout;

        return command.ExecuteScalar();
      }

      /// <summary>
      /// <para>Tries to execute the specified command text and returns the value in the first column of the first row in the first resultset returned by the query. Additional columns, rows and resultsets are ignored.</para>
      /// <para><see cref="System.Data.IDbCommand.ExecuteScalar"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="commandText"></param>
      /// <param name="commandTimeout"></param>
      /// <param name="result"></param>
      /// <returns>The value in the first column of the first row in the first resultset.</returns>
      public bool TryExecuteScalar(string commandText, int commandTimeout, out object? result)
      {
        try
        {
          result = ExecuteScalar(source, commandText, commandTimeout);
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      /// <summary>Returns a sequence of strings from all results, rows, columns and specified delimiters for easy parsing.</summary>
      /// <param name="nameSelector">The string representing the separation between result sets (if more than one).</param>
      /// <param name="valueSelector">The lambda used to convert the value to string.</param>
      /// <param name="nullValue">The lambda used to convert the value to string.</param>
      /// <param name="fieldSeparator">The string separator between columns (if more than one).</param>
      /// <param name="recordSeparator">The string separator between rows (if more than one).</param>
      /// <param name="resultSeparator">The string representing the separation between result sets (if more than one).</param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<string> ExecuteStrings(string commandText, int commandTimeout, System.Func<string, int, string> nameSelector, System.Func<object, int, string> valueSelector, string nullValue = "\u2400", string fieldSeparator = "\u241F", string recordSeparator = "\u241E", string resultSeparator = "\u241D")
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(nameSelector);
        System.ArgumentNullException.ThrowIfNull(valueSelector);

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

              yield return valueSelector(idr.GetValue(fieldIndex), fieldIndex) ?? idr.GetString(fieldIndex, nullValue);
            }
          }
        }
        while (idr.NextResult());
      }

      //    public static System.Data.DataTable ExecuteSchemaTable(this System.Data.IDbConnection source, Flux.Data.Sql.QName table, int commandTimeout, bool computeStatistics = true)
      //    {
      //      System.Data.DataTable schemaTable = null;

      //      using (var command = source.CreateCommand())
      //      {
      //        command.CommandText = $"SET ROWCOUNT 1; SELECT * FROM {table.QualifiedNameQuoted(3)} (NOLOCK);";

      //        using (var dataReader = command.ExecuteReader(System.Data.CommandBehavior.SingleRow))
      //        {
      //          schemaTable = dataReader.GetSchemaTableEx();

      //          schemaTable.Columns.Add("FullyQualifiedTableName", typeof(string));

      //          foreach (var dataRow in schemaTable.Rows.Cast<System.Data.DataRow>())
      //          {
      //            dataRow["FullyQualifiedTableName"] = table.QualifiedNameQuoted(4);
      //          }
      //        }
      //      }

      //      if (schemaTable != null && computeStatistics)
      //      {
      //        const string CountValue = nameof(CountValue);
      //        const string CountDistinct = nameof(CountDistinct);
      //        const string CountNull = nameof(CountNull);
      //        const string DataLengthAvg = nameof(DataLengthAvg);
      //        const string DataLengthMax = nameof(DataLengthMax);
      //        const string DataLengthMin = nameof(DataLengthMin);
      //        const string DataLengthSum = nameof(DataLengthSum);
      //        const string ValueAvg = nameof(ValueAvg);
      //        const string ValueMax = nameof(ValueMax);
      //        const string ValueMin = nameof(ValueMin);
      //        const string DateTimeMax = nameof(DateTimeMax);
      //        const string DateTimeMin = nameof(DateTimeMin);

      //        schemaTable.Columns.Add(CountValue, typeof(long));
      //        schemaTable.Columns.Add(CountDistinct, typeof(long));
      //        schemaTable.Columns.Add(CountNull, typeof(long));
      //        schemaTable.Columns.Add(DataLengthAvg, typeof(long));
      //        schemaTable.Columns.Add(DataLengthMax, typeof(long));
      //        schemaTable.Columns.Add(DataLengthMin, typeof(long));
      //        schemaTable.Columns.Add(DataLengthSum, typeof(long));
      //        schemaTable.Columns.Add(ValueAvg, typeof(long));
      //        schemaTable.Columns.Add(ValueMax, typeof(long));
      //        schemaTable.Columns.Add(ValueMin, typeof(long));
      //        schemaTable.Columns.Add(DateTimeMax, typeof(System.DateTime));
      //        schemaTable.Columns.Add(DateTimeMin, typeof(System.DateTime));

      //        foreach (var dataRow in schemaTable.Rows.Cast<System.Data.DataRow>())
      //        {
      //          var columnDefinition = Flux.Data.Sql.T.ParseColumnDefinition((string)dataRow[@"TsqlCurrentDefinition"]);

      //          var allowDBNull = columnDefinition.nullString.Equals(@"NULL");
      //          var columnName = columnDefinition.columnName;
      //          var dataTypeName = columnDefinition.dataTypeName;

      //          var columns = new System.Collections.Generic.List<string>();

      //          if (!Data.Sql.DataType.IsBlob(dataTypeName) && !Data.Sql.DataType.IsSpatial(dataTypeName) && !Data.Sql.DataType.IsXml(dataTypeName))
      //          {
      //            columns.Add($"COUNT_BIG(DISTINCT {columnName}) AS [{CountDistinct}]");
      //            columns.Add($"COUNT_BIG({columnName}) AS [{CountValue}]");
      //          }

      //          if (allowDBNull)
      //          {
      //            columns.Add($"ISNULL(SUM(CASE WHEN {columnName} IS NULL THEN CAST(1 AS bigint) END), 0) AS [{CountNull}]");
      //          }

      //          if (Data.Sql.DataType.IsBinary(dataTypeName) || Data.Sql.DataType.IsBlob(dataTypeName) || Data.Sql.DataType.IsString(dataTypeName))
      //          {
      //            columns.Add($"AVG(DATALENGTH({columnName})) AS [{DataLengthAvg}]");
      //            columns.Add($"MAX(DATALENGTH({columnName})) AS [{DataLengthMax}]");
      //            columns.Add($"MIN(DATALENGTH({columnName})) AS [{DataLengthMin}]");
      //            columns.Add($"SUM(DATALENGTH({columnName})) AS [{DataLengthSum}]");
      //          }

      //          if (Data.Sql.DataType.IsCurrency(dataTypeName) || Data.Sql.DataType.IsFixedPoint(dataTypeName) || Data.Sql.DataType.IsFloatingPoint(dataTypeName) || Data.Sql.DataType.IsInteger(dataTypeName))
      //          {
      //            columns.Add($"AVG({columnName}) AS [{ValueAvg}]");
      //            columns.Add($"MAX({columnName}) AS [{ValueMax}]");
      //            columns.Add($"MIN({columnName}) AS [{ValueMin}]");
      //          }

      //          if (Data.Sql.DataType.IsDateTime(dataTypeName))
      //          {
      //            columns.Add($"MAX({columnName}) AS [{DateTimeMax}]");
      //            columns.Add($"MIN({columnName}) AS [{DateTimeMin}]");
      //          }

      //          if (columns.Count > 0)
      //          {
      //            //foreach (var kvp in source.ExecuteDictionary($"SELECT TOP 0 {string.Join(Static.CommaSpace, columns)} FROM {table.QualifiedNameQuoted(3)} (NOLOCK); SELECT TOP 1 {string.Join(Static.CommaSpace, columns)} FROM {table.QualifiedNameQuoted(3)} (NOLOCK);", commandTimeout))
      //            foreach (var kvp in source.ExecuteDictionary($"SELECT TOP 1 {string.Join(Static.CommaSpace, columns)} FROM {table.QualifiedNameQuoted(3)} (NOLOCK);", commandTimeout))
      //            {
      //              dataRow[kvp.Key] = kvp.Value;
      //            }
      //          }

      //          //dataRow[indexStatistics] = source.ExecuteScalar($"SELECT TOP 0 {string.Join(Static.CommaSpace, columns)} FROM {table.QualifiedNameQuoted(3)} (NOLOCK); SELECT TOP 1 {string.Join(Static.CommaSpace, columns)} FROM {table.QualifiedNameQuoted(3)} (NOLOCK);", "NULL", 360);
      //        }
      //      }

      //      return schemaTable;
      //    }
    }
  }
}
