namespace Flux.Quantities
{
  public enum TorqueUnit
  {
    /// <summary>This is the default unit for <see cref="Torque"/>.</summary>
    NewtonMeter,
  }

  /// <summary>Torque unit of newton meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Torque"/>
  public readonly record struct Torque
    : System.IComparable, System.IComparable<Torque>, System.IFormattable, ISiUnitValueQuantifiable<double, TorqueUnit>
  {
    private readonly double m_value;

    public Torque(double value, TorqueUnit unit = TorqueUnit.NewtonMeter) => m_value = ConvertFromUnit(unit, value);

    public Torque(MetricPrefix prefix, double newtonMeter) => m_value = prefix.ConvertTo(newtonMeter, MetricPrefix.Unprefixed);

    public Torque(Energy energy, Angle angle) : this(energy.Value / angle.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Torque a, Torque b) => a.CompareTo(b) < 0;
    public static bool operator >(Torque a, Torque b) => a.CompareTo(b) > 0;
    public static bool operator <=(Torque a, Torque b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Torque a, Torque b) => a.CompareTo(b) >= 0;

    public static Torque operator -(Torque v) => new(-v.m_value);
    public static Torque operator *(Torque a, Torque b) => new(a.m_value * b.m_value);
    public static Torque operator /(Torque a, Torque b) => new(a.m_value / b.m_value);
    public static Torque operator %(Torque a, Torque b) => new(a.m_value % b.m_value);
    public static Torque operator +(Torque a, Torque b) => new(a.m_value + b.m_value);
    public static Torque operator -(Torque a, Torque b) => new(a.m_value - b.m_value);
    public static Torque operator *(Torque a, double b) => new(a.m_value * b);
    public static Torque operator /(Torque a, double b) => new(a.m_value / b);
    public static Torque operator %(Torque a, double b) => new(a.m_value % b);
    public static Torque operator +(Torque a, double b) => new(a.m_value + b);
    public static Torque operator -(Torque a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Torque o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Torque other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(TorqueUnit.NewtonMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(TorqueUnit.NewtonMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(TorqueUnit.NewtonMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(TorqueUnit unit, double value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(TorqueUnit unit, double value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, TorqueUnit from, TorqueUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(TorqueUnit unit)
      => unit switch
      {
        TorqueUnit.NewtonMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(TorqueUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(TorqueUnit unit, bool preferUnicode)
      => unit switch
      {
        TorqueUnit.NewtonMeter => preferUnicode ? "N\u22C5m" : "N·m",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TorqueUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TorqueUnit unit = TorqueUnit.NewtonMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Torque.Value"/> property is in <see cref="TorqueUnit.NewtonMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
