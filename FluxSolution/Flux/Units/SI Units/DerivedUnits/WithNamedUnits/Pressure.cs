namespace Flux.Units
{
  /// <summary>
  /// <para>Pressure, unit of Pascal. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Pressure"/></para>
  /// </summary>
  public readonly record struct Pressure
    : System.IComparable, System.IComparable<Pressure>, System.IFormattable, ISiUnitValueQuantifiable<double, PressureUnit>
  {
    private readonly double m_value;

    public Pressure(double value, PressureUnit unit = PressureUnit.Pascal) => m_value = ConvertFromUnit(unit, value);

    public Pressure(MetricPrefix prefix, double pascal) => m_value = prefix.ChangePrefix(pascal, MetricPrefix.Unprefixed);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Pressure a, Pressure b) => a.CompareTo(b) < 0;
    public static bool operator >(Pressure a, Pressure b) => a.CompareTo(b) > 0;
    public static bool operator <=(Pressure a, Pressure b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Pressure a, Pressure b) => a.CompareTo(b) >= 0;

    public static Pressure operator -(Pressure v) => new(-v.m_value);
    public static Pressure operator *(Pressure a, Pressure b) => new(a.m_value * b.m_value);
    public static Pressure operator /(Pressure a, Pressure b) => new(a.m_value / b.m_value);
    public static Pressure operator %(Pressure a, Pressure b) => new(a.m_value % b.m_value);
    public static Pressure operator +(Pressure a, Pressure b) => new(a.m_value + b.m_value);
    public static Pressure operator -(Pressure a, Pressure b) => new(a.m_value - b.m_value);
    public static Pressure operator *(Pressure a, double b) => new(a.m_value * b);
    public static Pressure operator /(Pressure a, double b) => new(a.m_value / b);
    public static Pressure operator %(Pressure a, double b) => new(a.m_value % b);
    public static Pressure operator +(Pressure a, double b) => new(a.m_value + b);
    public static Pressure operator -(Pressure a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Pressure o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Pressure other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(PressureUnit.Pascal, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(PressureUnit.Pascal, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetSiUnitValue(prefix);

      return value.ToSiFormattedString(format, formatProvider)
        + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString()
        + (fullName ? GetSiUnitName(prefix, value.IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));
    }

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(PressureUnit unit, double value)
      => unit switch
      {
        PressureUnit.Pascal => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(PressureUnit unit, double value)
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

    public static string GetUnitName(PressureUnit unit, bool preferPlural)
      => unit switch
      {
        PressureUnit.Psi => unit.ToString(), // No plural for these "useFullName".
        _ => unit.ToString().ToPluralUnitName(preferPlural)
      };

    public static string GetUnitSymbol(PressureUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.PressureUnit.Millibar => "mbar",
        Units.PressureUnit.Bar => preferUnicode ? "\u3374" : "bar",
        //Quantities.PressureUnit.HectoPascal => preferUnicode ? "\u3371" : "hPa",
        //Quantities.PressureUnit.KiloPascal => preferUnicode ? "\u33AA" : "kPa",
        Units.PressureUnit.Pascal => preferUnicode ? "\u33A9" : "Pa",
        Units.PressureUnit.Psi => "psi",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(PressureUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PressureUnit unit = PressureUnit.Pascal, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? GetUnitName(unit, value.IsConsideredPlural()) : GetUnitSymbol(unit, false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Pressure.Value"/> property is in <see cref="PressureUnit.Pascal"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
