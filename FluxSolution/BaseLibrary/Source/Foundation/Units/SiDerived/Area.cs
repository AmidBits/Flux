namespace Flux.Units
{
  /// <summary>Area.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Area"/>
  public struct Area
    : System.IComparable<Area>, System.IEquatable<Area>, IStandardizedScalar
  {
    private readonly double m_squareMeter;

    public Area(double squareMeter)
      => m_squareMeter = squareMeter;

    public double SquareMeter
      => m_squareMeter;

    #region Static methods
    /// <summary>Creates a new Area instance from the specified rectangular length and width.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    public static Area From(Length length, Length width)
      => new Area(length.Meter * width.Meter);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Area v)
      => v.m_squareMeter;
    public static explicit operator Area(double v)
      => new Area(v);

    public static bool operator <(Area a, Area b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Area a, Area b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Area a, Area b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Area a, Area b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Area a, Area b)
      => a.Equals(b);
    public static bool operator !=(Area a, Area b)
      => !a.Equals(b);

    public static Area operator -(Area v)
      => new Area(-v.m_squareMeter);
    public static Area operator +(Area a, Area b)
      => new Area(a.m_squareMeter + b.m_squareMeter);
    public static Area operator /(Area a, Area b)
      => new Area(a.m_squareMeter + b.m_squareMeter);
    public static Area operator *(Area a, Area b)
      => new Area(a.m_squareMeter + b.m_squareMeter);
    public static Area operator %(Area a, Area b)
      => new Area(a.m_squareMeter + b.m_squareMeter);
    public static Area operator -(Area a, Area b)
      => new Area(a.m_squareMeter + b.m_squareMeter);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Area other)
      => m_squareMeter.CompareTo(other.m_squareMeter);

    // IEquatable
    public bool Equals(Area other)
      => m_squareMeter == other.m_squareMeter;

    // IUnitStandardized
    public double GetScalar()
      => m_squareMeter;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Area o && Equals(o);
    public override int GetHashCode()
      => m_squareMeter.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_squareMeter} m²>";
    #endregion Object overrides
  }
}
