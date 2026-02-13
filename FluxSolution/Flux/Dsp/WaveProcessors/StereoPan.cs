namespace Flux.Dsp.WaveProcessors
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
        m_position = double.Clamp(value, -1, 1);

        if (m_position > Tools.PositiveThreshold)
        {
          m_positionInvAbs = 1 - m_position;
          m_scaledAbs = m_position * 0.5;
        }
        else if (m_position < Tools.NegativeThreshold)
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
      => (m_position > Tools.PositiveThreshold)
      ? (leftWave * m_positionInvAbs, leftWave * m_scaledAbs + rightWave * m_scaledAbsInv)
      : (m_position < Tools.NegativeThreshold)
      ? (leftWave * m_scaledAbsInv + rightWave * m_scaledAbs, rightWave * m_positionInvAbs)
      : (leftWave, rightWave);

    public Waves.IWaveStereo<double> ProcessStereoWave(Waves.IWaveStereo<double> stereo) => (Waves.WaveStereo<double>)ProcessStereoWave(stereo.SampleLeft, stereo.SampleRight);

    /// <summary>Apply stereo pan across the stereo field.</summary>
    /// <param name="position">Pan position in the range [-1, 1], where -1 means more of the stereo to the left, 1 means more of the stereo to the right and 0 means no change.</param>
    public static (double left, double right) Apply(double position, double left, double right)
      => (position > Tools.PositiveThreshold && position * 0.5 is var scaledRightAbs)
      ? (left * (1 - position), left * scaledRightAbs + right * (1 - scaledRightAbs))
      : (position < Tools.NegativeThreshold && position * -0.5 is var scaledLeftAbs)
      ? (left * (1 - scaledLeftAbs) + right * scaledLeftAbs, right * (-1 + position))
      : (left, right);
  }
}
