using System.Linq;

namespace Flux.IO
{
  public sealed class TabularReaderStream
    : Flux.Data.TabularDataReader
  {
    private readonly System.IO.StreamReader m_streamReader;

    private readonly char m_fieldSeparator;

    /// <summary>An array of the field provider types for the result.</summary>
    public System.Collections.Generic.List<string> FieldProviderTypes { get; } = new System.Collections.Generic.List<string>();

    public int RecordIndex { get; private set; }

    public TabularReaderStream(string path, System.Text.Encoding encoding, char fieldSeparator = ',')
    {
      m_streamReader = new System.IO.StreamReader(path, encoding);

      m_fieldSeparator = fieldSeparator;
    }

    /// <summary>Use this constructor to read up to three rows by specifying headerCount 1, 2 or 3.</summary>
    public void Initialize(int headerCount)
      => Initialize(headerCount, null, null, null);
    /// <summary>Use this constructor to read up to three rows by specifying headerCount 1 (field names), 2 (field types) or 3 (field provider types) with the option to specify them individually (this will override what is read).</summary>
    public void Initialize(int headerCount, System.Collections.Generic.IList<string>? fieldNames, System.Collections.Generic.IList<System.Type>? fieldTypes, System.Collections.Generic.IList<string>? fieldProviderTypes)
    {
      if (headerCount >= 1 && ReadFieldValues().ToList() is var streamFieldNames)
        FieldNames.AddRange(fieldNames ?? streamFieldNames!);
      else
        FieldNames.AddRange(fieldNames ?? throw new System.ArgumentNullException(nameof(fieldNames), @"Missing field names."));

      if (headerCount >= 2 && ReadFieldValues().Select(typeName => System.Type.GetType(typeName ?? @"Object")).ToList() is var streamFieldTypes)
        FieldTypes.AddRange(fieldTypes ?? streamFieldTypes!);
      else
        FieldTypes.AddRange(fieldTypes ?? throw new System.ArgumentOutOfRangeException(nameof(fieldNames), @"Missing field types."));

      if (FieldTypes.Any() && FieldNames.Any() && FieldTypes.Count != FieldNames.Count)
        throw new System.DataMisalignedException($"The number of field types ({FieldTypes.Count}) does not match the number of field names ({FieldNames.Count}).");

      if (headerCount >= 3 && ReadFieldValues().ToList() is var streamFieldProviderTypes)
        FieldProviderTypes.AddRange(fieldProviderTypes ?? streamFieldProviderTypes!);
      else
        FieldProviderTypes.AddRange(fieldProviderTypes ?? throw new System.ArgumentOutOfRangeException(nameof(fieldNames), @"Missing field provider types."));

      if (FieldProviderTypes.Any() && FieldNames.Any() && FieldProviderTypes.Count != FieldNames.Count)
        throw new System.DataMisalignedException($"The number of field provider types ({FieldProviderTypes.Count}) does not match the number of field types ({FieldNames.Count}).");
    }

    private readonly System.Text.StringBuilder m_fieldValue = new System.Text.StringBuilder();

    public System.Collections.Generic.IEnumerable<string> ReadCsvLine()
    {
      var inQuotedField = false;

      for (int read = m_streamReader.Read(), peek = m_streamReader.Peek(); read != -1; read = m_streamReader.Read(), peek = m_streamReader.Peek())
      {
        if (!inQuotedField)
        {
          if (read == '"')
          {
            inQuotedField = true;
          }
          else if (read == 13 || read == 10)
          {
            if (read == 13 && peek == 10)
            {
              m_streamReader.Read(); // Flush the following LineFeed character.
            }

            yield break;
          }
          else
          {
            yield return char.ConvertFromUtf32(read);
          }
        }
        else if (inQuotedField)
        {
          if (read == '"' && peek != '"')
          {
            inQuotedField = false;
          }
          else // Encountered a double quote but if was not followed by delimiter, carriage return, line feed or EOF, then treat it as a field value character.
          {
            if (read == '"' && peek == '"') // 
            {
              m_streamReader.Read(); // Discard the next double quote (it's just an escape character).
            }

            yield return char.ConvertFromUtf32(read);
          }
        }
      }
    }

    /// <summary>Extracts and returns a sequence of string fields</summary>
    public System.Collections.Generic.IEnumerable<string?> ReadFieldValues()
    {
      var isEscaped = false;

      while (m_streamReader.Read() is var read && read != -1 && m_streamReader.Peek() is var peek)
      {
        if (!isEscaped)
        {
          if (read == '"' && m_fieldValue.Length == 0)
          {
            isEscaped = true;
          }
          else if (read == m_fieldSeparator)
          {
            yield return getFieldValue(); // End Of Field.

            if (peek == -1) yield return string.Empty;
          }
          else if (read == '\r' || read == '\n')
          {
            yield return getFieldValue(); // End Of Field.

            if (read == '\r' && peek == '\n') m_streamReader.Read(); // Flush the following LineFeed character.

            yield break; // End Of Record.
          }
          else m_fieldValue.Append((char)read);
        }
        else if (isEscaped)
        {
          if (read == '"' && (peek == m_fieldSeparator || peek == '\r' || peek == '\n' || peek == -1)) // If it's a double quote followed by fieldSeparator, carriage return, line feed or EOF, then exit quoted mode.
          {
            isEscaped = false;
          }
          else // Encountered a double quote but if was not followed by delimiter, carriage return, line feed or EOF, then treat it as a field value character.
          {
            if (read == '"' && peek == '"') m_streamReader.Read(); // Discard the next double quote (it's just an escape character).

            m_fieldValue.Append((char)read);
          }
        }
      }

      string? getFieldValue()
      {
        string? value;

        if (m_fieldValue.Length == 1 && m_fieldValue[0] == '\u2400') // One Unicode NULL character.
        {
          value = null; // Return null.
        }
        else // There is text.
        {
          value = m_fieldValue.ToString(); // Return text;
        }

        m_fieldValue.Clear(); // Prepare for the next field value.

        return value;
      }
    }

    /// <summary>Converts the string values into appropriately parsed object types. Requires GetFieldType(index) to be implemented, and therefore FieldTypes needs to be initialized.</summary>
    public override bool Read()
    {
      FieldValues.Clear();
      FieldValues.Add(ReadFieldValues().Select((value, index) => ConvertToObject(value, GetFieldType(index))));

      if (FieldValues.Count > 0)
      {
        if (FieldValues.Count != FieldNames.Count)
        {
          throw new System.InvalidOperationException("The number of field values does not match the number of field names.");
        }

        RecordIndex++;

        return true;
      }

      return false;
    }

    protected override void DisposeManaged()
    {
      FieldProviderTypes.Clear();

      m_streamReader.Dispose();

      base.DisposeManaged();
    }

    /// <summary>Converts the string values into appropriately parsed object types. Requires GetFieldType(index) to be implemented, and therefore FieldTypes needs to be initialized.</summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/system.dbnull.value"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/system.boolean.parse"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/system.convert.frombase64string"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/system.datetime.parse"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/system.guid.parse"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/system.timespan.parse"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/system.convert.changetype"/>
    private static object ConvertToObject(string? value, System.Type type)
    {
      if (value == null || value == "\u2400") // The Unicode NULL character is already converted to a null value when reading from the stream, so convert null values to System.DBNull.
      {
        return System.DBNull.Value;
      }
      else if (type == typeof(bool)) // Parse method.
      {
        return bool.Parse(value);
      }
      else if (type == typeof(byte[])) // Convert base64 text to binary data (byte[]).
      {
        return System.Convert.FromBase64String(value);
      }
      else if (type == typeof(System.DateTime)) // Parse method. Must be formatted as ISO6801 'yyyy-MM-ddTHH:mm:ss.fffffff' (DateTimeKind.Unspecified).
      {
        return System.DateTime.Parse(value, System.Globalization.CultureInfo.CurrentCulture);
      }
      else if (type == typeof(System.Guid)) // Parse method.
      {
        return System.Guid.Parse(value);
      }
      else if (type == typeof(string)) // Obviously no conversion is needed for strings.
      {
        return value;
      }
      else if (type == typeof(System.TimeSpan)) // Parse method.
      {
        return System.TimeSpan.Parse(value, System.Globalization.CultureInfo.CurrentCulture);
      }
      else // All other input use ChangeType method
      {
        return System.Convert.ChangeType(value, type, System.Globalization.CultureInfo.CurrentCulture);
      }
    }
  }
}
