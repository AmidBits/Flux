namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.PowerRatioUnit unit, bool useFullName = false)
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
      /// <summary>This is the default unit for <see cref="PowerRatio"/>.</summary>
      DecibelWatt,
    }

    /// <summary>
    /// <para>Power ratio unit of decibel watts, defined as ten times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one watt. A.k.a. logarithmic power ratio.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Decibel"/></para>
    /// </summary>
    public readonly record struct PowerRatio
      : System.IComparable, System.IComparable<PowerRatio>, System.IFormattable, IUnitValueQuantifiable<double, PowerRatioUnit>
    {
      public const double ScalingFactor = 10;

      private readonly double m_value;

      public PowerRatio(double value, PowerRatioUnit unit = PowerRatioUnit.DecibelWatt)
        => m_value = unit switch
        {
          PowerRatioUnit.DecibelWatt => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public AmplitudeRatio ToAmplitudeRatio() => new(System.Math.Sqrt(m_value));

      #region Static methods

      /// <summary>Creates a new PowerRatio instance from the difference of the specified voltages (numerator and denominator).</summary>
      /// <param name="numerator"></param>
      /// <param name="denominator"></param>
      public static PowerRatio From(Power numerator, Power denominator) => new(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));
      /// <summary>Creates a new PowerRatio instance from the specified decibel change (i.e. a decibel interval).</summary>
      /// <param name="decibelChange"></param>
      public static PowerRatio FromDecibelChange(double decibelChange) => new(System.Math.Pow(10, decibelChange / ScalingFactor)); // Inverse of Log10.
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(PowerRatio a, PowerRatio b) => a.CompareTo(b) < 0;
      public static bool operator <=(PowerRatio a, PowerRatio b) => a.CompareTo(b) <= 0;
      public static bool operator >(PowerRatio a, PowerRatio b) => a.CompareTo(b) > 0;
      public static bool operator >=(PowerRatio a, PowerRatio b) => a.CompareTo(b) >= 0;

      public static PowerRatio operator -(PowerRatio v) => new(-v.m_value);
      public static PowerRatio operator +(PowerRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
      public static PowerRatio operator +(PowerRatio a, PowerRatio b) => a + b.m_value;
      public static PowerRatio operator /(PowerRatio a, double b) => new(a.m_value - b);
      public static PowerRatio operator /(PowerRatio a, PowerRatio b) => a / b.m_value;
      public static PowerRatio operator *(PowerRatio a, double b) => new(a.m_value + b);
      public static PowerRatio operator *(PowerRatio a, PowerRatio b) => a * b.m_value;
      public static PowerRatio operator -(PowerRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
      public static PowerRatio operator -(PowerRatio a, PowerRatio b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is PowerRatio o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(PowerRatio other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(PowerRatioUnit.DecibelWatt, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="PowerRatio.Value"/> property is in <see cref="PowerRatioUnit.DecibelWatt"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(PowerRatioUnit unit)
        => unit switch
        {
          PowerRatioUnit.DecibelWatt => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PowerRatioUnit unit = PowerRatioUnit.DecibelWatt, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
