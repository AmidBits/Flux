namespace Flux.Dsp.AudioProcessor
{
  public enum InverterMode
  {
    Bypass,
    /// <summary>Invert the negative polarity only.</summary>
    NegativePeekOnly,
    /// <summary>Invert each polarity independently of each other.</summary>
    PeeksIndependently,
    /// <summary>Invert across both polarities.</summary>
    PeekToPeek,
    /// <summary>Invert the positive polarity only.</summary>
    PositivePeekOnly
  }

  public class InverterMono : IAudioProcessorMono
  {
    public InverterMode Mode { get; internal set; }

    public InverterMono(InverterMode mode) => Mode = mode;
    public InverterMono() : this(InverterMode.PeekToPeek) { }

    public ISampleMono ProcessAudio(ISampleMono sample) => new MonoSample(Mode switch
    {
      InverterMode.PeekToPeek => (-sample.FrontCenter),
      InverterMode.PeeksIndependently when sample.FrontCenter < 0 => (-sample.FrontCenter - 1),
      InverterMode.NegativePeekOnly when sample.FrontCenter < 0 => (-sample.FrontCenter - 1),
      InverterMode.PeeksIndependently when sample.FrontCenter > 0 => (-sample.FrontCenter + 1),
      InverterMode.PositivePeekOnly when sample.FrontCenter > 0 => (-sample.FrontCenter + 1),
      _ => (sample.FrontCenter),
    });

    public static double InvertNegativePeekOnly(double sample) => sample < 0.0 ? -sample - 1.0 : sample;
    public static double InvertPeeksIndependently(double sample) => sample < 0.0 ? -sample - 1.0 : sample > 0.0 ? -sample + 1.0 : sample;
    public static double InvertPeekToPeek(double sample) => -sample;
    public static double InvertPositivePeekOnly(double sample) => sample > 0.0 ? -sample + 1.0 : sample;
  }

  public class InverterStereo : IAudioProcessorStereo
  {
    public InverterMono Left { get; }
    public InverterMono Right { get; }

    public InverterMode Mode { get => Left.Mode; }

    public InverterStereo(InverterMode modeL, InverterMode modeR)
    {
      Left = new InverterMono(modeL);
      Right = new InverterMono(modeR);
    }
    public InverterStereo(InverterMode mode)
    {
      Left = new InverterMono(mode);
      Right = new InverterMono(mode);
    }
    public InverterStereo() : this(InverterMode.PeekToPeek) { }

    public ISampleStereo ProcessAudio(ISampleStereo sample) => new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }
}
