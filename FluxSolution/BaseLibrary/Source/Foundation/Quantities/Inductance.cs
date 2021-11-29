namespace Flux.Quantity
{
  public enum InductanceUnit
  {
    Henry,
  }

  /// <summary>Electrical inductance unit of Henry.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Inductance"/>
#if NET5_0
  public struct Inductance
    : System.IComparable<Inductance>, System.IEquatable<Inductance>, IValuedUnit<double>
#else
  public record struct Inductance
    : System.IComparable<Inductance>, IValuedUnit<double>
#endif
  {
    private readonly double m_value;

    public Inductance(double value, InductanceUnit unit = InductanceUnit.Henry)
      => m_value = unit switch
      {
        InductanceUnit.Henry => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(InductanceUnit unit = InductanceUnit.Henry)
      => unit switch
      {
        InductanceUnit.Henry => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Inductance v)
      => v.m_value;
    public static explicit operator Inductance(double v)
      => new(v);

    public static bool operator <(Inductance a, Inductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Inductance a, Inductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Inductance a, Inductance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Inductance a, Inductance b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(Inductance a, Inductance b)
      => a.Equals(b);
    public static bool operator !=(Inductance a, Inductance b)
      => !a.Equals(b);
#endif

    public static Inductance operator -(Inductance v)
      => new(-v.m_value);
    public static Inductance operator +(Inductance a, double b)
      => new(a.m_value + b);
    public static Inductance operator +(Inductance a, Inductance b)
      => a + b.m_value;
    public static Inductance operator /(Inductance a, double b)
      => new(a.m_value / b);
    public static Inductance operator /(Inductance a, Inductance b)
      => a / b.m_value;
    public static Inductance operator *(Inductance a, double b)
      => new(a.m_value * b);
    public static Inductance operator *(Inductance a, Inductance b)
      => a * b.m_value;
    public static Inductance operator %(Inductance a, double b)
      => new(a.m_value % b);
    public static Inductance operator %(Inductance a, Inductance b)
      => a % b.m_value;
    public static Inductance operator -(Inductance a, double b)
      => new(a.m_value - b);
    public static Inductance operator -(Inductance a, Inductance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Inductance other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(Inductance other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Inductance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} H }}";
    #endregion Object overrides
  }
}
