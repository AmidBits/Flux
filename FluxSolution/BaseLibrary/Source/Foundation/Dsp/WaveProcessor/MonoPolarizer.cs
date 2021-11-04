namespace Flux.Dsp.AudioProcessor
{
  public enum MonoPolarizerMode
  {
    Bypass,
    BipolarToUnipolarNegative,
    BipolarToUnipolarPositive,
    UnipolarNegativeToBipolar,
    UnipolarPositiveToBipolar,
  }

  /// <summary>Enables inline conversion of the polarity range, e.g. bipolar to unipolar (or the other way around).</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polarization_(waves)"/>
  public sealed class MonoPolarizer
    : IWaveProcessorMono
  {
    public MonoPolarizerMode Mode { get; }

    public MonoPolarizer(MonoPolarizerMode mode)
    {
      Mode = mode;
    }
    public MonoPolarizer()
      : this(MonoPolarizerMode.BipolarToUnipolarPositive)
    { }

    public double ProcessAudio(double sample) => (Mode switch
    {
      MonoPolarizerMode.BipolarToUnipolarNegative => sample / 2.0 - 0.5,
      MonoPolarizerMode.BipolarToUnipolarPositive => sample / 2.0 + 0.5,
      MonoPolarizerMode.UnipolarNegativeToBipolar => sample < 0.0 ? sample * 2.0 + 1.0 : 0.0,
      MonoPolarizerMode.UnipolarPositiveToBipolar => sample > 0.0 ? sample * 2.0 - 1.0 : 0.0,
      _ => sample,
    });

    public static double ApplyBipolarToUnipolarNegative(double sample)
      => sample / 2.0 - 0.5;
    public static double ApplyBipolarToUnipolarPositive(double sample)
      => sample / 2.0 + 0.5;
    public static double ApplyUnipolarNegativeToBipolar(double sample)
      => sample < 0.0 ? sample * 2.0 + 1.0 : 0.0;
    public static double ApplyUnipolarPositiveToBipolar(double sample)
      => sample > 0.0 ? sample * 2.0 - 1.0 : 0.0;
  }
}
