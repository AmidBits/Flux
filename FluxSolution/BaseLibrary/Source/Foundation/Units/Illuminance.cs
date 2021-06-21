namespace Flux.Units
{
  /// <summary>Illuminance.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Illuminance"/>
  public struct Illuminance
    : System.IComparable<Illuminance>, System.IEquatable<Illuminance>, System.IFormattable
  {
    private readonly double m_lux;

    public Illuminance(double lux)
      => m_lux = lux;

    public double Lux
      => m_lux;
    public double Lumens
      => ConvertLuxToLumens(m_lux);

    #region Static methods
    public static Illuminance Add(Illuminance left, Illuminance right)
      => new Illuminance(left.m_lux + right.m_lux);
    public static double ConvertLuxToLumens(double lux)
      => lux * 0.0929;
    public static double ConvertLumensToLux(double lumens)
      => lumens / 0.0929;
    public static Illuminance Divide(Illuminance left, Illuminance right)
      => new Illuminance(left.m_lux / right.m_lux);
    public static Illuminance FromLumens(double lumens)
      => new Illuminance(ConvertLumensToLux(lumens));
    public static Illuminance Multiply(Illuminance left, Illuminance right)
      => new Illuminance(left.m_lux * right.m_lux);
    public static Illuminance Negate(Illuminance value)
      => new Illuminance(-value.m_lux);
    public static Illuminance Remainder(Illuminance dividend, Illuminance divisor)
      => new Illuminance(dividend.m_lux % divisor.m_lux);
    public static Illuminance Subtract(Illuminance left, Illuminance right)
      => new Illuminance(left.m_lux - right.m_lux);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Illuminance v)
      => v.m_lux;
    public static implicit operator Illuminance(double v)
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

    public static Illuminance operator +(Illuminance a, Illuminance b)
      => Add(a, b);
    public static Illuminance operator /(Illuminance a, Illuminance b)
      => Divide(a, b);
    public static Illuminance operator *(Illuminance a, Illuminance b)
      => Multiply(a, b);
    public static Illuminance operator -(Illuminance v)
      => Negate(v);
    public static Illuminance operator %(Illuminance a, Illuminance b)
      => Remainder(a, b);
    public static Illuminance operator -(Illuminance a, Illuminance b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Illuminance other)
      => m_lux.CompareTo(other.m_lux);

    // IEquatable
    public bool Equals(Illuminance other)
      => m_lux == other.m_lux;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Illuminance)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Illuminance o && Equals(o);
    public override int GetHashCode()
      => m_lux.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
