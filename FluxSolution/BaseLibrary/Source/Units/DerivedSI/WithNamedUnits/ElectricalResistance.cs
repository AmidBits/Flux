namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ElectricalResistanceUnit unit, bool preferUnicode, bool useFullName = false)
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
      /// <summary>This is the default unit for <see cref="ElectricalResistance"/>.</summary>
      Ohm,
      KiloOhm,
      MegaOhm,
    }

    /// <summary>Electric resistance, unit of Ohm.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/>
    public readonly record struct ElectricalResistance
      : System.IComparable, System.IComparable<ElectricalResistance>, System.IFormattable, IUnitValueQuantifiable<double, ElectricalResistanceUnit>
    {
      public static ElectricalResistance VonKlitzingConstant => new(25812.80745); // 25812.80745;

      private readonly double m_value;

      public ElectricalResistance(double value, ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm)
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
        return new(1 / sum);
      }

      /// <summary>Converts resistor values as if in serial configuration.</summary>
      public static ElectricalResistance FromSerialResistors(params double[] resistors)
      {
        var sum = 0.0;
        foreach (var resistor in resistors)
          sum += resistor;
        return new(sum);
      }
      #endregion Static methods

      #region Overloaded operators

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

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(ElectricalResistanceUnit.Ohm, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="ElectricalResistance.Value"/> property is in <see cref="ElectricalResistanceUnit.Ohm"/>.</para>
      /// </summary>
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

      public string ToUnitValueString(ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
