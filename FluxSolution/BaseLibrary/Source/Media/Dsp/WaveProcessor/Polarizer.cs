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
  public class PolarizerMono
    : IWaveProcessorMono
  {
    public PolarizerMode Mode { get; }

    public PolarizerMono(PolarizerMode mode)
    {
      Mode = mode;
    }
    public PolarizerMono() : this(PolarizerMode.BipolarToUnipolarPositive) { }

    public double ProcessAudio(double sample) => (Mode switch
    {
      PolarizerMode.BipolarToUnipolarNegative => (sample / 2.0 - 0.5),
      PolarizerMode.BipolarToUnipolarPositive => (sample / 2.0 + 0.5),
      PolarizerMode.UnipolarNegativeToBipolar => (sample < 0.0 ? sample * 2.0 + 1.0 : 0.0),
      PolarizerMode.UnipolarPositiveToBipolar => (sample > 0.0 ? sample * 2.0 - 1.0 : 0.0),
      _ => (sample),
    });

    public static double ApplyBipolarToUnipolarNegative(double sample) => sample / 2.0 - 0.5;
    public static double ApplyBipolarToUnipolarPositive(double sample) => sample / 2.0 + 0.5;
    public static double ApplyUnipolarNegativeToBipolar(double sample) => sample < 0.0 ? sample * 2.0 + 1.0 : 0.0;
    public static double ApplyUnipolarPositiveToBipolar(double sample) => sample > 0.0 ? sample * 2.0 - 1.0 : 0.0;
  }

  /// <see cref="https://en.wikipedia.org/wiki/Polarization_(waves)"/>
  public class PolarizerStereo : IWaveProcessorStereo
  {
    public PolarizerMono Left { get; }
    public PolarizerMono Right { get; }

    public PolarizerMode Mode { get => Left.Mode; }

    public PolarizerStereo(PolarizerMode modeL, PolarizerMode modeR)
    {
      Left = new PolarizerMono(modeL);
      Right = new PolarizerMono(modeR);
    }
    public PolarizerStereo(PolarizerMode mode)
      : this(mode, mode)
    {
    }
    public PolarizerStereo()
      : this(PolarizerMode.BipolarToUnipolarPositive)
    {
    }

    public StereoSample ProcessAudio(StereoSample sample)
      => new StereoSample(Left.ProcessAudio(sample.FrontLeft), Right.ProcessAudio(sample.FrontRight));
  }
}
