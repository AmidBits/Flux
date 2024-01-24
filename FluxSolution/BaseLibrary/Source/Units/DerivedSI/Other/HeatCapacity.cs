namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.HeatCapacityUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.HeatCapacityUnit.JoulePerKelvin => "J/K",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum HeatCapacityUnit
    {
      JoulePerKelvin,
    }

    /// <summary>Heat capacity, unit of Joule per Kelvin.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Heat_capacity"/>
    public readonly record struct HeatCapacity
      : System.IComparable, System.IComparable<HeatCapacity>, System.IFormattable, IUnitValueQuantifiable<double, HeatCapacityUnit>
    {
      public const HeatCapacityUnit DefaultUnit = HeatCapacityUnit.JoulePerKelvin;

      public static readonly HeatCapacity BoltzmannConstant = new(1.380649e-23);

      private readonly double m_value;

      public HeatCapacity(double value, HeatCapacityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          HeatCapacityUnit.JoulePerKelvin => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(HeatCapacity v) => v.m_value;
      public static explicit operator HeatCapacity(double v) => new(v);

      public static bool operator <(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) < 0;
      public static bool operator <=(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) <= 0;
      public static bool operator >(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) > 0;
      public static bool operator >=(HeatCapacity a, HeatCapacity b) => a.CompareTo(b) >= 0;

      public static HeatCapacity operator -(HeatCapacity v) => new(-v.m_value);
      public static HeatCapacity operator +(HeatCapacity a, double b) => new(a.m_value + b);
      public static HeatCapacity operator +(HeatCapacity a, HeatCapacity b) => a + b.m_value;
      public static HeatCapacity operator /(HeatCapacity a, double b) => new(a.m_value / b);
      public static HeatCapacity operator /(HeatCapacity a, HeatCapacity b) => a / b.m_value;
      public static HeatCapacity operator *(HeatCapacity a, double b) => new(a.m_value * b);
      public static HeatCapacity operator *(HeatCapacity a, HeatCapacity b) => a * b.m_value;
      public static HeatCapacity operator %(HeatCapacity a, double b) => new(a.m_value % b);
      public static HeatCapacity operator %(HeatCapacity a, HeatCapacity b) => a % b.m_value;
      public static HeatCapacity operator -(HeatCapacity a, double b) => new(a.m_value - b);
      public static HeatCapacity operator -(HeatCapacity a, HeatCapacity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is HeatCapacity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(HeatCapacity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(HeatCapacityUnit unit)
        => unit switch
        {
          HeatCapacityUnit.JoulePerKelvin => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(HeatCapacityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
