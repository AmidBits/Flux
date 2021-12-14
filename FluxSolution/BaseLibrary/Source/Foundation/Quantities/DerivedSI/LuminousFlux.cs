namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static LuminousFlux Create(this LuminousFluxUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this LuminousFluxUnit unit)
      => unit switch
      {
        LuminousFluxUnit.Lumen => @" lm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum LuminousFluxUnit
  {
    Lumen,
  }

  /// <summary>Luminous flux unit of lumen.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct LuminousFlux
    : System.IComparable<LuminousFlux>, System.IEquatable<LuminousFlux>, IValueGeneralizedUnit<double>, IValueSiDerivedUnit<double>
  {
    public const LuminousFluxUnit DefaultUnit = LuminousFluxUnit.Lumen;

    private readonly double m_value;

    public LuminousFlux(double value, LuminousFluxUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        LuminousFluxUnit.Lumen => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(LuminousFluxUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(LuminousFluxUnit unit = DefaultUnit)
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
      => $"{GetType().Name} {{ Value = {m_value} lm }}";
    #endregion Object overrides
  }
}
