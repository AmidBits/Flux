namespace Flux
{
  //public enum UrgfState
  //{
  //  Uninitialized,
  //  StartOfStream,
  //  StartOfFile,
  //  StartOfGroup,
  //  StartOfRecord,
  //  StartOfUnit,
  //  EndOfUnit,
  //  EndOfRecord,
  //  EndOfGroup,
  //  EndOfFile,
  //  EndOfStream,
  //}

  public class UrgfReader
  {
    public System.Collections.Generic.List<System.Data.DataSet> Read(string filePath)
    {
      using var sr = System.IO.File.OpenText(filePath);

      return Read(sr);
    }

    //private static System.Collections.Generic.List<System.Data.DataSet> ReadAll(string name, System.IO.TextReader reader)
    //{
    //  var all = new System.Collections.Generic.List<System.Data.DataSet>();

    //  while (reader.Peek() != -1)
    //  {
    //    var dataSet = ReadFile(reader, out int read);

    //    dataSet.DataSetName = all.Count.ToOrdinalName(name);

    //    all.Add(dataSet);
    //  }

    //  return all;
    //}

    //private static System.Data.DataSet ReadFile(System.IO.TextReader reader, out int read)
    //{
    //  var file = new System.Data.DataSet();

    //  do
    //  {
    //    var group = ReadGroup(reader, out read);

    //    group.TableName = file.Tables.Count.ToOrdinalName("Table");

    //    file.Tables.Add(group);
    //  }
    //  while (read != -1 && read != (int)TextDataSeparator.FileSeparator);

    //  return file;
    //}

    //private static System.Data.DataTable ReadGroup(System.IO.TextReader reader, out int read)
    //{
    //  var group = new System.Data.DataTable();

    //  do
    //  {
    //    var record = ReadRecord(reader, out read);

    //    for (var index = group.Columns.Count; index < record.Length; index++)
    //      group.Columns.Add(index.ToOrdinalName());

    //    group.Rows.Add(record);
    //  }
    //  while (read != -1 && read != (int)TextDataSeparator.GroupSeparator && read != (int)TextDataSeparator.FileSeparator);

    //  return group;
    //}

    //private static string[] ReadRecord(System.IO.TextReader reader, out int read)
    //{
    //  var record = new System.Collections.Generic.List<string>();

    //  do
    //  {
    //    var unit = ReadUnit(reader, out read);

    //    record.Add(unit);
    //  }
    //  while (read != -1 && read != (int)TextDataSeparator.RecordSeparator && read != (int)TextDataSeparator.GroupSeparator && read != (int)TextDataSeparator.FileSeparator);

    //  return record.ToArray();
    //}

    //private static string ReadUnit(System.IO.TextReader reader, out int read)
    //{
    //  var unit = new System.Text.StringBuilder();

    //  while ((read = reader.Read()) != -1 && read != (int)TextDataSeparator.UnitSeparator && read != (int)TextDataSeparator.RecordSeparator && read != (int)TextDataSeparator.GroupSeparator && read != (int)TextDataSeparator.FileSeparator)
    //    unit.Append((char)read);

    //  return unit.ToString();
    //}

    public static System.Collections.Generic.List<System.Data.DataSet> Read(System.IO.TextReader reader)
    {
      var read = -1;

      var dataSets = new System.Collections.Generic.List<System.Data.DataSet>();

      var list = new System.Collections.Generic.List<string>();
      var unit = new SpanMaker<char>();

      while (reader.Peek() != -1)
      {
        var dataSet = new System.Data.DataSet(dataSets.Count.ToSingleOrdinalColumnName("Set"));

        do
        {
          var dataTable = new System.Data.DataTable(dataSet.Tables.Count.ToSingleOrdinalColumnName("Table"));

          do
          {
            list.Clear();

            do
            {
              unit.Clear();

              while ((read = reader.Read()) != -1 && read != (int)Unicode.UnicodeInformationSeparator.UnitSeparator && read != (int)Unicode.UnicodeInformationSeparator.RecordSeparator && read != (int)Unicode.UnicodeInformationSeparator.GroupSeparator && read != (int)Unicode.UnicodeInformationSeparator.FileSeparator)
                unit = unit.Append((char)read);

              list.Add(unit.ToString());
            }
            while (read != -1 && read != (int)Unicode.UnicodeInformationSeparator.RecordSeparator && read != (int)Unicode.UnicodeInformationSeparator.GroupSeparator && read != (int)Unicode.UnicodeInformationSeparator.FileSeparator);

            for (var index = dataTable.Columns.Count; index < list.Count; index++)
              dataTable.Columns.Add(index.ToSingleOrdinalColumnName());

            dataTable.Rows.Add(list);
          }
          while (read != -1 && read != (int)Unicode.UnicodeInformationSeparator.GroupSeparator && read != (int)Unicode.UnicodeInformationSeparator.FileSeparator);

          dataSet.Tables.Add(dataTable);
        }
        while (read != -1 && read != (int)Unicode.UnicodeInformationSeparator.FileSeparator);

        dataSets.Add(dataSet);
      }

      return dataSets;
    }
  }

  public static partial class Fx
  {
    //public static System.Collections.Generic.IEnumerable<System.Data.DataSet> ReadUrgf(this System.IO.TextReader reader)
    //{
    //  System.Data.DataSet file = new();
    //  file.DataSetName = "DataSet 0";
    //  System.Data.DataTable group = new();
    //  group.TableName = "Table 0";
    //  System.Collections.Generic.List<string> record = new();
    //  System.Text.StringBuilder unit = new();

    //  while (reader.Read() is var read && read != -1 && (char)read is var c)
    //  {
    //    if (read == (int)TextDataSeparator.FileSeparator)
    //    {
    //      record.Add(unit.ToString());
    //      unit.Clear();
    //      group.Rows.Add(record.ToArray());
    //      record.Clear();
    //      file.Tables.Add(group);
    //      group = new($"Table {file.Tables.Count}");
    //      group.TableName = $"Table {file.Tables.Count}";

    //    }
    //    if (read == (int)TextDataSeparator.UnitSeparator || read == (int)TextDataSeparator.RecordSeparator || read == (int)TextDataSeparator.GroupSeparator || read == (int)TextDataSeparator.FileSeparator)
    //    {
    //      record.Add(unit.ToString());
    //      unit.Clear();

    //      if (read == (int)TextDataSeparator.RecordSeparator)
    //      {
    //        yield return record.ToArray();
    //        record.Clear();
    //      }
    //    }
    //    else
    //      unit.Append(c);
    //  }

    //  if (unit.Length > 0 || record.Count > 0 || group.Rows.Count > 0)
    //  {
    //    record.Add(unit.ToString());
    //    unit.Clear();

    //    yield return record.ToArray();
    //  }
    //}



    /// <summary>
    /// <para>Creates a sequence of fields by parsing URGF data.</para>
    /// </summary>
    /// <param name="reader">The <see cref="System.IO.TextReader"/> to read from.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static System.Collections.Generic.IEnumerable<string[]> ReadUrgf(this System.IO.TextReader reader)
    {
      var record = new System.Collections.Generic.List<string>();
      var unit = new System.Text.StringBuilder();

      while (reader.Read() is var read && read != -1 && (char)read is var c)
      {
        if (read == (int)Unicode.UnicodeInformationSeparator.UnitSeparator || read == (int)Unicode.UnicodeInformationSeparator.RecordSeparator)
        {
          record.Add(unit.ToString());
          unit.Clear();

          if (read == (int)Unicode.UnicodeInformationSeparator.RecordSeparator)
          {
            yield return record.ToArray();
            record.Clear();
          }
        }
        else
          unit.Append(c);
      }

      if (unit.Length > 0 || record.Count > 0)
      {
        record.Add(unit.ToString());
        unit.Clear();

        yield return record.ToArray();
      }
    }

    //if (unit.Length > 0 || record.Count > 0)
    //{
    //  record.Add(unit.ToString());
    //  unit.Clear();

    //  yield return record.ToArray();
    //}
  }
}
