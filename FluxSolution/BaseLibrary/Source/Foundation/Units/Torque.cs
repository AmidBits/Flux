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

    #region Static methods
    public static Torque Add(Torque left, Torque right)
      => new Torque(left.m_newtonMeter + right.m_newtonMeter);
    public static Torque Divide(Torque left, Torque right)
      => new Torque(left.m_newtonMeter / right.m_newtonMeter);
    public static Torque Multiply(Torque left, Torque right)
      => new Torque(left.m_newtonMeter * right.m_newtonMeter);
    public static Torque Negate(Torque value)
      => new Torque(-value.m_newtonMeter);
    public static Torque Remainder(Torque dividend, Torque divisor)
      => new Torque(dividend.m_newtonMeter % divisor.m_newtonMeter);
    public static Torque Subtract(Torque left, Torque right)
      => new Torque(left.m_newtonMeter - right.m_newtonMeter);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Torque v)
      => v.m_newtonMeter;
    public static implicit operator Torque(double v)
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

    public static Torque operator +(Torque a, Torque b)
      => Add(a, b);
    public static Torque operator /(Torque a, Torque b)
      => Divide(a, b);
    public static Torque operator *(Torque a, Torque b)
      => Multiply(a, b);
    public static Torque operator -(Torque v)
      => Negate(v);
    public static Torque operator %(Torque a, Torque b)
      => Remainder(a, b);
    public static Torque operator -(Torque a, Torque b)
      => Subtract(a, b);
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
