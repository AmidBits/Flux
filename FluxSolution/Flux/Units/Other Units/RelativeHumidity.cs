namespace Flux.Units
{
  /// <summary>
  /// <para>Relative humidity is represented as a percentage value, e.g. 34.5 for 34.5%.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Humidity#Relative_humidity"/></para>
  /// </summary>
  public readonly record struct RelativeHumidity
    : System.IComparable, System.IComparable<RelativeHumidity>, System.IFormattable, IUnitValueQuantifiable<double, RelativeHumidityUnit>
  {
    private readonly double m_value;

    public RelativeHumidity(double value, RelativeHumidityUnit unit = RelativeHumidityUnit.Percent) => m_value = ConvertFromUnit(unit, value);

    public RelativeHumidity(MetricPrefix prefix, double percent) => m_value = prefix.ConvertTo(percent, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(RelativeHumidity a, RelativeHumidity b) => a.CompareTo(b) < 0;
    public static bool operator <=(RelativeHumidity a, RelativeHumidity b) => a.CompareTo(b) <= 0;
    public static bool operator >(RelativeHumidity a, RelativeHumidity b) => a.CompareTo(b) > 0;
    public static bool operator >=(RelativeHumidity a, RelativeHumidity b) => a.CompareTo(b) >= 0;

    public static RelativeHumidity operator -(RelativeHumidity v) => new(-v.m_value);
    public static RelativeHumidity operator +(RelativeHumidity a, double b) => new(a.m_value + b);
    public static RelativeHumidity operator +(RelativeHumidity a, RelativeHumidity b) => a + b.m_value;
    public static RelativeHumidity operator /(RelativeHumidity a, double b) => new(a.m_value / b);
    public static RelativeHumidity operator /(RelativeHumidity a, RelativeHumidity b) => a / b.m_value;
    public static RelativeHumidity operator *(RelativeHumidity a, double b) => new(a.m_value * b);
    public static RelativeHumidity operator *(RelativeHumidity a, RelativeHumidity b) => a * b.m_value;
    public static RelativeHumidity operator %(RelativeHumidity a, double b) => new(a.m_value % b);
    public static RelativeHumidity operator %(RelativeHumidity a, RelativeHumidity b) => a % b.m_value;
    public static RelativeHumidity operator -(RelativeHumidity a, double b) => new(a.m_value - b);
    public static RelativeHumidity operator -(RelativeHumidity a, RelativeHumidity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is RelativeHumidity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(RelativeHumidity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(RelativeHumidityUnit.Percent, format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="RelativeHumidity.Value"/> property is in <see cref="RelativeHumidityUnit.Percent"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(RelativeHumidityUnit unit, double value)
      => unit switch
      {
        RelativeHumidityUnit.Percent => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(RelativeHumidityUnit unit, double value)
      => unit switch
      {
        RelativeHumidityUnit.Percent => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, RelativeHumidityUnit from, RelativeHumidityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(RelativeHumidityUnit unit)
      => unit switch
      {
        RelativeHumidityUnit.Percent => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(RelativeHumidityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(RelativeHumidityUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.RelativeHumidityUnit.Percent => "%",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(RelativeHumidityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(RelativeHumidityUnit unit = RelativeHumidityUnit.Percent, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
