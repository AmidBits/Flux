namespace Flux.Quantities
{
  public enum RelativeHumidityUnit
  {
    /// <summary>This is the default unit for <see cref="RelativeHumidity"/>.</summary>
    Percent,
  }

  /// <summary>
  /// <para>Relative humidity is represented as a percentage value, e.g. 34.5 for 34.5%.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Humidity#Relative_humidity"/></para>
  /// </summary>
  public readonly record struct RelativeHumidity
    : System.IComparable, System.IComparable<RelativeHumidity>, System.IFormattable, IUnitValueQuantifiable<double, RelativeHumidityUnit>
  {
    private readonly double m_value;

    public RelativeHumidity(double value, RelativeHumidityUnit unit = RelativeHumidityUnit.Percent)
      => m_value = unit switch
      {
        RelativeHumidityUnit.Percent => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(RelativeHumidityUnit.Percent, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="RelativeHumidity.Value"/> property is in <see cref="RelativeHumidityUnit.Percent"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(RelativeHumidityUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(RelativeHumidityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.RelativeHumidityUnit.Percent => preferUnicode ? "\u0025" : "\u0025",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(RelativeHumidityUnit unit)
      => unit switch
      {
        RelativeHumidityUnit.Percent => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitString(RelativeHumidityUnit unit = RelativeHumidityUnit.Percent, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    public string ToUnitValueNameString(RelativeHumidityUnit unit = RelativeHumidityUnit.Percent, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(RelativeHumidityUnit unit = RelativeHumidityUnit.Percent, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
