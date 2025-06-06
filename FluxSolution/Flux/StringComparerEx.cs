namespace Flux
{
  /// <summary>This is an extension type to the already built-in System.StringComparer, with the addition of being System.Char compatible, since char lack in implementation (for obvious reasons).</summary>
  /// <remarks>There is an obvious performance penalty in that the type char is compared using char.ToString() and so converted to strings.</remarks>
  public sealed class StringComparerEx
    : System.StringComparer // Inherited IComparer<string> and IEqualityComparer<string>.
    , System.Collections.Generic.IComparer<char>
    , System.Collections.Generic.IEqualityComparer<char>
  {
    /// <summary>Performs a case-sensitive comparison using the word comparison rules of the current culture.</summary>
    public new static StringComparerEx CurrentCulture { get; } = new StringComparerEx(System.StringComparer.CurrentCulture);
    /// <summary>Performs a case-insensitive comparison using the word comparison rules of the current culture.</summary>
    public new static StringComparerEx CurrentCultureIgnoreCase { get; } = new StringComparerEx(System.StringComparer.CurrentCultureIgnoreCase);
    /// <summary>Performs a case-sensitive (e.g. diacritics) comparison, ignoring nonspacing combining characters, using the word comparison rules of the current culture.</summary>
    public static StringComparerEx CurrentCultureIgnoreNonSpace { get; } = new StringComparerEx(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    /// <summary>Performs a case-insensitive (e.g. diacritics) comparison, ignoring nonspacing combining characters, using the word comparison rules of the current culture.</summary>
    public static StringComparerEx CurrentCultureIgnoreNonSpaceAndCase { get; } = new StringComparerEx(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    /// <summary>Performs a case-sensitive comparison using the word comparison rules of the invariant culture.</summary>
    public new static StringComparerEx InvariantCulture { get; } = new StringComparerEx(System.StringComparer.InvariantCulture);
    /// <summary>Performs a case-insensitive comparison using the word comparison rules of the invariant culture.</summary>
    public new static StringComparerEx InvariantCultureIgnoreCase { get; } = new StringComparerEx(System.StringComparer.InvariantCultureIgnoreCase);
    /// <summary>Performs a case-sensitive (e.g. diacritics) comparison, ignoring nonspacing combining characters, using the word comparison rules of the invariant culture.</summary>
    public static StringComparerEx InvariantCultureIgnoreNonSpace { get; } = new StringComparerEx(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    /// <summary>Performs a case-insensitive (e.g. diacritics) comparison, ignoring nonspacing combining characters, using the word comparison rules of the invariant culture.</summary>
    public static StringComparerEx InvariantCultureIgnoreNonSpaceAndCase { get; } = new StringComparerEx(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    /// <summary>Performs a case-sensitive ordinal comparison.</summary>
    public new static StringComparerEx Ordinal { get; } = new StringComparerEx(System.StringComparer.Ordinal);
    /// <summary>Performs a case-insensitive ordinal comparison.</summary>
    public new static StringComparerEx OrdinalIgnoreCase { get; } = new StringComparerEx(System.StringComparer.OrdinalIgnoreCase);

    private readonly System.StringComparer m_stringComparer;

    public StringComparerEx(System.Globalization.CultureInfo cultureInfo, System.Globalization.CompareOptions compareOptions)
      => m_stringComparer = System.StringComparer.Create(cultureInfo, compareOptions);
    private StringComparerEx(System.StringComparer stringComparer)
      => m_stringComparer = stringComparer; // Public instances must specify settings on creation.

    #region Implemented interfaces

    // IComparer<string>
    public override int Compare(string? x, string? y) => m_stringComparer.Compare(x, y);

    // IComparer<char>
    public int Compare(char x, char y) => m_stringComparer.Compare(x.ToString(), y.ToString());

    // IEqualityComparer<string>
    public override bool Equals(string? x, string? y) => m_stringComparer.Equals(x, y);

    // IEqualityComparer<char>
    public bool Equals(char x, char y) => m_stringComparer.Equals(x.ToString(), y.ToString());

    #endregion Implemented interfaces

    #region Object overrides

    public override int GetHashCode(string s) => m_stringComparer.GetHashCode(s);

    public int GetHashCode(char c) => m_stringComparer.GetHashCode(c.ToString());

    public override string ToString() => $"{GetType().Name} {{ {m_stringComparer} }}";

    #endregion Object overrides
  }
}
