namespace Flux.Dsp.AudioProcessor
{
  public class StereoBalance
    : IAudioProcessorStereo
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

    public StereoSample ProcessAudio(StereoSample sample)
      => new StereoSample(sample.FrontLeft * m_peakL, sample.FrontRight * m_peakR);

    /// <summary>Apply stereo balance using the specified position to an arbitrary stereo signal sample</summary>
    /// <param name="position">The balance position of the stereo samples [-1, 1] across the stereo field, where negative means to the left, positive means to the right and 0 means center.</param>
    /// <param name="left">The left stereo sample in the range [-1, 1].</param>
    /// <param name="right">The right stereo sample in the range [-1, 1].</param>
    public static (double left, double right) ApplyStereoBalance(double position, double left, double right)
      => position > Maths.EpsilonCpp32 ? (left * (1.0 - position), right) : position < Maths.EpsilonCpp32 ? (left, right * (1.0 + position)) : (left, right);
  }
}
