using System.Linq;

namespace Flux.Data
{
  /// <summary>SQL data type name functionality</summary>
  public struct TsqlDataType
    : System.IEquatable<TsqlDataType>
  {
    public static readonly TsqlDataType Empty;
    public bool IsEmpty => Equals(Empty);

#pragma warning disable CA1720 // Identifier contains type name
    #region SQL Data Type Names
    public const string Bigint = @"bigint";
    public const string Binary = @"binary";
    public const string Bit = @"bit";
    public const string Char = @"char";
    public const string Image = @"image";
    public const string Int = @"int";
    public const string Date = @"date";
    public const string Datetime = "datetime";
    public const string Datetime2 = @"datetime2";
    public const string Datetimeoffset = @"datetimeoffset";
    public const string Decimal = @"decimal";
    public const string Float = @"float";
    public const string Geography = @"geography";
    public const string Geometry = @"geometry";
    public const string Money = @"money";
    public const string Nchar = @"nchar";
    public const string Ntext = @"ntext";
    public const string Numeric = @"numeric"; // This is simply an alias for "decimal" in T-SQL.
    public const string Nvarchar = @"nvarchar";
    public const string Real = @"real";
    public const string Rowversion = @"rowversion";
    public const string Smalldatetime = @"smalldatetime";
    public const string Smallint = @"smallint";
    public const string Smallmoney = @"smallmoney";
    public const string SqlVariant = @"sql_variant";
    public const string Text = @"text";
    public const string Time = @"time";
    public const string Timestamp = @"timestamp";
    public const string Tinyint = @"tinyint";
    public const string Uniqueidentifier = @"uniqueidentifier";
    public const string Varbinary = @"varbinary";
    public const string Varchar = @"varchar";
    public const string Xml = @"xml";
    #endregion SQL Data Type Names
#pragma warning restore CA1720 // Identifier contains type name

    public string Name { get; private set; }
    public System.Collections.Generic.IReadOnlyList<string> Arguments { get; private set; }

    public string ToStringQuoted(bool ansi)
      => ansi ? Name.Wrap('"', '"') : Name.Wrap('[', ']');

    public TsqlDataType(string name, System.Collections.Generic.IEnumerable<string> arguments)
    {
      Name = name;
      Arguments = new System.Collections.Generic.List<string>(arguments);
    }
    public TsqlDataType(string name)
      : this(name, System.Linq.Enumerable.Empty<string>())
    {
    }

    #region Static members

    /// <summary>Returns the default argument of the specified dataTypeName.</summary>
    public static string GetDefaultArgument(string dataTypeName, bool beExplicit = false)
    {
      switch (dataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? string.Empty)
      {
        case Binary:
        case Char:
          return @"(8000)";
        case Datetime2 when beExplicit:
        case Datetimeoffset when beExplicit:
        case Time when beExplicit:
          return @"(7)";
        case Decimal:
        case Numeric:
          return @"(38,19)"; // There is no way of predicting the storage allocation, so the maximum and a scale of half. :)
        case Nchar:
          return @"(4000)";
        case Nvarchar:
        case Varbinary:
        case Varchar:
          return @"(MAX)";
        default:
          return string.Empty;
      }
    }

    /// <summary>Convert a sequence of data type arguments into its T-SQL string representation,</summary>
    public static string FromArguments(System.Collections.Generic.IEnumerable<string> dataTypeArguments)
      => string.Join(@",", dataTypeArguments) is var dta && dta.Length > 0 ? dta.Wrap('(', ')') : string.Empty;
    /// <summary>Convert a data type arguments string into a new sequence of argument values.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToArguments(string dataTypeArguments)
      => dataTypeArguments.ToStringBuilder().RemoveAll(char.IsWhiteSpace).Unwrap('(', ')').Split(System.StringSplitOptions.RemoveEmptyEntries, ',');

    private readonly static System.Text.RegularExpressions.Regex m_reParse = new System.Text.RegularExpressions.Regex(@"^\s*?(?<DataTypeName>\""[^\""]+\""|\[[^\]]+\]|\w+)\s*?(?<DataTypeArguments>\([\w\s\,]+\))?\s*?$");
    /// <summary></summary>
    public static TsqlDataType Parse(string expression)
    {
      var match = m_reParse.Match(expression);
      var dataTypeName = match.Groups[@"DataTypeName"].Value.TsqlUnenquote();
      var dataTypeArguments = ToArguments(match.Groups[@"DataTypeArguments"].Value);
      return new TsqlDataType(dataTypeName, dataTypeArguments);
    }
    /// <summary></summary>
    public static bool TryParse(string expression, out TsqlDataType result)
    {
      try
      {
        result = Parse(expression);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = default;
      return false;
    }

    /// <summary>Creates a new sequence of T-SQL data type names recognized in this class.</summary>
    public static System.Collections.Generic.IEnumerable<string> GetNames()
      => typeof(TsqlDataType).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy).Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(fi => fi.GetValue(null)).Cast<string>();

    /// <summary>Returns the T-SQL data type name corresponding to <paramref name="type"/>.</summary>
    public static string NameFromType(System.Type type)
    {
      if (type is null) throw new System.ArgumentNullException(nameof(type));
      else if (type == typeof(System.Boolean)) return Bit;
      else if (type == typeof(System.Byte)) return Tinyint;
      else if (type == typeof(System.Byte[])) return Varbinary;
      else if (type == typeof(System.DateTime)) return Datetime;
      else if (type == typeof(System.Decimal)) return Decimal;
      else if (type == typeof(System.Double)) return Float;
      else if (type == typeof(System.Guid)) return Uniqueidentifier;
      else if (type == typeof(System.Int16)) return Smallint;
      else if (type == typeof(System.Int32)) return Int;
      else if (type == typeof(System.Int64)) return Bigint;
      else if (type == typeof(System.SByte)) return Smallint; // Smallest type it can fit into.
      else if (type == typeof(System.Single)) return Real;
      else if (type == typeof(System.String)) return Nvarchar;
      else if (type == typeof(System.UInt16)) return Int; // Smallest type it can fit into.
      else if (type == typeof(System.UInt32)) return Bigint; // Smallest type it can fit into.
      else return SqlVariant; // Hybrid or variant type.
    }
    /// <summary>Returns the T-SQL data type name corresponding to <typeparamref name="T"/>.</summary>
    public static string NameFromTypeOfT<T>()
        => NameFromType(typeof(T));
    /// <summary>Returns the T-SQL data type name corresponding to the type of <paramref name="value"/>.</summary>
    public static string NameFromValueType(object? value) => value switch
    {
      System.Boolean _ => Bit,
      System.Byte _ => Tinyint,
      System.Byte[] _ => Varbinary,
      System.DateTime _ => Datetime,
      System.Decimal _ => Decimal,
      System.Double _ => Float,
      System.Guid _ => Uniqueidentifier,
      System.Int16 _ => Smallint,
      System.Int32 _ => Int,
      System.Int64 _ => Bigint,
      System.SByte _ => Tinyint,
      System.SByte[] _ => Varbinary,
      System.Single _ => Real,
      System.String _ => Nvarchar,
      System.UInt16 _ => Smallint,
      System.UInt32 _ => Int,
      System.UInt64 _ => Bigint,
      System.Object _ => SqlVariant,
      _ => throw new System.ArgumentOutOfRangeException(nameof(value))
    };

    /// <summary>Returns the <see cref="System.Data.DbType"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
    public static System.Data.DbType NameToDbType(string tsqlDataTypeName) => (tsqlDataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? throw new System.ArgumentNullException(nameof(tsqlDataTypeName))) switch
    {
      Bigint => System.Data.DbType.Int64,
      Binary => System.Data.DbType.Binary,
      Bit => System.Data.DbType.Boolean,
      Char => System.Data.DbType.AnsiStringFixedLength,
      Date => System.Data.DbType.Date,
      Datetime => System.Data.DbType.DateTime,
      Datetime2 => System.Data.DbType.DateTime2,
      Datetimeoffset => System.Data.DbType.DateTimeOffset,
      Decimal => System.Data.DbType.Decimal,
      Float => System.Data.DbType.Double,
      Image => System.Data.DbType.Binary,
      Int => System.Data.DbType.Int32,
      Money => System.Data.DbType.Decimal,
      Nchar => System.Data.DbType.StringFixedLength,
      Ntext => System.Data.DbType.String,
      Numeric => System.Data.DbType.Decimal,
      Nvarchar => System.Data.DbType.String,
      Real => System.Data.DbType.Single,
      Rowversion => System.Data.DbType.Binary,
      Smalldatetime => System.Data.DbType.DateTime,
      Smallint => System.Data.DbType.Int16,
      Smallmoney => System.Data.DbType.Decimal,
      SqlVariant => System.Data.DbType.Object,
      Text => System.Data.DbType.AnsiString,
      Time => System.Data.DbType.Time,
      Timestamp => System.Data.DbType.Binary,
      Tinyint => System.Data.DbType.Byte,
      Uniqueidentifier => System.Data.DbType.Guid,
      Varbinary => System.Data.DbType.Binary,
      Varchar => System.Data.DbType.AnsiString,
      Xml => System.Data.DbType.Xml,
      _ => throw new System.ArgumentOutOfRangeException(nameof(tsqlDataTypeName))
    };
    /// <summary>Returns the <see cref="System.Data.SqlTypes"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
    public static System.Type NameToSqlType(string tsqlDataTypeName) => (tsqlDataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? throw new System.ArgumentNullException(nameof(tsqlDataTypeName))) switch
    {
      Bigint => typeof(System.Data.SqlTypes.SqlInt64),
      Binary => typeof(System.Data.SqlTypes.SqlBinary),
      Bit => typeof(System.Data.SqlTypes.SqlBoolean),
      Char => typeof(System.Data.SqlTypes.SqlChars),
      Date => typeof(System.Data.SqlTypes.SqlDateTime),
      Datetime => typeof(System.Data.SqlTypes.SqlDateTime),
      Datetime2 => typeof(System.Data.SqlTypes.SqlDateTime), // Closest fit of the SqlTypes.
      Datetimeoffset => typeof(System.DateTimeOffset),
      Decimal => typeof(System.Data.SqlTypes.SqlDecimal),
      Float => typeof(System.Data.SqlTypes.SqlDouble),
      Image => typeof(System.Data.SqlTypes.SqlBinary),
      Int => typeof(System.Data.SqlTypes.SqlInt32),
      Money => typeof(System.Data.SqlTypes.SqlMoney),
      Nchar => typeof(System.Data.SqlTypes.SqlChars),
      Ntext => typeof(System.Data.SqlTypes.SqlChars),
      Numeric => typeof(System.Data.SqlTypes.SqlDecimal),
      Nvarchar => typeof(System.Data.SqlTypes.SqlChars),
      Real => typeof(System.Data.SqlTypes.SqlSingle),
      Rowversion => typeof(System.Data.SqlTypes.SqlBinary),
      Smalldatetime => typeof(System.Data.SqlTypes.SqlDateTime),
      Smallint => typeof(System.Data.SqlTypes.SqlInt16),
      Smallmoney => typeof(System.Data.SqlTypes.SqlMoney),
      Text => typeof(System.Data.SqlTypes.SqlChars),
      Time => typeof(System.Data.SqlTypes.SqlDateTime),
      Timestamp => typeof(System.Data.SqlTypes.SqlBinary),
      Tinyint => typeof(System.Data.SqlTypes.SqlByte),
      Uniqueidentifier => typeof(System.Data.SqlTypes.SqlGuid),
      Varbinary => typeof(System.Data.SqlTypes.SqlBinary),
      Varchar => typeof(System.Data.SqlTypes.SqlChars),
      Xml => typeof(System.Data.SqlTypes.SqlXml),
      SqlVariant => typeof(System.Object),
      _ => throw new System.ArgumentOutOfRangeException(nameof(tsqlDataTypeName))
    };
    /// <summary>Returns the <see cref="System.Type"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
    public static System.Type NameToType(string tsqlDataTypeName) => (tsqlDataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? throw new System.ArgumentNullException(nameof(tsqlDataTypeName))) switch
    {
      Bigint => typeof(System.Int64),
      Binary => typeof(System.Byte[]),
      Bit => typeof(System.Boolean),
      Char => typeof(System.String),
      Date => typeof(System.DateTime),
      Datetime => typeof(System.DateTime),
      Datetime2 => typeof(System.DateTime),
      Datetimeoffset => typeof(System.DateTimeOffset),
      Decimal => typeof(System.Decimal),
      Float => typeof(System.Double),
      Image => typeof(System.Byte[]),
      Int => typeof(System.Int32),
      Money => typeof(System.Decimal),
      Nchar => typeof(System.String),
      Ntext => typeof(System.String),
      Numeric => typeof(System.Decimal),
      Nvarchar => typeof(System.String),
      Real => typeof(System.Single),
      Rowversion => typeof(System.Byte[]),
      Smalldatetime => typeof(System.DateTime),
      Smallint => typeof(System.Int16),
      Smallmoney => typeof(System.Decimal),
      SqlVariant => typeof(System.Object),
      Text => typeof(System.String),
      Time => typeof(System.TimeSpan),
      Timestamp => typeof(System.Byte[]),
      Tinyint => typeof(System.Byte),
      Uniqueidentifier => typeof(System.Guid),
      Varbinary => typeof(System.Byte[]),
      Varchar => typeof(System.String),
      Xml => typeof(System.String),
      _ => throw new System.ArgumentOutOfRangeException(nameof(tsqlDataTypeName))
    };

    #endregion Static members

    // Operators
    public static bool operator ==(TsqlDataType left, TsqlDataType right)
      => left.Equals(right);
    public static bool operator !=(TsqlDataType left, TsqlDataType right)
      => !left.Equals(right);
    // System.IEquatable<SqlDefinitionNullability>
    public bool Equals(TsqlDataType other)
      => Name == other.Name && Arguments.SequenceEqual(other.Arguments);
    // System.Object Overrides
    public override bool Equals(object? obj)
      => obj is TsqlDataType o && Equals(o);
    public override int GetHashCode()
      => Name.GetHashCode(System.StringComparison.Ordinal);
    public override string ToString()
      => Name;
  }

  /// <summary>SQL data type name functionality</summary>
  //public struct TsqlDataTypeName
  //  : System.IEquatable<TsqlDataTypeName>
  //{
  //  #region SQL Data Type Names
  //  public const string Bigint = @"bigint";
  //  public const string Binary = @"binary";
  //  public const string Bit = @"bit";
  //  public const string Char = @"char";
  //  public const string Image = @"image";
  //  public const string Int = @"int";
  //  public const string Date = @"date";
  //  public const string Datetime = "datetime";
  //  public const string Datetime2 = @"datetime2";
  //  public const string Datetimeoffset = @"datetimeoffset";
  //  public const string Decimal = @"decimal";
  //  public const string Float = @"float";
  //  public const string Geography = @"geography";
  //  public const string Geometry = @"geometry";
  //  public const string Money = @"money";
  //  public const string Nchar = @"nchar";
  //  public const string Ntext = @"ntext";
  //  public const string Numeric = @"numeric"; // This is simply an alias for "decimal" in T-SQL.
  //  public const string Nvarchar = @"nvarchar";
  //  public const string Real = @"real";
  //  public const string Rowversion = @"rowversion";
  //  public const string Smalldatetime = @"smalldatetime";
  //  public const string Smallint = @"smallint";
  //  public const string Smallmoney = @"smallmoney";
  //  public const string SqlVariant = @"sql_variant";
  //  public const string Text = @"text";
  //  public const string Time = @"time";
  //  public const string Timestamp = @"timestamp";
  //  public const string Tinyint = @"tinyint";
  //  public const string Uniqueidentifier = @"uniqueidentifier";
  //  public const string Varbinary = @"varbinary";
  //  public const string Varchar = @"varchar";
  //  public const string Xml = @"xml";
  //  #endregion SQL Data Type Names

  //  public string Value { get; private set; }

  //  public string ToStringQuoted(bool ansi)
  //    => ansi ? Value.Wrap('"', '"') : Value.Wrap('[', ']');

  //  public TsqlDataTypeName(string value)
  //    => Value = value;

  //  // System.IEquatable<SqlDefinitionNullability>
  //  public bool Equals(TsqlDataTypeName other)
  //    => Value == other.Value;
  //  // System.Object Overrides
  //  public override bool Equals(object obj)
  //    => obj is TsqlDataTypeName ? this.Equals((TsqlDataTypeName)obj) : false;
  //  public override int GetHashCode()
  //    => Value.GetHashCode();
  //  public override string ToString()
  //    => Value;
  //  // Operators
  //  public static bool operator ==(TsqlDataTypeName left, TsqlDataTypeName right)
  //    => left.Equals(right);
  //  public static bool operator !=(TsqlDataTypeName left, TsqlDataTypeName right)
  //    => !left.Equals(right);

  //  public static TsqlDataTypeName Parse(string expression)
  //    => new TsqlDataTypeName(expression.Trim().UnquoteTsql());
  //  public static bool TryParse(string expression, out TsqlDataTypeName result)
  //  {
  //    try
  //    {
  //      result = Parse(expression);
  //      return true;
  //    }
  //    catch { }

  //    result = default;
  //    return false;
  //  }

  //  /// <summary>Creates a new sequence of T-SQL data type names recognized in this class.</summary>
  //  public static System.Collections.Generic.IEnumerable<string> GetDataTypeNames()
  //    => typeof(TsqlDataTypeName).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy).Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(fi => fi.GetValue(null)).Cast<string>();

  //  /// <summary>Returns the T-SQL data type name corresponding to <typeparamref name="T"/>.</summary>
  //  public static string From<T>()
  //  {
  //    if (typeof(T) == typeof(System.Boolean)) return Bit;
  //    if (typeof(T) == typeof(System.Byte)) return Tinyint;
  //    if (typeof(T) == typeof(System.Byte[])) return Varbinary;
  //    if (typeof(T) == typeof(System.DateTime)) return Datetime;
  //    if (typeof(T) == typeof(System.DateTimeOffset)) return Datetimeoffset;
  //    if (typeof(T) == typeof(System.Decimal)) return Decimal;
  //    if (typeof(T) == typeof(System.Double)) return Float;
  //    if (typeof(T) == typeof(System.Guid)) return Uniqueidentifier;
  //    if (typeof(T) == typeof(System.Int16)) return Smallint;
  //    if (typeof(T) == typeof(System.Int32)) return Int;
  //    if (typeof(T) == typeof(System.Int64)) return Bigint;
  //    if (typeof(T) == typeof(System.String)) return Nvarchar;
  //    if (typeof(T) == typeof(System.SByte)) return Tinyint;
  //    if (typeof(T) == typeof(System.Single)) return Real;
  //    if (typeof(T) == typeof(System.Xml.Linq.XNode)) return Xml;
  //    if (typeof(T) == typeof(System.Xml.XmlNode)) return Xml;
  //    if (typeof(T) == typeof(System.Object)) return SqlVariant; // Convert any type.
  //    throw new System.ArgumentOutOfRangeException(typeof(T).FullName);
  //  }
  //  /// <summary>Returns the T-SQL data type name corresponding to <typeparamref name="instance"/>.</summary>
  //  public static string From(object instance) => instance switch
  //  {
  //    System.Boolean _ => Bit,
  //    System.Byte _ => Smallint,
  //    System.Byte[] _ => Varbinary,
  //    System.DateTime _ => Datetime,
  //    System.DateTimeOffset _ => Datetimeoffset,
  //    System.Decimal _ => Decimal,
  //    System.Double _ => Float,
  //    System.Guid _ => Uniqueidentifier,
  //    System.Int16 _ => Smallint,
  //    System.Int32 _ => Int,
  //    System.Int64 _ => Bigint,
  //    System.SByte _ => Tinyint,
  //    System.SByte[] _ => Varbinary,
  //    System.Single _ => Real,
  //    System.String _ => Nvarchar,
  //    System.UInt16 _ => Smallint,
  //    System.UInt32 _ => Int,
  //    System.UInt64 _ => Bigint,
  //    System.Xml.Linq.XNode _ => Xml,
  //    System.Xml.XmlNode _ => Xml,
  //    System.Object _ => SqlVariant,
  //    _ => throw new System.ArgumentOutOfRangeException(nameof(instance)),
  //  };

  //  /// <summary>Returns the <see cref="System.Data.DbType"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
  //  public static System.Data.DbType ToDbType(string tsqlDataTypeName) => (tsqlDataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? throw new System.ArgumentNullException(nameof(tsqlDataTypeName))) switch
  //  {
  //    Bigint => System.Data.DbType.Int64,
  //    Binary => System.Data.DbType.Binary,
  //    Bit => System.Data.DbType.Boolean,
  //    Char => System.Data.DbType.AnsiStringFixedLength,
  //    Date => System.Data.DbType.Date,
  //    Datetime => System.Data.DbType.DateTime,
  //    Datetime2 => System.Data.DbType.DateTime2,
  //    Datetimeoffset => System.Data.DbType.DateTimeOffset,
  //    Decimal => System.Data.DbType.Decimal,
  //    Float => System.Data.DbType.Double,
  //    Image => System.Data.DbType.Binary,
  //    Int => System.Data.DbType.Int32,
  //    Money => System.Data.DbType.Decimal,
  //    Nchar => System.Data.DbType.StringFixedLength,
  //    Ntext => System.Data.DbType.String,
  //    Numeric => System.Data.DbType.Decimal,
  //    Nvarchar => System.Data.DbType.String,
  //    Real => System.Data.DbType.Single,
  //    Rowversion => System.Data.DbType.Binary,
  //    Smalldatetime => System.Data.DbType.DateTime,
  //    Smallint => System.Data.DbType.Int16,
  //    Smallmoney => System.Data.DbType.Decimal,
  //    SqlVariant => System.Data.DbType.Object,
  //    Text => System.Data.DbType.AnsiString,
  //    Time => System.Data.DbType.Time,
  //    Timestamp => System.Data.DbType.Binary,
  //    Tinyint => System.Data.DbType.Byte,
  //    Uniqueidentifier => System.Data.DbType.Guid,
  //    Varbinary => System.Data.DbType.Binary,
  //    Varchar => System.Data.DbType.AnsiString,
  //    Xml => System.Data.DbType.Xml,
  //    _ => throw new System.ArgumentOutOfRangeException(nameof(tsqlDataTypeName)),
  //  };
  //  /// <summary>Returns the <see cref="System.Data.SqlTypes"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
  //  public static System.Type ToSqlType(string tsqlDataTypeName) => (tsqlDataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? throw new System.ArgumentNullException(nameof(tsqlDataTypeName))) switch
  //  {
  //    Bigint => typeof(System.Data.SqlTypes.SqlInt64),
  //    Binary => typeof(System.Data.SqlTypes.SqlBinary),
  //    Bit => typeof(System.Data.SqlTypes.SqlBoolean),
  //    Char => typeof(System.Data.SqlTypes.SqlChars),
  //    Date => typeof(System.Data.SqlTypes.SqlDateTime),
  //    Datetime => typeof(System.Data.SqlTypes.SqlDateTime),
  //    Datetime2 => typeof(System.DateTime),
  //    Datetimeoffset => typeof(System.DateTimeOffset),
  //    Decimal => typeof(System.Data.SqlTypes.SqlDecimal),
  //    Float => typeof(System.Data.SqlTypes.SqlDouble),
  //    Image => typeof(System.Data.SqlTypes.SqlBinary),
  //    Int => typeof(System.Data.SqlTypes.SqlInt32),
  //    Money => typeof(System.Data.SqlTypes.SqlMoney),
  //    Nchar => typeof(System.Data.SqlTypes.SqlChars),
  //    Ntext => typeof(System.Data.SqlTypes.SqlChars),
  //    Numeric => typeof(System.Data.SqlTypes.SqlDecimal),
  //    Nvarchar => typeof(System.Data.SqlTypes.SqlChars),
  //    Real => typeof(System.Data.SqlTypes.SqlSingle),
  //    Rowversion => typeof(System.Data.SqlTypes.SqlBinary),
  //    Smalldatetime => typeof(System.Data.SqlTypes.SqlDateTime),
  //    Smallint => typeof(System.Data.SqlTypes.SqlInt16),
  //    Smallmoney => typeof(System.Data.SqlTypes.SqlMoney),
  //    Text => typeof(System.Data.SqlTypes.SqlChars),
  //    Time => typeof(System.TimeSpan),
  //    Timestamp => typeof(System.Data.SqlTypes.SqlBinary),
  //    Tinyint => typeof(System.Data.SqlTypes.SqlByte),
  //    Uniqueidentifier => typeof(System.Data.SqlTypes.SqlGuid),
  //    Varbinary => typeof(System.Data.SqlTypes.SqlBinary),
  //    Varchar => typeof(System.Data.SqlTypes.SqlChars),
  //    Xml => typeof(System.Data.SqlTypes.SqlXml),
  //    SqlVariant => typeof(System.Object),
  //    _ => throw new System.ArgumentOutOfRangeException(nameof(tsqlDataTypeName)),
  //  };
  //  /// <summary>Returns the <see cref="System.Type"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
  //  public static System.Type ToType(string tsqlDataTypeName) => (tsqlDataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? throw new System.ArgumentNullException(nameof(tsqlDataTypeName))) switch
  //  {
  //    Bigint => typeof(System.Int64),
  //    Binary => typeof(System.Byte[]),
  //    Bit => typeof(System.Boolean),
  //    Char => typeof(System.String),
  //    Date => typeof(System.DateTime),
  //    Datetime => typeof(System.DateTime),
  //    Datetime2 => typeof(System.DateTime),
  //    Datetimeoffset => typeof(System.DateTimeOffset),
  //    Decimal => typeof(System.Decimal),
  //    Float => typeof(System.Double),
  //    Image => typeof(System.Byte[]),
  //    Int => typeof(System.Int32),
  //    Money => typeof(System.Decimal),
  //    Nchar => typeof(System.String),
  //    Ntext => typeof(System.String),
  //    Numeric => typeof(System.Decimal),
  //    Nvarchar => typeof(System.String),
  //    Real => typeof(System.Single),
  //    Rowversion => typeof(System.Byte[]),
  //    Smalldatetime => typeof(System.DateTime),
  //    Smallint => typeof(System.Int16),
  //    Smallmoney => typeof(System.Decimal),
  //    SqlVariant => typeof(System.Object),
  //    Text => typeof(System.String),
  //    Time => typeof(System.TimeSpan),
  //    Timestamp => typeof(System.Byte[]),
  //    Tinyint => typeof(System.Byte),
  //    Uniqueidentifier => typeof(System.Guid),
  //    Varbinary => typeof(System.Byte[]),
  //    Varchar => typeof(System.String),
  //    Xml => typeof(System.String),
  //    _ => throw new System.ArgumentOutOfRangeException(nameof(tsqlDataTypeName)),
  //  };
  //}
}
