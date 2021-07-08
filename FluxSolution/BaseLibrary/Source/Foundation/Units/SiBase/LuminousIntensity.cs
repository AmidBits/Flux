namespace Flux.Units
{
  public enum LuminousIntensityUnit
  {
    Candela,
  }

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

    public double ToUnitValue(LuminousIntensityUnit unit)
    {
      switch (unit)
      {
        case LuminousIntensityUnit.Candela:
          return m_candela;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    public static LuminousIntensity FromUnitValue(LuminousIntensityUnit unit, double value)
    {
      switch (unit)
      {
        case LuminousIntensityUnit.Candela:
          return new LuminousIntensity(value);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(LuminousIntensity v)
      => v.m_candela;
    public static explicit operator LuminousIntensity(double v)
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

    public static LuminousIntensity operator -(LuminousIntensity v)
      => new LuminousIntensity(-v.m_candela);
    public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b)
      => new LuminousIntensity(a.m_candela + b.m_candela);
    public static LuminousIntensity operator /(LuminousIntensity a, LuminousIntensity b)
      => new LuminousIntensity(a.m_candela / b.m_candela);
    public static LuminousIntensity operator *(LuminousIntensity a, LuminousIntensity b)
      => new LuminousIntensity(a.m_candela * b.m_candela);
    public static LuminousIntensity operator %(LuminousIntensity a, LuminousIntensity b)
      => new LuminousIntensity(a.m_candela % b.m_candela);
    public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b)
      => new LuminousIntensity(a.m_candela - b.m_candela);
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
