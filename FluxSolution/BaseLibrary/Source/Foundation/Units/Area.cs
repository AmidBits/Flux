namespace Flux.Units
{
  /// <summary>Area.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Area"/>
  public struct Area
    : System.IComparable<Area>, System.IEquatable<Area>, System.IFormattable
  {
    private readonly double m_squareMeter;

    public Area(double squareMeter)
      => m_squareMeter = squareMeter;

    public double SquareMeter
      => m_squareMeter;

    #region Static methods
    public static Area Add(Area left, Area right)
      => new Area(left.m_squareMeter + right.m_squareMeter);
    public static Area Divide(Area left, Area right)
      => new Area(left.m_squareMeter / right.m_squareMeter);
    public static Area FromRectangule(double lengthInMeters, double widthInMeters)
      => new Area(lengthInMeters * widthInMeters);
    public static Area Multiply(Area left, Area right)
      => new Area(left.m_squareMeter * right.m_squareMeter);
    public static Area Negate(Area value)
      => new Area(-value.m_squareMeter);
    public static Area Remainder(Area dividend, Area divisor)
      => new Area(dividend.m_squareMeter % divisor.m_squareMeter);
    public static Area Subtract(Area left, Area right)
      => new Area(left.m_squareMeter - right.m_squareMeter);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Area v)
      => v.m_squareMeter;
    public static implicit operator Area(double v)
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

    public static Area operator +(Area a, Area b)
      => Add(a, b);
    public static Area operator /(Area a, Area b)
      => Divide(a, b);
    public static Area operator *(Area a, Area b)
      => Multiply(a, b);
    public static Area operator -(Area v)
      => Negate(v);
    public static Area operator %(Area a, Area b)
      => Remainder(a, b);
    public static Area operator -(Area a, Area b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Area other)
      => m_squareMeter.CompareTo(other.m_squareMeter);

    // IEquatable
    public bool Equals(Area other)
      => m_squareMeter == other.m_squareMeter;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Area)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Area o && Equals(o);
    public override int GetHashCode()
      => m_squareMeter.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
