namespace Flux.Quantity
{
  public enum InductanceUnit
  {
    Henry,
  }

  /// <summary>Electrical inductance unit of Henry.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Inductance"/>
  public struct Inductance
    : System.IComparable<Inductance>, System.IEquatable<Inductance>, IValuedUnit
  {
    private readonly double m_value;

    public Inductance(double value, InductanceUnit unit = InductanceUnit.Henry)
    {
      switch (unit)
      {
        case InductanceUnit.Henry:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(InductanceUnit unit = InductanceUnit.Henry)
    {
      switch (unit)
      {
        case InductanceUnit.Henry:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Overloaded operators
    public static explicit operator double(Inductance v)
      => v.m_value;
    public static explicit operator Inductance(double v)
      => new Inductance(v);

    public static bool operator <(Inductance a, Inductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Inductance a, Inductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Inductance a, Inductance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Inductance a, Inductance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Inductance a, Inductance b)
      => a.Equals(b);
    public static bool operator !=(Inductance a, Inductance b)
      => !a.Equals(b);

    public static Inductance operator -(Inductance v)
      => new Inductance(-v.m_value);
    public static Inductance operator +(Inductance a, double b)
      => new Inductance(a.m_value + b);
    public static Inductance operator +(Inductance a, Inductance b)
      => a + b.m_value;
    public static Inductance operator /(Inductance a, double b)
      => new Inductance(a.m_value / b);
    public static Inductance operator /(Inductance a, Inductance b)
      => a / b.m_value;
    public static Inductance operator *(Inductance a, double b)
      => new Inductance(a.m_value * b);
    public static Inductance operator *(Inductance a, Inductance b)
      => a * b.m_value;
    public static Inductance operator %(Inductance a, double b)
      => new Inductance(a.m_value % b);
    public static Inductance operator %(Inductance a, Inductance b)
      => a % b.m_value;
    public static Inductance operator -(Inductance a, double b)
      => new Inductance(a.m_value - b);
    public static Inductance operator -(Inductance a, Inductance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Inductance other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Inductance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Inductance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} H>";
    #endregion Object overrides
  }
}
