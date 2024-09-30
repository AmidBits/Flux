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
    : System.IComparable, System.IComparable<Torque>, System.IFormattable, IUnitValueQuantifiable<double, TorqueUnit>
  {
    private readonly double m_value;

    public Torque(double value, TorqueUnit unit = TorqueUnit.NewtonMeter) => m_value = ConvertFromUnit(unit, value);

    public Torque(Energy energy, Angle angle) : this(energy.Value / angle.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Torque a, Torque b) => a.CompareTo(b) < 0;
    public static bool operator <=(Torque a, Torque b) => a.CompareTo(b) <= 0;
    public static bool operator >(Torque a, Torque b) => a.CompareTo(b) > 0;
    public static bool operator >=(Torque a, Torque b) => a.CompareTo(b) >= 0;

    public static Torque operator -(Torque v) => new(-v.m_value);
    public static Torque operator +(Torque a, double b) => new(a.m_value + b);
    public static Torque operator +(Torque a, Torque b) => a + b.m_value;
    public static Torque operator /(Torque a, double b) => new(a.m_value / b);
    public static Torque operator /(Torque a, Torque b) => a / b.m_value;
    public static Torque operator *(Torque a, double b) => new(a.m_value * b);
    public static Torque operator *(Torque a, Torque b) => a * b.m_value;
    public static Torque operator %(Torque a, double b) => new(a.m_value % b);
    public static Torque operator %(Torque a, Torque b) => a % b.m_value;
    public static Torque operator -(Torque a, double b) => new(a.m_value - b);
    public static Torque operator -(Torque a, Torque b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Torque o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Torque other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(TorqueUnit.NewtonMeter, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Torque.Value"/> property is in <see cref="TorqueUnit.NewtonMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(TorqueUnit unit, double value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(TorqueUnit unit, double value)
      => unit switch
      {
        TorqueUnit.NewtonMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(TorqueUnit unit)
      => unit switch
      {
        TorqueUnit.NewtonMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(TorqueUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(TorqueUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.TorqueUnit.NewtonMeter => preferUnicode ? "N\u22C5m" : "N·m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TorqueUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TorqueUnit unit = TorqueUnit.NewtonMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
