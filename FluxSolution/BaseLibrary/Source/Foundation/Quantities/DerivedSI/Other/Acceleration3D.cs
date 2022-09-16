namespace Flux
{
  /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
  public readonly struct Acceleration3D
    : System.IEquatable<Acceleration3D>, IUnitQuantifiable<CartesianCoordinate3R, AccelerationUnit>
  {
    public const AccelerationUnit DefaultUnit = AccelerationUnit.MeterPerSecondSquared;

    private readonly CartesianCoordinate3R m_value;

    public Acceleration3D(CartesianCoordinate3R value, AccelerationUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static bool operator ==(Acceleration3D a, Acceleration3D b)
      => a.Equals(b);
    public static bool operator !=(Acceleration3D a, Acceleration3D b)
      => !a.Equals(b);

    public static Acceleration3D operator -(Acceleration3D v)
      => new(-v.m_value);
    public static Acceleration3D operator +(Acceleration3D a, double b)
      => new(a.m_value + b);
    public static Acceleration3D operator +(Acceleration3D a, Acceleration3D b)
      => new(a.m_value + b.m_value);
    public static Acceleration3D operator /(Acceleration3D a, double b)
      => new(a.m_value / b);
    public static Acceleration3D operator /(Acceleration3D a, Acceleration3D b)
      => new(a.m_value / b.m_value);
    public static Acceleration3D operator *(Acceleration3D a, double b)
      => new(a.m_value * b);
    public static Acceleration3D operator *(Acceleration3D a, Acceleration3D b)
      => new(a.m_value * b.m_value);
    public static Acceleration3D operator %(Acceleration3D a, double b)
      => new(a.m_value % b);
    public static Acceleration3D operator %(Acceleration3D a, Acceleration3D b)
      => new(a.m_value % b.m_value);
    public static Acceleration3D operator -(Acceleration3D a, double b)
      => new(a.m_value - b);
    public static Acceleration3D operator -(Acceleration3D a, Acceleration3D b)
      => new(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(Acceleration3D other)
      => m_value == other.m_value;

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public CartesianCoordinate3R Value { get => m_value; init => m_value = value; }
    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(AccelerationUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3R ToUnitValue(AccelerationUnit unit = DefaultUnit)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquared => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Acceleration3D o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
