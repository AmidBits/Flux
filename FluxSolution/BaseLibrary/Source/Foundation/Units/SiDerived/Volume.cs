namespace Flux.Units
{
  /// <summary>Volume unit of cubic meter.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Volume"/>
  public struct Volume
    : System.IComparable<Volume>, System.IEquatable<Volume>, IValuedUnit
  {
    private readonly double m_value;

    public Volume(double cubicMeter)
      => m_value = cubicMeter;

    public double Value
      => m_value;

    #region Static methods
    /// <summary>Creates a new Volumne instance from the specified rectangular length, width and height.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static Volume From(Length length, Length width, Length height)
      => new Volume(length.Value * width.Value * height.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Volume v)
      => v.m_value;
    public static explicit operator Volume(double v)
      => new Volume(v);

    public static bool operator <(Volume a, Volume b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Volume a, Volume b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Volume a, Volume b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Volume a, Volume b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Volume a, Volume b)
      => a.Equals(b);
    public static bool operator !=(Volume a, Volume b)
      => !a.Equals(b);

    public static Volume operator -(Volume v)
      => new Volume(-v.m_value);
    public static Volume operator +(Volume a, double b)
      => new Volume(a.m_value + b);
    public static Volume operator +(Volume a, Volume b)
      => a + b.m_value;
    public static Volume operator /(Volume a, double b)
      => new Volume(a.m_value / b);
    public static Volume operator /(Volume a, Volume b)
      => a / b.m_value;
    public static Volume operator *(Volume a, double b)
      => new Volume(a.m_value * b);
    public static Volume operator *(Volume a, Volume b)
      => a * b.m_value;
    public static Volume operator %(Volume a, double b)
      => new Volume(a.m_value % b);
    public static Volume operator %(Volume a, Volume b)
      => a % b.m_value;
    public static Volume operator -(Volume a, double b)
      => new Volume(a.m_value - b);
    public static Volume operator -(Volume a, Volume b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Volume other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Volume other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Volume o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} m³>";
    #endregion Object overrides
  }
}
