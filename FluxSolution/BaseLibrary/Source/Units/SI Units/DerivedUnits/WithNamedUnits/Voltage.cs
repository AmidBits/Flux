namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.VoltageUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.VoltageUnit.Volt => "V",
        Quantities.VoltageUnit.KiloVolt => preferUnicode ? "\u33B8" : "kV",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
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
      : System.IComparable, System.IComparable<Voltage>, System.IFormattable, ISiPrefixValueQuantifiable<double, VoltageUnit>
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
      public static Voltage From(ElectricCurrent current, ElectricalResistance resistance) => new(current.Value * resistance.Value);

      /// <summary>Creates a new Voltage instance from the specified power and current.</summary>
      /// <param name="power"></param>
      /// <param name="current"></param>
      public static Voltage From(Power power, ElectricCurrent current) => new(power.Value / current.Value);

      #endregion Static methods

      #region Overloaded operators

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

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(VoltageUnit.Volt, format, formatProvider);

      // ISiUnitValueQuantifiable<>
      public VoltageUnit BaseUnit => VoltageUnit.Volt;

      public VoltageUnit UnprefixedUnit => VoltageUnit.Volt;

      public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(UnprefixedUnit, preferUnicode, useFullName);

      public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToSiPrefixValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(UnprefixedUnit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      ///  <para>The unit of the <see cref="Voltage.Value"/> property is in <see cref="VoltageUnit.Volt"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(VoltageUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(VoltageUnit unit)
        => unit switch
        {
          VoltageUnit.Volt => m_value,
          VoltageUnit.KiloVolt => m_value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(VoltageUnit unit = VoltageUnit.Volt, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}