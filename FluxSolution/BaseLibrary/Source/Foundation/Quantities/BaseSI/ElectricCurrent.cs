namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static ElectricCurrent Create(this ElectricCurrentUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this ElectricCurrentUnit unit)
      => unit switch
      {
        ElectricCurrentUnit.Ampere => @" A",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum ElectricCurrentUnit
  {
    Milliampere,
    Ampere,
  }

  /// <summary>Electric current. SI unit of ampere. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_current"/>
  public struct ElectricCurrent
    : System.IComparable<ElectricCurrent>, System.IEquatable<ElectricCurrent>, IValueGeneralizedUnit<double>, IValueBaseUnitSI<double>
  {
    public const ElectricCurrentUnit DefaultUnit = ElectricCurrentUnit.Ampere;

    private readonly double m_value;

    public ElectricCurrent(double value, ElectricCurrentUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        ElectricCurrentUnit.Milliampere => value / 1000,
        ElectricCurrentUnit.Ampere => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double BaseUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(ElectricCurrentUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(ElectricCurrentUnit unit = DefaultUnit)
      => unit switch
      {
        ElectricCurrentUnit.Milliampere => m_value * 1000,
        ElectricCurrentUnit.Ampere => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
    /// <param name="power"></param>
    /// <param name="voltage"></param>
    public static ElectricCurrent From(Power power, Voltage voltage)
      => new(power.GeneralUnitValue / voltage.GeneralUnitValue);
    /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
    /// <param name="voltage"></param>
    /// <param name="resistance"></param>
    public static ElectricCurrent From(Voltage voltage, ElectricResistance resistance)
      => new(voltage.GeneralUnitValue / resistance.GeneralUnitValue);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricCurrent v)
      => v.m_value;
    public static explicit operator ElectricCurrent(double v)
      => new(v);

    public static bool operator <(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricCurrent a, ElectricCurrent b)
      => a.Equals(b);
    public static bool operator !=(ElectricCurrent a, ElectricCurrent b)
      => !a.Equals(b);

    public static ElectricCurrent operator -(ElectricCurrent v)
      => new(-v.m_value);
    public static ElectricCurrent operator +(ElectricCurrent a, double b)
      => new(a.m_value + b);
    public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b)
      => a + b.m_value;
    public static ElectricCurrent operator /(ElectricCurrent a, double b)
      => new(a.m_value / b);
    public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b)
      => a / b.m_value;
    public static ElectricCurrent operator *(ElectricCurrent a, double b)
      => new(a.m_value * b);
    public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b)
      => a * b.m_value;
    public static ElectricCurrent operator %(ElectricCurrent a, double b)
      => new(a.m_value % b);
    public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b)
      => a % b.m_value;
    public static ElectricCurrent operator -(ElectricCurrent a, double b)
      => new(a.m_value - b);
    public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCurrent other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(ElectricCurrent other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCurrent o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}