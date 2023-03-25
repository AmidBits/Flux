namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.PowerRatioUnit unit, bool preferUnicode, bool useFullName)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.PowerRatioUnit.DecibelWatt => "dBW",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum PowerRatioUnit
    {
      DecibelWatt,
    }

    /// <summary>Power ratio unit of decibel watts, defined as ten times the logarithm in base 10, is the strength of a signal expressed in decibels (dB) relative to one watt. A.k.a. logarithmic power ratio.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Decibel"/>
    public readonly record struct PowerRatio
      : System.IComparable, System.IComparable<PowerRatio>, System.IFormattable, IUnitQuantifiable<double, PowerRatioUnit>
    {
      public static readonly PowerRatio Zero;

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
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(PowerRatioUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(PowerRatioUnit unit = DefaultUnit)
        => unit switch
        {
          PowerRatioUnit.DecibelWatt => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
