namespace Flux.Units
{
  /// <summary>A unit for amount of substance.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct LuminousFlux
    : System.IComparable<LuminousFlux>, System.IEquatable<LuminousFlux>, IStandardizedScalar
  {
    private readonly double m_lumen;

    public LuminousFlux(double lumen)
      => m_lumen = lumen;

    public double Lumen
      => m_lumen;

    #region Overloaded operators
    public static explicit operator double(LuminousFlux v)
      => v.m_lumen;
    public static explicit operator LuminousFlux(double v)
      => new LuminousFlux(v);

    public static bool operator <(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(LuminousFlux a, LuminousFlux b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(LuminousFlux a, LuminousFlux b)
      => a.Equals(b);
    public static bool operator !=(LuminousFlux a, LuminousFlux b)
      => !a.Equals(b);

    public static LuminousFlux operator -(LuminousFlux v)
      => new LuminousFlux(-v.m_lumen);
    public static LuminousFlux operator +(LuminousFlux a, LuminousFlux b)
      => new LuminousFlux(a.m_lumen + b.m_lumen);
    public static LuminousFlux operator /(LuminousFlux a, LuminousFlux b)
      => new LuminousFlux(a.m_lumen / b.m_lumen);
    public static LuminousFlux operator *(LuminousFlux a, LuminousFlux b)
      => new LuminousFlux(a.m_lumen * b.m_lumen);
    public static LuminousFlux operator %(LuminousFlux a, LuminousFlux b)
      => new LuminousFlux(a.m_lumen % b.m_lumen);
    public static LuminousFlux operator -(LuminousFlux a, LuminousFlux b)
      => new LuminousFlux(a.m_lumen - b.m_lumen);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(LuminousFlux other)
      => m_lumen.CompareTo(other.m_lumen);

    // IEquatable
    public bool Equals(LuminousFlux other)
      => m_lumen == other.m_lumen;

    // IUnitStandardized
    public double GetScalar()
      => m_lumen;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is LuminousFlux o && Equals(o);
    public override int GetHashCode()
      => m_lumen.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_lumen} lm>";
    #endregion Object overrides
  }
}
