namespace Flux.Quantity
{
  public enum EnplethyUnit
  {
    Mole,
  }

  /// <summary>Enplethy, or amount of substance. SI unit of mole. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct Enplethy
    : System.IComparable<Enplethy>, System.IEquatable<Enplethy>, IValuedSiBaseUnit
  {
    // The unit of the Avagadro constant is the reciprocal mole, i.e. "per" mole.
    public static Enplethy AvagadroConstant
      => new Enplethy(6.02214076e23);

    private readonly double m_value;

    public Enplethy(double mole)
      => m_value = mole;

    public double Value
      => m_value;

    public double ToUnitValue(EnplethyUnit unit)
    {
      switch (unit)
      {
        case EnplethyUnit.Mole:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    public static Enplethy FromUnitValue(EnplethyUnit unit, double value)
    {
      switch (unit)
      {
        case EnplethyUnit.Mole:
          return new Enplethy(value);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Enplethy v)
      => v.m_value;
    public static explicit operator Enplethy(double v)
      => new Enplethy(v);

    public static bool operator <(Enplethy a, Enplethy b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Enplethy a, Enplethy b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Enplethy a, Enplethy b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Enplethy a, Enplethy b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Enplethy a, Enplethy b)
      => a.Equals(b);
    public static bool operator !=(Enplethy a, Enplethy b)
      => !a.Equals(b);

    public static Enplethy operator -(Enplethy v)
      => new Enplethy(-v.Value);
    public static Enplethy operator +(Enplethy a, double b)
      => new Enplethy(a.m_value + b);
    public static Enplethy operator +(Enplethy a, Enplethy b)
      => a + b.m_value;
    public static Enplethy operator /(Enplethy a, double b)
      => new Enplethy(a.m_value / b);
    public static Enplethy operator /(Enplethy a, Enplethy b)
      => a / b.m_value;
    public static Enplethy operator *(Enplethy a, double b)
      => new Enplethy(a.m_value * b);
    public static Enplethy operator *(Enplethy a, Enplethy b)
      => a * b.m_value;
    public static Enplethy operator %(Enplethy a, double b)
      => new Enplethy(a.m_value % b);
    public static Enplethy operator %(Enplethy a, Enplethy b)
      => a % b.m_value;
    public static Enplethy operator -(Enplethy a, double b)
      => new Enplethy(a.m_value - b);
    public static Enplethy operator -(Enplethy a, Enplethy b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Enplethy other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Enplethy other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Enplethy o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} mol>";
    #endregion Object overrides
  }
}