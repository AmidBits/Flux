namespace Flux.Dsp.AudioProcessor
{
  public enum PolarizerMode
  {
    Bypass,
    BipolarToUnipolarNegative,
    BipolarToUnipolarPositive,
    UnipolarNegativeToBipolar,
    UnipolarPositiveToBipolar,
  }

  /// <see cref="https://en.wikipedia.org/wiki/Polarization_(waves)"/>
  public class MonoPolarizer
    : IWaveProcessorMono
  {
    public PolarizerMode Mode { get; }

    public MonoPolarizer(PolarizerMode mode)
    {
      Mode = mode;
    }
    public MonoPolarizer() : this(PolarizerMode.BipolarToUnipolarPositive) { }

    public double ProcessAudio(double sample) => (Mode switch
    {
      PolarizerMode.BipolarToUnipolarNegative => (sample / 2.0 - 0.5),
      PolarizerMode.BipolarToUnipolarPositive => (sample / 2.0 + 0.5),
      PolarizerMode.UnipolarNegativeToBipolar => (sample < 0.0 ? sample * 2.0 + 1.0 : 0.0),
      PolarizerMode.UnipolarPositiveToBipolar => (sample > 0.0 ? sample * 2.0 - 1.0 : 0.0),
      _ => (sample),
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
