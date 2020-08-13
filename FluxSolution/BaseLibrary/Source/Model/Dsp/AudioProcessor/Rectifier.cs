namespace Flux.Dsp.AudioProcessor
{
  public enum RectifierMode
  {
    Bypass,
    /// <summary>Perform a positive full wave rectification.</summary>
    FullWave,
    /// <summary>Perform a positive half wave rectification.</summary>
    HalfWave,
    /// <summary>Perform a negative full wave rectification.</summary>
    NegativeFullWave,
    /// <summary>Perform a negative half wave rectification.</summary>
    NegativeHalfWave,
  }

  /// <see cref="https://en.wikipedia.org/wiki/Rectifier"/>
  public class RectifierMono : IAudioProcessorMono
  {
    public RectifierMode Mode { get; }

    private double m_threshold;
    /// <summary>The recifier threshold can be set within the constrained range [-1, 1].</summary>
    public double Threshold { get => m_threshold; set => m_threshold = Maths.Clamp(value, -1.0, 1.0); }

    public RectifierMono(RectifierMode mode, double threshold)
    {
      Mode = mode;
      Threshold = threshold;
    }
    public RectifierMono() : this(RectifierMode.FullWave, 0.0) { }

    public ISampleMono ProcessAudio(ISampleMono sample) => new MonoSample(Mode switch
    {
      RectifierMode.NegativeFullWave when sample.FrontCenter > m_threshold => (System.Math.Max(m_threshold - (sample.FrontCenter - m_threshold), -1)),
      RectifierMode.NegativeHalfWave when sample.FrontCenter > m_threshold => (m_threshold),
      RectifierMode.FullWave when sample.FrontCenter < m_threshold => (System.Math.Min(m_threshold + (m_threshold - sample.FrontCenter), 1)),
      RectifierMode.HalfWave when sample.FrontCenter < m_threshold => (m_threshold),
      _ => (sample.FrontCenter),
    });

    public static double RectifyFullWave(double sample, double threshold = 0.0) => sample < threshold ? System.Math.Min(threshold + (threshold - sample), 1.0) : sample;
    public static double RectifyHalfWave(double sample, double threshold = 0.0) => sample < threshold ? threshold : sample;
  }

  /// <see cref="https://en.wikipedia.org/wiki/Rectifier"/>
  public class RectifierStereo : IAudioProcessorStereo
  {
    public RectifierMono Left { get; }
    public RectifierMono Right { get; }

    public RectifierMode Mode { get => Left.Mode; }

    /// <summary>The recifier threshold can be set within the constrained range [-1, 1].</summary>
    public double Threshold { get => Left.Threshold; set => Right.Threshold = Left.Threshold = Maths.Clamp(value, -1.0, 1.0); }

    public RectifierStereo(RectifierMode leftMode, RectifierMode rightMode, double leftThreshold, double rightThreshold)
    {
      Left = new RectifierMono(leftMode, leftThreshold);
      Right = new RectifierMono(rightMode, rightThreshold);
    }
    public RectifierStereo(RectifierMode mode, double threshold) : this(mode, mode, threshold, threshold) { }
    public RectifierStereo() : this(RectifierMode.FullWave, 0.0) { }

    public ISampleStereo ProcessAudio(ISampleStereo sample) => new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }
}
