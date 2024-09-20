namespace Flux.Quantities
{
  public enum CurrentDensityUnit
  {
    /// <summary>This is the default unit for <see cref="CurrentDensity"/>.</summary>
    AmperePerSquareMeter,
  }

  /// <summary>Current density, unit of ampere per square meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Current_density"/>
  public readonly record struct CurrentDensity
    : System.IComparable, System.IComparable<CurrentDensity>, System.IFormattable, IUnitValueQuantifiable<double, CurrentDensityUnit>
  {
    private readonly double m_value;

    public CurrentDensity(double value, CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter)
      => m_value = unit switch
      {
        CurrentDensityUnit.AmperePerSquareMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    //public MetricMultiplicative ToMetricMultiplicative() => new(GetUnitValue(CurrentDensityUnit.AmperePerSquareMeter), MetricMultiplicativePrefix.One);

    #region Overloaded operators

    public static bool operator <(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) < 0;
    public static bool operator <=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) > 0;
    public static bool operator >=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) >= 0;

    public static CurrentDensity operator -(CurrentDensity v) => new(-v.m_value);
    public static CurrentDensity operator +(CurrentDensity a, double b) => new(a.m_value + b);
    public static CurrentDensity operator +(CurrentDensity a, CurrentDensity b) => a + b.m_value;
    public static CurrentDensity operator /(CurrentDensity a, double b) => new(a.m_value / b);
    public static CurrentDensity operator /(CurrentDensity a, CurrentDensity b) => a / b.m_value;
    public static CurrentDensity operator *(CurrentDensity a, double b) => new(a.m_value * b);
    public static CurrentDensity operator *(CurrentDensity a, CurrentDensity b) => a * b.m_value;
    public static CurrentDensity operator %(CurrentDensity a, double b) => new(a.m_value % b);
    public static CurrentDensity operator %(CurrentDensity a, CurrentDensity b) => a % b.m_value;
    public static CurrentDensity operator -(CurrentDensity a, double b) => new(a.m_value - b);
    public static CurrentDensity operator -(CurrentDensity a, CurrentDensity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is CurrentDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(CurrentDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitValueSymbolString(CurrentDensityUnit.AmperePerSquareMeter, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="CurrentDensity.Value"/> property is in <see cref="CurrentDensityUnit.AmperePerSquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

    public string GetUnitName(CurrentDensityUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(CurrentDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.CurrentDensityUnit.AmperePerSquareMeter => "A/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(CurrentDensityUnit unit)
      => unit switch
      {
        CurrentDensityUnit.AmperePerSquareMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
