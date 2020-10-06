namespace Flux
{
  /// <summary>This is an extension type to the already built-in System.StringComparer, with the addition of being System.Char compatible, since char lack in implementation (for obvious reasons).</summary>
  /// <remarks>There is an obvious performance penalty in that the type char is compared using char.ToString() and so proxied as strings.</remarks>
  public class StringComparerX
    : System.StringComparer
    , System.Collections.Generic.IComparer<char>
    , System.Collections.Generic.IEqualityComparer<char>
  {
    public new static StringComparerX CurrentCulture { get; } = new StringComparerX(System.StringComparer.CurrentCulture);
    public new static StringComparerX CurrentCultureIgnoreCase { get; } = new StringComparerX(System.StringComparer.CurrentCultureIgnoreCase);
    public static StringComparerX CurrentCultureIgnoreNonSpace { get; } = new StringComparerX(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    public static StringComparerX CurrentCultureIgnoreNonSpaceAndCase { get; } = new StringComparerX(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    public new static StringComparerX InvariantCulture { get; } = new StringComparerX(System.StringComparer.InvariantCulture);
    public new static StringComparerX InvariantCultureIgnoreCase { get; } = new StringComparerX(System.StringComparer.InvariantCultureIgnoreCase);
    public static StringComparerX InvariantCultureIgnoreNonSpace { get; } = new StringComparerX(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    public static StringComparerX InvariantCultureIgnoreNonSpaceAndCase { get; } = new StringComparerX(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    public new static StringComparerX Ordinal { get; } = new StringComparerX(System.StringComparer.Ordinal);
    public new static StringComparerX OrdinalIgnoreCase { get; } = new StringComparerX(System.StringComparer.OrdinalIgnoreCase);

    private readonly System.StringComparer m_stringComparer;

    public StringComparerX(System.Globalization.CultureInfo cultureInfo, bool ignoreCase)
      => m_stringComparer = System.StringComparer.Create(cultureInfo, ignoreCase);
    public StringComparerX(System.Globalization.CultureInfo cultureInfo, System.Globalization.CompareOptions compareOptions)
      => m_stringComparer = System.Globalization.GlobalizationExtensions.GetStringComparer((cultureInfo ?? throw new System.ArgumentNullException(nameof(cultureInfo))).CompareInfo, compareOptions);
    private StringComparerX(System.StringComparer stringComparer)
      => m_stringComparer = stringComparer; // Public instances must specify settings on creation.

    // IComparer<string>
    public override int Compare(string? x, string? y)
      => m_stringComparer.Compare(x, y);
    // IEqualityComparer<string>
    public override bool Equals(string? x, string? y)
      => m_stringComparer.Equals(x, y);
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
  }
}
