namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static AngularAcceleration Create(this AngularAccelerationUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this AngularAccelerationUnit unit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => @" rad/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AngularAccelerationUnit
  {
    RadianPerSecondSquare,
  }

  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public struct AngularAcceleration
    : System.IComparable<AngularAcceleration>, System.IEquatable<AngularAcceleration>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const AngularAccelerationUnit DefaultUnit = AngularAccelerationUnit.RadianPerSecondSquare;

    private readonly double m_value;

    public AngularAcceleration(double value, AngularAccelerationUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(AngularAccelerationUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(AngularAccelerationUnit unit = DefaultUnit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(AngularAcceleration v)
      => v.m_value;
    public static explicit operator AngularAcceleration(double v)
      => new(v);

    public static bool operator <(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AngularAcceleration a, AngularAcceleration b)
      => a.Equals(b);
    public static bool operator !=(AngularAcceleration a, AngularAcceleration b)
      => !a.Equals(b);

    public static AngularAcceleration operator -(AngularAcceleration v)
      => new(-v.m_value);
    public static AngularAcceleration operator +(AngularAcceleration a, double b)
      => new(a.m_value + b);
    public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b)
      => a + b.m_value;
    public static AngularAcceleration operator /(AngularAcceleration a, double b)
      => new(a.m_value / b);
    public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b)
      => a / b.m_value;
    public static AngularAcceleration operator *(AngularAcceleration a, double b)
      => new(a.m_value * b);
    public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b)
      => a * b.m_value;
    public static AngularAcceleration operator %(AngularAcceleration a, double b)
      => new(a.m_value % b);
    public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b)
      => a % b.m_value;
    public static AngularAcceleration operator -(AngularAcceleration a, double b)
      => new(a.m_value - b);
    public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularAcceleration other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(AngularAcceleration other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularAcceleration o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
