namespace Flux.Dsp
{
  public readonly record struct SampleQuadraphonic
    : IAudioChannelFrontLeft, IAudioChannelFrontRight, IAudioChannelBackLeft, IAudioChannelBackRight
  {
    private readonly double m_frontLeft;
    private readonly double m_frontRight;
    private readonly double m_backLeft;
    private readonly double m_backRight;

    public SampleQuadraphonic(in double frontLeft, in double frontRight, in double backLeft, in double backRight)
    {
      m_frontLeft = frontLeft;
      m_frontRight = frontRight;
      m_backLeft = backLeft;
      m_backRight = backRight;
    }

    public double FrontLeft { get => m_frontLeft; }
    public double FrontRight { get => m_frontRight; }
    public double BackLeft { get => m_backLeft; }
    public double BackRight { get => m_backRight; }
  }
}
