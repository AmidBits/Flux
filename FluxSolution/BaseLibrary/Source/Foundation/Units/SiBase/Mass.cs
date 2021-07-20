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

  /// <summary>Mass unit of kilogram.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Mass"/>
  public struct Mass
    : System.IComparable<Mass>, System.IEquatable<Mass>, IStandardizedScalar
  {
    private readonly double m_value;

    public Mass(double kilogram)
      => m_value = kilogram;

    public double Value
      => m_value;

    public double ToUnitValue(MassUnit unit)
    {
      switch (unit)
      {
        case MassUnit.Milligram:
          return m_value * 1000000;
        case MassUnit.Gram:
          return m_value * 1000;
        case MassUnit.Ounce:
          return m_value * 35.27396195;
        case MassUnit.Pound:
          return m_value / 0.45359237;
        case MassUnit.Kilogram:
          return m_value;
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
      => v.m_value;
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
      => new Mass(-v.m_value);
    public static Mass operator +(Mass a, Mass b)
      => new Mass(a.m_value + b.m_value);
    public static Mass operator /(Mass a, Mass b)
      => new Mass(a.m_value / b.m_value);
    public static Mass operator *(Mass a, Mass b)
      => new Mass(a.m_value * b.m_value);
    public static Mass operator %(Mass a, Mass b)
      => new Mass(a.m_value % b.m_value);
    public static Mass operator -(Mass a, Mass b)
      => new Mass(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Mass other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Mass other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Mass o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} kg>";
    #endregion Object overrides
  }
}
