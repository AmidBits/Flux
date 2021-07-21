namespace Flux.Units
{
  /// <summary>Density unit of kilograms per cubic meter.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Density"/>
  public struct Density
    : System.IComparable<Density>, System.IEquatable<Density>, IValuedUnit
  {
    private readonly double m_value;

    public Density(double kilogramPerCubicMeter)
      => m_value = kilogramPerCubicMeter;

    public double Value
      => m_value;

    #region Static methods
    public static Density From(Mass mass, Volume volume)
      => new Density(mass.Value / volume.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Density v)
      => v.m_value;
    public static explicit operator Density(double v)
      => new Density(v);

    public static bool operator <(Density a, Density b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Density a, Density b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Density a, Density b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Density a, Density b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Density a, Density b)
      => a.Equals(b);
    public static bool operator !=(Density a, Density b)
      => !a.Equals(b);

    public static Density operator -(Density v)
      => new Density(-v.m_value);
    public static Density operator +(Density a, double b)
      => new Density(a.m_value + b);
    public static Density operator +(Density a, Density b)
      => a + b.m_value;
    public static Density operator /(Density a, double b)
      => new Density(a.m_value / b);
    public static Density operator /(Density a, Density b)
      => a / b.m_value;
    public static Density operator *(Density a, double b)
      => new Density(a.m_value * b);
    public static Density operator *(Density a, Density b)
      => a * b.m_value;
    public static Density operator %(Density a, double b)
      => new Density(a.m_value % b);
    public static Density operator %(Density a, Density b)
      => a % b.m_value;
    public static Density operator -(Density a, double b)
      => new Density(a.m_value - b);
    public static Density operator -(Density a, Density b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Density other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Density other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Density o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} kg/m³>";
    #endregion Object overrides
  }
}
