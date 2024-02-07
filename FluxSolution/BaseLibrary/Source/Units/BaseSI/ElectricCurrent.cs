namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ElectricCurrentUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.ElectricCurrentUnit.Ampere => "A",
        Units.ElectricCurrentUnit.Milliampere => options.PreferUnicode ? "\u3383" : "mA",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ElectricCurrentUnit
    {
      /// <summary>This is the default unit for <see cref="ElectricCurrent"/>.</summary>
      Ampere,
      Milliampere,
    }

    /// <summary>Electric current. SI unit of ampere. This is a base quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Electric_current"/>
    public readonly record struct ElectricCurrent
      : System.IComparable, System.IComparable<ElectricCurrent>, System.IFormattable, IUnitValueQuantifiable<double, ElectricCurrentUnit>
    {
      private readonly double m_value;

      public ElectricCurrent(double value, ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere)
        => m_value = unit switch
        {
          ElectricCurrentUnit.Ampere => value,
          ElectricCurrentUnit.Milliampere => value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
      /// <param name="power"></param>
      /// <param name="voltage"></param>
      public static ElectricCurrent From(Power power, Voltage voltage) => new(power.Value / voltage.Value);
      /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
      /// <param name="voltage"></param>
      /// <param name="resistance"></param>
      public static ElectricCurrent From(Voltage voltage, ElectricalResistance resistance) => new(voltage.Value / resistance.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(ElectricCurrent v) => v.m_value;
      public static explicit operator ElectricCurrent(double v) => new(v);

      public static bool operator <(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) < 0;
      public static bool operator <=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) <= 0;
      public static bool operator >(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) > 0;
      public static bool operator >=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) >= 0;

      public static ElectricCurrent operator -(ElectricCurrent v) => new(-v.m_value);
      public static ElectricCurrent operator +(ElectricCurrent a, double b) => new(a.m_value + b);
      public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b) => a + b.m_value;
      public static ElectricCurrent operator /(ElectricCurrent a, double b) => new(a.m_value / b);
      public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b) => a / b.m_value;
      public static ElectricCurrent operator *(ElectricCurrent a, double b) => new(a.m_value * b);
      public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b) => a * b.m_value;
      public static ElectricCurrent operator %(ElectricCurrent a, double b) => new(a.m_value % b);
      public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b) => a % b.m_value;
      public static ElectricCurrent operator -(ElectricCurrent a, double b) => new(a.m_value - b);
      public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is ElectricCurrent o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(ElectricCurrent other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(ElectricCurrentUnit.Ampere, options);

      /// <summary>
      /// <para>The unit of the <see cref="ElectricCurrent.Value"/> property is in <see cref="ElectricCurrentUnit.Ampere"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ElectricCurrentUnit unit)
        => unit switch
        {
          ElectricCurrentUnit.Milliampere => m_value * 1000,
          ElectricCurrentUnit.Ampere => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ElectricCurrentUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
