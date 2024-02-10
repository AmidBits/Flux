namespace Flux
{
  namespace Dsp
  {
    /// <summary>Quad wave (back left, back right, front left and front right), range [-1, +1].</summary>
    public readonly record struct WaveQuad<TSelf>
      : IWaveQuad<TSelf>
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      public readonly static IWaveStereo<TSelf> Silence = new WaveStereo<TSelf>();

      private readonly TSelf m_waveBackLeft;
      private readonly TSelf m_waveBackRight;
      private readonly TSelf m_waveFrontLeft;
      private readonly TSelf m_waveFrontRight;

      public WaveQuad(TSelf waveBackLeft, TSelf waveBackRight, TSelf waveFrontLeft, TSelf waveFrontRight)
      {
        m_waveBackLeft = waveBackLeft;
        m_waveBackRight = waveBackRight;
        m_waveFrontLeft = waveFrontLeft;
        m_waveFrontRight = waveFrontRight;
      }

      public TSelf WaveBackLeft { get => m_waveBackLeft; init => m_waveBackLeft = value; }
      public TSelf WaveBackRight { get => m_waveBackRight; init => m_waveBackRight = value; }
      public TSelf WaveFrontLeft { get => m_waveFrontLeft; init => m_waveFrontLeft = value; }
      public TSelf WaveFrontRight { get => m_waveFrontRight; init => m_waveFrontRight = value; }

      #region Overloaded operators

      public static explicit operator (TSelf, TSelf, TSelf, TSelf)(WaveQuad<TSelf> wave) => (wave.WaveBackLeft, wave.WaveBackRight, wave.WaveFrontLeft, wave.WaveFrontRight);
      public static explicit operator WaveQuad<TSelf>((TSelf leftBackWave, TSelf rightBackWave, TSelf leftFrontWave, TSelf rightFrontWave) wave) => new(wave.leftBackWave, wave.rightBackWave, wave.leftFrontWave, wave.rightFrontWave);

      #endregion Overloaded operators  
    }
  }
}
