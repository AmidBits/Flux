namespace Flux
{
  /// <summary>This is an extension type to the already built-in System.StringComparer, with the addition of being System.Char compatible, since char lack in implementation (for obvious reasons).</summary>
  /// <remarks>There is an obvious performance penalty in that the type char is compared using <char>.ToString() and so proxied as strings.</remarks>
  public class StringComparerEx
    : System.StringComparer
    , System.Collections.Generic.IComparer<char>
    , System.Collections.Generic.IEqualityComparer<char>
  {
    public new static StringComparerEx CurrentCulture { get; private set; } = new StringComparerEx(System.StringComparer.CurrentCulture);
    public new static StringComparerEx CurrentCultureIgnoreCase { get; private set; } = new StringComparerEx(System.StringComparer.CurrentCultureIgnoreCase);
    public static StringComparerEx CurrentCultureIgnoreNonSpace { get; private set; } = new StringComparerEx(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    public static StringComparerEx CurrentCultureIgnoreNonSpaceAndCase { get; private set; } = new StringComparerEx(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    public new static StringComparerEx InvariantCulture { get; private set; } = new StringComparerEx(System.StringComparer.InvariantCulture);
    public new static StringComparerEx InvariantCultureIgnoreCase { get; private set; } = new StringComparerEx(System.StringComparer.InvariantCultureIgnoreCase);
    public static StringComparerEx InvariantCultureIgnoreNonSpace { get; private set; } = new StringComparerEx(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    public static StringComparerEx InvariantCultureIgnoreNonSpaceAndCase { get; private set; } = new StringComparerEx(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    public new static StringComparerEx Ordinal { get; private set; } = new StringComparerEx(System.StringComparer.Ordinal);
    public new static StringComparerEx OrdinalIgnoreCase { get; private set; } = new StringComparerEx(System.StringComparer.OrdinalIgnoreCase);

    private readonly System.StringComparer m_stringComparer;

    public StringComparerEx(System.Globalization.CultureInfo cultureInfo, bool ignoreCase)
      => m_stringComparer = System.StringComparer.Create(cultureInfo, ignoreCase);
    public StringComparerEx(System.Globalization.CultureInfo cultureInfo, System.Globalization.CompareOptions compareOptions)
      => m_stringComparer = System.Globalization.GlobalizationExtensions.GetStringComparer(cultureInfo.CompareInfo, compareOptions);
    private StringComparerEx(System.StringComparer stringComparer)
      => m_stringComparer = stringComparer; // Public instances must specify settings on creation.

    public override int Compare(string? x, string? y) => m_stringComparer.Compare(x, y);
    public override bool Equals(string? x, string? y) => m_stringComparer.Equals(x, y);
    public override int GetHashCode(string s) => m_stringComparer.GetHashCode(s);

    // IComparer
    public int Compare(char x, char y) => m_stringComparer.Compare(x.ToString(), y.ToString());
    // IEqualityComparer
    public bool Equals(char x, char y) => m_stringComparer.Equals(x.ToString(), y.ToString());
    public int GetHashCode(char c) => m_stringComparer.GetHashCode(c.ToString());
  }
}
