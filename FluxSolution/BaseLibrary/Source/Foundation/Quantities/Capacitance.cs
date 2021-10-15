namespace Flux.Quantity
{
  public enum CapacitanceUnit
  {
    Farad,
  }

  /// <summary>Electrical capacitance unit of Farad.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Capacitance"/>
  public struct Capacitance
    : System.IComparable<Capacitance>, System.IEquatable<Capacitance>, IValuedUnit
  {
    private readonly double m_value;

    public Capacitance(double value, CapacitanceUnit unit = CapacitanceUnit.Farad)
    {
      switch (unit)
      {
        case CapacitanceUnit.Farad:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(CapacitanceUnit unit = CapacitanceUnit.Farad)
    {
      switch (unit)
      {
        case CapacitanceUnit.Farad:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Overloaded operators
    public static explicit operator double(Capacitance v)
      => v.m_value;
    public static explicit operator Capacitance(double v)
      => new Capacitance(v);

    public static bool operator <(Capacitance a, Capacitance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Capacitance a, Capacitance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Capacitance a, Capacitance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Capacitance a, Capacitance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Capacitance a, Capacitance b)
      => a.Equals(b);
    public static bool operator !=(Capacitance a, Capacitance b)
      => !a.Equals(b);

    public static Capacitance operator -(Capacitance v)
      => new Capacitance(-v.m_value);
    public static Capacitance operator +(Capacitance a, double b)
      => new Capacitance(a.m_value + b);
    public static Capacitance operator +(Capacitance a, Capacitance b)
      => a + b.m_value;
    public static Capacitance operator /(Capacitance a, double b)
      => new Capacitance(a.m_value / b);
    public static Capacitance operator /(Capacitance a, Capacitance b)
      => a / b.m_value;
    public static Capacitance operator *(Capacitance a, double b)
      => new Capacitance(a.m_value * b);
    public static Capacitance operator *(Capacitance a, Capacitance b)
      => a * b.m_value;
    public static Capacitance operator %(Capacitance a, double b)
      => new Capacitance(a.m_value % b);
    public static Capacitance operator %(Capacitance a, Capacitance b)
      => a % b.m_value;
    public static Capacitance operator -(Capacitance a, double b)
      => new Capacitance(a.m_value - b);
    public static Capacitance operator -(Capacitance a, Capacitance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Capacitance other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Capacitance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Capacitance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} F>";
    #endregion Object overrides
  }
}
