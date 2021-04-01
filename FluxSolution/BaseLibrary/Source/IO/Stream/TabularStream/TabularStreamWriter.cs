using System.Linq;

namespace Flux.IO
{
  public sealed class TabularStreamWriter
    : Disposable
  {
    private readonly System.IO.StreamWriter _streamWriter;

    private readonly char _fieldSeparator;

    //private int _notifyAt = 10, _recordIndex;
    //public bool TimeToNotify => (_recordIndex % _notifyAt) == 0;
    //public int RecordIndex
    //{
    //  get => _recordIndex; private set
    //  {
    //    _recordIndex = value;

    //    if (_recordIndex > _notifyAt && _notifyAt <= 1000000)
    //    {
    //      _notifyAt = (_recordIndex - 1) * 10;
    //    }
    //  }
    //}

    public TabularStreamWriter(string path, System.Text.Encoding encoding, char fieldSeparator = ',')
    {
      _streamWriter = new System.IO.StreamWriter(path, false, encoding);

      _fieldSeparator = fieldSeparator;
    }

    /// <summary>Writes a sequence of object values to the stream, by converting them to strings using the ConvertToString method.</summary>
    public void WriteRecord(System.Collections.Generic.IEnumerable<object> values)
    {
      //RecordIndex++;

      WriteFields(values.Select(value => ConvertToString(value)));
    }

    /// <summary>Writes a sequence of string values to the stream, unmodified. Proper conversions must be performed prior to calling this method if needed (see WriteRecord/ConvertToString).</summary>
    public void WriteFields(System.Collections.Generic.IEnumerable<string> values)
    {
      if (values is null) throw new System.ArgumentNullException(nameof(values));

      var fieldIndex = 0;

      foreach (var value in values)
      {
        if (fieldIndex++ > 0)
        {
          _streamWriter.Write(_fieldSeparator);
        }

        if (value.IndexOfAny($"{_fieldSeparator}\"\r\n") > -1)
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
      switch (value)
      {
        case null:
        case System.DBNull _:
          return "\u2400"; // Convert null and System.DBNull to the Unicode NULL character (has to be escaped in C#) as a string.
        case string s:
          return s;
        case System.DateTime dt:
          return dt.ToString(@"yyyy-MM-ddTHH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture); // Convert datetime objects to a ISO8601 format in order to retain detail.
        case byte[] ba:
          return System.Convert.ToBase64String(ba); // Convert binary data to base64 in order to store as text.
        default:
          return value?.ToString() ?? string.Empty; // Get the default string value for all other types.
      }
    }
  }
}
