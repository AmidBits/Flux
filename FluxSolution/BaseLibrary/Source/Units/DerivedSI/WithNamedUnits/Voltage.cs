namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.VoltageUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.VoltageUnit.Volt => "V",
        Units.VoltageUnit.KiloVolt => options.PreferUnicode ? "\u33B8" : "kV",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum VoltageUnit
    {
      /// <summary>This is the default unit for <see cref="Voltage"/>.</summary>
      Volt,
      KiloVolt,
    }

    /// <summary>Voltage unit of volt.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Voltage"/>
    public readonly record struct Voltage
      : System.IComparable, System.IComparable<Voltage>, IUnitValueQuantifiable<double, VoltageUnit>
    {
      private readonly double m_value;

      public Voltage(double value, VoltageUnit unit = VoltageUnit.Volt)
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
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(VoltageUnit.Volt, options);

      /// <summary>
      ///  <para>The unit of the <see cref="Voltage.Value"/> property is in <see cref="VoltageUnit.Volt"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(VoltageUnit unit)
        => unit switch
        {
          VoltageUnit.Volt => m_value,
          VoltageUnit.KiloVolt => m_value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(VoltageUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
