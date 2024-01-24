namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.PowerRatioUnit unit, bool preferUnicode, bool useFullName)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.PowerRatioUnit.DecibelWatt => "dBW",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum PowerRatioUnit
    {
      DecibelWatt,
    }

    /// <summary>Power ratio unit of decibel watts, defined as ten times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one watt. A.k.a. logarithmic power ratio.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Decibel"/>
    public readonly record struct PowerRatio
      : System.IComparable, System.IComparable<PowerRatio>, System.IFormattable, IUnitValueQuantifiable<double, PowerRatioUnit>
    {
      public const PowerRatioUnit DefaultUnit = PowerRatioUnit.DecibelWatt;

      public const double ScalingFactor = 10;

      private readonly double m_value;

      public PowerRatio(double value, PowerRatioUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          PowerRatioUnit.DecibelWatt => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };


      public AmplitudeRatio ToAmplitudeRatio()
        => new(System.Math.Sqrt(m_value));

      #region Static methods
      /// <summary>Creates a new PowerRatio instance from the difference of the specified voltages (numerator and denominator).</summary>
      /// <param name="numerator"></param>
      /// <param name="denominator"></param>
      public static PowerRatio From(Power numerator, Power denominator)
        => new(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));
      /// <summary>Creates a new PowerRatio instance from the specified decibel change (i.e. a decibel interval).</summary>
      /// <param name="decibelChange"></param>
      public static PowerRatio FromDecibelChange(double decibelChange)
        => new(System.Math.Pow(10, decibelChange / ScalingFactor)); // Inverse of Log10.
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(PowerRatio v)
        => v.m_value;
      public static explicit operator PowerRatio(double v)
        => new(v);

      public static bool operator <(PowerRatio a, PowerRatio b)
        => a.CompareTo(b) < 0;
      public static bool operator <=(PowerRatio a, PowerRatio b)
        => a.CompareTo(b) <= 0;
      public static bool operator >(PowerRatio a, PowerRatio b)
        => a.CompareTo(b) > 0;
      public static bool operator >=(PowerRatio a, PowerRatio b)
        => a.CompareTo(b) >= 0;

      public static PowerRatio operator -(PowerRatio v)
        => new(-v.m_value);
      public static PowerRatio operator +(PowerRatio a, double b)
        => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
      public static PowerRatio operator +(PowerRatio a, PowerRatio b)
        => a + b.m_value;
      public static PowerRatio operator /(PowerRatio a, double b)
        => new(a.m_value - b);
      public static PowerRatio operator /(PowerRatio a, PowerRatio b)
        => a / b.m_value;
      public static PowerRatio operator *(PowerRatio a, double b)
        => new(a.m_value + b);
      public static PowerRatio operator *(PowerRatio a, PowerRatio b)
        => a * b.m_value;
      public static PowerRatio operator -(PowerRatio a, double b)
        => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
      public static PowerRatio operator -(PowerRatio a, PowerRatio b)
        => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is PowerRatio o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(PowerRatio other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(PowerRatioUnit unit)
        => unit switch
        {
          PowerRatioUnit.DecibelWatt => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PowerRatioUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
