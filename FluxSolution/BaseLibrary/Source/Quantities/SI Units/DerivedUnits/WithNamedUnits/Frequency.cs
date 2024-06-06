namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.FrequencyUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.FrequencyUnit.Hertz => preferUnicode ? "\u3390" : "Hz",
        Quantities.FrequencyUnit.KiloHertz => preferUnicode ? "\u3391" : "kHz",
        Quantities.FrequencyUnit.MegaHertz => preferUnicode ? "\u3392" : "MHz",
        Quantities.FrequencyUnit.GigaHertz => preferUnicode ? "\u3393" : "GHz",
        Quantities.FrequencyUnit.TeraHertz => preferUnicode ? "\u3394" : "THz",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum FrequencyUnit
    {
      /// <summary>This is the default unit for <see cref="Frequency"/>.</summary>
      Hertz,
      KiloHertz,
      MegaHertz,
      GigaHertz,
      TeraHertz,
    }

    /// <summary>Temporal frequency, unit of Hertz. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Frequency"/>
    public readonly record struct Frequency
      : System.IComparable, System.IComparable<Frequency>, System.IFormattable, ISiPrefixValueQuantifiable<double, FrequencyUnit>
    {
      /// <summary>
      /// <para>The fixed numerical value of the caesium frequency (delta)Cs, the unperturbed ground-state hyperfine transition frequency of the caesium 133 atom.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Caesium_standard"/></para>
      /// </summary>
      public static Frequency HyperfineTransitionFrequencyOfCs => new(9192631770);

      private readonly double m_value;

      public Frequency(double value, FrequencyUnit unit = FrequencyUnit.Hertz)
        => m_value = unit switch
        {
          FrequencyUnit.Hertz => value,
          FrequencyUnit.KiloHertz => value * 1000,
          FrequencyUnit.MegaHertz => value * 1000000,
          FrequencyUnit.GigaHertz => value * 1000000000,
          FrequencyUnit.TeraHertz => value * 1000000000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>
      /// <para>Constructs a frequency from sound-velocity and wavelength.</para>
      /// </summary>
      /// <param name="soundVelocity"></param>
      /// <param name="wavelength"></param>
      public Frequency(Speed soundVelocity, Length wavelength) : this(soundVelocity.Value / wavelength.Value) { }

      /// <summary>In digital signal processing (DSP), a normalized frequency is a ratio of a variable <see cref="Frequency"/> and a constant frequency associated with a system (e.g. sampling rate).</summary>
      public Time ComputeNormalizedFrequency(double systemFrequency) => new(1.0 / m_value);

      /// <summary>Creates a new Time instance representing the time it takes to complete one cycle at the frequency.</summary>
      public Time ComputePeriod() => new(1.0 / m_value);

      /// <summary>
      /// <para>Returns the angular velocity from the (rotational) frequency.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/></para>
      /// </summary>
      public AngularFrequency ToAngularVelocity() => new(System.Math.Tau * m_value);

      #region Static methods
      /// <remarks>Revolutions Per Minute (RPM) is officially a frequency and as such measured in Hertz (which is 'per second'). Conversion is a straight forward by a factor of 60 (i.e. seconds per minute)</remarks>
      /// <see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>

      public static double ConvertFrequencyToRpm(double frequency)
        => frequency * 60;

      /// <remarks>Revolutions Per Minute (RPM) is officially a frequency and as such measured in Hertz (which is 'per second'). Conversion is a straight forward by a factor of 60 (i.e. seconds per minute)</remarks>
      /// <see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>

      public static double ConvertRpmToFrequency(double revolutionPerMinute)
        => revolutionPerMinute / 60;

      /// <summary>Computes the normalized frequency (a.k.a. cycles/sample) of the specified frequency and sample rate. The normalized frequency represents a fractional part of the cycle, per sample.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Normalized_frequency_(unit)"/>
      public static double NormalizedFrequency(double frequency, double sampleRate)
        => frequency / sampleRate;

      ///// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in cents.</summary>
      ///// <param name="frequency"></param>
      ///// <param name="cent"></param>
      //public static Frequency PitchShift(Frequency frequency, Music.Cent cent)
      //  => new(frequency.Value * Music.Cent.ConvertCentToFrequencyRatio(cent.Value));

      ///// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in semitones.</summary>
      ///// <param name="frequency"></param>
      ///// <param name="semitone"></param>
      //public static Frequency PitchShift(Frequency frequency, Music.Semitone semitone)
      //  => new(frequency.Value * Music.Semitone.ConvertSemitoneToFrequencyRatio(semitone.Value));

      /// <summary>Computes the number of samples per cycle at the specified frequency and sample rate.</summary>
      public static double SamplesPerCycle(double frequency, double sampleRate)
        => sampleRate / frequency;

      /// <summary>Returns the <paramref name="frequency"/> pitch shifted by the <paramref name="frequencyRatio"/> (positive or negative).</summary>
      /// <param name="frequency"></param>
      /// <param name="frequencyRatio"></param>
      public static double ShiftPitch(double frequency, double frequencyRatio)
        => frequency * frequencyRatio;

      /// <summary>Creates a new Frequency instance from the specified acoustic properties of sound velocity and wavelength.</summary>
      /// <param name="soundVelocity"></param>
      /// <param name="wavelength"></param>

      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Frequency a, Frequency b) => a.CompareTo(b) < 0;
      public static bool operator <=(Frequency a, Frequency b) => a.CompareTo(b) <= 0;
      public static bool operator >(Frequency a, Frequency b) => a.CompareTo(b) > 0;
      public static bool operator >=(Frequency a, Frequency b) => a.CompareTo(b) >= 0;

      public static Frequency operator -(Frequency v) => new(-v.m_value);
      public static Frequency operator +(Frequency a, double b) => new(a.m_value + b);
      public static Frequency operator +(Frequency a, Frequency b) => a + b.m_value;
      public static Frequency operator /(Frequency a, double b) => new(a.m_value / b);
      public static Frequency operator /(Frequency a, Frequency b) => a / b.m_value;
      public static Frequency operator *(Frequency a, double b) => new(a.m_value * b);
      public static Frequency operator *(Frequency a, Frequency b) => a * b.m_value;
      public static Frequency operator %(Frequency a, double b) => new(a.m_value % b);
      public static Frequency operator %(Frequency a, Frequency b) => a % b.m_value;
      public static Frequency operator -(Frequency a, double b) => new(a.m_value - b);
      public static Frequency operator -(Frequency a, Frequency b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Frequency o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Frequency other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(FrequencyUnit.Hertz, format, formatProvider);

      // ISiUnitValueQuantifiable<>
      public FrequencyUnit BaseUnit => FrequencyUnit.Hertz;

      public FrequencyUnit UnprefixedUnit => FrequencyUnit.Hertz;

      public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(UnprefixedUnit, preferUnicode, useFullName);

      public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToSiPrefixValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(UnprefixedUnit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Frequency.Value"/> property is in <see cref="FrequencyUnit.Hertz"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(FrequencyUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(FrequencyUnit unit)
        => unit switch
        {
          FrequencyUnit.Hertz => m_value,
          FrequencyUnit.KiloHertz => m_value / 1000,
          FrequencyUnit.MegaHertz => m_value / 1000000,
          FrequencyUnit.GigaHertz => m_value / 1000000000,
          FrequencyUnit.TeraHertz => m_value / 1000000000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(FrequencyUnit unit = FrequencyUnit.Hertz, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
