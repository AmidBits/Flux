namespace Flux.Quantities
{
  public enum FrequencyUnit
  {
    /// <summary>This is the default unit for <see cref="Frequency"/>.</summary>
    Hertz,
    /// <summary>Represents the musical BPM.</summary>
    BeatsPerMinute
  }

  /// <summary>Temporal frequency, unit of Hertz. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Frequency"/>
  public readonly record struct Frequency
    : System.IComparable, System.IComparable<Frequency>, System.IFormattable, ISiUnitValueQuantifiable<double, FrequencyUnit>
  {
    /// <summary>
    /// <para>The musical pitch corresponding to an audio frequency of 440 Hz, serves as a tuning standard for the musical note of A above middle C, or A4 in scientific pitch notation, or MIDI note number 69.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/A440_(pitch_standard)"/></para>
    /// </summary>
    public static Frequency A440 { get; } = new(440);

    /// <summary>
    /// <para>The fixed numerical value of the caesium frequency (delta)Cs, the unperturbed ground-state hyperfine transition frequency of the caesium 133 atom.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Caesium_standard"/></para>
    /// </summary>
    public static Frequency UnperturbedGroundStateHyperfineStructureTransitionFrequencyOfTheCaesium133Atom { get; } = new(9192631770);

    private readonly double m_value;

    public Frequency(double value, FrequencyUnit unit = FrequencyUnit.Hertz) => m_value = ConvertFromUnit(unit, value);

    public Frequency(MetricPrefix prefix, double hertz) => m_value = prefix.ConvertTo(hertz, MetricPrefix.Unprefixed);

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
    public static bool operator >(Frequency a, Frequency b) => a.CompareTo(b) > 0;
    public static bool operator <=(Frequency a, Frequency b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Frequency a, Frequency b) => a.CompareTo(b) >= 0;

    public static Frequency operator -(Frequency v) => new(-v.m_value);
    public static Frequency operator *(Frequency a, Frequency b) => new(a.m_value * b.m_value);
    public static Frequency operator /(Frequency a, Frequency b) => new(a.m_value / b.m_value);
    public static Frequency operator %(Frequency a, Frequency b) => new(a.m_value % b.m_value);
    public static Frequency operator +(Frequency a, Frequency b) => new(a.m_value + b.m_value);
    public static Frequency operator -(Frequency a, Frequency b) => new(a.m_value - b.m_value);
    public static Frequency operator *(Frequency a, double b) => new(a.m_value * b);
    public static Frequency operator /(Frequency a, double b) => new(a.m_value / b);
    public static Frequency operator %(Frequency a, double b) => new(a.m_value % b);
    public static Frequency operator +(Frequency a, double b) => new(a.m_value + b);
    public static Frequency operator -(Frequency a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Frequency o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Frequency other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(FrequencyUnit.Hertz, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode)
      => prefix switch
      {
        MetricPrefix.Kilo => preferUnicode ? "\u3391" : "kHz",
        MetricPrefix.Mega => preferUnicode ? "\u3392" : "MHz",
        MetricPrefix.Giga => preferUnicode ? "\u3393" : "GHz",
        MetricPrefix.Tera => preferUnicode ? "\u3394" : "THz",
        _ => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(FrequencyUnit.Hertz, preferUnicode),
      };

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(FrequencyUnit unit, double value)
      => unit switch
      {
        FrequencyUnit.Hertz => value,

        FrequencyUnit.BeatsPerMinute => value / 60,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(FrequencyUnit unit, double value)
      => unit switch
      {
        FrequencyUnit.Hertz => value,

        FrequencyUnit.BeatsPerMinute => value / 60,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, FrequencyUnit from, FrequencyUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(FrequencyUnit unit)
      => unit switch
      {
        FrequencyUnit.Hertz => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(FrequencyUnit unit, bool preferPlural) => unit.ToString();

    public static string GetUnitSymbol(FrequencyUnit unit, bool preferUnicode)
      => unit switch
      {
        FrequencyUnit.Hertz => preferUnicode ? "\u3390" : "Hz",

        FrequencyUnit.BeatsPerMinute => "BPM",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(FrequencyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(FrequencyUnit unit = FrequencyUnit.Hertz, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, false) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Frequency.Value"/> property is in <see cref="FrequencyUnit.Hertz"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
