namespace Flux.Units
{
  /// <summary>A unit for amount of substance.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct Enplethy
    : System.IComparable<Enplethy>, System.IEquatable<Enplethy>, IStandardizedScalar
  {
    private readonly double m_mole;

    public Enplethy(double mole)
      => m_mole = mole;

    public double Mole
      => m_mole;

    #region Static methods
    public static Enplethy Add(Enplethy left, Enplethy right)
      => new Enplethy(left.m_mole + right.m_mole);
    public static Enplethy Divide(Enplethy left, Enplethy right)
      => new Enplethy(left.m_mole / right.m_mole);
    public static Enplethy Multiply(Enplethy left, Enplethy right)
      => new Enplethy(left.m_mole * right.m_mole);
    public static Enplethy Negate(Enplethy value)
      => new Enplethy(-value.m_mole);
    public static Enplethy Remainder(Enplethy dividend, Enplethy divisor)
      => new Enplethy(dividend.m_mole % divisor.m_mole);
    public static Enplethy Subtract(Enplethy left, Enplethy right)
      => new Enplethy(left.m_mole - right.m_mole);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Enplethy v)
      => v.m_mole;
    public static implicit operator Enplethy(double v)
      => new Enplethy(v);

    public static bool operator <(Enplethy a, Enplethy b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Enplethy a, Enplethy b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Enplethy a, Enplethy b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Enplethy a, Enplethy b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Enplethy a, Enplethy b)
      => a.Equals(b);
    public static bool operator !=(Enplethy a, Enplethy b)
      => !a.Equals(b);

    public static Enplethy operator +(Enplethy a, Enplethy b)
      => Add(a, b);
    public static Enplethy operator /(Enplethy a, Enplethy b)
      => Divide(a, b);
    public static Enplethy operator *(Enplethy a, Enplethy b)
      => Multiply(a, b);
    public static Enplethy operator -(Enplethy v)
      => Negate(v);
    public static Enplethy operator %(Enplethy a, Enplethy b)
      => Remainder(a, b);
    public static Enplethy operator -(Enplethy a, Enplethy b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Enplethy other)
      => m_mole.CompareTo(other.m_mole);

    // IEquatable
    public bool Equals(Enplethy other)
      => m_mole == other.m_mole;

    // IUnitStandardized
    public double GetScalar()
      => m_mole;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Enplethy o && Equals(o);
    public override int GetHashCode()
      => m_mole.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_mole} mol>";
    #endregion Object overrides
  }
}