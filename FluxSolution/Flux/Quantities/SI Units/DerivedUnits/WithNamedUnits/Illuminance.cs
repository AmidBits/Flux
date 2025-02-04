namespace Flux.Quantities
{
  public enum IlluminanceUnit
  {
    /// <summary>This is the default unit for <see cref="Illuminance"/>.</summary>
    Lux,
  }

  /// <summary>Illuminance unit of lux.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Illuminance"/>
  public readonly record struct Illuminance
    : System.IComparable, System.IComparable<Illuminance>, System.IFormattable, ISiUnitValueQuantifiable<double, IlluminanceUnit>
  {
    private readonly double m_value;

    public Illuminance(double value, IlluminanceUnit unit = IlluminanceUnit.Lux) => m_value = ConvertFromUnit(unit, value);

    public Illuminance(MetricPrefix prefix, double lux) => m_value = prefix.ConvertTo(lux, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Illuminance a, Illuminance b) => a.CompareTo(b) < 0;
    public static bool operator >(Illuminance a, Illuminance b) => a.CompareTo(b) > 0;
    public static bool operator <=(Illuminance a, Illuminance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Illuminance a, Illuminance b) => a.CompareTo(b) >= 0;

    public static Illuminance operator -(Illuminance v) => new(-v.m_value);
    public static Illuminance operator *(Illuminance a, Illuminance b) => new(a.m_value * b.m_value);
    public static Illuminance operator /(Illuminance a, Illuminance b) => new(a.m_value / b.m_value);
    public static Illuminance operator %(Illuminance a, Illuminance b) => new(a.m_value % b.m_value);
    public static Illuminance operator +(Illuminance a, Illuminance b) => new(a.m_value + b.m_value);
    public static Illuminance operator -(Illuminance a, Illuminance b) => new(a.m_value - b.m_value);
    public static Illuminance operator *(Illuminance a, double b) => new(a.m_value * b);
    public static Illuminance operator /(Illuminance a, double b) => new(a.m_value / b);
    public static Illuminance operator %(Illuminance a, double b) => new(a.m_value % b);
    public static Illuminance operator +(Illuminance a, double b) => new(a.m_value + b);
    public static Illuminance operator -(Illuminance a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Illuminance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Illuminance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(IlluminanceUnit.Lux, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(IlluminanceUnit.Lux, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(IlluminanceUnit unit, double value)
      => unit switch
      {
        IlluminanceUnit.Lux => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(IlluminanceUnit unit, double value)
      => unit switch
      {
        IlluminanceUnit.Lux => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, IlluminanceUnit from, IlluminanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(IlluminanceUnit unit)
      => unit switch
      {
        IlluminanceUnit.Lux => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(IlluminanceUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(IlluminanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.IlluminanceUnit.Lux => preferUnicode ? "\u33D3" : "lx",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(IlluminanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(IlluminanceUnit unit = IlluminanceUnit.Lux, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Illuminance.Value"/> property is in <see cref="IlluminanceUnit.Lux"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
