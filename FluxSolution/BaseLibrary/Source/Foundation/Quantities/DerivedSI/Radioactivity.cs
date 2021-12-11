namespace Flux
{
  public enum RadioactivityUnit
  {
    Becquerel,
  }

  /// <summary>Radioactivity unit of becquerel.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Power"/>
  public struct Radioactivity
    : System.IComparable<Radioactivity>, System.IEquatable<Radioactivity>, IUnitValueGeneralized<double>, IValueDerivedUnitSI<double>
  {
    private readonly double m_value;

    public Radioactivity(double value, RadioactivityUnit unit = RadioactivityUnit.Becquerel)
      => m_value = unit switch
      {
        RadioactivityUnit.Becquerel => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public double ToUnitValue(RadioactivityUnit unit = RadioactivityUnit.Becquerel)
      => unit switch
      {
        RadioactivityUnit.Becquerel => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Radioactivity v)
      => v.m_value;
    public static explicit operator Radioactivity(double v)
      => new(v);

    public static bool operator <(Radioactivity a, Radioactivity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Radioactivity a, Radioactivity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Radioactivity a, Radioactivity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Radioactivity a, Radioactivity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Radioactivity a, Radioactivity b)
      => a.Equals(b);
    public static bool operator !=(Radioactivity a, Radioactivity b)
      => !a.Equals(b);

    public static Radioactivity operator -(Radioactivity v)
      => new(-v.m_value);
    public static Radioactivity operator +(Radioactivity a, double b)
      => new(a.m_value + b);
    public static Radioactivity operator +(Radioactivity a, Radioactivity b)
      => a + b.m_value;
    public static Radioactivity operator /(Radioactivity a, double b)
      => new(a.m_value / b);
    public static Radioactivity operator /(Radioactivity a, Radioactivity b)
      => a / b.m_value;
    public static Radioactivity operator *(Radioactivity a, double b)
      => new(a.m_value * b);
    public static Radioactivity operator *(Radioactivity a, Radioactivity b)
      => a * b.m_value;
    public static Radioactivity operator %(Radioactivity a, double b)
      => new(a.m_value % b);
    public static Radioactivity operator %(Radioactivity a, Radioactivity b)
      => a % b.m_value;
    public static Radioactivity operator -(Radioactivity a, double b)
      => new(a.m_value - b);
    public static Radioactivity operator -(Radioactivity a, Radioactivity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Radioactivity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Radioactivity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Radioactivity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} Bq }}";
    #endregion Object overrides
  }
}
