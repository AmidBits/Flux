namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.AbsorbedDoseUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.AbsorbedDoseUnit.Gray => "Gy",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AbsorbedDoseUnit
    {
      Gray,
    }

    /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Force"/>
    public readonly record struct AbsorbedDose
      : System.IComparable, System.IComparable<AbsorbedDose>, IUnitValueQuantifiable<double, AbsorbedDoseUnit>
    {
      public const AbsorbedDoseUnit DefaultUnit = AbsorbedDoseUnit.Gray;

      private readonly double m_value;

      public AbsorbedDose(double value, AbsorbedDoseUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AbsorbedDoseUnit.Gray => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(AbsorbedDose v) => v.m_value;
      public static explicit operator AbsorbedDose(double v) => new(v);

      public static bool operator <(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) < 0;
      public static bool operator <=(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) <= 0;
      public static bool operator >(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) > 0;
      public static bool operator >=(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) >= 0;

      public static AbsorbedDose operator -(AbsorbedDose v) => new(-v.m_value);
      public static AbsorbedDose operator +(AbsorbedDose a, double b) => new(a.m_value + b);
      public static AbsorbedDose operator +(AbsorbedDose a, AbsorbedDose b) => a + b.m_value;
      public static AbsorbedDose operator /(AbsorbedDose a, double b) => new(a.m_value / b);
      public static AbsorbedDose operator /(AbsorbedDose a, AbsorbedDose b) => a / b.m_value;
      public static AbsorbedDose operator *(AbsorbedDose a, double b) => new(a.m_value * b);
      public static AbsorbedDose operator *(AbsorbedDose a, AbsorbedDose b) => a * b.m_value;
      public static AbsorbedDose operator %(AbsorbedDose a, double b) => new(a.m_value % b);
      public static AbsorbedDose operator %(AbsorbedDose a, AbsorbedDose b) => a % b.m_value;
      public static AbsorbedDose operator -(AbsorbedDose a, double b) => new(a.m_value - b);
      public static AbsorbedDose operator -(AbsorbedDose a, AbsorbedDose b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AbsorbedDose o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AbsorbedDose other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AbsorbedDoseUnit unit)
        => unit switch
        {
          AbsorbedDoseUnit.Gray => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AbsorbedDoseUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
