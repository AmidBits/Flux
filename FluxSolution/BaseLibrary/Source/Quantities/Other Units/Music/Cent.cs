namespace Flux.Quantities
{
  /// <summary>
  /// <para>Cent is a musical interval equal to one hundredth of one semitone.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Cent_(music)"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Interval_(music)"/></para>
  /// </summary>
  public readonly record struct Cent
    : System.IComparable, System.IComparable<Cent>, System.IFormattable, IValueQuantifiable<int>
  {
    public const double FrequencyRatio = 1.0005777895065548592967925757932;

    private readonly int m_value;

    public Cent(int cents) => m_value = cents;

    #region Static methods

    #region Conversion methods

    /// <summary>Convert a specified interval ratio to a number of cents.</summary>
    public static double ConvertFrequencyRatioToCent(double frequencyRatio) => System.Math.Log(frequencyRatio, 2) * 1200;

    /// <summary>Convert a specified number of cents to an interval ratio.</summary>
    public static double ConvertCentToFrequencyRatio(int cents) => System.Math.Pow(2, cents / 1200.0);

    #endregion // Conversion methods

    /// <summary>
    /// <para>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in cents.</para>
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="cents"></param>
    /// <returns></returns>
    public static double PitchShift(double frequency, int cents) => frequency * ConvertCentToFrequencyRatio(cents);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Cent a, Cent b) => a.CompareTo(b) < 0;
    public static bool operator <=(Cent a, Cent b) => a.CompareTo(b) <= 0;
    public static bool operator >(Cent a, Cent b) => a.CompareTo(b) > 0;
    public static bool operator >=(Cent a, Cent b) => a.CompareTo(b) >= 0;

    public static Cent operator -(Cent v) => new(-v.m_value);
    public static Cent operator +(Cent a, int b) => new(a.m_value + b);
    public static Cent operator +(Cent a, Cent b) => a + b.m_value;
    public static Cent operator /(Cent a, int b) => new(a.m_value / b);
    public static Cent operator /(Cent a, Cent b) => a / b.m_value;
    public static Cent operator *(Cent a, int b) => new(a.m_value * b);
    public static Cent operator *(Cent a, Cent b) => a * b.m_value;
    public static Cent operator %(Cent a, int b) => new(a.m_value % b);
    public static Cent operator %(Cent a, Cent b) => a % b.m_value;
    public static Cent operator -(Cent a, int b) => new(a.m_value - b);
    public static Cent operator -(Cent a, Cent b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Cent other) => m_value.CompareTo(other.m_value);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Cent o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider) + " cent".ToPluralUnitName(m_value != 1);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="Cent.Value"/> property is a musical interval in cents.</para>
    /// </summary>
    public int Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
