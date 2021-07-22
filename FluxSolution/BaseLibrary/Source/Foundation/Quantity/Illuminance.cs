namespace Flux.Quantity
{
  /// <summary>Illuminance unit of lux.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Illuminance"/>
  public struct Illuminance
    : System.IComparable<Illuminance>, System.IEquatable<Illuminance>, IValuedSiDerivedUnit
  {
    public const string Symbol = @"lx";

    private readonly double m_value;

    public Illuminance(double lux)
      => m_value = lux;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(Illuminance v)
      => v.m_value;
    public static explicit operator Illuminance(double v)
      => new Illuminance(v);

    public static bool operator <(Illuminance a, Illuminance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Illuminance a, Illuminance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Illuminance a, Illuminance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Illuminance a, Illuminance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Illuminance a, Illuminance b)
      => a.Equals(b);
    public static bool operator !=(Illuminance a, Illuminance b)
      => !a.Equals(b);

    public static Illuminance operator -(Illuminance v)
      => new Illuminance(-v.m_value);
    public static Illuminance operator +(Illuminance a, double b)
      => new Illuminance(a.m_value + b);
    public static Illuminance operator +(Illuminance a, Illuminance b)
      => a + b.m_value;
    public static Illuminance operator /(Illuminance a, double b)
      => new Illuminance(a.m_value / b);
    public static Illuminance operator /(Illuminance a, Illuminance b)
      => a / b.m_value;
    public static Illuminance operator *(Illuminance a, double b)
      => new Illuminance(a.m_value * b);
    public static Illuminance operator *(Illuminance a, Illuminance b)
      => a * b.m_value;
    public static Illuminance operator %(Illuminance a, double b)
      => new Illuminance(a.m_value % b);
    public static Illuminance operator %(Illuminance a, Illuminance b)
      => a % b.m_value;
    public static Illuminance operator -(Illuminance a, double b)
      => new Illuminance(a.m_value - b);
    public static Illuminance operator -(Illuminance a, Illuminance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Illuminance other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Illuminance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Illuminance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} {Symbol}>";
    #endregion Object overrides
  }
}
