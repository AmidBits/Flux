namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
    public static string GetUnitString(this Units.EnergyUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.EnergyUnit.Joule => "J",
        Units.EnergyUnit.ElectronVolt => "eV",
        Units.EnergyUnit.Calorie => preferUnicode ? "\u3388" : "cal",
        Units.EnergyUnit.WattHour => "W\u22C5h",
        Units.EnergyUnit.KilowattHour => "kW\u22C5h",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }
}

namespace Flux.Units
{
  public enum EnergyUnit
  {
    /// <summary>Joule.</summary>
    Joule,
    ElectronVolt,
    Calorie,
    WattHour,
    KilowattHour
  }

  /// <summary>Energy unit of Joule.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Energy"/>
  public readonly record struct Energy
    : System.IComparable, System.IComparable<Energy>, IUnitQuantifiable<double, EnergyUnit>
  {
    public const EnergyUnit DefaultUnit = EnergyUnit.Joule;

    private readonly double m_value;

    public Energy(double value, EnergyUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        EnergyUnit.Joule => value,
        EnergyUnit.ElectronVolt => value / 1.602176634e-19,
        EnergyUnit.Calorie => value / 4.184,
        EnergyUnit.WattHour => value / 3.6e3,
        EnergyUnit.KilowattHour => value / 3.6e6,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Energy v) => v.m_value;
    public static explicit operator Energy(double v) => new(v);

    public static bool operator <(Energy a, Energy b) => a.CompareTo(b) < 0;
    public static bool operator <=(Energy a, Energy b) => a.CompareTo(b) <= 0;
    public static bool operator >(Energy a, Energy b) => a.CompareTo(b) > 0;
    public static bool operator >=(Energy a, Energy b) => a.CompareTo(b) >= 0;

    public static Energy operator -(Energy v) => new(-v.m_value);
    public static Energy operator +(Energy a, double b) => new(a.m_value + b);
    public static Energy operator +(Energy a, Energy b) => a + b.m_value;
    public static Energy operator /(Energy a, double b) => new(a.m_value / b);
    public static Energy operator /(Energy a, Energy b) => a / b.m_value;
    public static Energy operator *(Energy a, double b) => new(a.m_value * b);
    public static Energy operator *(Energy a, Energy b) => a * b.m_value;
    public static Energy operator %(Energy a, double b) => new(a.m_value % b);
    public static Energy operator %(Energy a, Energy b) => a % b.m_value;
    public static Energy operator -(Energy a, double b) => new(a.m_value - b);
    public static Energy operator -(Energy a, Energy b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Energy o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Energy other) => m_value.CompareTo(other.m_value);

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
    public double Value { get => m_value; init => m_value = value; }

    // IUnitQuantifiable<>
    public string ToUnitString(EnergyUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    public double ToUnitValue(EnergyUnit unit = DefaultUnit)
      => unit switch
      {
        EnergyUnit.Joule => m_value,
        EnergyUnit.ElectronVolt => m_value * 1.602176634e-19,
        EnergyUnit.Calorie => m_value * 4.184,
        EnergyUnit.WattHour => m_value * 3.6e3,
        EnergyUnit.KilowattHour => m_value * 3.6e6,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #endregion Implemented interfaces

    public override string ToString() => ToQuantityString();
  }
}