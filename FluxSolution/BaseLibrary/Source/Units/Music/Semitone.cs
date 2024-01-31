namespace Flux
{
  namespace Units
  {
    /// <summary>Semitone, unit of itself. A musical interval equal to one hundred cents.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Semitone"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
    public readonly record struct Semitone
    : System.IComparable<Semitone>, IValueQuantifiable<int>
    {
      public const double FrequencyRatio = 1.0594630943592952645618252949463;

      private readonly int m_value;

      public Semitone(int semitones) => m_value = semitones;

      /// <summary>Shifts the pitch of the specified frequency, up or down, using a pitch interval specified in semitones.</summary>
      public Units.Frequency ShiftPitch(Units.Frequency frequency) => new(PitchShift(frequency.Value, m_value));

      public Cent ToCent() => new(ConvertSemitoneToCent(m_value));

      public double ToFrequencyRatio() => ConvertSemitoneToFrequencyRatio(m_value);

      #region Static methods

      /// <summary>Convert a specified interval ratio to a number of semitones.</summary>
      public static double ConvertFrequencyRatioToSemitone(double frequencyRatio) => System.Math.Log(frequencyRatio, 2) * 12;

      /// <summary>Convert a specified number of semitones to cents.</summary>
      public static int ConvertSemitoneToCent(int semitones) => semitones * 100;

      /// <summary>Convert a specified number of semitones to an interval ratio.</summary>
      public static double ConvertSemitoneToFrequencyRatio(int semitones) => System.Math.Pow(2, semitones / 12.0);

      /// <summary>Creates a new Semitone instance from the specified frequency ratio.</summary>
      /// <param name="frequencyRatio"></param>
      public static Semitone FromFrequencyRatio(double frequencyRatio) => new((int)ConvertFrequencyRatioToSemitone(frequencyRatio));

#if NET7_0_OR_GREATER
      /// <summary>Creates a new Cent instance from the specified ratio.</summary>
      /// <param name="ratio"></param>
      public static Semitone FromRatio(Units.Ratio ratio) => FromFrequencyRatio(ratio.Value);
#endif

      /// <summary>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in semitones.</summary>
      public static double PitchShift(double frequency, int semitones) => frequency * ConvertSemitoneToFrequencyRatio(semitones);

      #endregion Static methods

      #region Overloaded operators
      public static explicit operator int(Semitone v) => v.m_value;
      public static explicit operator Semitone(int v) => new(v);

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

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options = default)
        => $"{m_value} semitone{(m_value == 1 ? string.Empty : 's'.ToString())}";

      /// <summary>
      /// <para>The <see cref="Semitone.Value"/> property is a musical interval in semitones.</para>
      /// </summary>
      public int Value => m_value;

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
