using System.Linq;

namespace Flux
{
  // http://msdn.microsoft.com/query/dev12.query?appId=Dev12IDEF1&l=EN-US&k=k(System.IO.Stream);k(Stream);k(TargetFrameworkMoniker-.NETCore,Version%3Dv4.5.1);k(DevLang-csharp)&rd=true

  public static partial class XtendStream
  {
    /// <summary>Read a record (comma delimted and double quoted CSVs) from the System.IO.StreamReader.</summary>
    public static System.Collections.Generic.IList<string?> ReadRecord(this System.IO.StreamReader source)
    {
      return GetFields().ToArray();

      System.Collections.Generic.IEnumerable<string?> GetFields()
      {
        var sb = new System.Text.StringBuilder();
        int index = 0, read;
        char previous = ',', current, next;

        while ((read = source.Read()) != -1)
        {
          current = (char)read;
          next = (char)source.Peek();

          if (current == '\\' && (next == ',' || next == '\r' || next == '\n'))
          {
            source.Read();
            sb.Append(next);
          }
          else if (previous == '"' && current == ',' && next == '"') // field delimiter
          {
            var stringValue = sb.ToString(1, sb.Length - 2);
            if (stringValue.Length == 1 && stringValue[0] == '\u2400') { stringValue = null; }
            yield return stringValue;
            sb = new System.Text.StringBuilder();
            index++;
          }
          else if (previous == '"' && current == '\r' && next == '\n') // enough columns were read, end of line
          {
            source.Read();
            var stringValue = sb.ToString(1, sb.Length - 2);
            if (stringValue.Length == 1 && stringValue[0] == '\u2400') { stringValue = null; }
            yield return stringValue;
            yield break;
          }
          else
          {
            sb.Append(current);
          }

          previous = current;
        }
      }
    }
    /// <summary>Reads a sequence of records (comma delimted and double quoted CSVs) from the System.IO.StreamReader.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<string?>> ReadRecords(this System.IO.StreamReader source)
    {
      while (source.ReadRecord() is var record && record != null && record.Count() > 0)
      {
        yield return record;
      }
    }
  }
}
