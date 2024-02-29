namespace Flux.Dsp.AudioProcessor
{
  public record class StereoPan
    : IStereoWaveProcessable
  {
    private double m_position, m_positionInvAbs, m_scaledAbs, m_scaledAbsInv;
    /// <summary>The position of the pan across the stereo field in the range [-1, 1], where -1 means more stereo signal to the left, 1 means more stereo signal to the right, and 0 means no change.</summary>
    public double Position
    {
      get => m_position;
      set
      {
        m_position = System.Math.Clamp(value, -1, 1);

        if (m_position > Maths.EpsilonCpp32)
        {
          m_positionInvAbs = 1 - m_position;
          m_scaledAbs = m_position * 0.5;
        }
        else if (m_position < -Maths.EpsilonCpp32)
        {
          m_positionInvAbs = -1 + m_position;
          m_scaledAbs = m_position * -0.5;
        }
        else
        {
          m_positionInvAbs = 1;
          m_scaledAbs = 0;
        }

        m_scaledAbsInv = 1 - m_scaledAbs;
      }
    }

    public (double leftWave, double rightWave) ProcessStereoWave(double leftWave, double rightWave)
      => (m_position > Maths.EpsilonCpp32)
      ? (leftWave * m_positionInvAbs, leftWave * m_scaledAbs + rightWave * m_scaledAbsInv)
      : (m_position < -Maths.EpsilonCpp32)
      ? (leftWave * m_scaledAbsInv + rightWave * m_scaledAbs, rightWave * m_positionInvAbs)
      : (leftWave, rightWave);

    public IWaveStereo<double> ProcessStereoWave(IWaveStereo<double> stereo) => (WaveStereo<double>)ProcessStereoWave(stereo.SampleLeft, stereo.SampleRight);

    /// <summary>Apply stereo pan across the stereo field.</summary>
    /// <param name="position">Pan position in the range [-1, 1], where -1 means more of the stereo to the left, 1 means more of the stereo to the right and 0 means no change.</param>
    public static (double left, double right) Apply(double position, double left, double right)
      => (position > Maths.EpsilonCpp32 && position * 0.5 is var scaledRightAbs)
      ? (left * (1 - position), left * scaledRightAbs + right * (1 - scaledRightAbs))
      : (position < -Maths.EpsilonCpp32 && position * -0.5 is var scaledLeftAbs)
      ? (left * (1 - scaledLeftAbs) + right * scaledLeftAbs, right * (-1 + position))
      : (left, right);
  }
}
