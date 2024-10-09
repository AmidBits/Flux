namespace Flux.Quantities
{
  /// <summary>
  /// <para>Semitone is a musical interval equal to one hundred cents.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Semitone"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Interval_(music)"/></para>
  /// </summary>
  public readonly record struct Semitone
    : System.IComparable, System.IComparable<Semitone>, System.IFormattable, IValueQuantifiable<int>
  {
    public const double FrequencyRatio = 1.0594630943592952645618252949463;

    private readonly int m_value;

    public Semitone(int semitones) => m_value = semitones;

    public Cent ToCent() => new(ConvertSemitoneToCent(m_value));

    #region Static methods

    #region Conversion methods

    /// <summary>
    /// <para>Convert a specified interval ratio to a number of semitones.</para>
    /// </summary>
    public static double ConvertFrequencyRatioToSemitone(double frequencyRatio) => double.Log(frequencyRatio, 2) * 12;

    /// <summary>
    /// <para>Convert a specified number of semitones to cents.</para>
    /// </summary>
    public static int ConvertSemitoneToCent(int semitones) => semitones * 100;

    /// <summary>
    /// <para>Convert a specified number of semitones to an interval ratio.</para>
    /// </summary>
    public static double ConvertSemitoneToFrequencyRatio(int semitones) => double.Pow(2, semitones / 12.0);

    #endregion // Conversion methods

    /// <summary>
    /// <para>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in semitones.</para>
    /// </summary>
    public static double PitchShift(double frequency, int semitones) => frequency * ConvertSemitoneToFrequencyRatio(semitones);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Semitone a, Semitone b) => a.CompareTo(b) < 0;
    public static bool operator <=(Semitone a, Semitone b) => a.CompareTo(b) <= 0;
    public static bool operator >(Semitone a, Semitone b) => a.CompareTo(b) > 0;
    public static bool operator >=(Semitone a, Semitone b) => a.CompareTo(b) >= 0;

    public static Semitone operator -(Semitone v) => new(-v.m_value);
    public static Semitone operator +(Semitone a, int b) => new(a.m_value + b);
    public static Semitone operator +(Semitone a, Semitone b) => a + b.m_value;
    public static Semitone operator /(Semitone a, int b) => new(a.m_value / b);
    public static Semitone operator /(Semitone a, Semitone b) => a / b.m_value;
    public static Semitone operator *(Semitone a, int b) => new(a.m_value * b);
    public static Semitone operator *(Semitone a, Semitone b) => a * b.m_value;
    public static Semitone operator %(Semitone a, int b) => new(a.m_value % b);
    public static Semitone operator %(Semitone a, Semitone b) => a % b.m_value;
    public static Semitone operator -(Semitone a, int b) => new(a.m_value - b);
    public static Semitone operator -(Semitone a, Semitone b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Semitone o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Semitone other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider) + " semitone".ConvertUnitNameToPlural(m_value != 1);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="Semitone.Value"/> property is a musical interval in semitones.</para>
    /// </summary>
    public int Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
