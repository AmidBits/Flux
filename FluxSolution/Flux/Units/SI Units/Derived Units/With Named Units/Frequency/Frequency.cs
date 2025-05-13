namespace Flux.Units
{
  /// <summary>
  /// <para>Temporal frequency, unit of Hertz. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Frequency"/></para>
  /// </summary>
  public readonly record struct Frequency
    : System.IComparable, System.IComparable<Frequency>, System.IFormattable, ISiUnitValueQuantifiable<double, FrequencyUnit>
  {
    /// <summary>
    /// <para>The fixed numerical value of the caesium frequency (delta)Cs, the unperturbed ground-state hyperfine transition frequency of the caesium 133 atom.</para>
    /// <para>The ground state hyperfine structure transition frequency of the caesium-133 atom is exactly 9192631770 hertz (Hz).</para>
    /// <para>This is one of the fundamental physical constants of physics.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Caesium_standard"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/International_System_of_Units"/></para>
    /// </summary>
    public const double CaesiumStandard = 9192631770;

    /// <summary>
    /// <para>The musical pitch corresponding to an audio frequency of 440 Hz, serves as a tuning standard for the musical note of A above middle C, or A4 in scientific pitch notation, or MIDI note number 69.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/A440_(pitch_standard)"/></para>
    /// </summary>
    public static Frequency A4 { get; } = new(440);

    private readonly double m_value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    public Frequency(double value, FrequencyUnit unit = FrequencyUnit.Hertz) => m_value = ConvertFromUnit(unit, value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="hertz"></param>
    public Frequency(MetricPrefix prefix, double hertz) => m_value = prefix.ChangePrefix(hertz, MetricPrefix.Unprefixed);

    /// <summary>
    /// <para>Constructs a frequency from sound-velocity and wavelength.</para>
    /// </summary>
    /// <param name="soundVelocity"></param>
    /// <param name="wavelength"></param>
    public Frequency(Speed soundVelocity, Length wavelength) : this(soundVelocity.Value / wavelength.Value) { }

    /// <summary>
    /// <para>In digital signal processing (DSP), a normalized frequency is a ratio of a variable <see cref="Frequency"/> and a constant frequency associated with a system (e.g. sampling rate).</para>
    /// </summary>
    /// <param name="systemFrequency">E.g. sampling rate.</param>
    /// <returns></returns>
    public Time ComputeNormalizedFrequency(double systemFrequency) => new(1.0 / m_value);

    /// <summary>
    /// <para>Creates a new Time instance representing the time it takes to complete one cycle at the frequency.</para>
    /// </summary>
    /// <returns></returns>
    public Time ComputePeriod() => new(1.0 / m_value);

    /// <summary>
    /// <para>Returns the angular velocity from the (rotational) frequency.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Revolutions_per_minute"/></para>
    /// </summary>
    /// <returns></returns>
    public AngularFrequency ToAngularVelocity() => new(double.Tau * m_value);

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

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode)
      => prefix switch
      {
        MetricPrefix.Kilo => preferUnicode ? "\u3391" : "kHz",
        MetricPrefix.Mega => preferUnicode ? "\u3392" : "MHz",
        MetricPrefix.Giga => preferUnicode ? "\u3393" : "GHz",
        MetricPrefix.Tera => preferUnicode ? "\u3394" : "THz",
        _ => prefix.GetMetricPrefixSymbol(preferUnicode) + FrequencyUnit.Hertz.GetUnitSymbol(preferUnicode),
      };

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + GetSiUnitSymbol(prefix, false);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(FrequencyUnit unit, double value)
      => unit switch
      {
        FrequencyUnit.Hertz => value,

        FrequencyUnit.BeatsPerMinute => value / 60,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(FrequencyUnit unit, double value)
      => unit switch
      {
        FrequencyUnit.Hertz => value,

        FrequencyUnit.BeatsPerMinute => value / 60,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, FrequencyUnit from, FrequencyUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(FrequencyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(FrequencyUnit unit = FrequencyUnit.Hertz, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? unit.GetUnitName(false) : unit.GetUnitSymbol(false));
    }

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
