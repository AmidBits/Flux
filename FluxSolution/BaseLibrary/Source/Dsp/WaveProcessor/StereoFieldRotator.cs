namespace Flux.Dsp.AudioProcessor
{
  public record class StereoFieldRotator
    : IStereoWaveProcessable
  {
    private double m_cosC = 1, m_sinC; // The coefficients of the transformation matrix (used for processing speed).

    private double m_angle; // The rotational angle, represented in the range [-1, 1], where -1 = -180 degrees and 1 = 180 degrees.
    /// <summary>Rotational angle in the range [-1, 1], where -1 = -180 degrees, 1 = 180 degrees and 0 = no change.</summary>
    public double Angle
    {
      get => m_angle;
      set
      {
        m_angle = System.Math.Clamp(value, -1.0, 1.0);

        if (m_angle > GenericMath.EpsilonCpp32 || m_angle < -GenericMath.EpsilonCpp32)
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

    public SampleStereo ProcessStereoWave(SampleStereo stereo)
      => new(stereo.FrontLeft * m_cosC - stereo.FrontRight * m_sinC, stereo.FrontLeft * m_sinC + stereo.FrontRight * m_cosC);

    /// <summary>Apply rotatation of the stereo sample across the stereo field.</summary>
    /// <param name="angle">Rotational angle of the stereo samples [-1, 1] across the stereo field, where -1 = -180 degrees (left), 1 = 180 degrees (right) and 0 = no change.</param>
    /// <param name="left">Left stereo sample.</param>
    /// <param name="right">Right stereo sample.</param>
    public static (double left, double right) Apply(double angle, double left, double right)
      => (angle > GenericMath.EpsilonCpp32 || angle < GenericMath.EpsilonCpp32) && angle * System.Math.PI is var anglePi && System.Math.Cos(anglePi) is var cos && System.Math.Sin(anglePi) is var sin ? (left * cos - right * sin, left * sin + right * cos) : (left, right);
  }
}