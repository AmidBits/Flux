namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
    public static string GetUnitString(this Units.DynamicViscosityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.DynamicViscosityUnit.PascalSecond => preferUnicode ? "Pa\u22C5s" : "Pa·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }
}

namespace Flux.Units
{
  public enum DynamicViscosityUnit
  {
    PascalSecond,
  }

  /// <summary>Dynamic viscosity, unit of Pascal second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Dynamic_viscosity"/>
  public readonly record struct DynamicViscosity
    : System.IComparable, System.IComparable<DynamicViscosity>, System.IFormattable, IUnitQuantifiable<double, DynamicViscosityUnit>
  {
    public const DynamicViscosityUnit DefaultUnit = DynamicViscosityUnit.PascalSecond;

    private readonly double m_value;

    public DynamicViscosity(double value, DynamicViscosityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        DynamicViscosityUnit.PascalSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods

    public static DynamicViscosity From(Pressure pressure, Time time)
      => new(pressure.Value * time.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(DynamicViscosity v) => v.m_value;
    public static explicit operator DynamicViscosity(double v) => new(v);

    public static bool operator <(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) < 0;
    public static bool operator <=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) <= 0;
    public static bool operator >(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) > 0;
    public static bool operator >=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) >= 0;

    public static DynamicViscosity operator -(DynamicViscosity v) => new(-v.m_value);
    public static DynamicViscosity operator +(DynamicViscosity a, double b) => new(a.m_value + b);
    public static DynamicViscosity operator +(DynamicViscosity a, DynamicViscosity b) => a + b.m_value;
    public static DynamicViscosity operator /(DynamicViscosity a, double b) => new(a.m_value / b);
    public static DynamicViscosity operator /(DynamicViscosity a, DynamicViscosity b) => a / b.m_value;
    public static DynamicViscosity operator *(DynamicViscosity a, double b) => new(a.m_value * b);
    public static DynamicViscosity operator *(DynamicViscosity a, DynamicViscosity b) => a * b.m_value;
    public static DynamicViscosity operator %(DynamicViscosity a, double b) => new(a.m_value % b);
    public static DynamicViscosity operator %(DynamicViscosity a, DynamicViscosity b) => a % b.m_value;
    public static DynamicViscosity operator -(DynamicViscosity a, double b) => new(a.m_value - b);
    public static DynamicViscosity operator -(DynamicViscosity a, DynamicViscosity b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is DynamicViscosity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(DynamicViscosity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
    public double Value { get => m_value; init => m_value = value; }

    // IUnitQuantifiable<>
    public string ToUnitString(DynamicViscosityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    public double ToUnitValue(DynamicViscosityUnit unit = DefaultUnit)
      => unit switch
      {
        DynamicViscosityUnit.PascalSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #endregion Implemented interfaces

    public override string ToString() => ToQuantityString();
  }
}
