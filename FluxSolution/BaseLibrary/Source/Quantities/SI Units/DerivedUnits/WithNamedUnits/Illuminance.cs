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
    : System.IComparable, System.IComparable<Illuminance>, System.IFormattable, ISiPrefixValueQuantifiable<double, IlluminanceUnit>
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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Illuminance.Value"/> property is in <see cref="IlluminanceUnit.Lux"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(IlluminanceUnit.Lux, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(IlluminanceUnit.Lux, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    //public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    //public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

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

    //public string ToUnitValueNameString(IlluminanceUnit unit = IlluminanceUnit.Lux, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(IlluminanceUnit unit = IlluminanceUnit.Lux, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
