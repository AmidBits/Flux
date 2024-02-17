namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.FrequencyUnit unit, Units.UnitValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.FrequencyUnit.Hertz => options.PreferUnicode ? "\u3390" : "Hz",
        Units.FrequencyUnit.KiloHertz => options.PreferUnicode ? "\u3391" : "kHz",
        Units.FrequencyUnit.MegaHertz => options.PreferUnicode ? "\u3392" : "MHz",
        Units.FrequencyUnit.GigaHertz => options.PreferUnicode ? "\u3393" : "GHz",
        Units.FrequencyUnit.TeraHertz => options.PreferUnicode ? "\u3394" : "THz",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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
      : System.IComparable, System.IComparable<Frequency>, System.IFormattable, IUnitValueQuantifiable<double, FrequencyUnit>
    {
      /// <summary>
      /// <para>The fixed numerical value of the caesium frequency (delta)Cs, the unperturbed ground-state hyperfine transition frequency of the caesium 133 atom.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Caesium_standard"/></para>
      /// </summary>
      public static Frequency HyperfineTransitionFrequencyOfCs => new(9192631770);

      private readonly double m_hertz;

      public Frequency(double value, FrequencyUnit unit = FrequencyUnit.Hertz)
        => m_hertz = unit switch
        {
          FrequencyUnit.Hertz => value,
          FrequencyUnit.KiloHertz => value * 1000,
          FrequencyUnit.MegaHertz => value * 1000000,
          FrequencyUnit.GigaHertz => value * 1000000000,
          FrequencyUnit.TeraHertz => value * 1000000000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>In digital signal processing (DSP), a normalized frequency is a ratio of a variable <see cref="Frequency"/> and a constant frequency associated with a system (e.g. sampling rate).</summary>
      public Time ComputeNormalizedFrequency(double systemFrequency) => new(1.0 / m_hertz);

      /// <summary>Creates a new Time instance representing the time it takes to complete one cycle at the frequency.</summary>
      public Time ComputePeriod() => new(1.0 / m_hertz);

      /// <summary>
      /// <para>Returns the angular velocity from the (rotational) frequency.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/></para>
      /// </summary>
      public AngularFrequency ToAngularVelocity() => new(System.Math.Tau * m_hertz);

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

      public static Frequency From(Speed soundVelocity, Length wavelength)
        => new(soundVelocity.Value / wavelength.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator Frequency(double value) => new(value);
      public static explicit operator double(Frequency value) => value.m_hertz;

      public static bool operator <(Frequency a, Frequency b) => a.CompareTo(b) < 0;
      public static bool operator <=(Frequency a, Frequency b) => a.CompareTo(b) <= 0;
      public static bool operator >(Frequency a, Frequency b) => a.CompareTo(b) > 0;
      public static bool operator >=(Frequency a, Frequency b) => a.CompareTo(b) >= 0;

      public static Frequency operator -(Frequency v) => new(-v.m_hertz);
      public static Frequency operator +(Frequency a, double b) => new(a.m_hertz + b);
      public static Frequency operator +(Frequency a, Frequency b) => a + b.m_hertz;
      public static Frequency operator /(Frequency a, double b) => new(a.m_hertz / b);
      public static Frequency operator /(Frequency a, Frequency b) => a / b.m_hertz;
      public static Frequency operator *(Frequency a, double b) => new(a.m_hertz * b);
      public static Frequency operator *(Frequency a, Frequency b) => a * b.m_hertz;
      public static Frequency operator %(Frequency a, double b) => new(a.m_hertz % b);
      public static Frequency operator %(Frequency a, Frequency b) => a % b.m_hertz;
      public static Frequency operator -(Frequency a, double b) => new(a.m_hertz - b);
      public static Frequency operator -(Frequency a, Frequency b) => a - b.m_hertz;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Frequency o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Frequency other) => m_hertz.CompareTo(other.m_hertz);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(FrequencyUnit.Hertz, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Frequency.Value"/> property is in <see cref="FrequencyUnit.Hertz"/>.</para>
      /// </summary>
      public double Value => m_hertz;

      // IUnitQuantifiable<>
      public double GetUnitValue(FrequencyUnit unit)
        => unit switch
        {
          FrequencyUnit.Hertz => m_hertz,
          FrequencyUnit.KiloHertz => m_hertz / 1000,
          FrequencyUnit.MegaHertz => m_hertz / 1000000,
          FrequencyUnit.GigaHertz => m_hertz / 1000000000,
          FrequencyUnit.TeraHertz => m_hertz / 1000000000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(FrequencyUnit unit, UnitValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces
    }
  }
}
