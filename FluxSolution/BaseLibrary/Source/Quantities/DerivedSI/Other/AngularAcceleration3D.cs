namespace Flux
{
  /// <summary>Magnetic flux density unit of tesla.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public record struct AngularAcceleration3D
  {
    private readonly AngularAcceleration m_x;
    private readonly AngularAcceleration m_y;
    private readonly AngularAcceleration m_z;

    public AngularAcceleration3D(AngularAcceleration x, AngularAcceleration y, AngularAcceleration z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }

    public AngularAcceleration X => m_x;
    public AngularAcceleration Y => m_y;
    public AngularAcceleration Z => m_z;

    #region Overloaded operators
    public static AngularAcceleration3D operator -(AngularAcceleration3D v)
      => new(-v.m_x, -v.m_y, -v.m_z);
    public static AngularAcceleration3D operator +(AngularAcceleration3D a, double b)
      => new(a.m_x + b, a.m_y + b, a.m_z + b);
    public static AngularAcceleration3D operator +(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_x + b.m_x, a.m_y + b.m_y, a.m_z + b.m_z);
    public static AngularAcceleration3D operator /(AngularAcceleration3D a, double b)
      => new(a.m_x / b, a.m_y / b, a.m_z / b);
    public static AngularAcceleration3D operator /(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_x / b.m_x, a.m_y / b.m_y, a.m_z / b.m_z);
    public static AngularAcceleration3D operator *(AngularAcceleration3D a, double b)
      => new(a.m_x * b, a.m_y * b, a.m_z * b);
    public static AngularAcceleration3D operator *(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_x * b.m_x, a.m_y * b.m_y, a.m_z * b.m_z);
    public static AngularAcceleration3D operator %(AngularAcceleration3D a, double b)
      => new(a.m_x % b, a.m_y % b, a.m_z % b);
    public static AngularAcceleration3D operator %(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_x % b.m_x, a.m_y % b.m_y, a.m_z % b.m_z);
    public static AngularAcceleration3D operator -(AngularAcceleration3D a, double b)
      => new(a.m_x - b, a.m_y - b, a.m_z - b);
    public static AngularAcceleration3D operator -(AngularAcceleration3D a, AngularAcceleration3D b)
      => new(a.m_x - b.m_x, a.m_y - b.m_y, a.m_z - b.m_z);
    #endregion Overloaded operators
  }
}

//namespace Flux
//{
//  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
//  public readonly struct AngularAcceleration3D
//      : System.IEquatable<AngularAcceleration3D>, IUnitQuantifiable<CartesianCoordinate3R, AngularAccelerationUnit>
//  {
//    public const AngularAccelerationUnit DefaultUnit = AngularAccelerationUnit.RadianPerSecondSquared;

//    private readonly CartesianCoordinate3R m_value;

//    public AngularAcceleration3D(CartesianCoordinate3R value, AngularAccelerationUnit unit = DefaultUnit)
//      => m_value = unit switch
//      {
//        AngularAccelerationUnit.RadianPerSecondSquared => value,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };

//    #region Overloaded operators
//    public static bool operator ==(AngularAcceleration3D a, AngularAcceleration3D b)
//      => a.Equals(b);
//    public static bool operator !=(AngularAcceleration3D a, AngularAcceleration3D b)
//      => !a.Equals(b);

//    public static AngularAcceleration3D operator -(AngularAcceleration3D v)
//      => new(-v.m_value);
//    public static AngularAcceleration3D operator +(AngularAcceleration3D a, double b)
//      => new(a.m_value + b);
//    public static AngularAcceleration3D operator +(AngularAcceleration3D a, AngularAcceleration3D b)
//      => new(a.m_value + b.m_value);
//    public static AngularAcceleration3D operator /(AngularAcceleration3D a, double b)
//      => new(a.m_value / b);
//    public static AngularAcceleration3D operator /(AngularAcceleration3D a, AngularAcceleration3D b)
//      => new(a.m_value / b.m_value);
//    public static AngularAcceleration3D operator *(AngularAcceleration3D a, double b)
//      => new(a.m_value * b);
//    public static AngularAcceleration3D operator *(AngularAcceleration3D a, AngularAcceleration3D b)
//      => new(a.m_value * b.m_value);
//    public static AngularAcceleration3D operator %(AngularAcceleration3D a, double b)
//      => new(a.m_value % b);
//    public static AngularAcceleration3D operator %(AngularAcceleration3D a, AngularAcceleration3D b)
//      => new(a.m_value % b.m_value);
//    public static AngularAcceleration3D operator -(AngularAcceleration3D a, double b)
//      => new(a.m_value - b);
//    public static AngularAcceleration3D operator -(AngularAcceleration3D a, AngularAcceleration3D b)
//      => new(a.m_value - b.m_value);
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IEquatable
//    public bool Equals(AngularAcceleration3D other)
//      => m_value == other.m_value;

//    // IQuantifiable<>
//    [System.Diagnostics.Contracts.Pure] public CartesianCoordinate3R Value { get => m_value; init => m_value = value; }
//    // IUnitQuantifiable<>
//    [System.Diagnostics.Contracts.Pure]
//    public string ToUnitString(AngularAccelerationUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
//      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
//    [System.Diagnostics.Contracts.Pure]
//    public CartesianCoordinate3R ToUnitValue(AngularAccelerationUnit unit = DefaultUnit)
//      => unit switch
//      {
//        AngularAccelerationUnit.RadianPerSecondSquared => m_value,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override bool Equals(object? obj)
//      => obj is AngularAcceleration3D o && Equals(o);
//    public override int GetHashCode()
//      => m_value.GetHashCode();
//    public override string ToString()
//      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
//    #endregion Object overrides
//  }
//}
