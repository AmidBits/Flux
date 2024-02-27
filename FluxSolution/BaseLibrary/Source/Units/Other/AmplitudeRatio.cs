namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.AmplitudeRatioUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.AmplitudeRatioUnit.DecibelVolt => "dBV",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum AmplitudeRatioUnit
    {
      /// <summary>This is the default unit for <see cref="AmplitudeRatio"/>.</summary>
      DecibelVolt,
    }

    /// <summary>Amplitude ratio unit of decibel volt, defined as twenty times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one volt RMS. A.k.a. logarithmic root-power ratio.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Decibel"/>
    public readonly record struct AmplitudeRatio
      : System.IComparable, System.IComparable<AmplitudeRatio>, System.IFormattable, IUnitValueQuantifiable<double, AmplitudeRatioUnit>
    {
      public const double ScalingFactor = 20;

      private readonly double m_value;

      public AmplitudeRatio(double value, AmplitudeRatioUnit unit = AmplitudeRatioUnit.DecibelVolt)
        => m_value = unit switch
        {
          AmplitudeRatioUnit.DecibelVolt => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public PowerRatio ToPowerRatio() => new(System.Math.Pow(m_value, 2));

      #region Static methods

      /// <summary>Creates a new AmplitudeRatio instance from the difference of the specified voltages (numerator and denominator).</summary>
      /// <param name="numerator"></param>
      /// <param name="denominator"></param>
      public static AmplitudeRatio From(Voltage numerator, Voltage denominator) => new(ScalingFactor * System.Math.Log10(numerator.Value / denominator.Value));
      /// <summary>Creates a new AmplitudeRatio instance from the specified decibel change (i.e. a decibel interval).</summary>
      /// <param name="decibelChange"></param>
      public static AmplitudeRatio FromDecibelChange(double decibelChange) => new(System.Math.Pow(10, decibelChange / ScalingFactor)); // Inverse of Log10.

      #endregion Static methods

      #region Overloaded operators

      public static explicit operator double(AmplitudeRatio v) => v.m_value;
      public static explicit operator AmplitudeRatio(double v) => new(v);

      public static bool operator <(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) < 0;
      public static bool operator <=(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) <= 0;
      public static bool operator >(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) > 0;
      public static bool operator >=(AmplitudeRatio a, AmplitudeRatio b) => a.CompareTo(b) >= 0;

      public static AmplitudeRatio operator -(AmplitudeRatio v) => new(-v.m_value);
      public static AmplitudeRatio operator +(AmplitudeRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) + System.Math.Pow(10, b / ScalingFactor)));
      public static AmplitudeRatio operator +(AmplitudeRatio a, AmplitudeRatio b) => a + b.m_value;
      public static AmplitudeRatio operator /(AmplitudeRatio a, double b) => new(a.m_value - b);
      public static AmplitudeRatio operator /(AmplitudeRatio a, AmplitudeRatio b) => a / b.m_value;
      public static AmplitudeRatio operator *(AmplitudeRatio a, double b) => new(a.m_value + b);
      public static AmplitudeRatio operator *(AmplitudeRatio a, AmplitudeRatio b) => a * b.m_value;
      public static AmplitudeRatio operator -(AmplitudeRatio a, double b) => new(ScalingFactor * System.Math.Log10(System.Math.Pow(10, a.m_value / ScalingFactor) - System.Math.Pow(10, b / ScalingFactor)));
      public static AmplitudeRatio operator -(AmplitudeRatio a, AmplitudeRatio b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AmplitudeRatio o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AmplitudeRatio other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider)
        => ToUnitValueString(AmplitudeRatioUnit.DecibelVolt, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="AmplitudeRatio.Value"/> property is in <see cref="AmplitudeRatioUnit.DecibelVolt"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(AmplitudeRatioUnit unit)
        => unit switch
        {
          AmplitudeRatioUnit.DecibelVolt => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AmplitudeRatioUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
