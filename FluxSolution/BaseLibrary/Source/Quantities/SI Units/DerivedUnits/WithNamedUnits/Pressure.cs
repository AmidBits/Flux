namespace Flux.Quantities
{
  public enum PressureUnit
  {
    /// <summary>This is the default unit for <see cref="Pressure"/>.</summary>
    Pascal,
    Millibar,
    Bar,
    HectoPascal,
    KiloPascal,
    Psi,
  }

  /// <summary>Pressure, unit of Pascal. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Pressure"/>
  public readonly record struct Pressure
    : System.IComparable, System.IComparable<Pressure>, System.IFormattable, ISiPrefixValueQuantifiable<double, PressureUnit>
  {
    private readonly double m_value;

    public Pressure(double value, PressureUnit unit = PressureUnit.Pascal) => m_value = ConvertFromUnit(unit, value);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Pressure a, Pressure b) => a.CompareTo(b) < 0;
    public static bool operator <=(Pressure a, Pressure b) => a.CompareTo(b) <= 0;
    public static bool operator >(Pressure a, Pressure b) => a.CompareTo(b) > 0;
    public static bool operator >=(Pressure a, Pressure b) => a.CompareTo(b) >= 0;

    public static Pressure operator -(Pressure v) => new(-v.m_value);
    public static Pressure operator +(Pressure a, double b) => new(a.m_value + b);
    public static Pressure operator +(Pressure a, Pressure b) => a + b.m_value;
    public static Pressure operator /(Pressure a, double b) => new(a.m_value / b);
    public static Pressure operator /(Pressure a, Pressure b) => a / b.m_value;
    public static Pressure operator *(Pressure a, double b) => new(a.m_value * b);
    public static Pressure operator *(Pressure a, Pressure b) => a * b.m_value;
    public static Pressure operator %(Pressure a, double b) => new(a.m_value % b);
    public static Pressure operator %(Pressure a, Pressure b) => a % b.m_value;
    public static Pressure operator -(Pressure a, double b) => new(a.m_value - b);
    public static Pressure operator -(Pressure a, Pressure b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Pressure o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Pressure other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Pressure.Value"/> property is in <see cref="PressureUnit.Pascal"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(PressureUnit.Pascal, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(PressureUnit.Pascal, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(PressureUnit unit, double value)
      => unit switch
      {
        PressureUnit.Pascal => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(PressureUnit unit, double value)
      => unit switch
      {
        PressureUnit.Pascal => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, PressureUnit from, PressureUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(PressureUnit unit)
      => unit switch
      {
        PressureUnit.Pascal => 1,

        PressureUnit.Millibar => 100,
        PressureUnit.Bar => 100000,
        PressureUnit.Psi => 1 / (1290320000.0 / 8896443230521.0),

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(PressureUnit unit, bool preferPlural)
      => unit.ToString() + unit switch
      {
        PressureUnit.Psi => string.Empty, // No plural for these "useFullName".
        _ => GetUnitValue(unit).PluralStringSuffix()
      };

    public string GetUnitSymbol(PressureUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.PressureUnit.Millibar => "mbar",
        Quantities.PressureUnit.Bar => preferUnicode ? "\u3374" : "bar",
        //Quantities.PressureUnit.HectoPascal => preferUnicode ? "\u3371" : "hPa",
        //Quantities.PressureUnit.KiloPascal => preferUnicode ? "\u33AA" : "kPa",
        Quantities.PressureUnit.Pascal => preferUnicode ? "\u33A9" : "Pa",
        Quantities.PressureUnit.Psi => "psi",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(PressureUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PressureUnit unit = PressureUnit.Pascal, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
