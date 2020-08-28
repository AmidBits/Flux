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
    : IAudioProcessorMono
  {
    public PolarizerMode Mode { get; }

    public PolarizerMono(PolarizerMode mode)
    {
      Mode = mode;
    }
    public PolarizerMono() : this(PolarizerMode.BipolarToUnipolarPositive) { }

    public MonoSample ProcessAudio(MonoSample sample) => new MonoSample(Mode switch
    {
      PolarizerMode.BipolarToUnipolarNegative => (sample.FrontCenter / 2.0 - 0.5),
      PolarizerMode.BipolarToUnipolarPositive => (sample.FrontCenter / 2.0 + 0.5),
      PolarizerMode.UnipolarNegativeToBipolar => (sample.FrontCenter < 0.0 ? sample.FrontCenter * 2.0 + 1.0 : 0.0),
      PolarizerMode.UnipolarPositiveToBipolar => (sample.FrontCenter > 0.0 ? sample.FrontCenter * 2.0 - 1.0 : 0.0),
      _ => (sample.FrontCenter),
    });

    public static double ApplyBipolarToUnipolarNegative(double sample) => sample / 2.0 - 0.5;
    public static double ApplyBipolarToUnipolarPositive(double sample) => sample / 2.0 + 0.5;
    public static double ApplyUnipolarNegativeToBipolar(double sample) => sample < 0.0 ? sample * 2.0 + 1.0 : 0.0;
    public static double ApplyUnipolarPositiveToBipolar(double sample) => sample > 0.0 ? sample * 2.0 - 1.0 : 0.0;
  }

  /// <see cref="https://en.wikipedia.org/wiki/Polarization_(waves)"/>
  public class PolarizerStereo : IAudioProcessorStereo
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
      => new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }
}
