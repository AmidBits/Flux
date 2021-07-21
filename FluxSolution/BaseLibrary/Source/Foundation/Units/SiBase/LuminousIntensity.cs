namespace Flux.Units
{
  public enum LuminousIntensityUnit
  {
    Candela,
  }

  /// <summary>Luminous intensity unit of candela.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct LuminousIntensity
    : System.IComparable<LuminousIntensity>, System.IEquatable<LuminousIntensity>, IValuedUnit
  {
    private readonly double m_value;

    public LuminousIntensity(double candela)
      => m_value = candela;

    public double Value
      => m_value;

    public double ToUnitValue(LuminousIntensityUnit unit)
    {
      switch (unit)
      {
        case LuminousIntensityUnit.Candela:
          return m_value;
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
      => v.m_value;
    public static explicit operator LuminousIntensity(double v)
      => new LuminousIntensity(v);

    public static bool operator <(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(LuminousIntensity a, LuminousIntensity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(LuminousIntensity a, LuminousIntensity b)
      => a.Equals(b);
    public static bool operator !=(LuminousIntensity a, LuminousIntensity b)
      => !a.Equals(b);

    public static LuminousIntensity operator -(LuminousIntensity v)
      => new LuminousIntensity(-v.m_value);
    public static LuminousIntensity operator +(LuminousIntensity a, double b)
      => new LuminousIntensity(a.m_value + b);
    public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b)
      => a + b.m_value;
    public static LuminousIntensity operator /(LuminousIntensity a, double b)
      => new LuminousIntensity(a.m_value / b);
    public static LuminousIntensity operator /(LuminousIntensity a, LuminousIntensity b)
      => a / b.m_value;
    public static LuminousIntensity operator *(LuminousIntensity a, double b)
      => new LuminousIntensity(a.m_value * b);
    public static LuminousIntensity operator *(LuminousIntensity a, LuminousIntensity b)
      => a * b.m_value;
    public static LuminousIntensity operator %(LuminousIntensity a, double b)
      => new LuminousIntensity(a.m_value % b);
    public static LuminousIntensity operator %(LuminousIntensity a, LuminousIntensity b)
      => a % b.m_value;
    public static LuminousIntensity operator -(LuminousIntensity a, double b)
      => new LuminousIntensity(a.m_value - b);
    public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(LuminousIntensity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(LuminousIntensity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LuminousIntensity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} cd>";
    #endregion Object overrides
  }
}
