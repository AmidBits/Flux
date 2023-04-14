namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
    public static string GetUnitString(this Units.MassUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.MassUnit.Milligram => preferUnicode ? "\u338E" : "mg",
        Units.MassUnit.Gram => "g",
        Units.MassUnit.Ounce => "oz",
        Units.MassUnit.Pound => "lb",
        Units.MassUnit.Kilogram => preferUnicode ? "\u338F" : "kg",
        Units.MassUnit.Tonne => "t",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }
}

namespace Flux.Units
{
  public enum MassUnit
  {
    /// <summary>This is the default unit for mass.</summary>
    Kilogram,
    Milligram,
    Gram,
    Ounce,
    Pound,
    /// <summary>Metric ton.</summary>
    Tonne,
  }

  /// <summary>Mass. SI unit of kilogram. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Mass"/>
  public readonly record struct Mass
    : System.IComparable, System.IComparable<Mass>, System.IFormattable, IUnitQuantifiable<double, MassUnit>
  {
    public const MassUnit DefaultUnit = MassUnit.Kilogram;

    public static Mass ElectronMass
      => new(9.1093837015e-31);

    private readonly double m_value;

    public Mass(double value, MassUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        MassUnit.Milligram => value / 1000000,
        MassUnit.Gram => value / 1000,
        MassUnit.Ounce => value / 35.27396195,
        MassUnit.Pound => value * 0.45359237,
        MassUnit.Kilogram => value,
        MassUnit.Tonne => value * 1000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Mass v) => v.m_value;
    public static explicit operator Mass(double v) => new(v);

    public static bool operator <(Mass a, Mass b) => a.CompareTo(b) < 0;
    public static bool operator <=(Mass a, Mass b) => a.CompareTo(b) <= 0;
    public static bool operator >(Mass a, Mass b) => a.CompareTo(b) > 0;
    public static bool operator >=(Mass a, Mass b) => a.CompareTo(b) >= 0;

    public static Mass operator -(Mass v) => new(-v.m_value);
    public static Mass operator +(Mass a, double b) => new(a.m_value + b);
    public static Mass operator +(Mass a, Mass b) => a + b.m_value;
    public static Mass operator /(Mass a, double b) => new(a.m_value / b);
    public static Mass operator /(Mass a, Mass b) => a / b.m_value;
    public static Mass operator *(Mass a, double b) => new(a.m_value * b);
    public static Mass operator *(Mass a, Mass b) => a * b.m_value;
    public static Mass operator %(Mass a, double b) => new(a.m_value % b);
    public static Mass operator %(Mass a, Mass b) => a % b.m_value;
    public static Mass operator -(Mass a, double b) => new(a.m_value - b);
    public static Mass operator -(Mass a, Mass b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Mass o ? CompareTo(o) : -1;

    // IComparable<T>
    public int CompareTo(Mass other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
    public double Value { get => m_value; init => m_value = value; }

    // IUnitQuantifiable<>
    public string ToUnitString(MassUnit unit, string? valueFormat = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(valueFormat is null ? string.Empty : $":{valueFormat}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    public double ToUnitValue(MassUnit unit = DefaultUnit)
      => unit switch
      {
        MassUnit.Milligram => m_value * 1000000,
        MassUnit.Gram => m_value * 1000,
        MassUnit.Ounce => m_value * 35.27396195,
        MassUnit.Pound => m_value / 0.45359237,
        MassUnit.Kilogram => m_value,
        MassUnit.Tonne => m_value / 1000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #endregion Implemented interfaces

    public override string ToString() => ToQuantityString();
  }
}
