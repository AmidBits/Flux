namespace Flux.Units
{
  /// <summary>Volume.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Volume"/>
  public struct Volume
    : System.IComparable<Volume>, System.IEquatable<Volume>, IStandardizedScalar
  {
    private readonly double m_cubicMeter;

    public Volume(double cubicMeter)
      => m_cubicMeter = cubicMeter;

    public double CubicMeter
      => m_cubicMeter;

    #region Static methods
    public static Volume FromRectangularCuboid(double lengthInMeters, double widthInMeters, double heightInMeters)
      => new Volume(lengthInMeters * widthInMeters * heightInMeters);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Volume v)
      => v.m_cubicMeter;
    public static explicit operator Volume(double v)
      => new Volume(v);

    public static bool operator <(Volume a, Volume b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Volume a, Volume b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Volume a, Volume b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Volume a, Volume b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Volume a, Volume b)
      => a.Equals(b);
    public static bool operator !=(Volume a, Volume b)
      => !a.Equals(b);

    public static Volume operator -(Volume v)
      => new Volume(-v.m_cubicMeter);
    public static Volume operator +(Volume a, Volume b)
      => new Volume(a.m_cubicMeter + b.m_cubicMeter);
    public static Volume operator /(Volume a, Volume b)
      => new Volume(a.m_cubicMeter / b.m_cubicMeter);
    public static Volume operator *(Volume a, Volume b)
      => new Volume(a.m_cubicMeter * b.m_cubicMeter);
    public static Volume operator %(Volume a, Volume b)
      => new Volume(a.m_cubicMeter % b.m_cubicMeter);
    public static Volume operator -(Volume a, Volume b)
      => new Volume(a.m_cubicMeter - b.m_cubicMeter);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Volume other)
      => m_cubicMeter.CompareTo(other.m_cubicMeter);

    // IEquatable
    public bool Equals(Volume other)
      => m_cubicMeter == other.m_cubicMeter;

    // IUnitStandardized
    public double GetScalar()
      => m_cubicMeter;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Volume o && Equals(o);
    public override int GetHashCode()
      => m_cubicMeter.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_cubicMeter} m³>";
    #endregion Object overrides
  }
}
