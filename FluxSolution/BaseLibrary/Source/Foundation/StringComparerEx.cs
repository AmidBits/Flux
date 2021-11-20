namespace Flux
{
  /// <summary>This is an extension type to the already built-in System.StringComparer, with the addition of being System.Char compatible, since char lack in implementation (for obvious reasons).</summary>
  /// <remarks>There is an obvious performance penalty in that the type char is compared using char.ToString() and so proxied as strings.</remarks>
  public sealed class StringComparerEx
    : System.StringComparer
    , System.Collections.Generic.IComparer<char>
    , System.Collections.Generic.IEqualityComparer<char>
  {
    public new static StringComparerEx CurrentCulture { get; } = new StringComparerEx(System.StringComparer.CurrentCulture);
    public new static StringComparerEx CurrentCultureIgnoreCase { get; } = new StringComparerEx(System.StringComparer.CurrentCultureIgnoreCase);
    public static StringComparerEx CurrentCultureIgnoreNonSpace { get; } = new StringComparerEx(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    public static StringComparerEx CurrentCultureIgnoreNonSpaceAndCase { get; } = new StringComparerEx(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    public new static StringComparerEx InvariantCulture { get; } = new StringComparerEx(System.StringComparer.InvariantCulture);
    public new static StringComparerEx InvariantCultureIgnoreCase { get; } = new StringComparerEx(System.StringComparer.InvariantCultureIgnoreCase);
    public static StringComparerEx InvariantCultureIgnoreNonSpace { get; } = new StringComparerEx(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    public static StringComparerEx InvariantCultureIgnoreNonSpaceAndCase { get; } = new StringComparerEx(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    public new static StringComparerEx Ordinal { get; } = new StringComparerEx(System.StringComparer.Ordinal);
    public new static StringComparerEx OrdinalIgnoreCase { get; } = new StringComparerEx(System.StringComparer.OrdinalIgnoreCase);

    private readonly System.StringComparer m_stringComparer;

    public StringComparerEx(System.Globalization.CultureInfo cultureInfo, bool ignoreCase)
      => m_stringComparer = System.StringComparer.Create(cultureInfo, ignoreCase);
    public StringComparerEx(System.Globalization.CultureInfo cultureInfo, System.Globalization.CompareOptions compareOptions)
      => m_stringComparer = System.Globalization.GlobalizationExtensions.GetStringComparer((cultureInfo ?? throw new System.ArgumentNullException(nameof(cultureInfo))).CompareInfo, compareOptions);
    private StringComparerEx(System.StringComparer stringComparer)
      => m_stringComparer = stringComparer; // Public instances must specify settings on creation.

    // IComparer<string>
    public override int Compare(string? x, string? y)
      => m_stringComparer.Compare(x, y);
    // IEqualityComparer<string>
    public override bool Equals(string? x, string? y)
      => m_stringComparer.Equals(x, y);
    // Object overrides
    public override int GetHashCode(string s)
      => m_stringComparer.GetHashCode(s);

    // IComparer<char>
    public int Compare(char x, char y)
      => m_stringComparer.Compare(x.ToString(), y.ToString());
    // IEqualityComparer<char>
    public bool Equals(char x, char y)
      => m_stringComparer.Equals(x.ToString(), y.ToString());

    public int GetHashCode(char c)
      => m_stringComparer.GetHashCode(c.ToString());

    // Object overrides
    public override string ToString()
      => $"{GetType().Name} {{ {m_stringComparer} }}";
  }
}
