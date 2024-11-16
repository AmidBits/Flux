namespace Flux.Quantities
{
  public enum LuminousEfficacyUnit
  {
    /// <summary>This is the default unit for <see cref="LuminousEfficacy"/>.</summary>
    LumenPerWatt,
  }

  /// <summary>Torque unit of newton meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Luminous_efficacy"/>
  public readonly record struct LuminousEfficacy
    : System.IComparable, System.IComparable<LuminousEfficacy>, System.IFormattable, ISiUnitValueQuantifiable<double, LuminousEfficacyUnit>
  {
    public static LuminousEfficacy LuminousEfficacyOf540THzRadiation { get; } = new(683);

    private readonly double m_value;

    public LuminousEfficacy(double value, LuminousEfficacyUnit unit = LuminousEfficacyUnit.LumenPerWatt) => m_value = ConvertFromUnit(unit, value);

    public LuminousEfficacy(MetricPrefix prefix, double lumenPerWatt) => m_value = prefix.ConvertTo(lumenPerWatt, MetricPrefix.Unprefixed);

    public LuminousEfficacy(Energy energy, Angle angle) : this(energy.Value / angle.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) < 0;
    public static bool operator >(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) > 0;
    public static bool operator <=(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) <= 0;
    public static bool operator >=(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) >= 0;

    public static LuminousEfficacy operator -(LuminousEfficacy v) => new(-v.m_value);
    public static LuminousEfficacy operator *(LuminousEfficacy a, LuminousEfficacy b) => new(a.m_value * b.m_value);
    public static LuminousEfficacy operator /(LuminousEfficacy a, LuminousEfficacy b) => new(a.m_value / b.m_value);
    public static LuminousEfficacy operator %(LuminousEfficacy a, LuminousEfficacy b) => new(a.m_value % b.m_value);
    public static LuminousEfficacy operator +(LuminousEfficacy a, LuminousEfficacy b) => new(a.m_value + b.m_value);
    public static LuminousEfficacy operator -(LuminousEfficacy a, LuminousEfficacy b) => new(a.m_value - b.m_value);
    public static LuminousEfficacy operator *(LuminousEfficacy a, double b) => new(a.m_value * b);
    public static LuminousEfficacy operator /(LuminousEfficacy a, double b) => new(a.m_value / b);
    public static LuminousEfficacy operator %(LuminousEfficacy a, double b) => new(a.m_value % b);
    public static LuminousEfficacy operator +(LuminousEfficacy a, double b) => new(a.m_value + b);
    public static LuminousEfficacy operator -(LuminousEfficacy a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LuminousEfficacy o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LuminousEfficacy other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(LuminousEfficacyUnit.LumenPerWatt, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(LuminousEfficacyUnit.LumenPerWatt, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(LuminousEfficacyUnit.LumenPerWatt, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(LuminousEfficacyUnit unit, double value)
      => unit switch
      {
        LuminousEfficacyUnit.LumenPerWatt => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(LuminousEfficacyUnit unit, double value)
      => unit switch
      {
        LuminousEfficacyUnit.LumenPerWatt => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, LuminousEfficacyUnit from, LuminousEfficacyUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(LuminousEfficacyUnit unit)
      => unit switch
      {
        LuminousEfficacyUnit.LumenPerWatt => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(LuminousEfficacyUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(LuminousEfficacyUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.LuminousEfficacyUnit.LumenPerWatt => "lm/W",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(LuminousEfficacyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(LuminousEfficacyUnit unit = LuminousEfficacyUnit.LumenPerWatt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="LuminousEfficacy.Value"/> property is in <see cref="LuminousEfficacyUnit.LumenPerWatt"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
