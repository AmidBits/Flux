namespace Flux.Units
{
  /// <summary>Speed.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Speed"/>
  public struct Speed
    : System.IComparable<Speed>, System.IEquatable<Speed>, System.IFormattable
  {
    public static Speed SpeedOfLight
      => new Speed(299792458);
    public static Speed SpeedOfSound
      => new Speed(340.27);

    private readonly double m_metersPerSecond;

    public Speed(double metersPerSecond)
      => m_metersPerSecond = metersPerSecond;

    public double FeetPerSecond
      => ConvertMetersPerSecondToFeetPerSecond(m_metersPerSecond);
    public double KilometersPerHour
      => ConvertMetersPerSecondToKilometersPerHour(m_metersPerSecond);
    public double Knots
      => ConvertMetersPerSecondToKnots(m_metersPerSecond);
    public double MetersPerSecond
      => m_metersPerSecond;
    public double MilesPerHour
      => ConvertMetersPerSecondToMilesPerHour(m_metersPerSecond);

    #region Static methods
    public static Speed Add(Speed left, Speed right)
      => new Speed(left.m_metersPerSecond + right.m_metersPerSecond);
    public static double ConvertFeetPerSecondToMetersPerSecond(double feetPerSecond)
      => feetPerSecond * 0.3048;
    public static double ConvertKilometersPerHourToMetersPerSecond(double kilometersPerHour)
      => kilometersPerHour * 0.27;
    public static double ConvertKnotsToMetersPerSecond(double knots)
      => knots * (1852.0 / 3600.0);
    public static double ConvertMetersPerSecondToFeetPerSecond(double metersPerSecond)
      => metersPerSecond / 0.3048;
    public static double ConvertMetersPerSecondToKilometersPerHour(double metersPerSecond)
      => metersPerSecond * 3.6;
    public static double ConvertMetersPerSecondToKnots(double knots)
      => knots / (1852.0 / 3600.0);
    public static double ConvertMetersPerSecondToMilesPerHour(double metersPerSecond)
      => metersPerSecond * 2.2369362920544;
    public static double ConvertMilesPerHourToMetersPerSecond(double milesPerHour)
      => milesPerHour * 0.44704;
    public static Speed Divide(Speed left, Speed right)
      => new Speed(left.m_metersPerSecond / right.m_metersPerSecond);
    public static Speed FromFeetPerSecond(double feetPerSecond)
      => new Speed(ConvertFeetPerSecondToMetersPerSecond(feetPerSecond));
    public static Speed FromKilometersPerHour(double kilometersPerHour)
      => new Speed(ConvertKilometersPerHourToMetersPerSecond(kilometersPerHour));
    public static Speed FromKnots(double knots)
      => new Speed(ConvertKnotsToMetersPerSecond(knots));
    public static Speed FromMilesPerHour(double milesPerHour)
      => new Speed(ConvertMilesPerHourToMetersPerSecond(milesPerHour));
    public static Speed Multiply(Speed left, Speed right)
      => new Speed(left.m_metersPerSecond * right.m_metersPerSecond);
    public static Speed Negate(Speed value)
      => new Speed(-value.m_metersPerSecond);
    public static Speed Remainder(Speed dividend, Speed divisor)
      => new Speed(dividend.m_metersPerSecond % divisor.m_metersPerSecond);
    public static Speed Subtract(Speed left, Speed right)
      => new Speed(left.m_metersPerSecond - right.m_metersPerSecond);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Speed v)
      => v.m_metersPerSecond;
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
      => m_metersPerSecond.CompareTo(other.m_metersPerSecond);

    // IEquatable
    public bool Equals(Speed other)
      => m_metersPerSecond == other.m_metersPerSecond;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Speed)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Speed o && Equals(o);
    public override int GetHashCode()
      => m_metersPerSecond.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
