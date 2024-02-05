namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.TorqueUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.TorqueUnit.NewtonMeter => options.PreferUnicode ? "N\u22C5m" : "N·m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      ///  <para>The unit of the <see cref="Torque.Value"/> property is in <see cref="TorqueUnit.NewtonMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(TorqueUnit unit)
        => unit switch
        {
          TorqueUnit.NewtonMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(TorqueUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
