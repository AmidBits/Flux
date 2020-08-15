using System.Linq;

namespace Flux
{
  public static partial class XtendData
  {
    public static string ToCsv(this System.Data.IDataRecord source, string nullValue, bool quotes)
    {
      var sb = new System.Text.StringBuilder();

      for (var index = 0; index < source.FieldCount; index++)
      {
        if (index > 0)
        {
          sb.Append(',');
        }

        if (quotes)
        {
          sb.Append('"');
        }

        var value = source.GetStringEx(index, nullValue);

        sb.Append(value.Contains('"', System.StringComparison.Ordinal) ? value.Replace("\"", "\"\"", System.StringComparison.Ordinal) : value);

        if (quotes)
        {
          sb.Append('"');
        }
      }

      return sb.ToString();
    }
  }
}
