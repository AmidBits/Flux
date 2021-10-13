namespace Flux.Quantity
{
  /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
  public struct Acceleration
    : System.IComparable<Acceleration>, System.IEquatable<Acceleration>, IValuedUnit
  {
    public static Acceleration StandardAccelerationOfGravity
      => new Acceleration(9.80665);

    private readonly double m_value;

    public Acceleration(double value, AccelerationUnit unit = AccelerationUnit.MetersPerSecondSquare)
    {
      switch (unit)
      {
        case AccelerationUnit.MetersPerSecondSquare:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(AccelerationUnit unit = AccelerationUnit.MetersPerSecondSquare)
    {
      switch (unit)
      {
        case AccelerationUnit.MetersPerSecondSquare:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Overloaded operators
    public static explicit operator double(Acceleration v)
      => v.m_value;
    public static explicit operator Acceleration(double v)
      => new Acceleration(v);

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
      => new Acceleration(-v.m_value);
    public static Acceleration operator +(Acceleration a, double b)
      => new Acceleration(a.m_value + b);
    public static Acceleration operator +(Acceleration a, Acceleration b)
      => a + b.m_value;
    public static Acceleration operator /(Acceleration a, double b)
      => new Acceleration(a.m_value / b);
    public static Acceleration operator /(Acceleration a, Acceleration b)
      => a / b.m_value;
    public static Acceleration operator *(Acceleration a, double b)
      => new Acceleration(a.m_value * b);
    public static Acceleration operator *(Acceleration a, Acceleration b)
      => a * b.m_value;
    public static Acceleration operator %(Acceleration a, double b)
      => new Acceleration(a.m_value % b);
    public static Acceleration operator %(Acceleration a, Acceleration b)
      => a % b.m_value;
    public static Acceleration operator -(Acceleration a, double b)
      => new Acceleration(a.m_value - b);
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
      => $"<{GetType().Name}: {m_value} m/s²>";
    #endregion Object overrides
  }
}
