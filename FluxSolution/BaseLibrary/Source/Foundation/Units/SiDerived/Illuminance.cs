namespace Flux.Units
{
  /// <summary>Illuminance.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Illuminance"/>
  public struct Illuminance
    : System.IComparable<Illuminance>, System.IEquatable<Illuminance>, IStandardizedScalar
  {
    private readonly double m_lux;

    public Illuminance(double lux)
      => m_lux = lux;

    public double Lux
      => m_lux;

    #region Overloaded operators
    public static explicit operator double(Illuminance v)
      => v.m_lux;
    public static explicit operator Illuminance(double v)
      => new Illuminance(v);

    public static bool operator <(Illuminance a, Illuminance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Illuminance a, Illuminance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Illuminance a, Illuminance b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Illuminance a, Illuminance b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Illuminance a, Illuminance b)
      => a.Equals(b);
    public static bool operator !=(Illuminance a, Illuminance b)
      => !a.Equals(b);

    public static Illuminance operator -(Illuminance v)
      => new Illuminance(-v.m_lux);
    public static Illuminance operator +(Illuminance a, Illuminance b)
      => new Illuminance(a.m_lux + b.m_lux);
    public static Illuminance operator /(Illuminance a, Illuminance b)
      => new Illuminance(a.m_lux / b.m_lux);
    public static Illuminance operator *(Illuminance a, Illuminance b)
      => new Illuminance(a.m_lux * b.m_lux);
    public static Illuminance operator %(Illuminance a, Illuminance b)
      => new Illuminance(a.m_lux % b.m_lux);
    public static Illuminance operator -(Illuminance a, Illuminance b)
      => new Illuminance(a.m_lux - b.m_lux);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Illuminance other)
      => m_lux.CompareTo(other.m_lux);

    // IEquatable
    public bool Equals(Illuminance other)
      => m_lux == other.m_lux;

    // IUnitStandardized
    public double GetScalar()
      => m_lux;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Illuminance o && Equals(o);
    public override int GetHashCode()
      => m_lux.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_lux} lx>";
    #endregion Object overrides
  }
}
