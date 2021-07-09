namespace Flux.Units
{
  public enum SpeedUnit
  {
    FeetPerSecond,
    KilometersPerHour,
    Knots,
    MetersPerSecond,
    MilesPerHour,
  }

  /// <summary>Speed.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Speed"/>
  public struct Speed
    : System.IComparable<Speed>, System.IEquatable<Speed>, IStandardizedScalar
  {
    public static Speed SpeedOfLight
      => new Speed(299792458);
    public static Speed SpeedOfSound
      => new Speed(343.2);

    private readonly double m_meterPerSecond;

    public Speed(double meterPerSecond)
      => m_meterPerSecond = meterPerSecond;

    public double MeterPerSecond
      => m_meterPerSecond;

    public double ToUnitValue(SpeedUnit unit)
    {
      switch (unit)
      {
        case SpeedUnit.FeetPerSecond:
          return m_meterPerSecond * (1250.0 / 381.0);
        case SpeedUnit.KilometersPerHour:
          return m_meterPerSecond * (18.0 / 5.0);
        case SpeedUnit.Knots:
          return m_meterPerSecond * (3600.0 / 1852.0);
        case SpeedUnit.MetersPerSecond:
          return m_meterPerSecond;
        case SpeedUnit.MilesPerHour:
          return m_meterPerSecond * (3125.0 / 1397.0);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    /// <summary>Creates a new Speed instance from the specified length and time.</summary>
    /// <param name="length"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static Speed From(Length length, Time time)
      => new Speed(length.Meter / time.Second);
    /// <summary>Create a new Speed instance from the specified acoustic properties of frequency and wavelength.</summary>
    /// <param name="frequency"></param>
    /// <param name="wavelength"></param>
    public static Speed From(Frequency frequency, Length wavelength)
      => new Speed(frequency.Hertz * wavelength.Meter);
    public static Speed FromUnitValue(SpeedUnit unit, double value)
    {
      switch (unit)
      {
        case SpeedUnit.FeetPerSecond:
          return new Speed(value * (381.0 / 1250.0));
        case SpeedUnit.KilometersPerHour:
          return new Speed(value * (5.0 / 18.0));
        case SpeedUnit.Knots:
          return new Speed(value * (1852.0 / 3600.0));
        case SpeedUnit.MetersPerSecond:
          return new Speed(value);
        case SpeedUnit.MilesPerHour:
          return new Speed(value * (1397.0 / 3125.0));
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Speed v)
      => v.m_meterPerSecond;
    public static explicit operator Speed(double v)
      => new Speed(v);

    public static bool operator <(Speed a, Speed b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Speed a, Speed b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Speed a, Speed b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Speed a, Speed b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Speed a, Speed b)
      => a.Equals(b);
    public static bool operator !=(Speed a, Speed b)
      => !a.Equals(b);

    public static Speed operator -(Speed v)
      => new Speed(-v.m_meterPerSecond);
    public static Speed operator +(Speed a, Speed b)
      => new Speed(a.m_meterPerSecond + b.m_meterPerSecond);
    public static Speed operator /(Speed a, Speed b)
      => new Speed(a.m_meterPerSecond / b.m_meterPerSecond);
    public static Speed operator *(Speed a, Speed b)
      => new Speed(a.m_meterPerSecond * b.m_meterPerSecond);
    public static Speed operator %(Speed a, Speed b)
      => new Speed(a.m_meterPerSecond % b.m_meterPerSecond);
    public static Speed operator -(Speed a, Speed b)
      => new Speed(a.m_meterPerSecond - b.m_meterPerSecond);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Speed other)
      => m_meterPerSecond.CompareTo(other.m_meterPerSecond);

    // IEquatable
    public bool Equals(Speed other)
      => m_meterPerSecond == other.m_meterPerSecond;

    // IUnitStandardized
    public double GetScalar()
      => m_meterPerSecond;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Speed o && Equals(o);
    public override int GetHashCode()
      => m_meterPerSecond.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_meterPerSecond} m/s>";
    #endregion Object overrides
  }
}
