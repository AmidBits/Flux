namespace Flux.Units
{
  public enum EnplethyUnit
  {
    Mole,
  }

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

    public double ToUnitValue(EnplethyUnit unit)
    {
      switch (unit)
      {
        case EnplethyUnit.Mole:
          return m_mole;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    public static Enplethy FromUnitValue(double value, EnplethyUnit unit)
    {
      switch (unit)
      {
        case EnplethyUnit.Mole:
          return new Enplethy(value);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Enplethy v)
      => v.m_mole;
    public static explicit operator Enplethy(double v)
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

    public static Enplethy operator -(Enplethy v)
      => new Enplethy(-v.Mole);
    public static Enplethy operator +(Enplethy a, Enplethy b)
      => new Enplethy(a.m_mole + b.m_mole);
    public static Enplethy operator /(Enplethy a, Enplethy b)
      => new Enplethy(a.m_mole / b.m_mole);
    public static Enplethy operator *(Enplethy a, Enplethy b)
      => new Enplethy(a.m_mole * b.m_mole);
    public static Enplethy operator %(Enplethy a, Enplethy b)
      => new Enplethy(a.m_mole % b.m_mole);
    public static Enplethy operator -(Enplethy a, Enplethy b)
      => new Enplethy(a.m_mole - b.m_mole);
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
