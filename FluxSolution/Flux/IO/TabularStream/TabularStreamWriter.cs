namespace Flux.IO.TabularStream
{
  public sealed class TabularStreamWriter(string path, System.Text.Encoding encoding, char fieldSeparator = ',')
    : Disposable
  {
    private readonly System.IO.StreamWriter _streamWriter = new(path, false, encoding);

    /// <summary>Writes a sequence of object values to the stream, by converting them to strings using the ConvertToString method.</summary>
    public void WriteRecord(System.Collections.Generic.IEnumerable<object> values)
    {
      //RecordIndex++;

      WriteFields(values.Select(value => ConvertToString(value)));
    }

    /// <summary>Writes a sequence of string values to the stream, unmodified. Proper conversions must be performed prior to calling this method if needed (see WriteRecord/ConvertToString).</summary>
    public void WriteFields(System.Collections.Generic.IEnumerable<string> values)
    {
      System.ArgumentNullException.ThrowIfNull(values);

      var fieldIndex = 0;

      foreach (var value in values)
      {
        if (fieldIndex++ > 0)
        {
          _streamWriter.Write(fieldSeparator);
        }

        if (value.AsSpan().IndexOfAny($"{fieldSeparator}\"\r\n".ToCharArray()) > -1)
        {
          _streamWriter.Write('"');
          _streamWriter.Write(value.Replace("\"", "\"\"", System.StringComparison.Ordinal));
          _streamWriter.Write('"');
        }
        else
        {
          _streamWriter.Write(value);
        }
      }

      _streamWriter.WriteLine();
    }

    protected override void DisposeManaged()
    {
      _streamWriter.Dispose();
    }

    public static string ConvertToString(object value)
    {
      return value switch
      {
        null or System.DBNull _ => "\u2400",// Convert null and System.DBNull to the Unicode NULL character (has to be escaped in C#) as a string.
        string s => s,
        System.DateTime dt => dt.ToString(@"yyyy-MM-ddTHH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture),// Convert datetime objects to a ISO8601 format in order to retain detail.
        byte[] ba => System.Convert.ToBase64String(ba),// Convert binary data to base64 in order to store as text.
        _ => value?.ToString() ?? string.Empty,// Get the default string value for all other types.
      };
    }
  }
}
