namespace Flux.Units
{
  /// <summary>
  /// <para>A percentage (from Latin per centum 'by a hundred') is a number or ratio expressed as a fraction of 100.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Percentage"/></para>
  /// </summary>
  public readonly record struct Percentage
  : System.IComparable, System.IComparable<Percentage>, System.IFormattable, IValueQuantifiable<double>
  {
    private readonly double m_value;

    public Percentage(double value) => m_value = value >= 0 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));

    #region Static methods

    public static Percentage ConvertToReversePercentage(Percentage p)
      => new(double.Abs(p.m_value) / (p.m_value + 1));

    public static Percentage Parse(System.ReadOnlySpan<char> source, int? significantDigits = null, HalfRounding mode = HalfRounding.ToEven, System.Globalization.CultureInfo? cultureInfo = null)
    {
      cultureInfo ??= System.Globalization.CultureInfo.InvariantCulture;

      var vme = Globalization.Number.RegexDecimalNumber(cultureInfo).EnumerateMatches(source);

      if (vme.MoveNext())
      {
        var vms = source.Slice(vme.Current.Index, vme.Current.Length);

        if (double.TryParse(vms, System.Globalization.NumberStyles.Number, cultureInfo, out var value))
          return new(significantDigits.HasValue ? (value / 100).RoundByPrecision(mode, significantDigits.Value, 10) : value / 100);
      }

      throw new System.ArgumentException("Parse failed.", nameof(source));
    }

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Percentage a, Percentage b) => a.CompareTo(b) < 0;
    public static bool operator <=(Percentage a, Percentage b) => a.CompareTo(b) <= 0;
    public static bool operator >(Percentage a, Percentage b) => a.CompareTo(b) > 0;
    public static bool operator >=(Percentage a, Percentage b) => a.CompareTo(b) >= 0;

    public static Percentage operator -(Percentage v) => new(-v.m_value);
    public static Percentage operator +(Percentage a, double b) => new(a.m_value + b);
    public static Percentage operator +(Percentage a, Percentage b) => a + b.m_value;
    public static Percentage operator /(Percentage a, double b) => new(a.m_value / b);
    public static Percentage operator /(Percentage a, Percentage b) => a / b.m_value;
    public static Percentage operator *(Percentage a, double b) => new(a.m_value * b);
    public static Percentage operator *(Percentage a, Percentage b) => a * b.m_value;
    public static Percentage operator %(Percentage a, double b) => new(a.m_value % b);
    public static Percentage operator %(Percentage a, Percentage b) => a % b.m_value;
    public static Percentage operator -(Percentage a, double b) => new(a.m_value - b);
    public static Percentage operator -(Percentage a, Percentage b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Percentage o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Percentage other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
    {
      var percentage = m_value * 100;

      format ??= double.IsInteger(percentage) ? "P0" : "P";
      formatProvider ??= System.Globalization.CultureInfo.CurrentCulture;

      if (format.StartsWith('P'))
        return m_value.ToString(format, formatProvider);
      else
        return percentage.ToString(format, formatProvider) + '%';
    }

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="Percentage.Value"/> property is the ultraviolet index.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
