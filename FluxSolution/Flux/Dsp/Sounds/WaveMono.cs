namespace Flux
{
  namespace Dsp
  {
    /// <summary>A mono wave, range [-1.0, +1.0].</summary>
    public readonly record struct WaveMono<TSelf>
      : IWaveMono<TSelf>
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      public readonly static IWaveMono<TSelf> Silence = new WaveMono<TSelf>();

      private readonly TSelf m_wave;

      public WaveMono(TSelf value) => m_wave = value;

      public TSelf Wave { get => m_wave; init => m_wave = value; }

      #region Overloaded operators

      public static explicit operator TSelf(WaveMono<TSelf> mono) => mono.Wave;
      public static explicit operator WaveMono<TSelf>(TSelf mono) => new(mono);

      #endregion Overloaded operators
    }
  }
}