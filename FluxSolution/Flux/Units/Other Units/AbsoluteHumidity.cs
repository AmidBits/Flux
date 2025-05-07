namespace Flux.Units
{
  /// <summary>
  /// <para>Absolute humidity, unit of grams per cubic meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Humidity#Absolute_humidity"/></para>
  /// </summary>
  public readonly record struct AbsoluteHumidity
    : System.IComparable, System.IComparable<AbsoluteHumidity>, System.IFormattable, IUnitValueQuantifiable<double, AbsoluteHumidityUnit>
  {
    private readonly double m_value;

    public AbsoluteHumidity(double value, AbsoluteHumidityUnit unit = AbsoluteHumidityUnit.GramPerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    public AbsoluteHumidity(MetricPrefix prefix, double gramPerCubicMeter) => m_value = prefix.ChangePrefix(gramPerCubicMeter, MetricPrefix.Unprefixed);

    #region Static methods

    public static AbsoluteHumidity From(Mass mass, Volume volume) => new(mass.GetUnitValue(MassUnit.Gram) / volume.Value);

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AbsoluteHumidityUnit.GramPerCubicMeter, format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AbsoluteHumidity.Value"/> property is in <see cref="AbsoluteHumidityUnit.GramPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AbsoluteHumidityUnit unit, double value)
      => unit switch
      {
        AbsoluteHumidityUnit.GramPerCubicMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(AbsoluteHumidityUnit unit, double value)
      => unit switch
      {
        AbsoluteHumidityUnit.GramPerCubicMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, AbsoluteHumidityUnit from, AbsoluteHumidityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(AbsoluteHumidityUnit unit)
      => unit switch
      {
        AbsoluteHumidityUnit.GramPerCubicMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(AbsoluteHumidityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(AbsoluteHumidityUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.AbsoluteHumidityUnit.GramPerCubicMeter => "g/m³",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AbsoluteHumidityUnit unit) => ConvertFromUnit(unit, m_value);

    public string ToUnitString(AbsoluteHumidityUnit unit, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
