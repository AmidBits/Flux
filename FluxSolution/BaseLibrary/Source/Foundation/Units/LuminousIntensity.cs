namespace Flux.Units
{
  /// <summary>A unit for amount of substance.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct LuminousIntensity
    : System.IComparable<LuminousIntensity>, System.IEquatable<LuminousIntensity>, IStandardizedScalar
  {
    private readonly double m_candela;

    public LuminousIntensity(double candela)
      => m_candela = candela;

    public double Candela
      => m_candela;

    #region Static methods
    public static LuminousIntensity Add(LuminousIntensity left, LuminousIntensity right)
      => new LuminousIntensity(left.m_candela + right.m_candela);
    public static LuminousIntensity Divide(LuminousIntensity left, LuminousIntensity right)
      => new LuminousIntensity(left.m_candela / right.m_candela);
    public static LuminousIntensity Multiply(LuminousIntensity left, LuminousIntensity right)
      => new LuminousIntensity(left.m_candela * right.m_candela);
    public static LuminousIntensity Negate(LuminousIntensity value)
      => new LuminousIntensity(-value.m_candela);
    public static LuminousIntensity Remainder(LuminousIntensity dividend, LuminousIntensity divisor)
      => new LuminousIntensity(dividend.m_candela % divisor.m_candela);
    public static LuminousIntensity Subtract(LuminousIntensity left, LuminousIntensity right)
      => new LuminousIntensity(left.m_candela - right.m_candela);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(LuminousIntensity v)
      => v.m_candela;
    public static implicit operator LuminousIntensity(double v)
      => new LuminousIntensity(v);

    public static bool operator <(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(LuminousIntensity a, LuminousIntensity b)
      => a.Equals(b);
    public static bool operator !=(LuminousIntensity a, LuminousIntensity b)
      => !a.Equals(b);

    public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b)
      => Add(a, b);
    public static LuminousIntensity operator /(LuminousIntensity a, LuminousIntensity b)
      => Divide(a, b);
    public static LuminousIntensity operator *(LuminousIntensity a, LuminousIntensity b)
      => Multiply(a, b);
    public static LuminousIntensity operator -(LuminousIntensity v)
      => Negate(v);
    public static LuminousIntensity operator %(LuminousIntensity a, LuminousIntensity b)
      => Remainder(a, b);
    public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(LuminousIntensity other)
      => m_candela.CompareTo(other.m_candela);

    // IEquatable
    public bool Equals(LuminousIntensity other)
      => m_candela == other.m_candela;

    // IUnitStandardized
    public double GetScalar()
      => m_candela;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LuminousIntensity o && Equals(o);
    public override int GetHashCode()
      => m_candela.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_candela} cd>";
    #endregion Object overrides
  }
}
