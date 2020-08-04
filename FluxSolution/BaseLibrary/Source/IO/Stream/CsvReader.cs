using System.Linq;

namespace Flux.IO
{
  /// <summary></summary>
  /// <remarks>Fields with any embedded meta characters, i.e. ',', '\r', '\n' or '"', must be enclosed by double quotes (a.k.a. quoted). Additionally, embedded double quotes must each be represented by a pair of double quotes (i.e. they must be doubled up).</remarks>
  public class CsvReader
  {
    private readonly System.IO.TextReader _textReader;

    private char _fieldSeparator;
    public char FieldSeparator { get => _fieldSeparator; set => _fieldSeparator = value; }

    private char _rowSeparator;
    public char RowSeparator { get => _rowSeparator; set => _rowSeparator = value; }

    public CsvReader(System.IO.TextReader textReader, char fieldSeparator = '"', char rowSeparator = '\n')
    {
      _textReader = textReader;

      _fieldSeparator = fieldSeparator;
      _rowSeparator = rowSeparator;
    }

    public bool AtEndOfLine { get; private set; }
    public bool AtEndOfStream { get; private set; }

    public bool IsFieldBalanced { get; private set; }
    public bool IsFieldEnclosed { get; private set; }

    //public System.Text.StringBuilder ReadRawLine()
    //{
    //  var line = new System.Text.StringBuilder();

    //  while (ReadFieldRaw() is var field && field != null)
    //  {
    //    line.Append(field);

    //    if (AtEndOfLine) break;
    //  }

    //  return line.Length > 0 ? line : null;
    //}

    //public string ReadField()
    //{
    //  var raw = ReadFieldRaw();

    //  if (AtEndOfStream)
    //  {
    //    return null;
    //  }

    //  var size = raw[raw.Length - 2] == '\r' && raw[raw.Length - 1] == '\n' ? 2 : 1; raw.Remove(raw.Length - size, size);

    //  if (_containsEscapeEncloser)
    //  {
    //    for (var index = raw.Length - 2; index > 0; index--)
    //    {
    //      if (raw[index] == '"')
    //      {
    //        raw.Remove(index--, 1);
    //      }
    //    }

    //    return raw.ToString(1, raw.Length - 2);
    //  }
    //  else
    //  {
    //    return raw.ToString();
    //  }
    //}

    public string? ReadField()
    {
      if (AtEndOfStream) return null;

      IsFieldBalanced = true;
      IsFieldEnclosed = false;

      var fieldValue = new System.Text.StringBuilder();

      while (_textReader.Read() is var read)
      {
        if (read == '"')
        {
          IsFieldBalanced = !IsFieldBalanced;

          IsFieldEnclosed = true;
        }
        else if (read == _fieldSeparator)
        {
          if (IsFieldBalanced)
          {
            AtEndOfLine = false;

            break;
          }
        }
        else if (read == _rowSeparator)
        {
          if (IsFieldBalanced)
          {
            AtEndOfLine = true;

            if (_rowSeparator == '\n' && fieldValue.Length > 1 && fieldValue[fieldValue.Length - 1] == '\r') // If the line break is line feed, check for carriage return and remove it.
            {
              fieldValue.Remove(fieldValue.Length - 1, 1);
            }

            break;
          }
        }
        else if (read == -1)
        {
          AtEndOfLine = true;
          AtEndOfStream = true;

          break;
        }

        fieldValue.Append((char)read);
      }

      if (IsFieldEnclosed)
      {
        for (var index = fieldValue.Length - 2; index > 0; index--)
        {
          if (fieldValue[index] == '"')
          {
            fieldValue.Remove(index--, 1);
          }
        }

        return fieldValue.ToString(1, fieldValue.Length - 2);
      }
      else
      {
        return fieldValue.ToString();
      }
    }

    //public string ReadFields()
    //{
    //  IsFieldBalanced = true;
    //  IsFieldEnclosed = false;

    //  var fields = new System.Collections.Generic.List<string>();

    //  var fieldValue = new System.Text.StringBuilder();

    //  while (_textReader.Read() is var read)
    //  {
    //    if (read == '"')
    //    {
    //      IsFieldBalanced = !IsFieldBalanced;

    //      IsFieldEnclosed = true;
    //    }
    //    else if (read == _fieldSeparator && IsFieldBalanced)
    //    {
    //      AtEndOfLine = false;

    //      break;
    //    }
    //    else if (read == _rowSeparator && IsFieldBalanced)
    //    {
    //      AtEndOfLine = true;

    //      if (_rowSeparator == '\n' && fieldValue.Length > 1 && fieldValue[fieldValue.Length - 1] == '\r') // If the line break is line feed, check for carriage return and remove, just in case.
    //      {
    //        fieldValue.Remove(fieldValue.Length - 1, 1);
    //      }

    //      break;
    //    }
    //    else if (read == -1)
    //    {
    //      AtEndOfLine = true;
    //      AtEndOfStream = true;

    //      return null;
    //    }

    //    fieldValue.Append((char)read);
    //  }

    //  if (IsFieldEnclosed)
    //  {
    //    for (var index = fieldValue.Length - 2; index > 0; index--)
    //    {
    //      if (fieldValue[index] == '"')
    //      {
    //        fieldValue.Remove(index--, 1);
    //      }
    //    }

    //    return fieldValue.ToString(1, fieldValue.Length - 2);
    //  }
    //  else
    //  {
    //    return fieldValue.ToString();
    //  }
    //}

    //public System.Text.StringBuilder ReadFieldRaw()
    //{
    //  _balancedEscapeEncloser = true;
    //  _containsEscapeEncloser = false;

    //  var raw = new System.Text.StringBuilder();

    //  while (_textReader.Read() is var read)
    //  {
    //    if (read == -1)
    //    {
    //      AtEndOfLine = true;
    //      AtEndOfStream = true;

    //      break;
    //    }

    //    raw.Append((char)read);

    //    if (read == '"')
    //    {
    //      _balancedEscapeEncloser = !_balancedEscapeEncloser;

    //      _containsEscapeEncloser = true;
    //    }
    //    else if (read == _fieldSeparator && _balancedEscapeEncloser)
    //    {
    //      AtEndOfLine = false;

    //      break;
    //    }
    //    else if (read == _rowSeparator && _balancedEscapeEncloser)
    //    {
    //      AtEndOfLine = true;

    //      break;
    //    }
    //  }

    //  return raw;
    //}

    public string[]? ReadRecord()
    {
      if (AtEndOfStream) return null;

      var record = new System.Collections.Generic.List<string>();

      while (ReadField() is var field && field != null)
      {
        record.Add(field);

        if (AtEndOfLine) break;
      }

      return record.Count > 0 ? record.ToArray() : null;
    }
  }
}
