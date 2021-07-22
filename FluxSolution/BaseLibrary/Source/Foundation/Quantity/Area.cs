namespace Flux.Quantity
{
  /// <summary>Area, unit of square meter. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Area"/>
  public struct Area
    : System.IComparable<Area>, System.IEquatable<Area>, IValuedSiDerivedUnit
  {
    public const string Symbol = @"m²";

    private readonly double m_value;

    public Area(double squareMeter)
      => m_value = squareMeter;

    public double Value
      => m_value;

    #region Static methods
    /// <summary>Creates a new Area instance from the specified rectangular length and width.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    public static Area From(Length length, Length width)
      => new Area(length.Value * width.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Area v)
      => v.m_value;
    public static explicit operator Area(double v)
      => new Area(v);

    public static bool operator <(Area a, Area b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Area a, Area b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Area a, Area b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Area a, Area b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Area a, Area b)
      => a.Equals(b);
    public static bool operator !=(Area a, Area b)
      => !a.Equals(b);

    public static Area operator -(Area v)
      => new Area(-v.m_value);
    public static Area operator +(Area a, double b)
      => new Area(a.m_value + b);
    public static Area operator +(Area a, Area b)
      => a + b.m_value;
    public static Area operator /(Area a, double b)
      => new Area(a.m_value / b);
    public static Area operator /(Area a, Area b)
      => a / b.m_value;
    public static Area operator *(Area a, double b)
      => new Area(a.m_value * b);
    public static Area operator *(Area a, Area b)
      => a * b.m_value;
    public static Area operator %(Area a, double b)
      => new Area(a.m_value % b);
    public static Area operator %(Area a, Area b)
      => a % b.m_value;
    public static Area operator -(Area a, double b)
      => new Area(a.m_value - b);
    public static Area operator -(Area a, Area b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Area other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Area other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Area o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} {Symbol}>";
    #endregion Object overrides
  }
}
