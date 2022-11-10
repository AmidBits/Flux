namespace Flux.Dsp
{
  public readonly record struct Sample21
    : IAudioChannelFrontLeft, IAudioChannelFrontRight, IAudioChannelLowFrequencyEffect
  {
    private readonly double m_frontLeft;
    private readonly double m_frontRight;
    private readonly double m_lowFrequency;

    public Sample21(in double frontLeft, in double frontRight, in double lowFrequency)
    {
      m_frontLeft = frontLeft;
      m_frontRight = frontRight;
      m_lowFrequency = lowFrequency;
    }
    public Sample21(in SampleStereo stereo, in double lowFrequency)
      : this(stereo.FrontLeft, stereo.FrontRight, lowFrequency)
    { }

    public double FrontLeft { get => m_frontLeft; init => m_frontLeft = value; }
    public double FrontRight { get => m_frontRight; init => m_frontRight = value; }
    public double LowFrequency { get => m_lowFrequency; init => m_lowFrequency = value; }

    #region Static methods
    public static Sample21 From(SampleStereo front, SampleMono lowFrequency)
      => new(front.FrontLeft, front.FrontRight, lowFrequency.FrontCenter);
    #endregion Static methods
  }
}
