using System.Linq;

namespace Flux.Data
{
  public static partial class ExtensionMethods
  {
    /// System.Data.SqlClient is not in .NetStandard OBVIOUSLY! :)
    /// static System.Reflection.FieldInfo _rowsCopiedField = null;
    //public static int TotalRowsCopied(this System.Data.SqlClient.SqlBulkCopy bulkCopy)
    //  => (int)typeof(System.Data.SqlClient.SqlBulkCopy).GetField(@"_rowsCopied", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance).GetValue(bulkCopy);

    public static string TsqlEscapeSingleQuotes(this string source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Replace("'", "''", System.StringComparison.Ordinal);
    public static string TsqlUnescapeSingleQuotes(this string source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Replace("''", "'", System.StringComparison.Ordinal);

    public static string TsqlEnquote(this string source, bool ansi = false)
      => ansi ? source.Wrap('"', '"') : source.Wrap('[', ']');
    public static string TsqlUnenquote(this string source, bool ansi = false)
      => ansi ? (source.IsWrapped('"', '"') ? source.Unwrap('"', '"') : source) : (source.IsWrapped('[', ']') ? source.Unwrap('[', ']') : source);
  }

  public struct TsqlName
    : System.IEquatable<TsqlName>
  {
    public const string CsApplicationName = @"Application Name";
    public const string CsDatabase = @"Database";
    public const string CsDataSource = @"Data Source";
    public const string CsDriver = @"Driver";
    public const string CsInitialCatalog = @"Initial Catalog";
    public const string CsIntegratedSecurity = @"Integrated Security";
    public const string CsProvider = @"Provider";
    public const string CsServer = @"Server";
    public const string CsTrustedConnection = @"Trusted_Connection";
    public const string CsWorkstationID = @"Workstation ID";

    public static readonly TsqlName Empty;

    private readonly string[] m_parts;

    public TsqlName(string serverName, string databaseName, string schemaName, string objectName)
    {
      m_parts = new string[4] { serverName.TsqlUnenquote(), databaseName.TsqlUnenquote(), schemaName.TsqlUnenquote(), objectName.TsqlUnenquote() };

      ApplicationName = AssemblyInfo.Entry.Product ?? $"{System.Environment.UserDomainName}\\{System.Environment.UserName}";
      WorkstationID = System.Environment.MachineName;
    }

    /// <summary></summary>
    public string? ApplicationName { get; set; }

    /// <summary>Returns the database name.</summary>
    public string DatabaseName { get => m_parts[1]; set => m_parts[1] = value.TsqlUnenquote(); }
    /// <summary>Returns the database name quoted.</summary>
    public string DatabaseNameQuoted
      => m_parts[1].TsqlEnquote();

    /// <summary></summary>
    public TsqlName DefaultMergeName
      => new() { ServerName = ServerName, DatabaseName = DatabaseName, SchemaName = SchemaName, ObjectName = ObjectName + @"_DE" };

    /// <summary>Returns the schema name.</summary>
    public string SchemaName { get => m_parts[2]; set => m_parts[2] = value.TsqlUnenquote(); }
    /// <summary>Returns the schema name quoted.</summary>
    public string SchemaNameQuoted
      => m_parts[2].TsqlEnquote();

    /// <summary>Returns the object name.</summary>
    public string ObjectName { get => m_parts[3]; set => m_parts[3] = value.TsqlUnenquote(); }
    /// <summary>Returns the object name quoted.</summary>
    public string ObjectNameQuoted
      => m_parts[3].TsqlEnquote();

    /// <summary>Returns the sql instance.</summary>
    public string ServerName { get => m_parts[0]; set => m_parts[0] = value.TsqlUnenquote(); }
    /// <summary>Returns the sql instance qouted.</summary>
    public string ServerNameQuoted
      => m_parts[0].TsqlEnquote();

    /// <summary></summary>
    /// <remarks>return $"Driver=SQL Server;Server={ServerName};Database={DatabaseName};Trusted_Connection=Yes;Application Name={ApplicationName};Workstation ID={WorkstationID};";</remarks>
    public string TrustedConnectionStringOdbc
    {
      get
      {
        var csb = new System.Data.Common.DbConnectionStringBuilder
        {
          { CsDriver, @"SQL Server" },
          { CsServer, ServerName },
          { CsDatabase, DatabaseName },
          { CsTrustedConnection, @"Yes" }
        };
        if (ApplicationName is not null)
          csb.Add(CsApplicationName, ApplicationName);
        if (WorkstationID is not null)
          csb.Add(CsWorkstationID, WorkstationID);
        return csb.ToString();
      }
    }
    /// <summary></summary>
    /// <remarks>return $"Provider=SQLOLEDB;Data Source={ServerName};Initial Catalog={DatabaseName};Trusted_Connection=Yes;Application Name={ApplicationName};Workstation ID={WorkstationID};";</remarks>
    public string TrustedConnectionStringOleDb
    {
      get
      {
        var csb = new System.Data.Common.DbConnectionStringBuilder
        {
          { CsProvider, @"SQLOLEDB" },
          { CsDataSource, ServerName },
          { CsInitialCatalog, DatabaseName },
          { CsTrustedConnection, @"Yes" }
        };
        if (ApplicationName is not null)
          csb.Add(CsApplicationName, ApplicationName);
        if (WorkstationID is not null)
          csb.Add(CsWorkstationID, WorkstationID);
        return csb.ToString();
      }
    }
    /// <summary></summary>
    /// <remarks>return $"Data Source={ServerName};Initial Catalog={DatabaseName};Integrated Security=True;Application Name={ApplicationName};Workstation ID={WorkstationID};";</remarks>
    public string TrustedConnectionStringSqlClient
    {
      get
      {
        var csb = new System.Data.Common.DbConnectionStringBuilder
        {
          { CsDataSource, ServerName },
          { CsInitialCatalog, DatabaseName },
          { CsIntegratedSecurity, @"True" }
        };
        if (ApplicationName is not null)
          csb.Add(CsApplicationName, ApplicationName);
        if (WorkstationID is not null)
          csb.Add(CsWorkstationID, WorkstationID);
        return csb.ToString();
      }
    }

    /// <summary></summary>
    public string? WorkstationID { get; set; }

    public string QualifiedName(int count)
      => (count >= 1 && count <= 4) ? string.Join(".", m_parts.Skip(m_parts.Length - count).Take(count)) : throw new System.ArgumentOutOfRangeException(nameof(count), "A name consists of 1 to 4 parts.");
    public string QualifiedNameQuoted(int count)
      => (count >= 1 && count <= 4) ? string.Join(".", m_parts.Skip(m_parts.Length - count).Take(count).Select(s => s.TsqlEnquote())) : throw new System.ArgumentOutOfRangeException(nameof(count), "A name consists of 1 to 4 parts.");

    #region Static methods
    private static readonly System.Text.RegularExpressions.Regex m_reQualifiedNameSplitter = new(@"(?<!\[[^\]])\.(?![^\[]*\])");

    public static TsqlName Parse(string qualifiedName)
    {
      var names = m_reQualifiedNameSplitter.Split(qualifiedName);

      if (names.Length >= 1 && names.Length <= 4)
        if (names.Length < 4)
          names = names.Select(n => n.TsqlUnenquote()).Prepend(new string[4 - names.Length]).Select(n => n is null ? string.Empty : n).ToArray();
        else throw new System.ArgumentOutOfRangeException(nameof(qualifiedName), "The name cannot be parsed.");

      return new TsqlName(names[0], names[1], names[2], names[3]);
    }
    public static bool TryParse(string qualifiedName, out TsqlName? result)
    {
      try
      {
        result = Parse(qualifiedName);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(TsqlName left, TsqlName right)
      => left.Equals(right);
    public static bool operator !=(TsqlName left, TsqlName right)
      => !left.Equals(right);
    #endregion Overloaded operators

    #region Implemented interfaces
    // System.IEquatable<SqlName>
    public bool Equals(TsqlName other)
      => ToString() == other.ToString();
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is TsqlName o && Equals(o);
    public override int GetHashCode()
      => ToString().GetHashCode(System.StringComparison.Ordinal);
    public override string ToString()
      => $"{GetType().Name} {{ Name = {QualifiedNameQuoted(4)} }}";
    #endregion Object overrides
  }
}
