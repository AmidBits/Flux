namespace Flux.Dsp.AudioProcessor
{
  public class StereoFieldRotator : IAudioProcessorStereo
  {
    private double m_cosC = 1, m_sinC; // The coefficients of the transformation matrix (used for processing speed).

    private double m_angle; // The rotational angle, represented in the range [-1, 1], where -1 = -180 degrees and 1 = 180 degrees.
    public double Angle
    {
      get => m_angle;
      set
      {
        m_angle = Maths.Clamp(value, -1.0, 1.0);

        if (m_angle > Maths.EpsilonCpp32 || m_angle < -Maths.EpsilonCpp32)
        {
          var angle = m_angle * System.Math.PI;

          m_cosC = System.Math.Cos(angle);
          m_sinC = System.Math.Sin(angle);
        }
        else
        {
          m_cosC = 1.0;
          m_sinC = 1.0;
        }
      }
    }

    public ISampleStereo ProcessAudio(ISampleStereo sample) => new StereoSample(sample.FrontLeft * m_cosC - sample.FrontRight * m_sinC, sample.FrontLeft * m_sinC + sample.FrontRight * m_cosC);

    /// <summary>Apply rotatation of left and right using the specified angle to an arbitrary stereo signal sample</summary>
    /// <param name="angle">The rotational angle of the stereo samples [-1, 1] across the stereo field, where negative means to the left, positive means to the right and 0 means center.</param>
    /// <param name="left">The left stereo sample in the range [-1, 1].</param>
    /// <param name="right">The right stereo sample in the range [-1, 1].</param>
    public static (double left, double right) ApplyStereoFieldRotation(double angle, double left, double right) => (angle > Maths.EpsilonCpp32 || angle < Maths.EpsilonCpp32) && angle * System.Math.PI is var anglePi && System.Math.Cos(anglePi) is var cos && System.Math.Sin(anglePi) is var sin ? (left * cos - right * sin, left * sin + right * cos) : (left, right);
  }
}
