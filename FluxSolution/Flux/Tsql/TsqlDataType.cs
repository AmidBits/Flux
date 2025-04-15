namespace Flux.Data
{
  /// <summary>SQL data type name functionality</summary>
  public record struct TsqlDataType
  {
    #region DataType Name Constants
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
    #endregion DataType Name Constants

    public string Name { get; private set; }
    public System.Collections.Generic.IReadOnlyList<string> Arguments { get; private set; }

    public TsqlDataType(string name, System.Collections.Generic.IEnumerable<string> arguments)
    {
      Name = name;
      Arguments = new System.Collections.Generic.List<string>(arguments);
    }
    public TsqlDataType(string name)
      : this(name, System.Linq.Enumerable.Empty<string>())
    { }

    public readonly string ToStringQuoted(bool ansi)
      => ansi ? Name.Wrap('"', '"') : Name.Wrap('[', ']');

    #region Static members

    public static bool IsDataTypeName(string text)
      => Reflections.GetFieldInfos(typeof(TsqlDataType)).Where(fi => fi.IsConstant()).Select(fi => fi.GetValue(null)).Where(v => v is string).Contains(text);

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
      => new System.Text.StringBuilder(dataTypeArguments).RemoveAll(char.IsWhiteSpace).Unwrap('(', ')').Split(System.StringSplitOptions.RemoveEmptyEntries, ',');

    private static readonly System.Text.RegularExpressions.Regex m_reParse = new(@"^\s*?(?<DataTypeName>\""[^\""]+\""|\[[^\]]+\]|\w+)\s*?(?<DataTypeArguments>\([\w\s\,]+\))?\s*?$");
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
      catch { }

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
    public static System.Data.DbType NameToDbType(string tsqlDataTypeName)
      => (tsqlDataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? string.Empty) switch
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
    /// <summary>Returns the <see cref="System.Data.SqlDbType"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
    public static System.Data.SqlDbType NameToSqlDbType(string tsqlDataTypeName)
      => (tsqlDataTypeName?.ToLower(System.Globalization.CultureInfo.CurrentCulture) ?? string.Empty) switch
      {
        Bigint => System.Data.SqlDbType.BigInt,
        Binary => System.Data.SqlDbType.Binary,
        Bit => System.Data.SqlDbType.Bit,
        Char => System.Data.SqlDbType.Char,
        Date => System.Data.SqlDbType.Date,
        Datetime => System.Data.SqlDbType.DateTime,
        Datetime2 => System.Data.SqlDbType.DateTime2,
        Datetimeoffset => System.Data.SqlDbType.DateTimeOffset,
        Decimal => System.Data.SqlDbType.Decimal,
        Float => System.Data.SqlDbType.Float,
        Image => System.Data.SqlDbType.Binary,
        Int => System.Data.SqlDbType.Int,
        Money => System.Data.SqlDbType.Decimal,
        Nchar => System.Data.SqlDbType.NChar,
        Ntext => System.Data.SqlDbType.NText,
        Numeric => System.Data.SqlDbType.Decimal,
        Nvarchar => System.Data.SqlDbType.NVarChar,
        Real => System.Data.SqlDbType.Real,
        Rowversion => System.Data.SqlDbType.Binary,
        Smalldatetime => System.Data.SqlDbType.DateTime,
        Smallint => System.Data.SqlDbType.SmallInt,
        Smallmoney => System.Data.SqlDbType.Decimal,
        SqlVariant => System.Data.SqlDbType.Variant,
        Text => System.Data.SqlDbType.Text,
        Time => System.Data.SqlDbType.Time,
        Timestamp => System.Data.SqlDbType.Binary,
        Tinyint => System.Data.SqlDbType.TinyInt,
        Uniqueidentifier => System.Data.SqlDbType.UniqueIdentifier,
        Varbinary => System.Data.SqlDbType.Binary,
        Varchar => System.Data.SqlDbType.VarChar,
        Xml => System.Data.SqlDbType.Xml,
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

    #endregion // Static members

    public override readonly string ToString() => $"{GetType().Name} {{ Name = {Name} }}";
  }

#if TSQLDATATYPE

  public enum TsqlDataTypeId
  {
    Bigint = System.Data.DbType.Int64,
    Binary = System.Data.DbType.Binary,
    Bit = System.Data.DbType.Boolean,
    Char = System.Data.DbType.AnsiStringFixedLength,
    Date = System.Data.DbType.Date,
    Datetime = System.Data.DbType.DateTime,
    Datetime2 = System.Data.DbType.DateTime2,
    Datetimeoffset = System.Data.DbType.DateTimeOffset,
    Decimal = System.Data.DbType.Decimal,
    Float = System.Data.DbType.Double,
    Geography = -1,
    Geometry = -2,
    Image = System.Data.DbType.Binary,
    Int = System.Data.DbType.Int32,
    Money = System.Data.DbType.Decimal,
    Nchar = System.Data.DbType.StringFixedLength,
    Ntext = System.Data.DbType.String,
    Numeric = System.Data.DbType.Decimal,
    Nvarchar = System.Data.DbType.String,
    Real = System.Data.DbType.Single,
    Rowversion = System.Data.DbType.Binary,
    Smalldatetime = System.Data.DbType.DateTime,
    Smallint = System.Data.DbType.Int16,
    Smallmoney = System.Data.DbType.Decimal,
    SqlVariant = System.Data.DbType.Object,
    Text = System.Data.DbType.AnsiString,
    Time = System.Data.DbType.Time,
    Timestamp = System.Data.DbType.Binary,
    Tinyint = System.Data.DbType.Byte,
    Uniqueidentifier = System.Data.DbType.Guid,
    Varbinary = System.Data.DbType.Binary,
    Varchar = System.Data.DbType.AnsiString,
    Xml = System.Data.DbType.Xml,
  }

  /// <summary>SQL data type name functionality</summary>
  public static class TsqlDataTypes
  {
    public static TsqlDataTypeId ParseTsqlDataTypeName(this System.ReadOnlySpan<char> tsqlDataTypeName)
      => System.Enum.Parse<TsqlDataTypeId>(tsqlDataTypeName, true);

    public static bool TryParseTsqlDataTypeName(string tsqlDataTypeName, out TsqlDataTypeId result)
    {
      try
      {
        result = ParseTsqlDataTypeName(tsqlDataTypeName);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    public static System.Data.SqlDbType ConvertDbTypeToSqlDbType(System.Data.DbType dbType)
      => dbType switch
      {
        System.Data.DbType.AnsiString => System.Data.SqlDbType.VarChar,
        System.Data.DbType.Binary => System.Data.SqlDbType.VarBinary,
        System.Data.DbType.Byte => System.Data.SqlDbType.TinyInt,
        System.Data.DbType.Boolean => System.Data.SqlDbType.Bit,
        System.Data.DbType.Currency => System.Data.SqlDbType.Money,
        System.Data.DbType.Date => System.Data.SqlDbType.Date,
        System.Data.DbType.DateTime => System.Data.SqlDbType.DateTime,
        System.Data.DbType.Decimal => System.Data.SqlDbType.Decimal,
        System.Data.DbType.Double => System.Data.SqlDbType.Float,
        System.Data.DbType.Guid => System.Data.SqlDbType.UniqueIdentifier,
        System.Data.DbType.Int16 => System.Data.SqlDbType.SmallInt,
        System.Data.DbType.Int32 => System.Data.SqlDbType.Int,
        System.Data.DbType.Int64 => System.Data.SqlDbType.BigInt,
        System.Data.DbType.Object => System.Data.SqlDbType.Variant,
        System.Data.DbType.SByte => System.Data.SqlDbType.TinyInt,
        System.Data.DbType.Single => System.Data.SqlDbType.Real,
        System.Data.DbType.String => System.Data.SqlDbType.NVarChar,
        System.Data.DbType.Time => System.Data.SqlDbType.Time,
        System.Data.DbType.UInt16 => System.Data.SqlDbType.SmallInt,
        System.Data.DbType.UInt32 => System.Data.SqlDbType.Int,
        System.Data.DbType.UInt64 => System.Data.SqlDbType.BigInt,
        System.Data.DbType.VarNumeric => throw new NotImplementedException(),
        System.Data.DbType.AnsiStringFixedLength => System.Data.SqlDbType.Char,
        System.Data.DbType.StringFixedLength => System.Data.SqlDbType.NChar,
        System.Data.DbType.Xml => System.Data.SqlDbType.Xml,
        System.Data.DbType.DateTime2 => System.Data.SqlDbType.DateTime2,
        System.Data.DbType.DateTimeOffset => System.Data.SqlDbType.DateTimeOffset,
        _ => throw new NotImplementedException(),
      };

    public static System.Type ConvertDbTypeToSqlType(System.Data.DbType dbType)
      => dbType switch
      {
        System.Data.DbType.AnsiString => typeof(System.Data.SqlTypes.SqlString),
        System.Data.DbType.Binary => typeof(System.Data.SqlTypes.SqlBinary),
        System.Data.DbType.Byte => typeof(System.Data.SqlTypes.SqlByte),
        System.Data.DbType.Boolean => typeof(System.Data.SqlTypes.SqlBoolean),
        System.Data.DbType.Currency => typeof(System.Data.SqlTypes.SqlMoney),
        System.Data.DbType.Date => typeof(System.Data.SqlTypes.SqlDateTime),
        System.Data.DbType.DateTime => typeof(System.Data.SqlTypes.SqlDateTime),
        System.Data.DbType.Decimal => typeof(System.Data.SqlTypes.SqlDecimal),
        System.Data.DbType.Double => typeof(System.Data.SqlTypes.SqlDouble),
        System.Data.DbType.Guid => typeof(System.Data.SqlTypes.SqlGuid),
        System.Data.DbType.Int16 => typeof(System.Data.SqlTypes.SqlInt16),
        System.Data.DbType.Int32 => typeof(System.Data.SqlTypes.SqlInt32),
        System.Data.DbType.Int64 => typeof(System.Data.SqlTypes.SqlInt64),
        System.Data.DbType.Object => throw new NotImplementedException(),
        System.Data.DbType.SByte => throw new NotImplementedException(),
        System.Data.DbType.Single => throw new NotImplementedException(),
        System.Data.DbType.String => throw new NotImplementedException(),
        System.Data.DbType.Time => typeof(System.Data.SqlTypes.SqlDateTime),
        System.Data.DbType.UInt16 => typeof(System.Data.SqlTypes.SqlInt16),
        System.Data.DbType.UInt32 => typeof(System.Data.SqlTypes.SqlInt32),
        System.Data.DbType.UInt64 => typeof(System.Data.SqlTypes.SqlInt64),
        System.Data.DbType.VarNumeric => throw new NotImplementedException(),
        System.Data.DbType.AnsiStringFixedLength => typeof(System.Data.SqlTypes.SqlChars),
        System.Data.DbType.StringFixedLength => typeof(System.Data.SqlTypes.SqlChars),
        System.Data.DbType.Xml => typeof(System.Data.SqlTypes.SqlXml),
        System.Data.DbType.DateTime2 => typeof(System.Data.SqlTypes.SqlDateTime),
        System.Data.DbType.DateTimeOffset => typeof(System.Data.SqlTypes.SqlDateTime),
        _ => throw new NotImplementedException(),
      };

    public static System.Type ConvertDbTypeToSystemType(System.Data.DbType dbType)
      => dbType switch
      {
        System.Data.DbType.AnsiString => typeof(System.String),
        System.Data.DbType.Binary => typeof(System.Byte[]),
        System.Data.DbType.Byte => typeof(System.Byte),
        System.Data.DbType.Boolean => typeof(System.Boolean),
        System.Data.DbType.Currency => typeof(System.Decimal),
        System.Data.DbType.Date => typeof(System.DateOnly),
        System.Data.DbType.DateTime => typeof(System.DateTime),
        System.Data.DbType.Decimal => typeof(System.Decimal),
        System.Data.DbType.Double => typeof(System.Double),
        System.Data.DbType.Guid => typeof(System.Guid),
        System.Data.DbType.Int16 => typeof(System.Int16),
        System.Data.DbType.Int32 => typeof(System.Int32),
        System.Data.DbType.Int64 => typeof(System.Int64),
        System.Data.DbType.Object => typeof(System.Object),
        System.Data.DbType.SByte => typeof(System.SByte),
        System.Data.DbType.Single => typeof(System.Single),
        System.Data.DbType.String => typeof(System.String),
        System.Data.DbType.Time => typeof(System.TimeOnly),
        System.Data.DbType.UInt16 => typeof(System.UInt16),
        System.Data.DbType.UInt32 => typeof(System.UInt32),
        System.Data.DbType.UInt64 => typeof(System.UInt64),
        System.Data.DbType.VarNumeric => throw new NotImplementedException(),
        System.Data.DbType.AnsiStringFixedLength => typeof(System.String),
        System.Data.DbType.StringFixedLength => typeof(System.String),
        System.Data.DbType.Xml => typeof(System.Xml.Linq.XDocument),
        System.Data.DbType.DateTime2 => typeof(System.DateTime),
        System.Data.DbType.DateTimeOffset => typeof(System.DateTimeOffset),
        _ => throw new NotImplementedException(),
      };

    public static TsqlDataTypeId ConvertDbTypeToTsqlDataType(System.Data.DbType dbType) => (TsqlDataTypeId)dbType;

    public static System.Type ConvertSqlDbTypeToSystemType(System.Data.SqlDbType sqlDbType)
      => sqlDbType switch
      {
        System.Data.SqlDbType.BigInt => typeof(System.Int64),
        System.Data.SqlDbType.Binary => typeof(System.Byte[]),
        System.Data.SqlDbType.Bit => typeof(System.Boolean),
        System.Data.SqlDbType.Char => typeof(System.String),
        System.Data.SqlDbType.DateTime => typeof(System.DateTime),
        System.Data.SqlDbType.Decimal => typeof(System.Decimal),
        System.Data.SqlDbType.Float => typeof(System.Double),
        System.Data.SqlDbType.Image => typeof(System.Byte[]),
        System.Data.SqlDbType.Int => typeof(System.Int32),
        System.Data.SqlDbType.Money => typeof(System.Decimal),
        System.Data.SqlDbType.NChar => typeof(System.String),
        System.Data.SqlDbType.NText => typeof(System.String),
        System.Data.SqlDbType.NVarChar => typeof(System.String),
        System.Data.SqlDbType.Real => typeof(System.Single),
        System.Data.SqlDbType.UniqueIdentifier => typeof(System.Guid),
        System.Data.SqlDbType.SmallDateTime => typeof(System.DateTime),
        System.Data.SqlDbType.SmallInt => typeof(System.Int16),
        System.Data.SqlDbType.SmallMoney => typeof(System.Decimal),
        System.Data.SqlDbType.Text => typeof(System.String),
        System.Data.SqlDbType.Timestamp => typeof(System.Byte[]),
        System.Data.SqlDbType.TinyInt => typeof(System.Byte),
        System.Data.SqlDbType.VarBinary => typeof(System.Byte[]),
        System.Data.SqlDbType.VarChar => typeof(System.String),
        System.Data.SqlDbType.Variant => typeof(System.Object),
        System.Data.SqlDbType.Xml => typeof(System.Xml.Linq.XDocument),
        System.Data.SqlDbType.Udt => throw new NotImplementedException(),
        System.Data.SqlDbType.Structured => throw new NotImplementedException(),
        System.Data.SqlDbType.Date => typeof(System.DateOnly),
        System.Data.SqlDbType.Time => typeof(System.TimeOnly),
        System.Data.SqlDbType.DateTime2 => typeof(System.DateTime),
        System.Data.SqlDbType.DateTimeOffset => typeof(System.DateTimeOffset),
        System.Data.SqlDbType.Json => throw new NotImplementedException(),
        _ => throw new NotImplementedException(),
      };

    public static System.Data.DbType ConvertSqlTypeToDbType(System.Type sqlType)
      => (sqlType == typeof(System.Data.SqlTypes.SqlBinary))
      ? System.Data.DbType.Binary
      : (sqlType == typeof(System.Data.SqlTypes.SqlBoolean))
      ? System.Data.DbType.Boolean
      : (sqlType == typeof(System.Data.SqlTypes.SqlByte))
      ? System.Data.DbType.Byte
      : (sqlType == typeof(System.Data.SqlTypes.SqlChars))
      ? System.Data.DbType.StringFixedLength
      : (sqlType == typeof(System.Data.SqlTypes.SqlBytes))
      ? System.Data.DbType.Binary
      : (sqlType == typeof(System.Data.SqlTypes.SqlDateTime))
      ? System.Data.DbType.DateTime
      : (sqlType == typeof(System.Data.SqlTypes.SqlDecimal))
      ? System.Data.DbType.Decimal
      : (sqlType == typeof(System.Data.SqlTypes.SqlDouble))
      ? System.Data.DbType.Double
      : (sqlType == typeof(System.Data.SqlTypes.SqlGuid))
      ? System.Data.DbType.Guid
      : (sqlType == typeof(System.Data.SqlTypes.SqlInt16))
      ? System.Data.DbType.Int16
      : (sqlType == typeof(System.Data.SqlTypes.SqlInt32))
      ? System.Data.DbType.Int32
      : (sqlType == typeof(System.Data.SqlTypes.SqlInt64))
      ? System.Data.DbType.Int64
      : (sqlType == typeof(System.Data.SqlTypes.SqlMoney))
      ? System.Data.DbType.Currency
      : (sqlType == typeof(System.Data.SqlTypes.SqlSingle))
      ? System.Data.DbType.Single
      : (sqlType == typeof(System.Data.SqlTypes.SqlString))
      ? System.Data.DbType.String
      : (sqlType == typeof(System.Data.SqlTypes.SqlXml))
      ? System.Data.DbType.Xml
      : throw new System.NotImplementedException();

    public static System.Type ConvertSqlTypeToSystemType(System.Type sqlType)
      => (sqlType == typeof(System.Data.SqlTypes.SqlBinary))
      ? typeof(System.Byte[])
      : (sqlType == typeof(System.Data.SqlTypes.SqlBoolean))
      ? typeof(System.Boolean)
      : (sqlType == typeof(System.Data.SqlTypes.SqlByte))
      ? typeof(System.Byte)
      : (sqlType == typeof(System.Data.SqlTypes.SqlChars))
      ? typeof(System.String)
      : (sqlType == typeof(System.Data.SqlTypes.SqlBytes))
      ? typeof(System.Byte[])
      : (sqlType == typeof(System.Data.SqlTypes.SqlDateTime))
      ? typeof(System.DateTime)
      : (sqlType == typeof(System.Data.SqlTypes.SqlDecimal))
      ? typeof(System.Decimal)
      : (sqlType == typeof(System.Data.SqlTypes.SqlDouble))
      ? typeof(System.Double)
      : (sqlType == typeof(System.Data.SqlTypes.SqlGuid))
      ? typeof(System.Guid)
      : (sqlType == typeof(System.Data.SqlTypes.SqlInt16))
      ? typeof(System.Int16)
      : (sqlType == typeof(System.Data.SqlTypes.SqlInt32))
      ? typeof(System.Int32)
      : (sqlType == typeof(System.Data.SqlTypes.SqlInt64))
      ? typeof(System.Int64)
      : (sqlType == typeof(System.Data.SqlTypes.SqlMoney))
      ? typeof(System.Decimal)
      : (sqlType == typeof(System.Data.SqlTypes.SqlSingle))
      ? typeof(System.Single)
      : (sqlType == typeof(System.Data.SqlTypes.SqlString))
      ? typeof(System.String)
      : (sqlType == typeof(System.Data.SqlTypes.SqlXml))
      ? typeof(System.Xml.Linq.XDocument)
      : throw new System.NotImplementedException();

    public static System.Type ConvertSystemTypeToSqlType(System.Type systemType)
      => (systemType == typeof(System.Boolean))
      ? typeof(System.Data.SqlTypes.SqlBoolean)
      : (systemType == typeof(System.Byte))
      ? typeof(System.Data.SqlTypes.SqlByte)
      : (systemType == typeof(System.Byte[]))
      ? typeof(System.Data.SqlTypes.SqlBytes)
      : (systemType == typeof(System.DateTime))
      ? typeof(System.Data.SqlTypes.SqlDateTime)
      : (systemType == typeof(System.DateTimeOffset))
      ? typeof(System.Data.SqlTypes.SqlDateTime)
      : (systemType == typeof(System.Decimal))
      ? typeof(System.Data.SqlTypes.SqlDecimal)
      : (systemType == typeof(System.Double))
      ? typeof(System.Data.SqlTypes.SqlDouble)
      : (systemType == typeof(System.Guid))
      ? typeof(System.Data.SqlTypes.SqlGuid)
      : (systemType == typeof(System.Int16))
      ? typeof(System.Data.SqlTypes.SqlInt16)
      : (systemType == typeof(System.Int32))
      ? typeof(System.Data.SqlTypes.SqlInt32)
      : (systemType == typeof(System.Int64))
      ? typeof(System.Data.SqlTypes.SqlInt64)
      : (systemType == typeof(System.String))
      ? typeof(System.Data.SqlTypes.SqlString)
      : (systemType == typeof(System.SByte))
      ? typeof(System.Data.SqlTypes.SqlByte)
      : (systemType == typeof(System.Single))
      ? typeof(System.Data.SqlTypes.SqlSingle)
      : (systemType == typeof(System.Xml.Linq.XNode))
      ? typeof(System.Data.SqlTypes.SqlXml)
      : (systemType == typeof(System.Xml.XmlNode))
      ? typeof(System.Data.SqlTypes.SqlXml)
      : throw new System.ArgumentOutOfRangeException(systemType.FullName);

    public static TsqlDataTypeId ConvertSystemTypeToTsqlDataType(System.Type systemType)
    {
      return (systemType == typeof(System.Boolean))
      ? Data.TsqlDataTypeId.Bit
      : (systemType == typeof(System.Byte))
      ? Data.TsqlDataTypeId.Tinyint
      : (systemType == typeof(System.Byte[]))
      ? Data.TsqlDataTypeId.Varbinary
      : (systemType == typeof(System.DateTime))
      ? Data.TsqlDataTypeId.Datetime
      : (systemType == typeof(System.DateTimeOffset))
      ? Data.TsqlDataTypeId.Datetimeoffset
      : (systemType == typeof(System.Decimal))
      ? Data.TsqlDataTypeId.Decimal
      : (systemType == typeof(System.Double))
      ? Data.TsqlDataTypeId.Float
      : (systemType == typeof(System.Guid))
      ? Data.TsqlDataTypeId.Uniqueidentifier
      : (systemType == typeof(System.Int16))
      ? Data.TsqlDataTypeId.Smallint
      : (systemType == typeof(System.Int32))
      ? Data.TsqlDataTypeId.Int
      : (systemType == typeof(System.Int64))
      ? Data.TsqlDataTypeId.Bigint
      : (systemType == typeof(System.String))
      ? Data.TsqlDataTypeId.Nvarchar
      : (systemType == typeof(System.SByte))
      ? Data.TsqlDataTypeId.Tinyint
      : (systemType == typeof(System.Single))
      ? Data.TsqlDataTypeId.Real
      : (systemType == typeof(System.Xml.Linq.XNode))
      ? Data.TsqlDataTypeId.Xml
      : (systemType == typeof(System.Xml.XmlNode))
      ? Data.TsqlDataTypeId.Xml
      : (systemType == typeof(System.Object))
      ? Data.TsqlDataTypeId.SqlVariant // Convert any type.
      : throw new System.ArgumentOutOfRangeException(systemType.FullName);
    }

    public static System.Data.DbType ConvertTsqlDataTypeToDbType(TsqlDataTypeId tsqlDataType) => (System.Data.DbType)tsqlDataType;

    /// <summary>Returns the <see cref="System.Data.SqlTypes"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
    public static System.Type ConvertTsqlDataTypeToSqlType(TsqlDataTypeId tsqlDataType)
      => tsqlDataType switch
      {
        TsqlDataTypeId.Bigint => typeof(System.Data.SqlTypes.SqlInt64),
        TsqlDataTypeId.Binary => typeof(System.Data.SqlTypes.SqlBinary),
        TsqlDataTypeId.Bit => typeof(System.Data.SqlTypes.SqlBoolean),
        TsqlDataTypeId.Char => typeof(System.Data.SqlTypes.SqlChars),
        TsqlDataTypeId.Date => typeof(System.Data.SqlTypes.SqlDateTime),
        TsqlDataTypeId.Datetime => typeof(System.Data.SqlTypes.SqlDateTime),
        TsqlDataTypeId.Datetime2 => typeof(System.DateTime),
        TsqlDataTypeId.Datetimeoffset => typeof(System.DateTimeOffset),
        TsqlDataTypeId.Decimal => typeof(System.Data.SqlTypes.SqlDecimal),
        TsqlDataTypeId.Float => typeof(System.Data.SqlTypes.SqlDouble),
        //TsqlDataTypeEx.Image => typeof(System.Data.SqlTypes.SqlBinary), // Same as TsqlDataTypeEx.Binary
        TsqlDataTypeId.Int => typeof(System.Data.SqlTypes.SqlInt32),
        //TsqlDataTypeEx.Money => typeof(System.Data.SqlTypes.SqlMoney), // Same as TsqlDataTypeEx.Decimal
        TsqlDataTypeId.Nchar => typeof(System.Data.SqlTypes.SqlChars),
        TsqlDataTypeId.Ntext => typeof(System.Data.SqlTypes.SqlChars),
        //TsqlDataTypeEx.Numeric => typeof(System.Data.SqlTypes.SqlDecimal), // Same as TsqlDataTypeEx.Decimal
        //TsqlDataTypeEx.Nvarchar => typeof(System.Data.SqlTypes.SqlChars), // TsqlDataTypeEx.Ntext
        TsqlDataTypeId.Real => typeof(System.Data.SqlTypes.SqlSingle),
        //TsqlDataTypeEx.Rowversion => typeof(System.Data.SqlTypes.SqlBinary), // Same as TsqlDataTypeEx.Binary
        //TsqlDataTypeEx.Smalldatetime => typeof(System.Data.SqlTypes.SqlDateTime), // Same as TsqlDataTypeEx.Datetime
        TsqlDataTypeId.Smallint => typeof(System.Data.SqlTypes.SqlInt16),
        //TsqlDataTypeEx.Smallmoney => typeof(System.Data.SqlTypes.SqlMoney), // Same as TsqlDataTypeEx.Decimal
        TsqlDataTypeId.Text => typeof(System.Data.SqlTypes.SqlChars),
        TsqlDataTypeId.Time => typeof(System.TimeSpan),
        //TsqlDataTypeEx.Timestamp => typeof(System.Data.SqlTypes.SqlBinary), // Same as TsqlDataTypeEx.Binary
        TsqlDataTypeId.Tinyint => typeof(System.Data.SqlTypes.SqlByte),
        TsqlDataTypeId.Uniqueidentifier => typeof(System.Data.SqlTypes.SqlGuid),
        //TsqlDataTypeEx.Varbinary => typeof(System.Data.SqlTypes.SqlBinary), // Same as TsqlDataTypeEx.Binary
        //TsqlDataTypeEx.Varchar => typeof(System.Data.SqlTypes.SqlChars), // Same as TsqlDataTypeEx.Char
        TsqlDataTypeId.Xml => typeof(System.Data.SqlTypes.SqlXml),
        TsqlDataTypeId.SqlVariant => typeof(System.Object),
        _ => throw new System.ArgumentOutOfRangeException(nameof(tsqlDataType)),
      };

    /// <summary>Returns the <see cref="System.Type"/> corresponding to the specified <paramref name="tsqlDataTypeName"/>.</summary>
    public static System.Type ConvertTsqlDataTypeToSystemType(TsqlDataTypeId tsqlDataType)
      => tsqlDataType switch
      {
        TsqlDataTypeId.Bigint => typeof(System.Int64),
        TsqlDataTypeId.Binary => typeof(System.Byte[]),
        TsqlDataTypeId.Bit => typeof(System.Boolean),
        TsqlDataTypeId.Char => typeof(System.String),
        TsqlDataTypeId.Date => typeof(System.DateTime),
        TsqlDataTypeId.Datetime => typeof(System.DateTime),
        TsqlDataTypeId.Datetime2 => typeof(System.DateTime),
        TsqlDataTypeId.Datetimeoffset => typeof(System.DateTimeOffset),
        TsqlDataTypeId.Decimal => typeof(System.Decimal),
        TsqlDataTypeId.Float => typeof(System.Double),
        //TsqlDataTypeEx.Image => typeof(System.Byte[]),
        TsqlDataTypeId.Int => typeof(System.Int32),
        //TsqlDataTypeEx.Money => typeof(System.Decimal),
        TsqlDataTypeId.Nchar => typeof(System.String),
        TsqlDataTypeId.Ntext => typeof(System.String),
        //TsqlDataTypeEx.Numeric => typeof(System.Decimal),
        //TsqlDataTypeEx.Nvarchar => typeof(System.String),
        TsqlDataTypeId.Real => typeof(System.Single),
        //TsqlDataTypeEx.Rowversion => typeof(System.Byte[]),
        //TsqlDataTypeEx.Smalldatetime => typeof(System.DateTime),
        TsqlDataTypeId.Smallint => typeof(System.Int16),
        //TsqlDataTypeEx.Smallmoney => typeof(System.Decimal),
        TsqlDataTypeId.SqlVariant => typeof(System.Object),
        TsqlDataTypeId.Text => typeof(System.String),
        TsqlDataTypeId.Time => typeof(System.TimeSpan),
        //TsqlDataTypeEx.Timestamp => typeof(System.Byte[]),
        TsqlDataTypeId.Tinyint => typeof(System.Byte),
        TsqlDataTypeId.Uniqueidentifier => typeof(System.Guid),
        //TsqlDataTypeEx.Varbinary => typeof(System.Byte[]),
        //TsqlDataTypeEx.Varchar => typeof(System.String),
        TsqlDataTypeId.Xml => typeof(System.String),
        _ => throw new System.ArgumentOutOfRangeException(nameof(tsqlDataType)),
      };
  }

#endif

}
