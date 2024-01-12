namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ElectricalResistanceUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.ElectricalResistanceUnit.Ohm => preferUnicode ? "\u2126" : "ohm",
        Units.ElectricalResistanceUnit.KiloOhm => preferUnicode ? "\u33C0" : "kiloohm",
        Units.ElectricalResistanceUnit.MegaOhm => preferUnicode ? "\u33C1" : "megaohm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ElectricalResistanceUnit
    {
      /// <summary>Ohm.</summary>
      Ohm,
      /// <summary>Kilo-Ohm.</summary>
      KiloOhm,
      /// <summary>Mega-Ohm.</summary>
      MegaOhm,
    }

    /// <summary>Electric resistance, unit of Ohm.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/>
    public readonly record struct ElectricalResistance
      : System.IComparable, System.IComparable<ElectricalResistance>, IUnitValueQuantifiable<double, ElectricalResistanceUnit>
    {
      public const ElectricalResistanceUnit DefaultUnit = ElectricalResistanceUnit.Ohm;

      public static ElectricalResistance VonKlitzing
        => new(25812.80745); // 25812.80745;

      private readonly double m_value;

      public ElectricalResistance(double value, ElectricalResistanceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ElectricalResistanceUnit.Ohm => value,
          ElectricalResistanceUnit.KiloOhm => value * 1000,
          ElectricalResistanceUnit.MegaOhm => value * 1000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public ElectricalConductance ToElectricalConductance()
        => new(1 / m_value);

      #region Static methods
      /// <summary>Creates a new ElectricResistance instance from the specified voltage and current.</summary>
      /// <param name="voltage"></param>
      /// <param name="current"></param>
      public static ElectricalResistance From(Voltage voltage, ElectricCurrent current)
        => new(voltage.Value / current.Value);

      /// <summary>Converts resistor values as if in parallel configuration.</summary>
      public static ElectricalResistance FromParallelResistors(params double[] resistors)
      {
        var sum = 0.0;
        foreach (var resistor in resistors)
          sum += 1 / resistor;
        return (ElectricalResistance)(1 / sum);
      }

      /// <summary>Converts resistor values as if in serial configuration.</summary>
      public static ElectricalResistance FromSerialResistors(params double[] resistors)
      {
        var sum = 0.0;
        foreach (var resistor in resistors)
          sum += resistor;
        return (ElectricalResistance)sum;
      }
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(ElectricalResistance v) => v.m_value;
      public static explicit operator ElectricalResistance(double v) => new(v);

      public static bool operator <(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) < 0;
      public static bool operator <=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) <= 0;
      public static bool operator >(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) > 0;
      public static bool operator >=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) >= 0;

      public static ElectricalResistance operator -(ElectricalResistance v) => new(-v.m_value);
      public static ElectricalResistance operator +(ElectricalResistance a, double b) => new(a.m_value + b);
      public static ElectricalResistance operator +(ElectricalResistance a, ElectricalResistance b) => a + b.m_value;
      public static ElectricalResistance operator /(ElectricalResistance a, double b) => new(a.m_value / b);
      public static ElectricalResistance operator /(ElectricalResistance a, ElectricalResistance b) => a / b.m_value;
      public static ElectricalResistance operator *(ElectricalResistance a, double b) => new(a.m_value * b);
      public static ElectricalResistance operator *(ElectricalResistance a, ElectricalResistance b) => a * b.m_value;
      public static ElectricalResistance operator %(ElectricalResistance a, double b) => new(a.m_value % b);
      public static ElectricalResistance operator %(ElectricalResistance a, ElectricalResistance b) => a % b.m_value;
      public static ElectricalResistance operator -(ElectricalResistance a, double b) => new(a.m_value - b);
      public static ElectricalResistance operator -(ElectricalResistance a, ElectricalResistance b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is ElectricalResistance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(ElectricalResistance other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ElectricalResistanceUnit unit)
        => unit switch
        {
          ElectricalResistanceUnit.Ohm => m_value,
          ElectricalResistanceUnit.KiloOhm => m_value / 1000,
          ElectricalResistanceUnit.MegaOhm => m_value / 1000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ElectricalResistanceUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
