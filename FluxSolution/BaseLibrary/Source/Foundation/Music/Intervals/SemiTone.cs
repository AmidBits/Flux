namespace Flux.Music.Interval
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Semitone"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
  public static class Semitone
  {
    public const double FrequencyRatio = 1.0594630943592952645618252949463;

    /// <summary>Convert a specified interval ratio to cents.</summary>
    public static int ConvertFrequencyRatioToSemitone(double frequencyRatio)
      => (int)(System.Math.Log(frequencyRatio, 2.0) * 12.0);

    /// <summary>Convert a specified semitone to an interval ratio.</summary>
    public static double ConvertSemitoneToFrequencyRatio(int semitones)
      => System.Math.Pow(2.0, semitones / 12.0);

    /// <summary>Adjusts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public static double ShiftPitch(double frequency, int semitones)
      => System.Math.Pow(FrequencyRatio, semitones) * frequency;
  }
}
