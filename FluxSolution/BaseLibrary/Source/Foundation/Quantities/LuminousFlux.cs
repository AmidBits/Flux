namespace Flux.Quantity
{
  public enum LuminousFluxUnit
  {
    Lumen,
  }

  /// <summary>Luminous flux unit of lumen.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
#if NET5_0
  public struct LuminousFlux
    : System.IComparable<LuminousFlux>, System.IEquatable<LuminousFlux>, IValuedUnit<double>
#else
  public record struct LuminousFlux
    : System.IComparable<LuminousFlux>, IValuedUnit<double>
#endif
  {
    private readonly double m_value;

    public LuminousFlux(double value, LuminousFluxUnit unit = LuminousFluxUnit.Lumen)
      => m_value = unit switch
      {
        LuminousFluxUnit.Lumen => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(LuminousFluxUnit unit = LuminousFluxUnit.Lumen)
      => unit switch
      {
        LuminousFluxUnit.Lumen => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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

#if NET5_0
    public static bool operator ==(LuminousFlux a, LuminousFlux b)
      => a.Equals(b);
    public static bool operator !=(LuminousFlux a, LuminousFlux b)
      => !a.Equals(b);
#endif

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

#if NET5_0
    // IEquatable
    public bool Equals(LuminousFlux other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is LuminousFlux o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} lm }}";
    #endregion Object overrides
  }
}
