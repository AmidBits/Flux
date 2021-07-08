namespace Flux.Units
{
  /// <summary>Acceleration.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
  public struct Acceleration
    : System.IComparable<Acceleration>, System.IEquatable<Acceleration>, IStandardizedScalar
  {
    private readonly double m_meterPerSecondSquare;

    public Acceleration(double meterPerSecondSquare)
      => m_meterPerSecondSquare = meterPerSecondSquare;

    public double MeterPerSecondSquare
      => m_meterPerSecondSquare;

    #region Overloaded operators
    public static explicit operator double(Acceleration v)
      => v.m_meterPerSecondSquare;
    public static explicit operator Acceleration(double v)
      => new Acceleration(v);

    public static bool operator <(Acceleration a, Acceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Acceleration a, Acceleration b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Acceleration a, Acceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Acceleration a, Acceleration b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Acceleration a, Acceleration b)
      => a.Equals(b);
    public static bool operator !=(Acceleration a, Acceleration b)
      => !a.Equals(b);

    public static Acceleration operator -(Acceleration v)
      => new Acceleration(-v.m_meterPerSecondSquare);
    public static Acceleration operator +(Acceleration a, Acceleration b)
      => new Acceleration(a.m_meterPerSecondSquare + b.m_meterPerSecondSquare);
    public static Acceleration operator /(Acceleration a, Acceleration b)
      => new Acceleration(a.m_meterPerSecondSquare / b.m_meterPerSecondSquare);
    public static Acceleration operator *(Acceleration a, Acceleration b)
      => new Acceleration(a.m_meterPerSecondSquare * b.m_meterPerSecondSquare);
    public static Acceleration operator %(Acceleration a, Acceleration b)
      => new Acceleration(a.m_meterPerSecondSquare % b.m_meterPerSecondSquare);
    public static Acceleration operator -(Acceleration a, Acceleration b)
      => new Acceleration(a.m_meterPerSecondSquare - b.m_meterPerSecondSquare);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Acceleration other)
      => m_meterPerSecondSquare.CompareTo(other.m_meterPerSecondSquare);

    // IEquatable
    public bool Equals(Acceleration other)
      => m_meterPerSecondSquare == other.m_meterPerSecondSquare;

    // IUnitStandardized
    public double GetScalar()
      => m_meterPerSecondSquare;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Acceleration o && Equals(o);
    public override int GetHashCode()
      => m_meterPerSecondSquare.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_meterPerSecondSquare} m/s²>";
    #endregion Object overrides
  }
}
