namespace Flux.Units
{
  public enum ElectricCurrentUnit
  {
    Milliampere,
    Ampere,
  }

  /// <summary>Electric current.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_current"/>
  public struct ElectricCurrent
    : System.IComparable<ElectricCurrent>, System.IEquatable<ElectricCurrent>, IStandardizedScalar
  {
    private readonly double m_ampere;

    public ElectricCurrent(double ampere)
      => m_ampere = ampere;

    public double Ampere
      => m_ampere;

    public double ToUnitValue(ElectricCurrentUnit unit)
      => ConvertAmpereToUnit(m_ampere, unit);

    #region Static methods
    public static ElectricCurrent Add(ElectricCurrent left, ElectricCurrent right)
      => new ElectricCurrent(left.m_ampere + right.m_ampere);
    public static double ConvertAmpereToUnit(double ampere, ElectricCurrentUnit unit)
    {
      switch (unit)
      {
        case ElectricCurrentUnit.Milliampere:
          return ampere * 1000;
        case ElectricCurrentUnit.Ampere:
          return ampere;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    public static double ConvertUnitToAmpere(double value, ElectricCurrentUnit unit)
    {
      switch (unit)
      {
        case ElectricCurrentUnit.Milliampere:
          return value / 1000;
        case ElectricCurrentUnit.Ampere:
          return value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    public static ElectricCurrent Divide(ElectricCurrent left, ElectricCurrent right)
      => new ElectricCurrent(left.m_ampere / right.m_ampere);
    public static ElectricCurrent FromUnitValue(ElectricCurrentUnit unit, double value)
      => new ElectricCurrent(ConvertUnitToAmpere(value, unit));
    public static ElectricCurrent FromVR(Voltage v, ElectricResistance r)
      => new ElectricCurrent(v.Volt / r.Ohm);
    public static ElectricCurrent Multiply(ElectricCurrent left, ElectricCurrent right)
      => new ElectricCurrent(left.m_ampere * right.m_ampere);
    public static ElectricCurrent Negate(ElectricCurrent value)
      => new ElectricCurrent(-value.m_ampere);
    public static ElectricCurrent Remainder(ElectricCurrent dividend, ElectricCurrent divisor)
      => new ElectricCurrent(dividend.m_ampere % divisor.m_ampere);
    public static ElectricCurrent Subtract(ElectricCurrent left, ElectricCurrent right)
      => new ElectricCurrent(left.m_ampere - right.m_ampere);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricCurrent v)
      => v.m_ampere;
    public static explicit operator ElectricCurrent(double v)
      => new ElectricCurrent(v);

    public static bool operator <(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(ElectricCurrent a, ElectricCurrent b)
      => a.Equals(b);
    public static bool operator !=(ElectricCurrent a, ElectricCurrent b)
      => !a.Equals(b);

    public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b)
      => Add(a, b);
    public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b)
      => Divide(a, b);
    public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b)
      => Multiply(a, b);
    public static ElectricCurrent operator -(ElectricCurrent v)
      => Negate(v);
    public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b)
      => Remainder(a, b);
    public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCurrent other)
      => m_ampere.CompareTo(other.m_ampere);

    // IEquatable
    public bool Equals(ElectricCurrent other)
      => m_ampere == other.m_ampere;

    // IUnitStandardized
    public double GetScalar()
      => m_ampere;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCurrent o && Equals(o);
    public override int GetHashCode()
      => m_ampere.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_ampere} A>";
    #endregion Object overrides
  }
}
