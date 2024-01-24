namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.ElectricChargeUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.ElectricChargeUnit.Coulomb => "C",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ElectricChargeUnit
    {
      /// <summary>Coulomb.</summary>
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
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ElectricChargeUnit unit)
        => unit switch
        {
          ElectricChargeUnit.Coulomb => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ElectricChargeUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
