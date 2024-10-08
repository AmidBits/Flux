namespace Flux.Quantities
{
  public enum CapacitanceUnit
  {
    /// <summary>This is the default unit for <see cref="Capacitance"/>.</summary>
    Farad,
    MicroFarad,
    NanoFarad,
    PicoFarad,
  }

  /// <summary>Electrical capacitance unit of Farad.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Capacitance"/>
  public readonly record struct Capacitance
    : System.IComparable, System.IComparable<Capacitance>, System.IFormattable, ISiUnitValueQuantifiable<double, CapacitanceUnit>
  {
    private readonly double m_value;

    public Capacitance(double value, CapacitanceUnit unit = CapacitanceUnit.Farad)
      => m_value = unit switch
      {
        CapacitanceUnit.Farad => value,
        CapacitanceUnit.MicroFarad => value * 1000000,
        CapacitanceUnit.NanoFarad => value * 1000000000,
        CapacitanceUnit.PicoFarad => value * 1000000000000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

    public static bool operator <(Capacitance a, Capacitance b) => a.CompareTo(b) < 0;
    public static bool operator <=(Capacitance a, Capacitance b) => a.CompareTo(b) <= 0;
    public static bool operator >(Capacitance a, Capacitance b) => a.CompareTo(b) > 0;
    public static bool operator >=(Capacitance a, Capacitance b) => a.CompareTo(b) >= 0;

    public static Capacitance operator -(Capacitance v) => new(-v.m_value);
    public static Capacitance operator +(Capacitance a, double b) => new(a.m_value + b);
    public static Capacitance operator +(Capacitance a, Capacitance b) => a + b.m_value;
    public static Capacitance operator /(Capacitance a, double b) => new(a.m_value / b);
    public static Capacitance operator /(Capacitance a, Capacitance b) => a / b.m_value;
    public static Capacitance operator *(Capacitance a, double b) => new(a.m_value * b);
    public static Capacitance operator *(Capacitance a, Capacitance b) => a * b.m_value;
    public static Capacitance operator %(Capacitance a, double b) => new(a.m_value % b);
    public static Capacitance operator %(Capacitance a, Capacitance b) => a % b.m_value;
    public static Capacitance operator -(Capacitance a, double b) => new(a.m_value - b);
    public static Capacitance operator -(Capacitance a, Capacitance b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Capacitance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Capacitance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(CapacitanceUnit.Farad, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(CapacitanceUnit.Farad, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Capacitance.Value"/> property is in <see cref="CapacitanceUnit.Farad"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(CapacitanceUnit unit, double value)
      => unit switch
      {
        CapacitanceUnit.Farad => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(CapacitanceUnit unit, double value)
      => unit switch
      {
        CapacitanceUnit.Farad => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, CapacitanceUnit from, CapacitanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(CapacitanceUnit unit)
      => unit switch
      {
        CapacitanceUnit.Farad => 1,

        CapacitanceUnit.MicroFarad => 1000000,
        CapacitanceUnit.NanoFarad => 1000000000,
        CapacitanceUnit.PicoFarad => 1000000000000,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(CapacitanceUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(CapacitanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.CapacitanceUnit.Farad => "F",
        Quantities.CapacitanceUnit.MicroFarad => preferUnicode ? "\u338C" : "\u00B5F",
        Quantities.CapacitanceUnit.NanoFarad => preferUnicode ? "\u338B" : "nF",
        Quantities.CapacitanceUnit.PicoFarad => preferUnicode ? "\u338A" : "pF",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(CapacitanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(CapacitanceUnit unit = CapacitanceUnit.Farad, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
