namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static AbsoluteHumidity Create(this AbsoluteHumidityUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this AbsoluteHumidityUnit unit)
      => unit switch
      {
        AbsoluteHumidityUnit.GramsPerCubicMeter => @" g/m³",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AbsoluteHumidityUnit
  {
    GramsPerCubicMeter,
  }

  /// <summary>Absolute humidity unit of grams per cubic meter.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Humidity#Absolute_humidity"/>
  public struct AbsoluteHumidity
    : System.IComparable<AbsoluteHumidity>, System.IEquatable<AbsoluteHumidity>, IValueGeneralizedUnit<double>
  {
    public const AbsoluteHumidityUnit DefaultUnit = AbsoluteHumidityUnit.GramsPerCubicMeter;

    private readonly double m_value;

    public AbsoluteHumidity(double value, AbsoluteHumidityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AbsoluteHumidityUnit.GramsPerCubicMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(AbsoluteHumidityUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(AbsoluteHumidityUnit unit = DefaultUnit)
      => unit switch
      {
        AbsoluteHumidityUnit.GramsPerCubicMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static AbsoluteHumidity From(double grams, Volume volume)
      => new(grams / volume.Value);
    public static AbsoluteHumidity From(Mass mass, Volume volume)
      => From(mass.Value * 1000, volume);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AbsoluteHumidity v)
      => v.m_value;
    public static explicit operator AbsoluteHumidity(double v)
      => new(v);

    public static bool operator <(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.Equals(b);
    public static bool operator !=(AbsoluteHumidity a, AbsoluteHumidity b)
      => !a.Equals(b);

    public static AbsoluteHumidity operator -(AbsoluteHumidity v)
      => new(-v.m_value);
    public static AbsoluteHumidity operator +(AbsoluteHumidity a, double b)
      => new(a.m_value + b);
    public static AbsoluteHumidity operator +(AbsoluteHumidity a, AbsoluteHumidity b)
      => a + b.m_value;
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, double b)
      => new(a.m_value / b);
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, AbsoluteHumidity b)
      => a / b.m_value;
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, double b)
      => new(a.m_value * b);
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, AbsoluteHumidity b)
      => a * b.m_value;
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, double b)
      => new(a.m_value % b);
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, AbsoluteHumidity b)
      => a % b.m_value;
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, double b)
      => new(a.m_value - b);
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, AbsoluteHumidity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AbsoluteHumidity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(AbsoluteHumidity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AbsoluteHumidity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
