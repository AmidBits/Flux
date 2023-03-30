namespace Flux
{
  public static partial class ExtensionMethodsIDataRecord
  {
    public static string ToCsv(this System.Data.IDataRecord source, string nullValue, bool quotes)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (nullValue is null) throw new System.ArgumentNullException(nameof(nullValue));

      var sb = new System.Text.StringBuilder();

      for (var index = 0; index < source.FieldCount; index++)
      {
        if (index > 0)
          sb.Append(',');

        if (quotes)
          sb.Append('"');

        var value = source.GetStringEx(index, nullValue);

        sb.Append(value.Contains('"', System.StringComparison.Ordinal) ? value.Replace("\"", "\"\"", System.StringComparison.Ordinal) : value);

        if (quotes)
          sb.Append('"');
      }

      return sb.ToString();
    }
  }
}
