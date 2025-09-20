namespace Flux.Units
{
  /// <summary>
  /// <para>DigitalStorage, units in power-of-2.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Binary_prefix"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Binary_prefix#Definitions"/></para>
  /// </summary>
  public readonly record struct DigitalInformation
    : System.IComparable, System.IComparable<DigitalInformation>, System.IFormattable, IUnitValueQuantifiable<System.Numerics.BigInteger, DigitalInformationUnit>
  {
    private readonly System.Numerics.BigInteger m_value;

    public DigitalInformation(System.Numerics.BigInteger value, DigitalInformationUnit unit = DigitalInformationUnit.Byte) => m_value = ConvertFromUnit(unit, value);

    #region Static methods

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(DigitalInformation a, DigitalInformation b) => a.CompareTo(b) < 0;
    public static bool operator <=(DigitalInformation a, DigitalInformation b) => a.CompareTo(b) <= 0;
    public static bool operator >(DigitalInformation a, DigitalInformation b) => a.CompareTo(b) > 0;
    public static bool operator >=(DigitalInformation a, DigitalInformation b) => a.CompareTo(b) >= 0;

    public static DigitalInformation operator -(DigitalInformation v) => new(-v.m_value);
    public static DigitalInformation operator +(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value + b);
    public static DigitalInformation operator +(DigitalInformation a, DigitalInformation b) => a + b.m_value;
    public static DigitalInformation operator /(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value / b);
    public static DigitalInformation operator /(DigitalInformation a, DigitalInformation b) => a / b.m_value;
    public static DigitalInformation operator *(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value * b);
    public static DigitalInformation operator *(DigitalInformation a, DigitalInformation b) => a * b.m_value;
    public static DigitalInformation operator %(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value % b);
    public static DigitalInformation operator %(DigitalInformation a, DigitalInformation b) => a % b.m_value;
    public static DigitalInformation operator -(DigitalInformation a, System.Numerics.BigInteger b) => new(a.m_value - b);
    public static DigitalInformation operator -(DigitalInformation a, DigitalInformation b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(DigitalInformation other) => m_value.CompareTo(other.m_value);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is DigitalInformation o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(DigitalInformationUnit.Byte, format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="DigitalInformationUnit.Value"/> property is in <see cref="DigitalInformationUnit.Byte"/>.</para>
    /// </summary>
    public System.Numerics.BigInteger Value => m_value;

    #endregion // IValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Numerics.BigInteger ConvertFromUnit(DigitalInformationUnit unit, System.Numerics.BigInteger value)
      => unit switch
      {
        DigitalInformationUnit.Byte => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static System.Numerics.BigInteger ConvertToUnit(DigitalInformationUnit unit, System.Numerics.BigInteger value)
      => unit switch
      {
        DigitalInformationUnit.Byte => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static System.Numerics.BigInteger ConvertUnit(System.Numerics.BigInteger value, DigitalInformationUnit from, DigitalInformationUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public System.Numerics.BigInteger GetUnitValue(DigitalInformationUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(DigitalInformationUnit unit = DigitalInformationUnit.Byte, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
