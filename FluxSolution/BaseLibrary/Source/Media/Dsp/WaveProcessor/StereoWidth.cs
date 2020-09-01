namespace Flux.Dsp.AudioProcessor
{
  public class StereoWidth
    : IWaveProcessorStereo
  {
    private double m_stereoCoefficient; // The coefficients of the transformation matrix (used for processing speed).

    private double m_width;
    ///<summar>The stereo width, represented in the range [-1, 1], where -1 = mono, <0 = decrese stereo width, 0 = no change, and >0 = increase stereo width.</summar>
    public double Width
    {
      get => m_width;
      set
      {
        m_width = Maths.Clamp(value, -1.0, 1.0);

        var tmp = 1 / System.Math.Max(m_width + 1, 2);

        m_stereoCoefficient = (m_width > Maths.EpsilonCpp32 || m_width < -Maths.EpsilonCpp32) ? (m_width + 1) / 2 : 1;
      }
    }

    public StereoSample ProcessAudio(StereoSample sample)
    {
      var m = (sample.FrontLeft + sample.FrontRight) / 2;
      var s = (sample.FrontRight - sample.FrontLeft) * m_stereoCoefficient;

      return new StereoSample(m - s, m + s);
    }
  }
}
