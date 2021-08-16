namespace Flux.Quantity
{
  public enum LuminousIntensityUnit
  {
    Candela,
  }

  /// <summary>Luminous intensity. SI unit of candela. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct LuminousIntensity
    : System.IComparable<LuminousIntensity>, System.IEquatable<LuminousIntensity>, IValuedUnit
  {
    private readonly double m_value;

    public LuminousIntensity(double value, LuminousIntensityUnit unit = LuminousIntensityUnit.Candela)
    {
      switch (unit)
      {
        case LuminousIntensityUnit.Candela:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(LuminousIntensityUnit unit = LuminousIntensityUnit.Candela)
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
