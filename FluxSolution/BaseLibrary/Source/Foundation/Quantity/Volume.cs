namespace Flux.Quantity
{
  public enum VolumeUnit
  {
    Millilitre,
    Centilitre,
    Decilitre,
    Litre,
    UKGallon,
    USGallon,
    CubicFeet,
    CubicYard,
    CubicMeter,
    CubicMile,
    CubicKilometer,
  }

  /// <summary>Volume, unit of cubic meter. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Volume"/>
  public struct Volume
    : System.IComparable<Volume>, System.IEquatable<Volume>, IValuedSiDerivedUnit
  {
    private readonly double m_value;

    public Volume(double cubicMeter)
      => m_value = cubicMeter;

    public double Value
      => m_value;

    public double ToUnitValue(VolumeUnit unit)
    {
      switch (unit)
      {
        case VolumeUnit.Millilitre:
          return m_value * 1000000;
        case VolumeUnit.Centilitre:
          return m_value * 100000;
        case VolumeUnit.Decilitre:
          return m_value * 10000;
        case VolumeUnit.Litre:
          return m_value * 1000;
        case VolumeUnit.UKGallon:
          return m_value / 0.004546;
        case VolumeUnit.USGallon:
          return m_value / 0.003785;
        case VolumeUnit.CubicFeet:
          return m_value * (1953125000.0 / 55306341.0);
        case VolumeUnit.CubicYard:
          return m_value * (1953125000.0 / 1493271207.0);
        case VolumeUnit.CubicMeter:
          return m_value;
        case VolumeUnit.CubicMile:
          return m_value / (8140980127813632.0 / 1953125.0);
        case VolumeUnit.CubicKilometer:
          return m_value / 1e9;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    //473176473/125000000000 m^3 (cubic meters)
    #region Static methods
    /// <summary>Creates a new Volumne instance from the specified rectangular length, width and height.</summary>
    /// <param name="length"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static Volume From(Length length, Length width, Length height)
      => new Volume(length.Value * width.Value * height.Value);
    public static Volume FromUnitValue(VolumeUnit unit, double value)
    {
      switch (unit)
      {
        case VolumeUnit.Millilitre:
          return new Volume(value / 1000000);
        case VolumeUnit.Centilitre:
          return new Volume(value / 100000);
        case VolumeUnit.Decilitre:
          return new Volume(value / 10000);
        case VolumeUnit.Litre:
          return new Volume(value / 1000);
        case VolumeUnit.UKGallon:
          return new Volume(value * 0.004546);
        case VolumeUnit.USGallon:
          return new Volume(value * 0.003785);
        case VolumeUnit.CubicFeet:
          return new Volume(value / (1953125000.0 / 55306341.0));
        case VolumeUnit.CubicYard:
          return new Volume(value / (1953125000.0 / 1493271207.0));
        case VolumeUnit.CubicMeter:
          return new Volume(value);
        case VolumeUnit.CubicMile:
          return new Volume(value * (8140980127813632.0 / 1953125.0)); // 
        case VolumeUnit.CubicKilometer:
          return new Volume(value * 1e9);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
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
