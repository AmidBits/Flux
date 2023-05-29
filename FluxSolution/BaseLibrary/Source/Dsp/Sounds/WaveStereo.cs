namespace Flux
{
  namespace Dsp
  {
    /// <summary>Stereo (left and right) wave, range [-1, +1].</summary>
    public readonly record struct WaveStereo<TSelf>
      : IWaveStereo<TSelf>
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      public readonly static IWaveStereo<TSelf> Zero = new WaveStereo<TSelf>();

      private readonly TSelf m_leftWave;
      private readonly TSelf m_rightWave;

      public WaveStereo(TSelf leftWave, TSelf rightWave)
      {
        m_leftWave = leftWave;
        m_rightWave = rightWave;
      }

      public TSelf LeftWave { get => m_leftWave; init => m_leftWave = value; }
      public TSelf RightWave { get => m_rightWave; init => m_rightWave = value; }

      #region Overloaded operators

      public static explicit operator (TSelf, TSelf)(WaveStereo<TSelf> stereo) => (stereo.LeftWave, stereo.RightWave);
      public static explicit operator WaveStereo<TSelf>((TSelf leftWave, TSelf rightWave) stereo) => new(stereo.leftWave, stereo.rightWave);

      #endregion Overloaded operators  
    }
  }
}
