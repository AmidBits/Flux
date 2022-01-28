namespace Flux
{
  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public struct AngularAcceleration3D
      : System.IEquatable<AngularAcceleration3D>, IValueSiDerivedUnit<CartesianCoordinate3>
  {
    public const AngularAccelerationUnit DefaultUnit = AngularAccelerationUnit.RadianPerSecondSquare;

    private readonly CartesianCoordinate3 m_value;

    public AngularAcceleration3D(CartesianCoordinate3 value, AngularAccelerationUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public CartesianCoordinate3 Value
      => m_value;

    public string ToUnitString(AngularAccelerationUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    public CartesianCoordinate3 ToUnitValue(AngularAccelerationUnit unit = DefaultUnit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static bool operator ==(AngularAcceleration3D a, AngularAcceleration3D b)
      => a.Equals(b);
    public static bool operator !=(AngularAcceleration3D a, AngularAcceleration3D b)
      => !a.Equals(b);

    public static AngularAcceleration3D operator -(AngularAcceleration3D v)
      => new(-v.m_value);
    public static AngularAcceleration3D operator +(AngularAcceleration3D a, double b)
      => new(a.m_value + b);
    public static AngularAcceleration3D operator +(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_value + b.m_value);
    public static AngularAcceleration3D operator /(AngularAcceleration3D a, double b)
      => new(a.m_value / b);
    public static AngularAcceleration3D operator /(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_value / b.m_value);
    public static AngularAcceleration3D operator *(AngularAcceleration3D a, double b)
      => new(a.m_value * b);
    public static AngularAcceleration3D operator *(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_value * b.m_value);
    public static AngularAcceleration3D operator %(AngularAcceleration3D a, double b)
      => new(a.m_value % b);
    public static AngularAcceleration3D operator %(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_value % b.m_value);
    public static AngularAcceleration3D operator -(AngularAcceleration3D a, double b)
      => new(a.m_value - b);
    public static AngularAcceleration3D operator -(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(AngularAcceleration3D other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularAcceleration3D o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
