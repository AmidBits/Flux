namespace Flux.Dsp.Samples
{
  /// <summary>A stereo (left and right) wave, range [-1.0, +1.0].</summary>
  public readonly record struct SampleStereo<TSelf>
    : ISampleStereo<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public readonly static ISampleStereo<TSelf> Silence = new SampleStereo<TSelf>();

    private readonly TSelf m_sampleLeft;
    private readonly TSelf m_sampleRight;

    public SampleStereo(TSelf sampleLeft, TSelf sampleRight)
    {
      m_sampleLeft = sampleLeft;
      m_sampleRight = sampleRight;
    }

    public TSelf SampleLeft { get => m_sampleLeft; init => m_sampleLeft = value; }
    public TSelf SampleRight { get => m_sampleRight; init => m_sampleRight = value; }

    public Dsp.Samples.SampleMono<TSelf> ToMonoSample() => new((m_sampleLeft + m_sampleRight) / TSelf.CreateChecked(2));

    #region Static methods

    /// <summary>Convert a set of stereo samples into a mono sample.</summary>
    public static double ConvertStereoToMono(double sampleL, double sampleR)
      => (sampleL + sampleR) / 2;

    /// <summary>Convert Fl/Fr (stereo) into Fl/Fc/Fr (stereo plus center) signals. This is a stab, not a/the mathematical solution.</summary>
    /// <param name="sampleFrontLeft">Front left.</param>
    /// <param name="sampleFrontRight">Front right.</param>
    /// <see href="https://hydrogenaud.io/index.php?PHPSESSID=1n6jd2rvvcdc1g2cjmdbml0g17&topic=17267.msg174102#msg174102"/>
    public static (double sampleFrontLeft, double sampleFrontCenter, double sampleFrontRight) ConvertStereoToFlFcFr(double sampleFrontLeft, double sampleFrontRight)
      => (sampleFrontLeft - sampleFrontRight / 2, (sampleFrontLeft + sampleFrontRight) / 2, sampleFrontRight - sampleFrontLeft / 2);

    #endregion // Static methods

    #region Overloaded operators

    public static explicit operator (TSelf leftSample, TSelf rightSample)(SampleStereo<TSelf> stereo) => (stereo.SampleLeft, stereo.SampleRight);
    public static explicit operator SampleStereo<TSelf>((TSelf leftSample, TSelf rightSample) stereo) => new(stereo.leftSample, stereo.rightSample);

    #endregion // Overloaded operators  
  }
}
