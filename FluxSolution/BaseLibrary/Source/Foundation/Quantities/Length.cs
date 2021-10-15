namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this Quantity.LengthUnit unit)
    {
      switch (unit)
      {
        case Quantity.LengthUnit.Millimeter:
          return @" mm";
        case Quantity.LengthUnit.Centimeter:
          return @" cm";
        case Quantity.LengthUnit.Inch:
          return @" in";
        case Quantity.LengthUnit.Decimeter:
          return @" dm";
        case Quantity.LengthUnit.Foot:
          return @" ft";
        case Quantity.LengthUnit.Yard:
          return @" yd";
        case Quantity.LengthUnit.Meter:
          return @" m";
        case Quantity.LengthUnit.NauticalMile:
          return @" nm";
        case Quantity.LengthUnit.Mile:
          return @" mi";
        case Quantity.LengthUnit.Kilometer:
          return @" km";
        case Quantity.LengthUnit.AstronomicalUnit:
          return @" au";
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
  }

  namespace Quantity
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
      AstronomicalUnit
    }

    /// <summary>Length. SI unit of meter. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Length"/>
    public struct Length
      : System.IComparable<Length>, System.IEquatable<Length>, IValuedUnit
    {
      private readonly double m_value;

      public Length(double value, LengthUnit unit = LengthUnit.Meter)
      {
        switch (unit)
        {
          case LengthUnit.Millimeter:
            m_value = value / 1000;
            break;
          case LengthUnit.Centimeter:
            m_value = value / 100;
            break;
          case LengthUnit.Inch:
            m_value = value * 0.0254;
            break;
          case LengthUnit.Decimeter:
            m_value = value / 10;
            break;
          case LengthUnit.Foot:
            m_value = value * 0.3048;
            break;
          case LengthUnit.Yard:
            m_value = value * 0.9144;
            break;
          case LengthUnit.Meter:
            m_value = value;
            break;
          case LengthUnit.NauticalMile:
            m_value = value * 1852;
            break;
          case LengthUnit.Mile:
            m_value = value * 1609.344;
            break;
          case LengthUnit.Kilometer:
            m_value = value * 1000;
            break;
          case LengthUnit.AstronomicalUnit:
            m_value = value * 149597870700;
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(unit));
        }
      }

      public double Value
        => m_value;

      public string ToUnitString(LengthUnit unit = LengthUnit.Meter, string? format = null)
        => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
      public double ToUnitValue(LengthUnit unit = LengthUnit.Meter)
      {
        switch (unit)
        {
          case LengthUnit.Millimeter:
            return m_value * 1000;
          case LengthUnit.Centimeter:
            return m_value * 100;
          case LengthUnit.Inch:
            return m_value / 0.0254;
          case LengthUnit.Decimeter:
            return m_value * 10;
          case LengthUnit.Foot:
            return m_value / 0.3048;
          case LengthUnit.Yard:
            return m_value / 0.9144;
          case LengthUnit.Meter:
            return m_value;
          case LengthUnit.NauticalMile:
            return m_value / 1852;
          case LengthUnit.Mile:
            return m_value / 1609.344;
          case LengthUnit.Kilometer:
            return m_value / 1000;
          case LengthUnit.AstronomicalUnit:
            return m_value / 149597870700;
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
        => new Length(phaseVelocity.Value / frequency.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Length v)
        => v.m_value;
      public static explicit operator Length(double v)
        => new Length(v);

      public static bool operator <(Length a, Length b)
        => a.CompareTo(b) < 0;
      public static bool operator <=(Length a, Length b)
        => a.CompareTo(b) <= 0;
      public static bool operator >(Length a, Length b)
        => a.CompareTo(b) > 0;
      public static bool operator >=(Length a, Length b)
        => a.CompareTo(b) >= 0;

      public static bool operator ==(Length a, Length b)
        => a.Equals(b);
      public static bool operator !=(Length a, Length b)
        => !a.Equals(b);

      public static Length operator -(Length v)
        => new Length(-v.m_value);
      public static Length operator +(Length a, double b)
        => new Length(a.m_value + b);
      public static Length operator +(Length a, Length b)
        => a + b.m_value;
      public static Length operator /(Length a, double b)
        => new Length(a.m_value / b);
      public static Length operator /(Length a, Length b)
        => a / b.m_value;
      public static Length operator *(Length a, double b)
        => new Length(a.m_value * b);
      public static Length operator *(Length a, Length b)
        => a * b.m_value;
      public static Length operator %(Length a, double b)
        => new Length(a.m_value % b);
      public static Length operator %(Length a, Length b)
        => a % b.m_value;
      public static Length operator -(Length a, double b)
        => new Length(a.m_value - b);
      public static Length operator -(Length a, Length b)
        => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable
      public int CompareTo(Length other)
        => m_value.CompareTo(other.m_value);

      // IEquatable
      public bool Equals(Length other)
        => m_value == other.m_value;
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is Length o && Equals(o);
      public override int GetHashCode()
        => m_value.GetHashCode();
      public override string ToString()
        => $"<{nameof(Length)}: {ToUnitString()}>";
      #endregion Object overrides
    }
  }
}