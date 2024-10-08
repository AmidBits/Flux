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

    //public MetricMultiplicative ToMetricMultiplicative() => new(m_value, MetricMultiplicativePrefix.One);

    #region Overloaded operators

    public static bool operator <(Illuminance a, Illuminance b) => a.CompareTo(b) < 0;
    public static bool operator <=(Illuminance a, Illuminance b) => a.CompareTo(b) <= 0;
    public static bool operator >(Illuminance a, Illuminance b) => a.CompareTo(b) > 0;
    public static bool operator >=(Illuminance a, Illuminance b) => a.CompareTo(b) >= 0;

    public static Illuminance operator -(Illuminance v) => new(-v.m_value);
    public static Illuminance operator +(Illuminance a, double b) => new(a.m_value + b);
    public static Illuminance operator +(Illuminance a, Illuminance b) => a + b.m_value;
    public static Illuminance operator /(Illuminance a, double b) => new(a.m_value / b);
    public static Illuminance operator /(Illuminance a, Illuminance b) => a / b.m_value;
    public static Illuminance operator *(Illuminance a, double b) => new(a.m_value * b);
    public static Illuminance operator *(Illuminance a, Illuminance b) => a * b.m_value;
    public static Illuminance operator %(Illuminance a, double b) => new(a.m_value % b);
    public static Illuminance operator %(Illuminance a, Illuminance b) => a % b.m_value;
    public static Illuminance operator -(Illuminance a, double b) => new(a.m_value - b);
    public static Illuminance operator -(Illuminance a, Illuminance b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Illuminance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Illuminance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Illuminance.Value"/> property is in <see cref="IlluminanceUnit.Lux"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(IlluminanceUnit.Lux, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(IlluminanceUnit.Lux, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(IlluminanceUnit unit, double value)
      => unit switch
      {
        IlluminanceUnit.Lux => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(IlluminanceUnit unit, double value)
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

    public string GetUnitName(IlluminanceUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? GetUnitValue(unit).ToPluralString(us) : us;

    public string GetUnitSymbol(IlluminanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.IlluminanceUnit.Lux => preferUnicode ? "\u33D3" : "lx",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(IlluminanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(IlluminanceUnit unit = IlluminanceUnit.Lux, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
