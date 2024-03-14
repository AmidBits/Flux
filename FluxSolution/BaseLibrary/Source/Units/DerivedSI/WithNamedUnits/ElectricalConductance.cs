namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ElectricalConductanceUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.ElectricalConductanceUnit.Siemens => preferUnicode ? "\u2127" : "S",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ElectricalConductanceUnit
    {
      /// <summary>This is the default unit for <see cref="CurrentDensity"/>. Siemens = (1/ohm).</summary>
      Siemens,
    }

    /// <summary>Electrical conductance, unit of Siemens.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/>
    public readonly record struct ElectricalConductance
      : System.IComparable, System.IComparable<ElectricalConductance>, System.IFormattable, IUnitValueQuantifiable<double, ElectricalConductanceUnit>
    {
      private readonly double m_value;

      public ElectricalConductance(double value, ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens)
        => m_value = unit switch
        {
          ElectricalConductanceUnit.Siemens => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public ElectricalResistance ToElectricResistance()
        => new(1 / m_value);

      #region Static methods
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) < 0;
      public static bool operator <=(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) <= 0;
      public static bool operator >(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) > 0;
      public static bool operator >=(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) >= 0;

      public static ElectricalConductance operator -(ElectricalConductance v) => new(-v.m_value);
      public static ElectricalConductance operator +(ElectricalConductance a, double b) => new(a.m_value + b);
      public static ElectricalConductance operator +(ElectricalConductance a, ElectricalConductance b) => a + b.m_value;
      public static ElectricalConductance operator /(ElectricalConductance a, double b) => new(a.m_value / b);
      public static ElectricalConductance operator /(ElectricalConductance a, ElectricalConductance b) => a / b.m_value;
      public static ElectricalConductance operator *(ElectricalConductance a, double b) => new(a.m_value * b);
      public static ElectricalConductance operator *(ElectricalConductance a, ElectricalConductance b) => a * b.m_value;
      public static ElectricalConductance operator %(ElectricalConductance a, double b) => new(a.m_value % b);
      public static ElectricalConductance operator %(ElectricalConductance a, ElectricalConductance b) => a % b.m_value;
      public static ElectricalConductance operator -(ElectricalConductance a, double b) => new(a.m_value - b);
      public static ElectricalConductance operator -(ElectricalConductance a, ElectricalConductance b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is ElectricalConductance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(ElectricalConductance other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(ElectricalConductanceUnit.Siemens, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="ElectricalConductance.Value"/> property is in <see cref="ElectricalConductanceUnit.Siemens"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ElectricalConductanceUnit unit)
        => unit switch
        {
          ElectricalConductanceUnit.Siemens => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
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
