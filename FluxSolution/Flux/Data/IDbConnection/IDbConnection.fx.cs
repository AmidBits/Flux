//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
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
//  }
//}
