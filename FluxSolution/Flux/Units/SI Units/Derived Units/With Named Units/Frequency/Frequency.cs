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
    public Frequency(MetricPrefix prefix, double hertz) => m_value = prefix.ConvertPrefix(hertz, MetricPrefix.Unprefixed);

    /// <summary>
    /// <para>Constructs a frequency from sound-velocity and wavelength.</para>
    /// </summary>
    /// <param name="soundVelocity"></param>
    /// <param name="wavelength"></param>
    public Frequency(Speed soundVelocity, Length wavelength) : this(soundVelocity.Value / wavelength.Value) { }

    /// <summary>
    /// <para>Returns the angular frequency corresponding to the frequency.</para>
    /// </summary>
    public AngularFrequency AngularFrequency
      => new(m_value * double.Tau);

    /// <summary>
    /// <para>Returns the period corresponding to the frequency.</para>
    /// </summary>
    public Time Period
      => new(1.0 / m_value);

    /// <summary>
    /// <para>Returns the number of revolutions per minute (RPM) corresponding to the frequency.</para>
    /// </summary>
    public double RevolutionsPerMinute
      => m_value * 60;

    /// <summary>
    /// <para>Returns the normalized frequency corresponding to the frequency and sample rate.</para>
    /// <para>In digital signal processing (DSP), a normalized frequency is a ratio of a variable <see cref="Frequency"/> and a constant frequency associated with a system (e.g. sampling rate).</para>
    /// <para>This is also the same as cycles per sample.</para>
    /// </summary>
    /// <param name="sampleRate"></param>
    /// <returns></returns>
    public Frequency NormalizedFrequency(double sampleRate)
      => new(m_value / sampleRate);

    /// <summary>
    /// <para>Computes the number of samples per cycle at the specified frequency and sample rate.</para>
    /// </summary>
    /// <param name="sampleRate"></param>
    /// <returns></returns>
    public double SamplesPerPeriod(double sampleRate)
      => sampleRate / m_value;

    #region Static methods

    /// <summary>
    /// <para>Constructs a frequency from an angular frequency.</para>
    /// </summary>
    /// <param name="angularFrequency"></param>
    /// <returns></returns>
    public static Frequency FromAngularFrequency(double angularFrequency)
      => new(angularFrequency / double.Tau);

    public static Frequency FromNormalizedFrequency(double normalizedFrequency, double sampleRate)
      => new(normalizedFrequency * sampleRate);

    /// <summary>
    /// <para>Constructs a frequency from a period.</para>
    /// </summary>
    /// <param name="period"></param>
    /// <returns></returns>
    public static Frequency FromPeriod(double period)
      => new(1.0 / period);

    /// <summary>
    /// <para>Constructs a frequency from revolutions per minute (RPM).</para>
    /// </summary>
    /// <param name="rpm"></param>
    /// <returns></returns>
    public static Frequency FromRevolutionsPerMinute(double rpm)
      => new(rpm / 60.0);

    /// <summary>Returns the <paramref name="frequency"/> pitch shifted by the <paramref name="frequencyRatio"/> (positive or negative).</summary>
    /// <param name="frequency"></param>
    /// <param name="frequencyRatio"></param>
    public static double ShiftPitch(double frequency, double frequencyRatio)
      => frequency * frequencyRatio;

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator Frequency(double value) => new(value);
    public static implicit operator double(Frequency value) => value.m_value;

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + GetSiUnitSymbol(prefix, false);

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

    public string ToUnitString(FrequencyUnit unit = FrequencyUnit.Hertz, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
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
