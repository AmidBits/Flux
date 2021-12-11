namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this Quantity.SpeedUnit unit)
      => unit switch
      {
        Quantity.SpeedUnit.FeetPerSecond => @" ft/s",
        Quantity.SpeedUnit.KilometersPerHour => @" km/h",
        Quantity.SpeedUnit.Knots => @" knot",
        Quantity.SpeedUnit.MetersPerSecond => @" m/h",
        Quantity.SpeedUnit.MilesPerHour => @" mph",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantity
  {
    public enum SpeedUnit
    {
      FeetPerSecond,
      KilometersPerHour,
      Knots,
      MetersPerSecond,
      MilesPerHour,
    }

    /// <summary>Speed (a.k.a. velocity) unit of meters per second.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Speed"/>
    public struct Speed
      : System.IComparable<Speed>, System.IEquatable<Speed>, IUnitValueStandardized<double>, IValueDerivedUnitSI<double>
    {
      public static Speed SpeedOfLightInVacuum
        => new(299792458);

      public static Speed ApproximateSpeedOfSoundInAir
        => new(343);
      public static Speed ApproximateSpeedOfSoundInDiamond
        => new(12000);
      public static Speed ApproximateSpeedOfSoundInIron
        => new(5120);
      public static Speed ApproximateSpeedOfSoundInWater
        => new(1481);

      private readonly double m_value;

      public Speed(double value, SpeedUnit unit = SpeedUnit.MetersPerSecond)
        => m_value = unit switch
        {
          SpeedUnit.FeetPerSecond => value * (381.0 / 1250.0),
          SpeedUnit.KilometersPerHour => value * (5.0 / 18.0),
          SpeedUnit.Knots => value * (1852.0 / 3600.0),
          SpeedUnit.MetersPerSecond => value,
          SpeedUnit.MilesPerHour => value * (1397.0 / 3125.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public double DerivedUnitValue
        => m_value;

      public double StandardUnitValue
        => m_value;

      public string ToUnitString(SpeedUnit unit = SpeedUnit.MetersPerSecond, string? format = null)
        => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
      public double ToUnitValue(SpeedUnit unit = SpeedUnit.MetersPerSecond)
        => unit switch
        {
          SpeedUnit.FeetPerSecond => m_value * (1250.0 / 381.0),
          SpeedUnit.KilometersPerHour => m_value * (18.0 / 5.0),
          SpeedUnit.Knots => m_value * (3600.0 / 1852.0),
          SpeedUnit.MetersPerSecond => m_value,
          SpeedUnit.MilesPerHour => m_value * (3125.0 / 1397.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      /// <summary>Create a new Speed instance representing phase velocity from the specified frequency and wavelength.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Phase_velocity"/>
      /// <param name="frequency"></param>
      /// <param name="wavelength"></param>
      public static Speed ComputePhaseVelocity(Frequency frequency, Length wavelength)
        => new(frequency.StandardUnitValue * wavelength.StandardUnitValue);

      /// <summary>Creates a new Speed instance from the specified length and time.</summary>
      /// <param name="length"></param>
      /// <param name="time"></param>
      /// <returns></returns>
      public static Speed From(Length length, Time time)
        => new(length.StandardUnitValue / time.StandardUnitValue);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Speed v)
        => v.m_value;
      public static explicit operator Speed(double v)
        => new(v);

      public static bool operator <(Speed a, Speed b)
        => a.CompareTo(b) < 0;
      public static bool operator <=(Speed a, Speed b)
        => a.CompareTo(b) <= 0;
      public static bool operator >(Speed a, Speed b)
        => a.CompareTo(b) > 0;
      public static bool operator >=(Speed a, Speed b)
        => a.CompareTo(b) >= 0;

      public static bool operator ==(Speed a, Speed b)
        => a.Equals(b);
      public static bool operator !=(Speed a, Speed b)
        => !a.Equals(b);

      public static Speed operator -(Speed v)
        => new(-v.m_value);
      public static Speed operator +(Speed a, double b)
        => new(a.m_value + b);
      public static Speed operator +(Speed a, Speed b)
        => a + b.m_value;
      public static Speed operator /(Speed a, double b)
        => new(a.m_value / b);
      public static Speed operator /(Speed a, Speed b)
        => a / b.m_value;
      public static Speed operator *(Speed a, double b)
        => new(a.m_value * b);
      public static Speed operator *(Speed a, Speed b)
        => a * b.m_value;
      public static Speed operator %(Speed a, double b)
        => new(a.m_value % b);
      public static Speed operator %(Speed a, Speed b)
        => a % b.m_value;
      public static Speed operator -(Speed a, double b)
        => new(a.m_value - b);
      public static Speed operator -(Speed a, Speed b)
        => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable
      public int CompareTo(Speed other)
        => m_value.CompareTo(other.m_value);

      // IEquatable
      public bool Equals(Speed other)
        => m_value == other.m_value;
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is Speed o && Equals(o);
      public override int GetHashCode()
        => m_value.GetHashCode();
      public override string ToString()
        => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
      #endregion Object overrides
    }
  }
}
