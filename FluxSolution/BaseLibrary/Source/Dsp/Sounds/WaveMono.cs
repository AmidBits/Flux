namespace Flux
{
  namespace Dsp
  {
    /// <summary>Mono wave, range [-1, +1].</summary>
    public readonly record struct WaveMono<TSelf>
    : IWaveMono<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
#endif
    {
      public readonly static IWaveMono<TSelf> Zero = new WaveMono<TSelf>();

      private readonly TSelf m_wave;

      public WaveMono(TSelf value)
      {
        m_wave = value;
      }

      public TSelf Wave { get => m_wave; init => m_wave = value; }

      #region Overloaded operators

      public static explicit operator TSelf(WaveMono<TSelf> value) => value.Wave;
      public static explicit operator WaveMono<TSelf>(TSelf value) => new(value);

      #endregion Overloaded operators
    }
  }
}