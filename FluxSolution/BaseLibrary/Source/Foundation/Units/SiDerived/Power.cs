namespace Flux.Units
{
  /// <summary>Power.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Power"/>
  public struct Power
    : System.IComparable<Power>, System.IEquatable<Power>, IStandardizedScalar
  {
    private readonly double m_watt;

    public Power(double watt)
      => m_watt = watt;

    public double Watt
      => m_watt;

    #region Static methods
    /// <summary>Creates a new Power instance from the specified current and voltage.</summary>
    /// <param name="current"></param>
    /// <param name="voltage"></param>
    public static Power From(ElectricCurrent current, Voltage voltage)
      => new Power(current.Ampere * voltage.Volt);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Power v)
      => v.m_watt;
    public static explicit operator Power(double v)
      => new Power(v);

    public static bool operator <(Power a, Power b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Power a, Power b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Power a, Power b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Power a, Power b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Power a, Power b)
      => a.Equals(b);
    public static bool operator !=(Power a, Power b)
      => !a.Equals(b);

    public static Power operator -(Power v)
      => new Power(-v.m_watt);
    public static Power operator +(Power a, Power b)
      => new Power(a.m_watt + b.m_watt);
    public static Power operator /(Power a, Power b)
      => new Power(a.m_watt / b.m_watt);
    public static Power operator *(Power a, Power b)
      => new Power(a.m_watt * b.m_watt);
    public static Power operator %(Power a, Power b)
      => new Power(a.m_watt % b.m_watt);
    public static Power operator -(Power a, Power b)
      => new Power(a.m_watt - b.m_watt);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Power other)
      => m_watt.CompareTo(other.m_watt);

    // IEquatable
    public bool Equals(Power other)
      => m_watt == other.m_watt;

    // IUnitStandardized
    public double GetScalar()
      => m_watt;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Power o && Equals(o);
    public override int GetHashCode()
      => m_watt.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_watt} W>";
    #endregion Object overrides
  }
}
