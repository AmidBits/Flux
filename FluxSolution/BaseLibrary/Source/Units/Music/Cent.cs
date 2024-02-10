namespace Flux
{
  namespace Units
  {
    /// <summary>Cent, unit of itself. Musical interval equal to one hundredth of one semitone.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Cent_(music)"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
    public readonly record struct Cent
      : System.IComparable, System.IComparable<Cent>, System.IFormattable, IValueQuantifiable<int>
    {
      public const double FrequencyRatio = 1.0005777895065548592967925757932;

      private readonly int m_value;

      public Cent(int cents) => m_value = cents;

      /// <summary>Shifts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
      public Units.Frequency ShiftPitch(Units.Frequency frequency) => new(PitchShift(frequency.Value, m_value));

      public double ToFrequencyRatio() => ConvertCentToFrequencyRatio(m_value);

      #region Static methods

      /// <summary>Convert a specified interval ratio to a number of cents.</summary>
      public static double ConvertFrequencyRatioToCent(double frequencyRatio) => System.Math.Log(frequencyRatio, 2) * 1200;
      /// <summary>Convert a specified number of cents to an interval ratio.</summary>
      public static double ConvertCentToFrequencyRatio(int cents) => System.Math.Pow(2, cents / 1200.0);

      /// <summary>Creates a new Cent instance from the specified frequency ratio.</summary>
      /// <param name="frequencyRatio"></param>
      public static Cent FromFrequencyRatio(double frequencyRatio) => new((int)ConvertFrequencyRatioToCent(frequencyRatio));

#if NET7_0_OR_GREATER
      /// <summary>Creates a new Cent instance from the specified ratio.</summary>
      /// <param name="ratio"></param>
      public static Cent FromRatio(Units.Ratio ratio) => FromFrequencyRatio(ratio.Value);
#endif

      /// <summary>Applies pitch shifting of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
      public static double PitchShift(double frequency, int cents) => frequency * ConvertCentToFrequencyRatio(cents);

      #endregion Static methods

      #region Overloaded operators
      public static explicit operator int(Cent v) => v.m_value;
      public static explicit operator Cent(int v) => new(v);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => ToValueString(TextOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(TextOptions options = default)
        => $"{m_value} cent{(m_value == 1 ? string.Empty : 's'.ToString())}";

      /// <summary>
      /// <para>The <see cref="Cent.Value"/> property is a musical interval in cents.</para>
      /// </summary>
      public int Value => m_value;

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
