namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
    public static string GetUnitString(this Units.TorqueUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.TorqueUnit.NewtonMeter => preferUnicode ? "N\u22C5m" : "N·m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }
}

namespace Flux.Units
{
  public enum TorqueUnit
  {
    NewtonMeter,
  }

  /// <summary>Torque unit of newton meter.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Torque"/>
  public readonly record struct Torque
    : System.IComparable, System.IComparable<Torque>, System.IFormattable, IUnitQuantifiable<double, TorqueUnit>
  {
    public const TorqueUnit DefaultUnit = TorqueUnit.NewtonMeter;

    private readonly double m_value;

    public Torque(double value, TorqueUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        TorqueUnit.NewtonMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods

    public static Torque From(Energy energy, Angle angle)
      => new(energy.Value / angle.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Torque v) => v.m_value;
    public static explicit operator Torque(double v) => new(v);

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
    public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
    public double Value { get => m_value; init => m_value = value; }

    // IUnitQuantifiable<>
    public string ToUnitString(TorqueUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    public double ToUnitValue(TorqueUnit unit = DefaultUnit)
      => unit switch
      {
        TorqueUnit.NewtonMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #endregion Implemented interfaces

    public override string ToString() => ToQuantityString();
  }
}
