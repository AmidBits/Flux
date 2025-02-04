namespace Flux
{
  namespace Dsp
  {
    /// <summary>A stereo (left and right) wave, range [-1.0, +1.0].</summary>
    public readonly record struct WaveStereo<TSelf>
      : IWaveStereo<TSelf>
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      public readonly static IWaveStereo<TSelf> Silence = new WaveStereo<TSelf>();

      private readonly TSelf m_waveLeft;
      private readonly TSelf m_waveRight;

      public WaveStereo(TSelf waveLeft, TSelf waveRight)
      {
        m_waveLeft = waveLeft;
        m_waveRight = waveRight;
      }

      public TSelf SampleLeft { get => m_waveLeft; init => m_waveLeft = value; }
      public TSelf SampleRight { get => m_waveRight; init => m_waveRight = value; }

      #region Overloaded operators

      public static explicit operator (TSelf leftWave, TSelf rightWave)(WaveStereo<TSelf> stereo) => (stereo.SampleLeft, stereo.SampleRight);
      public static explicit operator WaveStereo<TSelf>((TSelf leftWave, TSelf rightWave) stereo) => new(stereo.leftWave, stereo.rightWave);

      #endregion Overloaded operators  
    }
  }
}
