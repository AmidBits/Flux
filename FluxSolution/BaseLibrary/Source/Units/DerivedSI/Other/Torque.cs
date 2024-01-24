namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.TorqueUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.TorqueUnit.NewtonMeter => preferUnicode ? "N\u22C5m" : "N·m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum TorqueUnit
    {
      NewtonMeter,
    }

    /// <summary>Torque unit of newton meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Torque"/>
    public readonly record struct Torque
      : System.IComparable, System.IComparable<Torque>, System.IFormattable, IUnitValueQuantifiable<double, TorqueUnit>
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
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(TorqueUnit unit)
        => unit switch
        {
          TorqueUnit.NewtonMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(TorqueUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
