namespace Flux
{
  public static partial class UnitsExtensionMethods
  {
    public static string GetUnitString(this Units.VoltageUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.VoltageUnit.Volt => "V",
        Units.VoltageUnit.KiloVolt => preferUnicode ? "\u33B8" : "kV",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum VoltageUnit
    {
      /// <summary>Volt.</summary>
      Volt,
      KiloVolt,
    }

    /// <summary>Voltage unit of volt.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Voltage"/>
    public readonly record struct Voltage
      : System.IComparable, System.IComparable<Voltage>, IUnitQuantifiable<double, VoltageUnit>
    {
      public const VoltageUnit DefaultUnit = VoltageUnit.Volt;

      private readonly double m_value;

      public Voltage(double value, VoltageUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          VoltageUnit.Volt => value,
          VoltageUnit.KiloVolt => value * 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      /// <summary>Creates a new Voltage instance from the specified current and resistance.</summary>
      /// <param name="current"></param>
      /// <param name="resistance"></param>

      public static Voltage From(ElectricCurrent current, ElectricalResistance resistance)
        => new(current.Value * resistance.Value);
      /// <summary>Creates a new Voltage instance from the specified power and current.</summary>
      /// <param name="power"></param>
      /// <param name="current"></param>

      public static Voltage From(Power power, ElectricCurrent current)
        => new(power.Value / current.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Voltage v) => v.m_value;
      public static explicit operator Voltage(double v) => new(v);

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

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(VoltageUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(VoltageUnit unit = DefaultUnit)
        => unit switch
        {
          VoltageUnit.Volt => m_value,
          VoltageUnit.KiloVolt => m_value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
