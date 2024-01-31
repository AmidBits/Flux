namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ElectricalConductanceUnit unit, QuantifiableValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.ElectricalConductanceUnit.Siemens => options.PreferUnicode ? "\u2127" : "S",
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
      : System.IComparable, System.IComparable<ElectricalConductance>, IUnitValueQuantifiable<double, ElectricalConductanceUnit>
    {
      public const ElectricalConductanceUnit DefaultUnit = ElectricalConductanceUnit.Siemens;

      private readonly double m_value;

      public ElectricalConductance(double value, ElectricalConductanceUnit unit = DefaultUnit)
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
      public static explicit operator double(ElectricalConductance v) => v.m_value;
      public static explicit operator ElectricalConductance(double v) => new(v);

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

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options = default) => ToUnitValueString(DefaultUnit, options);

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

      public string ToUnitValueString(ElectricalConductanceUnit unit = DefaultUnit, QuantifiableValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
