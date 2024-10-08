namespace Flux.Quantities
{
  public enum VoltageUnit
  {
    /// <summary>This is the default unit for <see cref="Voltage"/>.</summary>
    Volt,
    //KiloVolt,
  }

  /// <summary>Voltage unit of volt.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Voltage"/>
  public readonly record struct Voltage
    : System.IComparable, System.IComparable<Voltage>, System.IFormattable, ISiUnitValueQuantifiable<double, VoltageUnit>
  {
    private readonly double m_value;

    public Voltage(double value, VoltageUnit unit = VoltageUnit.Volt) => m_value = ConvertFromUnit(unit, value);

    #region Static methods

    /// <summary>Creates a new Voltage instance from the specified current and resistance.</summary>
    /// <param name="current"></param>
    /// <param name="resistance"></param>
    public static Voltage From(ElectricCurrent current, ElectricalResistance resistance) => new(current.Value * resistance.Value);

    /// <summary>Creates a new Voltage instance from the specified power and current.</summary>
    /// <param name="power"></param>
    /// <param name="current"></param>
    public static Voltage From(Power power, ElectricCurrent current) => new(power.Value / current.Value);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Voltage a, Voltage b) => a.CompareTo(b) < 0;
    public static bool operator <=(Voltage a, Voltage b) => a.CompareTo(b) <= 0;
    public static bool operator >(Voltage a, Voltage b) => a.CompareTo(b) > 0;
    public static bool operator >=(Voltage a, Voltage b) => a.CompareTo(b) >= 0;

    public static Voltage operator -(Voltage v) => new(-v.m_value);
    public static Voltage operator +(Voltage a, double b) => new(a.m_value + b);
    public static Voltage operator +(Voltage a, Voltage b) => a + b.m_value;
    public static Voltage operator /(Voltage a, double b) => new(a.m_value / b);
    public static Voltage operator /(Voltage a, Voltage b) => a / b.m_value;
    public static Voltage operator *(Voltage a, double b) => new(a.m_value * b);
    public static Voltage operator *(Voltage a, Voltage b) => a * b.m_value;
    public static Voltage operator %(Voltage a, double b) => new(a.m_value % b);
    public static Voltage operator %(Voltage a, Voltage b) => a % b.m_value;
    public static Voltage operator -(Voltage a, double b) => new(a.m_value - b);
    public static Voltage operator -(Voltage a, Voltage b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Voltage o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Voltage other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Voltage.Value"/> property is in <see cref="VoltageUnit.Volt"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(VoltageUnit.Volt, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix switch
    {
      MetricPrefix.Kilo => preferUnicode ? "\u33B8" : "k" + GetUnitSymbol(VoltageUnit.Volt, preferUnicode),

      _ => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(VoltageUnit.Volt, preferUnicode),
    };

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(VoltageUnit unit, double value)
      => unit switch
      {
        VoltageUnit.Volt => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(VoltageUnit unit, double value)
      => unit switch
      {
        VoltageUnit.Volt => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, VoltageUnit from, VoltageUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(VoltageUnit unit)
      => unit switch
      {
        VoltageUnit.Volt => 1,

        //VoltageUnit.KiloVolt => 1000,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(VoltageUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(VoltageUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.VoltageUnit.Volt => "V",

        //Quantities.VoltageUnit.KiloVolt => preferUnicode ? "\u33B8" : "kV",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };


    public double GetUnitValue(VoltageUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(VoltageUnit unit = VoltageUnit.Volt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
