namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Frequency Create(this FrequencyUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this FrequencyUnit unit)
      => unit switch
      {
        FrequencyUnit.Hertz => @" Hz",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum FrequencyUnit
  {
    Hertz,
  }

  /// <summary>Temporal frequency unit of Hertz. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Frequency"/>
  public struct Frequency
    : System.IComparable<Frequency>, System.IEquatable<Frequency>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const FrequencyUnit DefaultUnit = FrequencyUnit.Hertz;

    public static Frequency HyperfineTransitionFrequencyOfCs133
      => new(9192631770);

    private readonly double m_value;

    public Frequency(double value, FrequencyUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        FrequencyUnit.Hertz => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    /// <summary>Creates a new Time instance representing the time it takes to complete one cycle at the frequency.</summary>
    public Time ToPeriod()
      => new(1.0 / m_value);

    public string ToUnitString(FrequencyUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(FrequencyUnit unit = DefaultUnit)
      => unit switch
      {
        FrequencyUnit.Hertz => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Frequency instance from the specified acoustic properties of sound velocity and wavelength.</summary>
    /// <param name="soundVelocity"></param>
    /// <param name="wavelength"></param>
    public static Frequency ComputeFrequency(Speed soundVelocity, Length wavelength)
      => new(soundVelocity.GeneralUnitValue / wavelength.GeneralUnitValue);
    /// <summary>Computes the normalized frequency (a.k.a. cycles/sample) of the specified frequency and sample rate. The normalized frequency represents a fractional part of the cycle, per sample.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Normalized_frequency_(unit)"/>
    public static double ComputeNormalizedFrequency(double frequency, double sampleRate)
      => frequency / sampleRate;
    /// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in cents.</summary>
    /// <param name="frequency"></param>
    /// <param name="cents"></param>
    public static Frequency ComputePitchShift(Frequency frequency, Cent cents)
      => new(frequency.GeneralUnitValue * Cent.ConvertCentToFrequencyRatio(cents.Cents));
    /// <summary>Creates a new Frequency instance from the specified frequency shifted in pitch (positive or negative) by the interval specified in semitones.</summary>
    /// <param name="frequency"></param>
    /// <param name="semitones"></param>
    public static Frequency ComputePitchShift(Frequency frequency, Semitone semitones)
      => new(frequency.GeneralUnitValue * Semitone.ConvertSemitoneToFrequencyRatio(semitones.Semitones));
    /// <summary>Computes the number of samples per cycle at the specified frequency and sample rate.</summary>
    public static double ComputeSamplesPerCycle(double frequency, double sampleRate)
      => sampleRate / frequency;
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator Frequency(double value)
      => new(value);
    public static explicit operator double(Frequency value)
      => value.m_value;

    public static bool operator <(Frequency a, Frequency b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Frequency a, Frequency b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Frequency a, Frequency b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Frequency a, Frequency b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Frequency a, Frequency b)
      => a.Equals(b);
    public static bool operator !=(Frequency a, Frequency b)
      => !a.Equals(b);

    public static Frequency operator -(Frequency v)
      => new(-v.m_value);
    public static Frequency operator +(Frequency a, double b)
      => new(a.m_value + b);
    public static Frequency operator +(Frequency a, Frequency b)
      => a + b.m_value;
    public static Frequency operator /(Frequency a, double b)
      => new(a.m_value / b);
    public static Frequency operator /(Frequency a, Frequency b)
      => a / b.m_value;
    public static Frequency operator *(Frequency a, double b)
      => new(a.m_value * b);
    public static Frequency operator *(Frequency a, Frequency b)
      => a * b.m_value;
    public static Frequency operator %(Frequency a, double b)
      => new(a.m_value % b);
    public static Frequency operator %(Frequency a, Frequency b)
      => a % b.m_value;
    public static Frequency operator -(Frequency a, double b)
      => new(a.m_value - b);
    public static Frequency operator -(Frequency a, Frequency b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Frequency other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Frequency other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Frequency o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}