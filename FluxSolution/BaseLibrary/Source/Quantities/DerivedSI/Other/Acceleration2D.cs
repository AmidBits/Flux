namespace Flux
{
  /// <summary>Magnetic flux density unit of tesla.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
  public record struct Acceleration2D
  {
    private readonly Acceleration m_x;
    private readonly Acceleration m_y;

    public Acceleration2D(Acceleration x, Acceleration y)
    {
      m_x = x;
      m_y = y;
    }

    public Acceleration X => m_x;
    public Acceleration Y => m_y;

    #region Overloaded operators
    public static Acceleration2D operator -(Acceleration2D v)
      => new(-v.m_x, -v.m_y);
    public static Acceleration2D operator +(Acceleration2D a, double b)
      => new(a.m_x + b, a.m_y + b);
    public static Acceleration2D operator +(Acceleration2D a, Acceleration2D b)
      => new(a.m_x + b.m_x, a.m_y + b.m_y);
    public static Acceleration2D operator /(Acceleration2D a, double b)
      => new(a.m_x / b, a.m_y / b);
    public static Acceleration2D operator /(Acceleration2D a, Acceleration2D b)
      => new(a.m_x / b.m_x, a.m_y / b.m_y);
    public static Acceleration2D operator *(Acceleration2D a, double b)
      => new(a.m_x * b, a.m_y * b);
    public static Acceleration2D operator *(Acceleration2D a, Acceleration2D b)
      => new(a.m_x * b.m_x, a.m_y * b.m_y);
    public static Acceleration2D operator %(Acceleration2D a, double b)
      => new(a.m_x % b, a.m_y % b);
    public static Acceleration2D operator %(Acceleration2D a, Acceleration2D b)
      => new(a.m_x % b.m_x, a.m_y % b.m_y);
    public static Acceleration2D operator -(Acceleration2D a, double b)
      => new(a.m_x - b, a.m_y - b);
    public static Acceleration2D operator -(Acceleration2D a, Acceleration2D b)
      => new(a.m_x - b.m_x, a.m_y - b.m_y);
    #endregion Overloaded operators
  }
}

//namespace Flux
//{
//  /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
//  public readonly struct Acceleration2D
//    : System.IEquatable<Acceleration2D>, IUnitQuantifiable<CartesianCoordinate2R, AccelerationUnit>
//  {
//    public const AccelerationUnit DefaultUnit = AccelerationUnit.MeterPerSecondSquared;

//    private readonly CartesianCoordinate2R m_value;

//    public Acceleration2D(CartesianCoordinate2R value, AccelerationUnit unit = DefaultUnit)
//      => m_value = unit switch
//      {
//        AccelerationUnit.MeterPerSecondSquared => value,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };

//    #region Overloaded operators
//    public static bool operator ==(Acceleration2D a, Acceleration2D b)
//      => a.Equals(b);
//    public static bool operator !=(Acceleration2D a, Acceleration2D b)
//      => !a.Equals(b);

//    public static Acceleration2D operator -(Acceleration2D v)
//      => new(-v.m_value);
//    public static Acceleration2D operator +(Acceleration2D a, double b)
//      => new(a.m_value + b);
//    public static Acceleration2D operator +(Acceleration2D a, Acceleration2D b)
//      => new(a.m_value + b.m_value);
//    public static Acceleration2D operator /(Acceleration2D a, double b)
//      => new(a.m_value / b);
//    public static Acceleration2D operator /(Acceleration2D a, Acceleration2D b)
//      => new(a.m_value / b.m_value);
//    public static Acceleration2D operator *(Acceleration2D a, double b)
//      => new(a.m_value * b);
//    public static Acceleration2D operator *(Acceleration2D a, Acceleration2D b)
//      => new(a.m_value * b.m_value);
//    public static Acceleration2D operator %(Acceleration2D a, double b)
//      => new(a.m_value % b);
//    public static Acceleration2D operator %(Acceleration2D a, Acceleration2D b)
//      => new(a.m_value % b.m_value);
//    public static Acceleration2D operator -(Acceleration2D a, double b)
//      => new(a.m_value - b);
//    public static Acceleration2D operator -(Acceleration2D a, Acceleration2D b)
//      => new(a.m_value - b.m_value);
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IEquatable
//    public bool Equals(Acceleration2D other)
//      => m_value == other.m_value;

//    // IQuantifiable<>
//    [System.Diagnostics.Contracts.Pure] public CartesianCoordinate2R Value { get => m_value; init => m_value = value; }
//    // IUnitQuantifiable<>
//    [System.Diagnostics.Contracts.Pure]
//    public string ToUnitString(AccelerationUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
//      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
//    [System.Diagnostics.Contracts.Pure]
//    public CartesianCoordinate2R ToUnitValue(AccelerationUnit unit = DefaultUnit)
//      => unit switch
//      {
//        AccelerationUnit.MeterPerSecondSquared => m_value,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override bool Equals(object? obj)
//      => obj is Acceleration2D o && Equals(o);
//    public override int GetHashCode()
//      => m_value.GetHashCode();
//    public override string ToString()
//      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
//    #endregion Object overrides
//  }
//}
