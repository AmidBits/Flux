namespace Flux.Units
{
  /// <summary>Density.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Density"/>
  public struct Density
    : System.IComparable<Density>, System.IEquatable<Density>, IStandardizedScalar
  {
    private readonly double m_kilogramPerCubicMeter;

    public Density(double kilogramPerCubicMeter)
      => m_kilogramPerCubicMeter = kilogramPerCubicMeter;

    public double KilogramPerCubicMeter
      => m_kilogramPerCubicMeter;

    #region Static methods
    public static Density Add(Density left, Density right)
      => new Density(left.m_kilogramPerCubicMeter + right.m_kilogramPerCubicMeter);
    public static Density Divide(Density left, Density right)
      => new Density(left.m_kilogramPerCubicMeter / right.m_kilogramPerCubicMeter);
    public static Density Multiply(Density left, Density right)
      => new Density(left.m_kilogramPerCubicMeter * right.m_kilogramPerCubicMeter);
    public static Density Negate(Density value)
      => new Density(-value.m_kilogramPerCubicMeter);
    public static Density Remainder(Density dividend, Density divisor)
      => new Density(dividend.m_kilogramPerCubicMeter % divisor.m_kilogramPerCubicMeter);
    public static Density Subtract(Density left, Density right)
      => new Density(left.m_kilogramPerCubicMeter - right.m_kilogramPerCubicMeter);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Density v)
      => v.m_kilogramPerCubicMeter;
    public static implicit operator Density(double v)
      => new Density(v);

    public static bool operator <(Density a, Density b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Density a, Density b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Density a, Density b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Density a, Density b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Density a, Density b)
      => a.Equals(b);
    public static bool operator !=(Density a, Density b)
      => !a.Equals(b);

    public static Density operator +(Density a, Density b)
      => Add(a, b);
    public static Density operator /(Density a, Density b)
      => Divide(a, b);
    public static Density operator *(Density a, Density b)
      => Multiply(a, b);
    public static Density operator -(Density v)
      => Negate(v);
    public static Density operator %(Density a, Density b)
      => Remainder(a, b);
    public static Density operator -(Density a, Density b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Density other)
      => m_kilogramPerCubicMeter.CompareTo(other.m_kilogramPerCubicMeter);

    // IEquatable
    public bool Equals(Density other)
      => m_kilogramPerCubicMeter == other.m_kilogramPerCubicMeter;

    // IUnitStandardized
    public double GetScalar()
      => m_kilogramPerCubicMeter;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Density o && Equals(o);
    public override int GetHashCode()
      => m_kilogramPerCubicMeter.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_kilogramPerCubicMeter} kg/m³>";
    #endregion Object overrides
  }
}
