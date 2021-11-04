namespace Flux.Quantity
{
  public enum LuminousFluxUnit
  {
    Lumen,
  }

  /// <summary>Luminous flux unit of lumen.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct LuminousFlux
    : System.IComparable<LuminousFlux>, System.IEquatable<LuminousFlux>, IValuedUnit
  {
    private readonly double m_value;

    public LuminousFlux(double value, LuminousFluxUnit unit = LuminousFluxUnit.Lumen)
    {
      switch (unit)
      {
        case LuminousFluxUnit.Lumen:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(LuminousFluxUnit unit = LuminousFluxUnit.Lumen)
    {
      switch (unit)
      {
        case LuminousFluxUnit.Lumen:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Overloaded operators
    public static explicit operator double(LuminousFlux v)
      => v.m_value;
    public static explicit operator LuminousFlux(double v)
      => new(v);

    public static bool operator <(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(LuminousFlux a, LuminousFlux b)
      => a.Equals(b);
    public static bool operator !=(LuminousFlux a, LuminousFlux b)
      => !a.Equals(b);

    public static LuminousFlux operator -(LuminousFlux v)
      => new(-v.m_value);
    public static LuminousFlux operator +(LuminousFlux a, double b)
      => new(a.m_value + b);
    public static LuminousFlux operator +(LuminousFlux a, LuminousFlux b)
      => a + b.m_value;
    public static LuminousFlux operator /(LuminousFlux a, double b)
      => new(a.m_value / b);
    public static LuminousFlux operator /(LuminousFlux a, LuminousFlux b)
      => a / b.m_value;
    public static LuminousFlux operator *(LuminousFlux a, double b)
      => new(a.m_value * b);
    public static LuminousFlux operator *(LuminousFlux a, LuminousFlux b)
      => a * b.m_value;
    public static LuminousFlux operator %(LuminousFlux a, double b)
      => new(a.m_value % b);
    public static LuminousFlux operator %(LuminousFlux a, LuminousFlux b)
      => a % b.m_value;
    public static LuminousFlux operator -(LuminousFlux a, double b)
      => new(a.m_value - b);
    public static LuminousFlux operator -(LuminousFlux a, LuminousFlux b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(LuminousFlux other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(LuminousFlux other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LuminousFlux o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} lm>";
    #endregion Object overrides
  }
}
