namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Acceleration Create(this AccelerationUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this AccelerationUnit unit)
      => unit switch
      {
        AccelerationUnit.MetersPerSecondSquare => @" m/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AccelerationUnit
  {
    MetersPerSecondSquare,
  }

  /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
  public struct Acceleration
    : System.IComparable<Acceleration>, System.IEquatable<Acceleration>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const AccelerationUnit DefaultUnit = AccelerationUnit.MetersPerSecondSquare;

    public static Acceleration StandardAccelerationOfGravity
      => new(9.80665);

    private readonly double m_value;

    public Acceleration(double value, AccelerationUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AccelerationUnit.MetersPerSecondSquare => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(AccelerationUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(AccelerationUnit unit = DefaultUnit)
      => unit switch
      {
        AccelerationUnit.MetersPerSecondSquare => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Acceleration v)
      => v.m_value;
    public static explicit operator Acceleration(double v)
      => new(v);

    public static bool operator <(Acceleration a, Acceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Acceleration a, Acceleration b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Acceleration a, Acceleration b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Acceleration a, Acceleration b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Acceleration a, Acceleration b)
      => a.Equals(b);
    public static bool operator !=(Acceleration a, Acceleration b)
      => !a.Equals(b);

    public static Acceleration operator -(Acceleration v)
      => new(-v.m_value);
    public static Acceleration operator +(Acceleration a, double b)
      => new(a.m_value + b);
    public static Acceleration operator +(Acceleration a, Acceleration b)
      => a + b.m_value;
    public static Acceleration operator /(Acceleration a, double b)
      => new(a.m_value / b);
    public static Acceleration operator /(Acceleration a, Acceleration b)
      => a / b.m_value;
    public static Acceleration operator *(Acceleration a, double b)
      => new(a.m_value * b);
    public static Acceleration operator *(Acceleration a, Acceleration b)
      => a * b.m_value;
    public static Acceleration operator %(Acceleration a, double b)
      => new(a.m_value % b);
    public static Acceleration operator %(Acceleration a, Acceleration b)
      => a % b.m_value;
    public static Acceleration operator -(Acceleration a, double b)
      => new(a.m_value - b);
    public static Acceleration operator -(Acceleration a, Acceleration b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Acceleration other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Acceleration other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Acceleration o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
