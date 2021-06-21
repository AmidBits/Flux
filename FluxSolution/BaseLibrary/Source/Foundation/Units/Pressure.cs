namespace Flux.Units
{
  /// <summary>Pressure.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Pressure"/>
  public struct Pressure
    : System.IComparable<Pressure>, System.IEquatable<Pressure>
  {
    private readonly double m_pascal;

    public Pressure(double pascal)
      => m_pascal = pascal;

    public double Pascal
      => m_pascal;
    public double Psi
      => ConvertPascalToPsi(m_pascal);

    #region Static methods
    public static Pressure Add(Pressure left, Pressure right)
      => new Pressure(left.m_pascal + right.m_pascal);
    public static double ConvertPascalToPsi(double pascal)
      => pascal / 6894.7572932; // * 0.0001450377;
    public static double ConvertPsiToPascal(double psi)
      => psi * 6894.7572932;
    public static Pressure Divide(Pressure left, Pressure right)
      => new Pressure(left.m_pascal / right.m_pascal);
    public static Pressure FromPsi(double psi)
      => new Pressure(ConvertPsiToPascal(psi));
    public static Pressure Multiply(Pressure left, Pressure right)
      => new Pressure(left.m_pascal * right.m_pascal);
    public static Pressure Negate(Pressure value)
      => new Pressure(-value.m_pascal);
    public static Pressure Remainder(Pressure dividend, Pressure divisor)
      => new Pressure(dividend.m_pascal % divisor.m_pascal);
    public static Pressure Subtract(Pressure left, Pressure right)
      => new Pressure(left.m_pascal - right.m_pascal);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Pressure v)
      => v.m_pascal;
    public static implicit operator Pressure(double v)
      => new Pressure(v);

    public static bool operator <(Pressure a, Pressure b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Pressure a, Pressure b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Pressure a, Pressure b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Pressure a, Pressure b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Pressure a, Pressure b)
      => a.Equals(b);
    public static bool operator !=(Pressure a, Pressure b)
      => !a.Equals(b);

    public static Pressure operator +(Pressure a, Pressure b)
      => Add(a, b);
    public static Pressure operator /(Pressure a, Pressure b)
      => Divide(a, b);
    public static Pressure operator *(Pressure a, Pressure b)
      => Multiply(a, b);
    public static Pressure operator -(Pressure v)
      => Negate(v);
    public static Pressure operator %(Pressure a, Pressure b)
      => Remainder(a, b);
    public static Pressure operator -(Pressure a, Pressure b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Pressure other)
      => m_pascal.CompareTo(other.m_pascal);

    // IEquatable
    public bool Equals(Pressure other)
      => m_pascal == other.m_pascal;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Pressure o && Equals(o);
    public override int GetHashCode()
      => m_pascal.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_pascal} Pa>";
    #endregion Object overrides
  }
}
