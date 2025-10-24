namespace Flux
{
  public static partial class XtensionDataTable
  {
    //public static string ToUrgfString(this System.Data.DataColumnCollection source) => string.Join('\u001F', source.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName));
    //public static string ToUrgfString(this System.Data.DataRowCollection source) => string.Join('\u001E', source.Cast<System.Data.DataRow>().Select(dr => dr.ItemArray.ToUrgfString()));
    //public static string ToUrgfString(this System.Data.DataTable source) => source.Columns.ToUrgfString() + '\u001E' + source.Rows.ToUrgfString();
    //public static string ToUrgfString(this System.Data.DataTableCollection source) => string.Join('\u001D', source.Cast<System.Data.DataTable>().Select(dt => dt.ToUrgfString()));

    //public static int GetColumnOrdinalIndex(this System.Data.DataTable source, string columnName)
    //  => source.Columns[columnName]?.Ordinal ?? throw new System.ArgumentOutOfRangeException(nameof(columnName));

    //    /// <summary>Returns a new sequence with all values for that column.</summary>
    //    public static System.Collections.Generic.List<object> GetValuesInColumn(this System.Data.DataTable source, int columnIndex, bool reverseOrder = false)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      if (columnIndex < 0 || columnIndex >= source.Columns.Count) throw new System.ArgumentOutOfRangeException(nameof(source));

    //      var list = source.Rows.Cast<System.Data.DataRow>().Select(dr => dr[columnIndex]).ToList();

    //      if (reverseOrder)
    //        list.Reverse();

    //      return list;
    //    }

    //    /// <summary>Removes (as in deletes) all DataColumn objects matching the specified names.</summary>
    //    public static void RemoveAllColumnsIn(this System.Data.DataTable source, params string[] columnNames)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).Join(columnNames, s => s, cn => cn, (s, cn) => cn).ToList().ForEach(cn => source.Columns.Remove(cn));
    //    }

    //    /// <summary>Removes (as in deletes) all DataColumn objects NOT matching the specified names.</summary>
    //    public static void RemoveAllColumnsExcept(this System.Data.DataTable source, params string[] columnNames)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).Except(columnNames).ToList().ForEach(cn => source.Columns.Remove(cn));
    //    }

    //    /// <summary>Removes (as in deletes) all DataRow objects matching the specified filter expression.</summary>
    //    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable.select?view=netcore-3.1#System_Data_DataTable_Select_System_String_"/>
    //    /// <seealso cref="https://www.csharp-examples.net/dataview-rowfilter/"/>
    //    public static void RemoveAllRowsMatching(this System.Data.DataTable source, string filterExpression)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      source.Select(filterExpression).ToList().ForEach(dr => source.Rows.Remove(dr));
    //    }

    //    /// <summary>Removes (as in deletes) all DataRow objects NOT matching the specified filter expression.</summary>
    //    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable.select?view=netcore-3.1#System_Data_DataTable_Select_System_String_"/>
    //    /// <seealso cref="https://www.csharp-examples.net/dataview-rowfilter/"/>
    //    public static void RemoveAllRowsNotMatching(this System.Data.DataTable source, string filterExpression)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      source.Select($"NOT ({filterExpression})").ToList().ForEach(dr => source.Rows.Remove(dr));
    //    }

    extension(System.Data.DataTable source)
    {
      /// <summary>
      /// <para>Reverse <paramref name="count"/> columns at <paramref name="startIndex"/> of the <paramref name="source"/> <see cref="System.Data.DataTable"/> in-place. The process re-orders the columns (using the SetOrdinal() method) within the data table.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="startIndex"></param>
      /// <param name="count"></param>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public void FlipColumnsInPlace(int startIndex, int count)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (startIndex < 0 || startIndex > source.Columns.Count - 2) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
        if (count < 1 || startIndex + count > source.Columns.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

        for (int columnIndex = startIndex + count - 1; columnIndex >= startIndex; columnIndex--)
          source.Columns[startIndex].SetOrdinal(columnIndex);
      }

      /// <summary>
      /// <para>Reverse <paramref name="count"/> columns at <paramref name="startIndex"/> of the <paramref name="source"/> <see cref="System.Data.DataTable"/> in-place. The process re-orders the columns (using the SetOrdinal() method) within the data table.</para>
      /// </summary>
      /// <param name="source"></param>
      public void FlipColumnsInPlace()
        => FlipColumnsInPlace(source, 0, source.Columns.Count);

      /// <summary>
      /// <para>Reverse <paramref name="count"/> rows at <paramref name="startIndex"/> of the <paramref name="source"/> <see cref="System.Data.DataTable"/> in-place. The process swaps itemArray's within the data table.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="startIndex"></param>
      /// <param name="count"></param>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public void FlipRowsInPlace(int startIndex, int count)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (startIndex < 0 || startIndex > source.Rows.Count - 2) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
        if (count < 1 || startIndex + count > source.Rows.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

        var sourceRowIndex = startIndex;
        var targetRowIndex = startIndex + count - 1;

        while (sourceRowIndex < targetRowIndex)
          (source.Rows[targetRowIndex].ItemArray, source.Rows[sourceRowIndex].ItemArray) = (source.Rows[sourceRowIndex++].ItemArray, source.Rows[targetRowIndex--].ItemArray);
      }

      /// <summary>
      /// <para>Reverse all rows in the <paramref name="source"/> <see cref="System.Data.DataTable"/>. The process swaps itemArray's within the data table.</para>
      /// </summary>
      public void FlipRowsInPlace()
        => FlipRowsInPlace(source, 0, source.Rows.Count);

      /// <summary>
      /// <para>Creates a new <see cref="System.Data.DataTable"/> containing the <paramref name="source"/> data rotated (counter-clockwise) to the left.</para>
      /// </summary>
      /// <param name="targetColumnNames">If less target column names than needed are specified, "Column[ordinalIndex]" will be used.</param>
      public System.Data.DataTable RotateToCopyCcw(params string[] targetColumnNames)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var target = new System.Data.DataTable(source.TableName);

        var rowCount = source.Rows.Count;
        var columnCount = source.Columns.Count;

        for (var index = 0; index < rowCount; index++)
          target.Columns.Add(targetColumnNames.EnsureColumnName(index), typeof(string));

        for (var columnIndex = columnCount - 1; columnIndex >= 0; columnIndex--)
        {
          var itemArray = new object[rowCount];
          for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            itemArray[rowIndex] = source.Rows[rowIndex][columnIndex];
          target.Rows.Add(itemArray);
        }

        return target;
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Data.DataTable"/> containing the <paramref name="source"/> data rotated (clockwise) to the right.</para>
      /// </summary>
      /// <param name="targetColumnNames">If less target column names than needed are specified, "Column_[ordinalIndex]" will be used.</param>
      public System.Data.DataTable RotateToCopyCw(params string[] targetColumnNames)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var target = new System.Data.DataTable(source.TableName);

        var rowCount = source.Rows.Count;
        var columnCount = source.Columns.Count;

        for (var index = 0; index < rowCount; index++)
          target.Columns.Add(targetColumnNames.EnsureColumnName(index));

        for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
          var itemArray = new object[rowCount];
          for (var rowIndex = rowCount - 1; rowIndex >= 0; rowIndex--)
            itemArray[rowCount - 1 - rowIndex] = source.Rows[rowIndex][columnIndex];
          target.Rows.Add(itemArray);
        }

        return target;
      }

      //public static int[] MaxColumnWidths(this System.Data.DataView source, ConsoleStringOptions? options = null)
      //{
      //  System.ArgumentNullException.ThrowIfNull(source);
      //  System.ArgumentNullException.ThrowIfNull(source.Table);

      //  options ??= new ConsoleStringOptions();

      //  if (source.Count == 0) throw new System.ArgumentOutOfRangeException(nameof(source), "The DataView is empty.");

      //  var maxColumnWidths = source.Table.Columns.Cast<System.Data.DataColumn>().Select((e, i) => source.Cast<System.Data.DataRowView>().Max(dr => $"{dr[i]}".Length));

      //  if (options.IncludeColumnNames)
      //    maxColumnWidths = maxColumnWidths
      //      .Zip(source.Table.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName.Length))
      //      .Select(w => options.IncludeColumnNames ? System.Math.Max(w.First, w.Second) : w.First);

      //  var maxColumnWidth = maxColumnWidths.Max();

      //  return maxColumnWidths.Select(mw => options.UniformWidth ? System.Math.Max(mw, maxColumnWidth) : mw).ToArray();
      //}

      /// <summary>Returns the data table as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
      public string ToConsoleString(ConsoleFormatOptions? options = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        options ??= ConsoleFormatOptions.Default;

        if (source.Rows.Count == 0) throw new System.ArgumentOutOfRangeException(nameof(source), "The DataView is empty.");

        var sb = new System.Text.StringBuilder();

        #region MaxWidths

        var maxWidths = new int[source.Columns.Count];

        for (var r = source.Rows.Count - 1; r >= 0; r--)
          for (var c = maxWidths.Length - 1; c >= 0; c--)
            maxWidths[c] = int.Max(maxWidths[c], $"{source.Rows[r][c]}".Length);

        if (options.IncludeColumnNames)
          for (var c = maxWidths.Length - 1; c >= 0; c--)
            maxWidths[c] = int.Max(maxWidths[c], source.Columns[c].ColumnName.Length);

        var maxWidth = maxWidths.Max();

        if (options.UniformWidth)
          for (var c = maxWidths.Length - 1; c >= 0; c--)
            maxWidths[c] = maxWidth;

        #endregion // MaxWidths

        var verticalString = options.CreateVerticalString(maxWidths);

        var horizontalFormat = options.CreateHorizontalFormat(maxWidths);

        if (options.IncludeColumnNames)
        {
          var horizontalString = options.CreateHorizontalString(source.Columns.Cast<System.Data.DataColumn>().ToArray(), maxWidths);

          sb.AppendLine(horizontalString);

          if (verticalString is not null)
            sb.AppendLine(verticalString);
        }

        for (var r = 0; r < source.Rows.Count; r++)
        {
          if (verticalString is not null && r > 0)
            sb.AppendLine(verticalString);

          var horizontalString = options.CreateHorizontalString(source.Rows[r].ItemArray, maxWidths);

          sb.AppendLine(horizontalString);
        }

        return sb.ToString();
      }

      /// <summary>
      /// <para>Creates a new jagged array with the specified section from the <paramref name="source"/> <see cref="System.Data.DataTable"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="includeColumnNames">Whether to include column names as the first row.</param>
      /// <param name="columnStartIndex"></param>
      /// <param name="columnCount"></param>
      /// <param name="rowStartIndex"></param>
      /// <param name="rowCount"></param>
      /// <returns></returns>
      public object?[][] ToJaggedArray(bool includeColumnNames, int columnStartIndex, int columnCount, int rowStartIndex, int rowCount)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var arrayStartIndex = includeColumnNames ? 1 : 0;

        var array = new object?[arrayStartIndex + rowCount][];

        if (includeColumnNames)
          array[0] = source.Columns.Cast<System.Data.DataColumn>().Skip(columnStartIndex).Take(columnCount).Select(dc => dc.ColumnName).ToArray();

        for (var index = 0; index < rowCount; index++)
          array[arrayStartIndex + index] = source.Rows[rowStartIndex + index].ItemArray.ToCopy(columnStartIndex, columnCount);

        return array;
      }

      /// <summary>
      /// <para>Creates a new jagged array from the <paramref name="source"/> <see cref="System.Data.DataTable"/>.</para>
      /// <para>This function do NOT make copies of each <see cref="System.Data.DataRow.ItemArray"/> in <paramref name="source"/>. The new jagged array points to the same arrays as the <see cref="System.Data.DataTable"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="includeColumnNames">Whether to include column names as the first row.</param>
      /// <returns></returns>
      public object?[][] ToJaggedArray(bool includeColumnNames)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var arrayStartIndex = includeColumnNames ? 1 : 0;

        var rowCount = source.Rows.Count;

        var array = new object?[rowCount][];

        if (includeColumnNames)
          array[0] = [.. source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName)];

        for (var index = 0; index < rowCount; index++)
          array[arrayStartIndex + index] = source.Rows[index].ItemArray;

        return array;
      }

      public object[,] ToTwoDimensionalArray(bool includeColumnNames, int rowStartIndex, int rowCount, int columnStartIndex, int columnCount)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var offset = includeColumnNames ? 1 : 0;

        var array = new object[rowCount + offset, columnCount];

        if (includeColumnNames)
          for (var column = columnStartIndex + columnCount - 1; column >= columnStartIndex; column--)
            array[0, column - columnStartIndex] = source.Columns[column].ColumnName;

        for (var row = rowStartIndex + rowCount - 1; row >= rowStartIndex; row--)
          for (var column = columnStartIndex + columnCount - 1; column >= columnStartIndex; column--)
            array[row - rowStartIndex + offset, column - columnStartIndex] = source.Rows[row][column];

        return array;
      }

      public object[,] ToTwoDimensionalArray(bool includeColumnNames)
        => ToTwoDimensionalArray(source, includeColumnNames, 0, source.Rows.Count, 0, source.Columns.Count);

      /// <summary>
      /// <para>Creates a new <see cref="System.Xml.Linq.XDocument"/> with the data from the <see cref="System.Data.DataTable"/>.</para>
      /// </summary>
      public System.Xml.Linq.XDocument ToXDocument()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var document = new System.Xml.Linq.XDocument();
        using (var writer = document.CreateWriter())
          source.WriteXml(writer);
        return document;
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Xml.XmlDocument"/> with the data from the <see cref="System.Data.DataTable"/>.</para>
      /// </summary>
      public System.Xml.XmlDocument ToXmlDocument()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var document = new System.Xml.XmlDocument();
        using (var writer = document.CreateNavigator()?.AppendChild())
          source.WriteXml(writer);
        return document;
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Data.DataTable"/> containing the source data transposed (pivoted).</para>
      /// </summary>
      /// <param name="targetColumnNames">If less target column names than needed are specified, "Column_[ordinalIndex]" will be used.</param>
      public System.Data.DataTable TransposeToCopy(params string[] targetColumnNames)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var target = new System.Data.DataTable(source.TableName);

        var rowCount = source.Rows.Count;
        var columnCount = source.Columns.Count;

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

      public void WriteUrgf(System.IO.TextWriter target, bool alsoWriteColumnNames)
      {
        if (alsoWriteColumnNames)
          source.Columns.WriteUrgf(target);

        source.Rows.WriteUrgf(target);
      }
    }
  }
}
