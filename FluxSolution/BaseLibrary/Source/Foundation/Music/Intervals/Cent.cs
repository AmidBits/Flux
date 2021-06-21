namespace Flux.Music.Interval
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cent_(music)"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
  public static class Cent
  {
    public const double FrequencyRatio = 1.0005777895065548592967925757932;

    /// <summary>Convert a specified interval ratio to cents.</summary>
    public static int ConvertFrequencyRatioToCents(double frequencyRatio)
      => (int)(System.Math.Log(frequencyRatio, 2.0) * 1200.0);

    /// <summary>Convert a specified cents to an interval ratio.</summary>
    public static double ConvertCentsToFrequencyRatio(int cents)
      => System.Math.Pow(2.0, cents / 1200.0);

    /// <summary>Adjusts the pitch of the specified frequency, up or down, using a pitch interval specified in cents.</summary>
    public static double ShiftPitch(double frequency, int cents)
      => System.Math.Pow(FrequencyRatio, cents) * frequency;
  }
}
