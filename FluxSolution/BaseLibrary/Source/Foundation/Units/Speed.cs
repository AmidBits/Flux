namespace Flux.Units
{
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

    public double FootPerSecond
      => ConvertMeterPerSecondToFootPerSecond(m_meterPerSecond);
    public double KilometerPerHour
      => ConvertMeterPerSecondToKilometerPerHour(m_meterPerSecond);
    public double Knot
      => ConvertMeterPerSecondToKnot(m_meterPerSecond);
    public double MeterPerSecond
      => m_meterPerSecond;
    public double MilePerHour
      => ConvertMeterPerSecondToMilePerHour(m_meterPerSecond);
    public double NauticalMilePerHour
      => ConvertMeterPerSecondToNauticalMilePerHour(m_meterPerSecond);

    #region Static methods
    public static Speed Add(Speed left, Speed right)
      => new Speed(left.m_meterPerSecond + right.m_meterPerSecond);
    public static double ConvertFootPerSecondToMeterPerSecond(double footPerSecond)
      => footPerSecond * 0.3048;
    public static double ConvertKilometerPerHourToMeterPerSecond(double kilometerPerHour)
      => kilometerPerHour * 0.27;
    public static double ConvertKnotToMeterPerSecond(double knots)
      => knots * (1852.0 / 3600.0);
    public static double ConvertMeterPerSecondToFootPerSecond(double meterPerSecond)
      => meterPerSecond / 0.3048;
    public static double ConvertMeterPerSecondToKilometerPerHour(double meterPerSecond)
      => meterPerSecond * 3.6;
    public static double ConvertMeterPerSecondToKnot(double knot)
      => knot / (1852.0 / 3600.0);
    public static double ConvertMeterPerSecondToMilePerHour(double meterPerSecond)
      => meterPerSecond * 2.2369362920544;
    public static double ConvertMeterPerSecondToNauticalMilePerHour(double meterPerSecond)
      => meterPerSecond * 1.9438444924406;
    public static double ConvertMilePerHourToMeterPerSecond(double milePerHour)
      => milePerHour * 0.44704;
    public static double ConvertNauticalMilePerHourToMeterPerSecond(double nauticalMilePerHour)
      => nauticalMilePerHour / 1.9438444924406;
    public static Speed Divide(Speed left, Speed right)
      => new Speed(left.m_meterPerSecond / right.m_meterPerSecond);
    public static Frequency FromAcousticsAsSoundVelocity(Frequency frequency, Length waveLength)
      => new Frequency(frequency.Hertz * waveLength.Meter);
    public static Speed FromFeetPerSecond(double feetPerSecond)
      => new Speed(ConvertFootPerSecondToMeterPerSecond(feetPerSecond));
    public static Speed FromKilometersPerHour(double kilometersPerHour)
      => new Speed(ConvertKilometerPerHourToMeterPerSecond(kilometersPerHour));
    public static Speed FromKnots(double knots)
      => new Speed(ConvertKnotToMeterPerSecond(knots));
    public static Speed FromMilesPerHour(double milesPerHour)
      => new Speed(ConvertMilePerHourToMeterPerSecond(milesPerHour));
    public static Speed FromNauticalMilesPerHour(double nauticalMilesPerHour)
      => new Speed(ConvertNauticalMilePerHourToMeterPerSecond(nauticalMilesPerHour));
    public static Speed Multiply(Speed left, Speed right)
      => new Speed(left.m_meterPerSecond * right.m_meterPerSecond);
    public static Speed Negate(Speed value)
      => new Speed(-value.m_meterPerSecond);
    public static Speed Remainder(Speed dividend, Speed divisor)
      => new Speed(dividend.m_meterPerSecond % divisor.m_meterPerSecond);
    public static Speed Subtract(Speed left, Speed right)
      => new Speed(left.m_meterPerSecond - right.m_meterPerSecond);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Speed v)
      => v.m_meterPerSecond;
    public static implicit operator Speed(double v)
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

    public static Speed operator +(Speed a, Speed b)
      => Add(a, b);
    public static Speed operator /(Speed a, Speed b)
      => Divide(a, b);
    public static Speed operator *(Speed a, Speed b)
      => Multiply(a, b);
    public static Speed operator -(Speed v)
      => Negate(v);
    public static Speed operator %(Speed a, Speed b)
      => Remainder(a, b);
    public static Speed operator -(Speed a, Speed b)
      => Subtract(a, b);
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
