namespace Flux
{
  /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
  public struct Acceleration2D
    : System.IEquatable<Acceleration2D>, IValueSiDerivedUnit<CartesianCoordinate2>
  {
    public const AccelerationUnit DefaultUnit = AccelerationUnit.MeterPerSecondSquare;

    private readonly CartesianCoordinate2 m_value;

    public Acceleration2D(CartesianCoordinate2 value, AccelerationUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        AccelerationUnit.MeterPerSecondSquare => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public CartesianCoordinate2 Value
      => m_value;

    public string ToUnitString(AccelerationUnit unit = DefaultUnit, string? format = null)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitSymbol()}";
    public CartesianCoordinate2 ToUnitValue(AccelerationUnit unit = DefaultUnit)
      => unit switch
      {
        AccelerationUnit.MeterPerSecondSquare => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static bool operator ==(Acceleration2D a, Acceleration2D b)
      => a.Equals(b);
    public static bool operator !=(Acceleration2D a, Acceleration2D b)
      => !a.Equals(b);

    public static Acceleration2D operator -(Acceleration2D v)
      => new(-v.m_value);
    public static Acceleration2D operator +(Acceleration2D a, double b)
      => new(a.m_value + b);
    public static Acceleration2D operator +(Acceleration2D a, Acceleration2D b)
      => new(a.m_value + b.m_value);
    public static Acceleration2D operator /(Acceleration2D a, double b)
      => new(a.m_value / b);
    public static Acceleration2D operator /(Acceleration2D a, Acceleration2D b)
      => new(a.m_value / b.m_value);
    public static Acceleration2D operator *(Acceleration2D a, double b)
      => new(a.m_value * b);
    public static Acceleration2D operator *(Acceleration2D a, Acceleration2D b)
      => new(a.m_value * b.m_value);
    public static Acceleration2D operator %(Acceleration2D a, double b)
      => new(a.m_value % b);
    public static Acceleration2D operator %(Acceleration2D a, Acceleration2D b)
      => new(a.m_value % b.m_value);
    public static Acceleration2D operator -(Acceleration2D a, double b)
      => new(a.m_value - b);
    public static Acceleration2D operator -(Acceleration2D a, Acceleration2D b)
      => new(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Acceleration2D other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Acceleration2D o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
