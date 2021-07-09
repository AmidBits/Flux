namespace Flux.Units
{
  public enum MassUnit
  {
    Milligram,
    Gram,
    Ounce,
    Pound,
    Kilogram,
  }

  /// <summary>Mass.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Mass"/>
  public struct Mass
    : System.IComparable<Mass>, System.IEquatable<Mass>, IStandardizedScalar
  {
    private readonly double m_kilogram;

    public Mass(double kilogram)
      => m_kilogram = kilogram;

    public double Kilogram
      => m_kilogram;

    public double ToUnitValue(MassUnit unit)
    {
      switch (unit)
      {
        case MassUnit.Milligram:
          return m_kilogram * 1000000;
        case MassUnit.Gram:
          return m_kilogram * 1000;
        case MassUnit.Ounce:
          return m_kilogram * 35.27396195;
        case MassUnit.Pound:
          return m_kilogram / 0.45359237;
        case MassUnit.Kilogram:
          return m_kilogram;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    public static Mass FromUnitValue(MassUnit unit, double value)
    {
      switch (unit)
      {
        case MassUnit.Milligram:
          return new Mass(value / 1000000);
        case MassUnit.Gram:
          return new Mass(value / 1000);
        case MassUnit.Ounce:
          return new Mass(value / 35.27396195);
        case MassUnit.Pound:
          return new Mass(value * 0.45359237);
        case MassUnit.Kilogram:
          return new Mass(value);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Mass v)
      => v.m_kilogram;
    public static explicit operator Mass(double v)
      => new Mass(v);

    public static bool operator <(Mass a, Mass b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Mass a, Mass b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Mass a, Mass b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Mass a, Mass b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Mass a, Mass b)
      => a.Equals(b);
    public static bool operator !=(Mass a, Mass b)
      => !a.Equals(b);

    public static Mass operator -(Mass v)
      => new Mass(-v.m_kilogram);
    public static Mass operator +(Mass a, Mass b)
      => new Mass(a.m_kilogram + b.m_kilogram);
    public static Mass operator /(Mass a, Mass b)
      => new Mass(a.m_kilogram / b.m_kilogram);
    public static Mass operator *(Mass a, Mass b)
      => new Mass(a.m_kilogram * b.m_kilogram);
    public static Mass operator %(Mass a, Mass b)
      => new Mass(a.m_kilogram % b.m_kilogram);
    public static Mass operator -(Mass a, Mass b)
      => new Mass(a.m_kilogram - b.m_kilogram);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Mass other)
      => m_kilogram.CompareTo(other.m_kilogram);

    // IEquatable
    public bool Equals(Mass other)
      => m_kilogram == other.m_kilogram;

    // IUnitStandardized
    public double GetScalar()
      => m_kilogram;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Mass o && Equals(o);
    public override int GetHashCode()
      => m_kilogram.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_kilogram} kg>";
    #endregion Object overrides
  }
}
