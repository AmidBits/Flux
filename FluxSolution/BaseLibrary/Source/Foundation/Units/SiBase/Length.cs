namespace Flux.Units
{
  public enum LengthUnit
  {
    Millimeter,
    Centimeter,
    Inch,
    Decimeter,
    Foot,
    Yard,
    Meter,
    NauticalMile,
    Mile,
    Kilometer,
  }

  /// <summary>Length.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Length"/>
  public struct Length
    : System.IComparable<Length>, System.IEquatable<Length>, IStandardizedScalar
  {
    private readonly double m_meter;

    public Length(double meter)
      => m_meter = meter;

    public double Meter
      => m_meter;

    public double ToUnitValue(LengthUnit unit)
    {
      switch (unit)
      {
        case LengthUnit.Millimeter:
          return m_meter * 1000;
        case LengthUnit.Centimeter:
          return m_meter * 100;
        case LengthUnit.Inch:
          return m_meter / 0.0254;
        case LengthUnit.Decimeter:
          return m_meter * 10;
        case LengthUnit.Foot:
          return m_meter / 0.3048;
        case LengthUnit.Yard:
          return m_meter / 0.9144;
        case LengthUnit.Meter:
          return m_meter;
        case LengthUnit.NauticalMile:
          return m_meter / 1852;
        case LengthUnit.Mile:
          return m_meter / 1609.344;
        case LengthUnit.Kilometer:
          return m_meter / 1000;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    /// <summary>Computes the wavelength from the specified phase velocity and frequency. A wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per second) for the wavelength distance.</summary>
    /// <param name="phaseVelocity">The constant speed of the traveling wave. If these are sound waves then typically this is the speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
    /// <param name="frequency"></param>
    /// <returns>The wavelength of the frequency cycle at the phase velocity.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Wavelength"/>
    public static Length ComputeWavelength(Speed phaseVelocity, Frequency frequency)
      => new Length(phaseVelocity.MeterPerSecond / frequency.Hertz);

    public static Length FromUnitValue(LengthUnit unit, double value)
    {
      switch (unit)
      {
        case LengthUnit.Millimeter:
          return new Length(value / 1000);
        case LengthUnit.Centimeter:
          return new Length(value / 100);
        case LengthUnit.Inch:
          return new Length(value * 0.0254);
        case LengthUnit.Decimeter:
          return new Length(value / 10);
        case LengthUnit.Foot:
          return new Length(value * 0.3048);
        case LengthUnit.Yard:
          return new Length(value * 0.9144);
        case LengthUnit.Meter:
          return new Length(value);
        case LengthUnit.NauticalMile:
          return new Length(value * 1852);
        case LengthUnit.Mile:
          return new Length(value * 1609.344);
        case LengthUnit.Kilometer:
          return new Length(value * 1000);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Length v)
      => v.m_meter;
    public static explicit operator Length(double v)
      => new Length(v);

    public static bool operator <(Length a, Length b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Length a, Length b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Length a, Length b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Length a, Length b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Length a, Length b)
      => a.Equals(b);
    public static bool operator !=(Length a, Length b)
      => !a.Equals(b);

    public static Length operator -(Length v)
      => new Length(-v.m_meter);
    public static Length operator +(Length a, Length b)
      => new Length(a.m_meter + b.m_meter);
    public static Length operator /(Length a, Length b)
      => new Length(a.m_meter / b.m_meter);
    public static Length operator *(Length a, Length b)
      => new Length(a.m_meter * b.m_meter);
    public static Length operator %(Length a, Length b)
      => new Length(a.m_meter % b.m_meter);
    public static Length operator -(Length a, Length b)
      => new Length(a.m_meter - b.m_meter);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Length other)
      => m_meter.CompareTo(other.m_meter);

    // IEquatable
    public bool Equals(Length other)
      => m_meter == other.m_meter;

    // IUnitStandardized
    public double GetScalar()
      => m_meter;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Length o && Equals(o);
    public override int GetHashCode()
      => m_meter.GetHashCode();
    public override string ToString()
      => $"<{nameof(Length)}: {m_meter} m>";
    #endregion Object overrides
  }
}
