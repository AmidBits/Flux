namespace Flux
{
  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public struct AngularAcceleration2D
    : System.IEquatable<AngularAcceleration2D>, ISiDerivedUnitQuantifiable<CartesianCoordinateR2, AngularAccelerationUnit>
  {
    public const AngularAccelerationUnit DefaultUnit = AngularAccelerationUnit.RadianPerSecondSquare;

    private readonly CartesianCoordinateR2 m_value;

    public AngularAcceleration2D(CartesianCoordinateR2 value, AngularAccelerationUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static bool operator ==(AngularAcceleration2D a, AngularAcceleration2D b)
      => a.Equals(b);
    public static bool operator !=(AngularAcceleration2D a, AngularAcceleration2D b)
      => !a.Equals(b);

    public static AngularAcceleration2D operator -(AngularAcceleration2D v)
      => new(-v.m_value);
    public static AngularAcceleration2D operator +(AngularAcceleration2D a, double b)
      => new(a.m_value + b);
    public static AngularAcceleration2D operator +(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_value + b.m_value);
    public static AngularAcceleration2D operator /(AngularAcceleration2D a, double b)
      => new(a.m_value / b);
    public static AngularAcceleration2D operator /(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_value / b.m_value);
    public static AngularAcceleration2D operator *(AngularAcceleration2D a, double b)
      => new(a.m_value * b);
    public static AngularAcceleration2D operator *(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_value * b.m_value);
    public static AngularAcceleration2D operator %(AngularAcceleration2D a, double b)
      => new(a.m_value % b);
    public static AngularAcceleration2D operator %(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_value % b.m_value);
    public static AngularAcceleration2D operator -(AngularAcceleration2D a, double b)
      => new(a.m_value - b);
    public static AngularAcceleration2D operator -(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(AngularAcceleration2D other)
      => m_value == other.m_value;

    // ISiDerivedUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinateR2 Value
      => m_value;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(AngularAccelerationUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinateR2 ToUnitValue(AngularAccelerationUnit unit = DefaultUnit)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularAcceleration2D o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
