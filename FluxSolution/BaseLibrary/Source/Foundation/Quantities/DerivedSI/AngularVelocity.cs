namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static AngularVelocity Create(this AngularVelocityUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this AngularVelocityUnit unit)
      => unit switch
      {
        AngularVelocityUnit.RadianPerSecond => @" rad/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }


  public enum AngularVelocityUnit
  {
    RadianPerSecond,
  }

  /// <summary>Angular velocity, unit of radians per second. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_velocity"/>
  public struct AngularVelocity
    : System.IComparable<AngularVelocity>, System.IEquatable<AngularVelocity>, IValueGeneralizedUnit<double>, IValueSiDerivedUnit<double>
  {
    public const AngularVelocityUnit DefaultUnit = AngularVelocityUnit.RadianPerSecond;

    private readonly double m_value;

    public AngularVelocity(double value, AngularVelocityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AngularVelocityUnit.RadianPerSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(AngularVelocityUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(AngularVelocityUnit unit = DefaultUnit)
      => unit switch
      {
        AngularVelocityUnit.RadianPerSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static AngularVelocity From(Angle angle, Time time)
      => new(angle.Value / time.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AngularVelocity v)
      => v.m_value;
    public static explicit operator AngularVelocity(double v)
      => new(v);

    public static bool operator <(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AngularVelocity a, AngularVelocity b)
      => a.Equals(b);
    public static bool operator !=(AngularVelocity a, AngularVelocity b)
      => !a.Equals(b);

    public static AngularVelocity operator -(AngularVelocity v)
      => new(-v.m_value);
    public static AngularVelocity operator +(AngularVelocity a, AngularVelocity b)
      => new(a.m_value + b.m_value);
    public static AngularVelocity operator /(AngularVelocity a, AngularVelocity b)
      => new(a.m_value / b.m_value);
    public static AngularVelocity operator *(AngularVelocity a, AngularVelocity b)
      => new(a.m_value * b.m_value);
    public static AngularVelocity operator %(AngularVelocity a, AngularVelocity b)
      => new(a.m_value % b.m_value);
    public static AngularVelocity operator -(AngularVelocity a, AngularVelocity b)
      => new(a.m_value - b.m_value);

    public static AngularVelocity operator +(AngularVelocity a, double b)
      => new(a.m_value + b);
    public static AngularVelocity operator /(AngularVelocity a, double b)
      => new(a.m_value / b);
    public static AngularVelocity operator *(AngularVelocity a, double b)
      => new(a.m_value * b);
    public static AngularVelocity operator %(AngularVelocity a, double b)
      => new(a.m_value % b);
    public static AngularVelocity operator -(AngularVelocity a, double b)
      => new(a.m_value - b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularVelocity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(AngularVelocity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularVelocity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
