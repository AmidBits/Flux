namespace Flux
{
  /// <summary>This is an extension type to the already built-in System.StringComparer, with the addition of being System.Char compatible, since char lack in implementation (for obvious reasons).</summary>
  /// <remarks>There is an obvious performance penalty in that the type char is compared using <char>.ToString() and so proxied as strings.</remarks>
  public class StringComparer
    : System.StringComparer
    , System.Collections.Generic.IComparer<char>
    , System.Collections.Generic.IEqualityComparer<char>
  {
    public new static StringComparer CurrentCulture { get; private set; } = new StringComparer(System.StringComparer.CurrentCulture);
    public new static StringComparer CurrentCultureIgnoreCase { get; private set; } = new StringComparer(System.StringComparer.CurrentCultureIgnoreCase);
    public static StringComparer CurrentCultureIgnoreNonSpace { get; private set; } = new StringComparer(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    public static StringComparer CurrentCultureIgnoreNonSpaceAndCase { get; private set; } = new StringComparer(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    public new static StringComparer InvariantCulture { get; private set; } = new StringComparer(System.StringComparer.InvariantCulture);
    public new static StringComparer InvariantCultureIgnoreCase { get; private set; } = new StringComparer(System.StringComparer.InvariantCultureIgnoreCase);
    public static StringComparer InvariantCultureIgnoreNonSpace { get; private set; } = new StringComparer(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreNonSpace);
    public static StringComparer InvariantCultureIgnoreNonSpaceAndCase { get; private set; } = new StringComparer(System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace);
    public new static StringComparer Ordinal { get; private set; } = new StringComparer(System.StringComparer.Ordinal);
    public new static StringComparer OrdinalIgnoreCase { get; private set; } = new StringComparer(System.StringComparer.OrdinalIgnoreCase);

    private readonly System.StringComparer m_stringComparer;

    public StringComparer(System.Globalization.CultureInfo cultureInfo, bool ignoreCase)
      => m_stringComparer = System.StringComparer.Create(cultureInfo, ignoreCase);
    public StringComparer(System.Globalization.CultureInfo cultureInfo, System.Globalization.CompareOptions compareOptions)
      => m_stringComparer = System.Globalization.GlobalizationExtensions.GetStringComparer(cultureInfo.CompareInfo, compareOptions);
    private StringComparer(System.StringComparer stringComparer)
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
