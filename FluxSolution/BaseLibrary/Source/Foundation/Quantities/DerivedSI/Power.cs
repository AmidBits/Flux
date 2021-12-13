namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Power Create(this PowerUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this PowerUnit unit)
      => unit switch
      {
        PowerUnit.Watt => @" W",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum PowerUnit
  {
    Watt,
  }

  /// <summary>Power unit of watt.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Power"/>
  public struct Power
    : System.IComparable<Power>, System.IEquatable<Power>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const PowerUnit DefaultUnit = PowerUnit.Watt;

    private readonly double m_value;

    public Power(double value, PowerUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        PowerUnit.Watt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(PowerUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(PowerUnit unit = DefaultUnit)
      => unit switch
      {
        PowerUnit.Watt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Power instance from the specified current and voltage.</summary>
    /// <param name="current"></param>
    /// <param name="voltage"></param>
    public static Power From(ElectricCurrent current, Voltage voltage)
      => new(current.GeneralUnitValue * voltage.GeneralUnitValue);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Power v)
      => v.m_value;
    public static explicit operator Power(double v)
      => new(v);

    public static bool operator <(Power a, Power b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Power a, Power b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Power a, Power b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Power a, Power b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Power a, Power b)
      => a.Equals(b);
    public static bool operator !=(Power a, Power b)
      => !a.Equals(b);

    public static Power operator -(Power v)
      => new(-v.m_value);
    public static Power operator +(Power a, double b)
      => new(a.m_value + b);
    public static Power operator +(Power a, Power b)
      => a + b.m_value;
    public static Power operator /(Power a, double b)
      => new(a.m_value / b);
    public static Power operator /(Power a, Power b)
      => a / b.m_value;
    public static Power operator *(Power a, double b)
      => new(a.m_value * b);
    public static Power operator *(Power a, Power b)
      => a * b.m_value;
    public static Power operator %(Power a, double b)
      => new(a.m_value % b);
    public static Power operator %(Power a, Power b)
      => a % b.m_value;
    public static Power operator -(Power a, double b)
      => new(a.m_value - b);
    public static Power operator -(Power a, Power b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Power other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Power other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Power o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name}  {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
