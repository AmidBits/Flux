namespace Flux.Dsp
{
  public readonly record struct Sample51
    : IAudioChannelBackLeft, IAudioChannelBackRight, IAudioChannelFrontCenter, IAudioChannelFrontLeft, IAudioChannelFrontRight, IAudioChannelLowFrequencyEffect
  {
    private readonly double m_frontLeft;
    private readonly double m_frontRight;
    private readonly double m_frontCenter;
    private readonly double m_lowFrequency;
    private readonly double m_backLeft;
    private readonly double m_backRight;

    public Sample51(in double frontLeft, in double frontRight, in double frontCenter, in double lowFrequency, in double backLeft, in double backRight)
    {
      m_frontLeft = frontLeft;
      m_frontRight = frontRight;
      m_frontCenter = frontCenter;
      m_lowFrequency = lowFrequency;
      m_backLeft = backLeft;
      m_backRight = backRight;
    }

    public double FrontLeft { get => m_frontLeft; init => m_frontLeft = value; }
    public double FrontRight { get => m_frontRight; init => m_frontRight = value; }
    public double FrontCenter { get => m_frontCenter; init => m_frontCenter = value; }
    public double LowFrequency { get => m_lowFrequency; init => m_lowFrequency = value; }
    public double BackLeft { get => m_backLeft; init => m_backLeft = value; }
    public double BackRight { get => m_backRight; init => m_backRight = value; }

    #region Static methods
    public static Sample51 From(SampleStereo frontLeftRight, SampleMono frontCenter, SampleMono lowFrequency, SampleStereo backLeftRight)
      => new(frontLeftRight.FrontLeft, frontLeftRight.FrontRight, frontCenter.FrontCenter, lowFrequency.FrontCenter, backLeftRight.FrontLeft, backLeftRight.FrontRight);
    #endregion Static methods
  }
}
