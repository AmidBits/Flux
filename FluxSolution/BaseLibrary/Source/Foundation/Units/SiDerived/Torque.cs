namespace Flux.Units
{
  /// <summary>Torque.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Torque"/>
  public struct Torque
    : System.IComparable<Torque>, System.IEquatable<Torque>, IStandardizedScalar
  {
    private readonly double m_newtonMeter;

    public Torque(double newtonMeter)
      => m_newtonMeter = newtonMeter;

    public double NewtonMeter
      => m_newtonMeter;

    #region Overloaded operators
    public static explicit operator double(Torque v)
      => v.m_newtonMeter;
    public static explicit operator Torque(double v)
      => new Torque(v);

    public static bool operator <(Torque a, Torque b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Torque a, Torque b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Torque a, Torque b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Torque a, Torque b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Torque a, Torque b)
      => a.Equals(b);
    public static bool operator !=(Torque a, Torque b)
      => !a.Equals(b);

    public static Torque operator -(Torque v)
      => new Torque(-v.m_newtonMeter);
    public static Torque operator +(Torque a, Torque b)
      => new Torque(a.m_newtonMeter + b.m_newtonMeter);
    public static Torque operator /(Torque a, Torque b)
      => new Torque(a.m_newtonMeter / b.m_newtonMeter);
    public static Torque operator *(Torque a, Torque b)
      => new Torque(a.m_newtonMeter * b.m_newtonMeter);
    public static Torque operator %(Torque a, Torque b)
      => new Torque(a.m_newtonMeter % b.m_newtonMeter);
    public static Torque operator -(Torque a, Torque b)
      => new Torque(a.m_newtonMeter - b.m_newtonMeter);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Torque other)
      => m_newtonMeter.CompareTo(other.m_newtonMeter);

    // IEquatable
    public bool Equals(Torque other)
      => m_newtonMeter == other.m_newtonMeter;

    // IUnitStandardized
    public double GetScalar()
      => m_newtonMeter;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Torque o && Equals(o);
    public override int GetHashCode()
      => m_newtonMeter.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_newtonMeter} N m>";
    #endregion Object overrides
  }
}
