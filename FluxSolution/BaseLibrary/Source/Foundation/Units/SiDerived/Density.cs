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

    #region Overloaded operators
    public static explicit operator double(Density v)
      => v.m_kilogramPerCubicMeter;
    public static explicit operator Density(double v)
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

    public static Density operator -(Density v)
      => new Density(-v.m_kilogramPerCubicMeter);
    public static Density operator +(Density a, Density b)
      => new Density(a.m_kilogramPerCubicMeter + b.m_kilogramPerCubicMeter);
    public static Density operator /(Density a, Density b)
      => new Density(a.m_kilogramPerCubicMeter / b.m_kilogramPerCubicMeter);
    public static Density operator %(Density a, Density b)
      => new Density(a.m_kilogramPerCubicMeter % b.m_kilogramPerCubicMeter);
    public static Density operator *(Density a, Density b)
      => new Density(a.m_kilogramPerCubicMeter * b.m_kilogramPerCubicMeter);
    public static Density operator -(Density a, Density b)
      => new Density(a.m_kilogramPerCubicMeter - b.m_kilogramPerCubicMeter);
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
