namespace Flux.Quantity
{
  /// <summary>Semitone unit of itself.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Semitone"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
  public struct Semitone
    : System.IComparable<Semitone>, System.IEquatable<Semitone>, IValuedUnit
  {
    public const double FrequencyRatio = 1.0594630943592952645618252949463;

    private readonly int m_value;

    public Semitone(int semitones)
      => m_value = semitones;

    public int Semitones
      => m_value;

    public double Value
      => m_value;

    /// <summary>Shifts the pitch of the specified frequency, up or down, using a pitch interval specified in semitones.</summary>
    public Frequency ShiftPitch(Frequency frequency)
      => new Frequency(PitchShift(frequency.Value, m_value));

    public Cent ToCent()
      => new Cent(ConvertSemitoneToCent(m_value));
    public double ToFrequencyRatio()
      => ConvertSemitoneToFrequencyRatio(m_value);

    #region Static methods
    /// <summary>Convert a specified interval ratio to a number of semitones.</summary>
    public static double ConvertFrequencyRatioToSemitone(double frequencyRatio)
      => System.Math.Log(frequencyRatio, 2) * 12;
    /// <summary>Convert a specified number of semitones to cents.</summary>
    public static int ConvertSemitoneToCent(int semitones)
      => semitones * 100;
    /// <summary>Convert a specified number of semitones to an interval ratio.</summary>
    public static double ConvertSemitoneToFrequencyRatio(int semitones)
      => System.Math.Pow(2, semitones / 12.0);

    /// <summary>Creates a new Semitone instance from the specified frequency ratio.</summary>
    /// <param name="frequencyRatio"></param>
    public static Semitone FromFrequencyRatio(double frequencyRatio)
      => new Semitone((int)ConvertFrequencyRatioToSemitone(frequencyRatio));

    /// <summary>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in semitones.</summary>
    public static double PitchShift(double frequency, int semitones)
      => frequency * ConvertSemitoneToFrequencyRatio(semitones);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator int(Semitone v)
      => v.m_value;
    public static explicit operator Semitone(int v)
      => new Semitone(v);

    public static bool operator <(Semitone a, Semitone b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Semitone a, Semitone b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Semitone a, Semitone b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Semitone a, Semitone b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Semitone a, Semitone b)
      => a.Equals(b);
    public static bool operator !=(Semitone a, Semitone b)
      => !a.Equals(b);

    public static Semitone operator -(Semitone v)
      => new Semitone(-v.m_value);
    public static Semitone operator +(Semitone a, int b)
      => new Semitone(a.m_value + b);
    public static Semitone operator +(Semitone a, Semitone b)
      => a + b.m_value;
    public static Semitone operator /(Semitone a, int b)
      => new Semitone(a.m_value / b);
    public static Semitone operator /(Semitone a, Semitone b)
      => a / b.m_value;
    public static Semitone operator *(Semitone a, int b)
      => new Semitone(a.m_value * b);
    public static Semitone operator *(Semitone a, Semitone b)
      => a * b.m_value;
    public static Semitone operator %(Semitone a, int b)
      => new Semitone(a.m_value % b);
    public static Semitone operator %(Semitone a, Semitone b)
      => a % b.m_value;
    public static Semitone operator -(Semitone a, int b)
      => new Semitone(a.m_value - b);
    public static Semitone operator -(Semitone a, Semitone b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Semitone other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Semitone other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Semitone o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value}>";
    #endregion Object overrides
  }
}
