namespace Flux
{
  /// <summary>Magnetic flux density unit of tesla.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public record struct AngularAcceleration2D
  {
    private readonly AngularAcceleration m_x;
    private readonly AngularAcceleration m_y;

    public AngularAcceleration2D(AngularAcceleration x, AngularAcceleration y)
    {
      m_x = x;
      m_y = y;
    }

    public AngularAcceleration X => m_x;
    public AngularAcceleration Y => m_y;

    #region Overloaded operators
    public static AngularAcceleration2D operator -(AngularAcceleration2D v)
      => new(-v.m_x, -v.m_y);
    public static AngularAcceleration2D operator +(AngularAcceleration2D a, double b)
      => new(a.m_x + b, a.m_y + b);
    public static AngularAcceleration2D operator +(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_x + b.m_x, a.m_y + b.m_y);
    public static AngularAcceleration2D operator /(AngularAcceleration2D a, double b)
      => new(a.m_x / b, a.m_y / b);
    public static AngularAcceleration2D operator /(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_x / b.m_x, a.m_y / b.m_y);
    public static AngularAcceleration2D operator *(AngularAcceleration2D a, double b)
      => new(a.m_x * b, a.m_y * b);
    public static AngularAcceleration2D operator *(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_x * b.m_x, a.m_y * b.m_y);
    public static AngularAcceleration2D operator %(AngularAcceleration2D a, double b)
      => new(a.m_x % b, a.m_y % b);
    public static AngularAcceleration2D operator %(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_x % b.m_x, a.m_y % b.m_y);
    public static AngularAcceleration2D operator -(AngularAcceleration2D a, double b)
      => new(a.m_x - b, a.m_y - b);
    public static AngularAcceleration2D operator -(AngularAcceleration2D a, AngularAcceleration2D b)
      => new(a.m_x - b.m_x, a.m_y - b.m_y);
    #endregion Overloaded operators
  }
}

//namespace Flux
//{
//  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
//  public readonly struct AngularAcceleration2D
//    : System.IEquatable<AngularAcceleration2D>, IUnitQuantifiable<CartesianCoordinate2R, AngularAccelerationUnit>
//  {
//    public const AngularAccelerationUnit DefaultUnit = AngularAccelerationUnit.RadianPerSecondSquared;

//    private readonly CartesianCoordinate2R m_value;

//    public AngularAcceleration2D(CartesianCoordinate2R value, AngularAccelerationUnit unit = DefaultUnit)
//      => m_value = unit switch
//      {
//        AngularAccelerationUnit.RadianPerSecondSquared => value,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };

//    #region Overloaded operators
//    public static bool operator ==(AngularAcceleration2D a, AngularAcceleration2D b)
//      => a.Equals(b);
//    public static bool operator !=(AngularAcceleration2D a, AngularAcceleration2D b)
//      => !a.Equals(b);

//    public static AngularAcceleration2D operator -(AngularAcceleration2D v)
//      => new(-v.m_value);
//    public static AngularAcceleration2D operator +(AngularAcceleration2D a, double b)
//      => new(a.m_value + b);
//    public static AngularAcceleration2D operator +(AngularAcceleration2D a, AngularAcceleration2D b)
//      => new(a.m_value + b.m_value);
//    public static AngularAcceleration2D operator /(AngularAcceleration2D a, double b)
//      => new(a.m_value / b);
//    public static AngularAcceleration2D operator /(AngularAcceleration2D a, AngularAcceleration2D b)
//      => new(a.m_value / b.m_value);
//    public static AngularAcceleration2D operator *(AngularAcceleration2D a, double b)
//      => new(a.m_value * b);
//    public static AngularAcceleration2D operator *(AngularAcceleration2D a, AngularAcceleration2D b)
//      => new(a.m_value * b.m_value);
//    public static AngularAcceleration2D operator %(AngularAcceleration2D a, double b)
//      => new(a.m_value % b);
//    public static AngularAcceleration2D operator %(AngularAcceleration2D a, AngularAcceleration2D b)
//      => new(a.m_value % b.m_value);
//    public static AngularAcceleration2D operator -(AngularAcceleration2D a, double b)
//      => new(a.m_value - b);
//    public static AngularAcceleration2D operator -(AngularAcceleration2D a, AngularAcceleration2D b)
//      => new(a.m_value - b.m_value);
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IEquatable
//    public bool Equals(AngularAcceleration2D other)
//      => m_value == other.m_value;

//    // IQuantifiable<>
//    [System.Diagnostics.Contracts.Pure] public CartesianCoordinate2R Value { get => m_value; init => m_value = value; }
//    // IUnitQuantifiable<>
//    [System.Diagnostics.Contracts.Pure]
//    public string ToUnitString(AngularAccelerationUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
//      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
//    [System.Diagnostics.Contracts.Pure]
//    public CartesianCoordinate2R ToUnitValue(AngularAccelerationUnit unit = DefaultUnit)
//      => unit switch
//      {
//        AngularAccelerationUnit.RadianPerSecondSquared => m_value,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override bool Equals(object? obj)
//      => obj is AngularAcceleration2D o && Equals(o);
//    public override int GetHashCode()
//      => m_value.GetHashCode();
//    public override string ToString()
//      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
//    #endregion Object overrides
//  }
//}
