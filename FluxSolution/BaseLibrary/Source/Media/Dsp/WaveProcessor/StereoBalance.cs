namespace Flux.Dsp.AudioProcessor
{
  public class StereoBalance
    : IWaveProcessorStereo
  {
    private double m_peakL = 1, m_peakR = 1; // The max peaks of the each channel, represented in the range [0, 1], where 0 is silent and 1 is full volume (no mix), i.e. 100%.

    private double m_position; // The position of the balance across the stereo field, represented in the range [-1, 1], where -1 is left and 1 is right.
    public double Position
    {
      get => m_position;
      set
      {
        m_position = Maths.Clamp(value, -1.0, 1.0);

        if (m_position > Maths.EpsilonCpp32)
        {
          m_peakR = 1.0;
          m_peakL = 1.0 - m_position;
        }
        else if (m_position < -Maths.EpsilonCpp32)
        {
          m_peakL = 1.0;
          m_peakR = 1.0 + m_position;
        }
        else
        {
          m_peakL = 1.0;
          m_peakR = 1.0;
        }
      }
    }

    public StereoSample ProcessAudio(StereoSample stereo)
      => new StereoSample(stereo.FrontLeft * m_peakL, stereo.FrontRight * m_peakR);

    /// <summary>Apply balance across the stereo field.</summary>
    /// <param name="position">Balance position in the range [-1, 1], where -1 = more left, 1 means more right and 0 means no change.</param>
    /// <param name="left">Left stereo sample.</param>
    /// <param name="right">Right stereo sample.</param>
    public static (double left, double right) Apply(double position, double left, double right)
      => position > Maths.EpsilonCpp32 ? (left * (1.0 - position), right) : position < Maths.EpsilonCpp32 ? (left, right * (1.0 + position)) : (left, right);
  }
}
