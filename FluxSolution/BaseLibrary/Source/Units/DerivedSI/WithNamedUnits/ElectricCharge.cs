namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ElectricChargeUnit unit, QuantifiableValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.ElectricChargeUnit.Coulomb => "C",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ElectricChargeUnit
    {
      /// <summary>This is the default unit for <see cref="ElectricCharge"/>.</summary>
      Coulomb,
    }

    /// <summary>Electric charge unit of Coulomb.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Electric_charge"/>
    public readonly record struct ElectricCharge
      : System.IComparable, System.IComparable<ElectricCharge>, IUnitValueQuantifiable<double, ElectricChargeUnit>
    {
      public const ElectricChargeUnit DefaultUnit = ElectricChargeUnit.Coulomb;

      public static ElectricCharge ElementaryCharge => new(1.602176634e-19);

      private readonly double m_value;

      public ElectricCharge(double value, ElectricChargeUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ElectricChargeUnit.Coulomb => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(ElectricCharge v) => v.m_value;
      public static explicit operator ElectricCharge(double v) => new(v);

      public static bool operator <(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) < 0;
      public static bool operator <=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) <= 0;
      public static bool operator >(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) > 0;
      public static bool operator >=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) >= 0;

      public static ElectricCharge operator -(ElectricCharge v) => new(-v.m_value);
      public static ElectricCharge operator +(ElectricCharge a, double b) => new(a.m_value + b);
      public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b) => a + b.m_value;
      public static ElectricCharge operator /(ElectricCharge a, double b) => new(a.m_value / b);
      public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b) => a / b.m_value;
      public static ElectricCharge operator *(ElectricCharge a, double b) => new(a.m_value * b);
      public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b) => a * b.m_value;
      public static ElectricCharge operator %(ElectricCharge a, double b) => new(a.m_value % b);
      public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b) => a % b.m_value;
      public static ElectricCharge operator -(ElectricCharge a, double b) => new(a.m_value - b);
      public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is ElectricCharge o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(ElectricCharge other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options = default) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="ElectricCharge.Value"/> property is in <see cref="ElectricChargeUnit.Ohm"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ElectricChargeUnit unit)
        => unit switch
        {
          ElectricChargeUnit.Coulomb => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ElectricChargeUnit unit, QuantifiableValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
