namespace Flux.Dsp
{
  public readonly record struct SampleStereo
    : IAudioChannelFrontLeft, IAudioChannelFrontRight
  {
    public readonly static SampleStereo Zero;

    private readonly double m_frontLeft;
    private readonly double m_frontRight;

    public SampleStereo(double frontLeft, double frontRight)
    {
      m_frontLeft = frontLeft;
      m_frontRight = frontRight;
    }
    public SampleStereo(double frontCenter)
      : this(frontCenter, frontCenter)
    { }

    public double FrontLeft { get => m_frontLeft; init => m_frontLeft = value; }
    public double FrontRight { get => m_frontRight; init => m_frontRight = value; }

    public SampleMono ToMono()
      => new(ConvertStereoToMono(FrontLeft, FrontRight));

    #region Static methods
    public static double ConvertStereoToMono(double frontLeft, double frontRight)
      => (frontLeft + frontRight) / 2;

    /// <summary>Mix one or more stereo signals. One stereo signal will be returned as is.</summary>
    public static SampleStereo Mix(System.Collections.Generic.IEnumerable<SampleStereo> stereo)
    {
      using var e = stereo.GetEnumerator();

      if (e.MoveNext())
      {
        var count = 1;
        var sumL = e.Current.m_frontLeft;
        var sumR = e.Current.m_frontRight;

        while (e.MoveNext())
        {
          count++;
          sumL += e.Current.m_frontLeft;
          sumR += e.Current.m_frontRight;
        }

        return count > 1 && System.Math.Sqrt(count) is var sqrtCount ? new(sumL / sqrtCount, sumR / sqrtCount) : new(sumL, sumR);
      }
      else throw new System.ArgumentException(@"The sequence is empty.");
    }
    public static SampleStereo Mix(params SampleStereo[] stereo)
      => Mix(stereo.AsEnumerable());
    #endregion Static methods
  }
}
