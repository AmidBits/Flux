namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.PermeabilityUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.PermeabilityUnit.HenryPerMeter => "H/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum PermeabilityUnit
    {
      HenryPerMeter,
    }

    /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Force"/>
    public readonly record struct Permeability
      : System.IComparable, System.IComparable<Permeability>, IUnitValueQuantifiable<double, PermeabilityUnit>
    {
      public const PermeabilityUnit DefaultUnit = PermeabilityUnit.HenryPerMeter;

      private readonly double m_value;

      public Permeability(double value, PermeabilityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          PermeabilityUnit.HenryPerMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Permeability v) => v.m_value;
      public static explicit operator Permeability(double v) => new(v);

      public static bool operator <(Permeability a, Permeability b) => a.CompareTo(b) < 0;
      public static bool operator <=(Permeability a, Permeability b) => a.CompareTo(b) <= 0;
      public static bool operator >(Permeability a, Permeability b) => a.CompareTo(b) > 0;
      public static bool operator >=(Permeability a, Permeability b) => a.CompareTo(b) >= 0;

      public static Permeability operator -(Permeability v) => new(-v.m_value);
      public static Permeability operator +(Permeability a, double b) => new(a.m_value + b);
      public static Permeability operator +(Permeability a, Permeability b) => a + b.m_value;
      public static Permeability operator /(Permeability a, double b) => new(a.m_value / b);
      public static Permeability operator /(Permeability a, Permeability b) => a / b.m_value;
      public static Permeability operator *(Permeability a, double b) => new(a.m_value * b);
      public static Permeability operator *(Permeability a, Permeability b) => a * b.m_value;
      public static Permeability operator %(Permeability a, double b) => new(a.m_value % b);
      public static Permeability operator %(Permeability a, Permeability b) => a % b.m_value;
      public static Permeability operator -(Permeability a, double b) => new(a.m_value - b);
      public static Permeability operator -(Permeability a, Permeability b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Permeability o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Permeability other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(PermeabilityUnit unit)
        => unit switch
        {
          PermeabilityUnit.HenryPerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PermeabilityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
