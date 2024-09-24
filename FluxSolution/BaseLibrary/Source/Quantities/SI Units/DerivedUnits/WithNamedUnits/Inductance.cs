namespace Flux.Quantities
{
  public enum InductanceUnit
  {
    /// <summary>This is the default unit for <see cref="Inductance"/>.</summary>
    Henry,
  }

  /// <summary>Electrical inductance unit of Henry.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Inductance"/>
  public readonly record struct Inductance
    : System.IComparable, System.IComparable<Inductance>, System.IFormattable, ISiPrefixValueQuantifiable<double, InductanceUnit>
  {
    private readonly double m_value;

    public Inductance(double value, InductanceUnit unit = InductanceUnit.Henry)
      => m_value = unit switch
      {
        InductanceUnit.Henry => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

    public static bool operator <(Inductance a, Inductance b) => a.CompareTo(b) < 0;
    public static bool operator <=(Inductance a, Inductance b) => a.CompareTo(b) <= 0;
    public static bool operator >(Inductance a, Inductance b) => a.CompareTo(b) > 0;
    public static bool operator >=(Inductance a, Inductance b) => a.CompareTo(b) >= 0;

    public static Inductance operator -(Inductance v) => new(-v.m_value);
    public static Inductance operator +(Inductance a, double b) => new(a.m_value + b);
    public static Inductance operator +(Inductance a, Inductance b) => a + b.m_value;
    public static Inductance operator /(Inductance a, double b) => new(a.m_value / b);
    public static Inductance operator /(Inductance a, Inductance b) => a / b.m_value;
    public static Inductance operator *(Inductance a, double b) => new(a.m_value * b);
    public static Inductance operator *(Inductance a, Inductance b) => a * b.m_value;
    public static Inductance operator %(Inductance a, double b) => new(a.m_value % b);
    public static Inductance operator %(Inductance a, Inductance b) => a % b.m_value;
    public static Inductance operator -(Inductance a, double b) => new(a.m_value - b);
    public static Inductance operator -(Inductance a, Inductance b) => a - b.m_value;

    #endregion // Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Inductance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Inductance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixValueSymbolString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Inductance.Value"/> property is in <see cref="InductanceUnit.Henry"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiPrefixValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(InductanceUnit.Henry, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(InductanceUnit.Henry, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitQuantifiable<>

    public string GetUnitName(InductanceUnit unit, bool preferPlural) => unit.ToString() is var us && preferPlural ? GetUnitValue(unit).ToPluralString(us) : us;

    public string GetUnitSymbol(InductanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.InductanceUnit.Henry => "H",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(InductanceUnit unit)
      => unit switch
      {
        InductanceUnit.Henry => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(InductanceUnit unit = InductanceUnit.Henry, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(InductanceUnit unit = InductanceUnit.Henry, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
