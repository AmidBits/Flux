using System.Linq;

namespace Flux
{
  // http://msdn.microsoft.com/query/dev12.query?appId=Dev12IDEF1&l=EN-US&k=k(System.IO.Stream);k(Stream);k(TargetFrameworkMoniker-.NETCore,Version%3Dv4.5.1);k(DevLang-csharp)&rd=true

  public static partial class XtensionsStream
  {
    /// <summary>Write a record (comma delimted and double quoted CSVs) to the System.IO.StreamWriter.</summary>
    public static void WriteRecord(this System.IO.StreamWriter source, object[] values)
    {
      void WriteField(object value)
      {
        source.Write('"');

        if (value is System.DBNull)
        {
          source.Write('\u2400');
        }
        else if (value is System.DateTime)
        {
          source.Write(((System.DateTime)value).ToString(@"O"));
        }
        else if (value is System.Byte[])
        {
          source.Write(System.Convert.ToBase64String((System.Byte[])value));
        }
        else
        {
          source.Write(System.Text.RegularExpressions.Regex.Replace(value.ToString(), @"([,\r\n])", @"\$1"));
        }

        source.Write('"');
      }

      for (var index = 0; index < values.Length; index++)
      {
        if (index > 0)
        {
          source.Write(',');
        }

        WriteField(values[index]);
      }

      source.WriteLine();
    }
    /// <summary>Writes a sequence of records (comma delimted and double quoted CSVs) from the System.IO.StreamReader.</summary>
    public static void WriteRecords(this System.IO.StreamWriter source, System.Collections.Generic.IEnumerable<object[]> records)
    {
      foreach (var record in records)
      {
        source.WriteRecord(record);
      }
    }
  }
}
