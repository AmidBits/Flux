using System.Linq;

namespace Flux.Data
{
  public record struct TsqlColumnDefinition
  {
    public static readonly TsqlColumnDefinition Empty;

    /// <summary>Returns the column name.</summary>
    public string ColumnName { get; private set; }

    /// <summary>Returns the column data type information.</summary>
    // public TsqlDataType DataType { get; private set; }

    /// <summary>Returns the column data type name.</summary>
    public string DataTypeName { get; private set; }

    /// <summary>Returns the column data type arguments.</summary>
    public System.Collections.Generic.IReadOnlyList<string> DataTypeArguments { get; private set; }

    /// <summary>Maintaince the nullability of the column..</summary>
    public TsqlNullability Nullability { get; private set; }

    public TsqlColumnDefinition(string columnName, string dataTypeName, System.Collections.Generic.IEnumerable<string> dataTypeArguments, TsqlNullability nullability)
    {
      ColumnName = columnName.TsqlUnenquote();
      DataTypeName = dataTypeName.TsqlUnenquote();
      DataTypeArguments = dataTypeArguments.Select(s => s.ToStringBuilder().RemoveAll(char.IsWhiteSpace).ToString()).ToList();
      Nullability = nullability;
    }

    public string ToString(bool ansi)
      => $"{ColumnName.TsqlEnquote(ansi)} {DataTypeName.TsqlEnquote(ansi)} {FromDataTypeArguments(DataTypeArguments)} {Nullability}";

    #region Static methods
    /// <summary>Convert a sequence of data type arguments into its T-SQL string representation,</summary>
    public static string FromDataTypeArguments(System.Collections.Generic.IEnumerable<string> dataTypeArguments)
      => string.Join(@",", dataTypeArguments) is var dta && dta.Length > 0 ? dta.Wrap('(', ')') : string.Empty;
    /// <summary>Convert a data type arguments string into a new sequence of argument values.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToDataTypeArguments(string dataTypeArgumentsAsString)
      => dataTypeArgumentsAsString.ToStringBuilder().RemoveAll(char.IsWhiteSpace).Unwrap('(', ')').Split(System.StringSplitOptions.RemoveEmptyEntries, new char[] { ',' });

    private static readonly System.Text.RegularExpressions.Regex m_reParse = new(@"^\s*?(?<ColumnName>\""[^\""]+\""|\[[^\]]+\]|\w+)\s*?(?<DataTypeName>\""[^\""]+\""|\[[^\]]+\]|\w+)\s*?(?<DataTypeArguments>\([\w\s\,]+\))?\s*?(?<Nullability>NOT\s+NULL|NULL)\s*?$");
    public static TsqlColumnDefinition Parse(string tsqlColumnDefinition)
    {
      var match = m_reParse.Match(tsqlColumnDefinition);

      var columnName = match.Groups[nameof(ColumnName)].Value;
      var dataTypeName = match.Groups[nameof(DataTypeName)].Value;
      var dataTypeArguments = ToDataTypeArguments(match.Groups[nameof(DataTypeArguments)].Value);
      var isNullable = TsqlNullability.Parse(match.Groups[@"Nullability"].Value);

      return new(columnName, dataTypeName, dataTypeArguments, isNullable);
    }
    //private static readonly System.Text.RegularExpressions.Regex m_regexParse = new System.Text.RegularExpressions.Regex(@"^\d*?(\""(?<ColumnName>[^\""]+)\""|\[(?<ColumnName>[^\]]+)\]|(?<ColumnName>\w+))\s*?(\""(?<DataTypeName>[^\""]+)\""|\[(?<DataTypeName>[^\]]+)\]|(?<DataTypeName>\w+))\s*?(\s*?\(\s*?(?<DataTypeArguments>[\w\s\,]+)?\s*?\)\s*?)?\s*?(?<Nullability>NOT\s+NULL|NULL)\s*?$");
    //public static SqlColumnDefinition Parse(string tsqlColumnDefinition)
    //{
    //  var match = m_regexParse.Match(tsqlColumnDefinition.NormalizeAll(' ', System.Char.IsWhiteSpace));

    //  if (match.Success && match.Groups[nameof(ColumnName)] is var g1 && g1.Success && match.Groups[nameof(DataTypeName)] is var g2 && g2.Success && match.Groups[nameof(DataTypeArguments)] is var g3 && match.Groups[nameof(Nullability)] is var g4 && g4.Success)
    //  {
    //    return new SqlColumnDefinition(g1.Value, g2.Value, g3.Value, g4.Value);
    //  }
    //  else throw new System.ArgumentOutOfRangeException(nameof(tsqlColumnDefinition));
    //}
    public static bool TryParse(string tsqlColumnDefinition, out TsqlColumnDefinition? result)
    {
      try
      {
        result = Parse(tsqlColumnDefinition);
        return true;
      }
      catch
      { }

      result = default;
      return false;
    }
    #endregion Static methods

    #region Object overrides
    public override string ToString() => ToString(false);
    #endregion Object overrides

    //public static void Validate(string columnName, string dataTypeName, int[] dataTypeArguments, bool isNullable)
    //{
    //  dataTypeName = dataTypeName.Unwrap('[', ']');

    //  if (string.IsNullOrWhiteSpace(dataTypeName) || !SqlType.GetTypeNames().Contains(dataTypeName))
    //  {
    //    throw new System.ArgumentOutOfRangeException($"Unrecognized data type name '{dataTypeName}'.");
    //  }

    //  dataTypeArguments = dataTypeArguments.Unwrap('(', ')');

    //  if (dataTypeArguments is null || (dataTypeArguments.Length > 0 && !System.Text.RegularExpressions.Regex.IsMatch(dataTypeArguments, @"^\s*(\d+(\s*,\s*\d+)?|MAX)\s*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)))
    //  {
    //    throw new System.ArgumentException($"Invalid data type arguments '{dataTypeArguments}'.", nameof(dataTypeArguments));
    //  }

    //  if (string.IsNullOrWhiteSpace(nullString) || !NullForms.Any((nf => nf.Equals(nullString))))
    //  {
    //    throw new System.ArgumentOutOfRangeException($"Invalid null string '{nullString}' (must be 'NULL' or 'NOT NULL').");
    //  }
    //}
  }
}