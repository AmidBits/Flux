namespace Flux.Quantities
{
  public enum AbsoluteHumidityUnit
  {
    /// <summary>This is the default unit for <see cref="AbsoluteHumidity"/>.</summary>
    GramsPerCubicMeter,
    KilogramsPerCubicMeter,
  }

  /// <summary>
  /// <para>Absolute humidity, unit of grams per cubic meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Humidity#Absolute_humidity"/></para>
  /// </summary>
  public readonly record struct AbsoluteHumidity
    : System.IComparable, System.IComparable<AbsoluteHumidity>, System.IFormattable, IUnitValueQuantifiable<double, AbsoluteHumidityUnit>
  {
    private readonly double m_value;

    public AbsoluteHumidity(double value, AbsoluteHumidityUnit unit = AbsoluteHumidityUnit.GramsPerCubicMeter)
      => m_value = unit switch
      {
        AbsoluteHumidityUnit.GramsPerCubicMeter => value,
        AbsoluteHumidityUnit.KilogramsPerCubicMeter => value / 1000.0,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static AbsoluteHumidity From(double grams, Volume volume)
      => new(grams / volume.Value);
    public static AbsoluteHumidity From(Mass mass, Volume volume)
      => From(mass.Value * 1000, volume);
    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(AbsoluteHumidity a, AbsoluteHumidity b) => a.CompareTo(b) < 0;
    public static bool operator <=(AbsoluteHumidity a, AbsoluteHumidity b) => a.CompareTo(b) <= 0;
    public static bool operator >(AbsoluteHumidity a, AbsoluteHumidity b) => a.CompareTo(b) > 0;
    public static bool operator >=(AbsoluteHumidity a, AbsoluteHumidity b) => a.CompareTo(b) >= 0;

    public static AbsoluteHumidity operator -(AbsoluteHumidity v) => new(-v.m_value);
    public static AbsoluteHumidity operator +(AbsoluteHumidity a, double b) => new(a.m_value + b);
    public static AbsoluteHumidity operator +(AbsoluteHumidity a, AbsoluteHumidity b) => a + b.m_value;
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, double b) => new(a.m_value / b);
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, AbsoluteHumidity b) => a / b.m_value;
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, double b) => new(a.m_value * b);
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, AbsoluteHumidity b) => a * b.m_value;
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, double b) => new(a.m_value % b);
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, AbsoluteHumidity b) => a % b.m_value;
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, double b) => new(a.m_value - b);
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, AbsoluteHumidity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AbsoluteHumidity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AbsoluteHumidity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(AbsoluteHumidityUnit.GramsPerCubicMeter, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="AbsoluteHumidity.Value"/> property is in <see cref="AbsoluteHumidityUnit.GramsPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(AbsoluteHumidityUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(AbsoluteHumidityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AbsoluteHumidityUnit.GramsPerCubicMeter => "g/m³",
        Quantities.AbsoluteHumidityUnit.KilogramsPerCubicMeter => "kg/m³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AbsoluteHumidityUnit unit)
      => unit switch
      {
        AbsoluteHumidityUnit.GramsPerCubicMeter => m_value,
        AbsoluteHumidityUnit.KilogramsPerCubicMeter => m_value * 1000.0,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitString(AbsoluteHumidityUnit unit = AbsoluteHumidityUnit.GramsPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    public string ToUnitValueNameString(AbsoluteHumidityUnit unit = AbsoluteHumidityUnit.GramsPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(AbsoluteHumidityUnit unit = AbsoluteHumidityUnit.GramsPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
