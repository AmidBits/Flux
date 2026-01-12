namespace Flux.Units
{
  /// <summary>
  /// <para>Torque, unit of newton meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Torque"/></para>
  /// </summary>
  public readonly record struct Torque
    : System.IComparable, System.IComparable<Torque>, System.IFormattable, ISiUnitValueQuantifiable<double, TorqueUnit>
  {
    private readonly double m_value;

    public Torque(double value, TorqueUnit unit = TorqueUnit.NewtonMeter) => m_value = ConvertFromUnit(unit, value);

    public Torque(MetricPrefix prefix, double newtonMeter) => m_value = prefix.ConvertPrefix(newtonMeter, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + TorqueUnit.NewtonMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(TorqueUnit unit, double value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(TorqueUnit unit, double value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, TorqueUnit from, TorqueUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(TorqueUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TorqueUnit unit = TorqueUnit.NewtonMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(Numbers.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

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
