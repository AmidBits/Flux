namespace Flux.Media.Dsp.WaveGenerator
{
  /// <summary>Pink noise oscillator. Can only be used by instance.</summary>
  /// <see cref="http://stackoverflow.com/questions/616897/how-can-i-make-a-pink-noise-generator"/>
  /// <see cref="http://www.firstpr.com.au/dsp/pink-noise/"/>
  public class PinkNoise3
    : WhiteNoise
  {
    public const double A0 = 0.02109238, A1 = 0.07113478, A2 = 0.68873558;
    public const double P0 = 0.3190, P1 = 0.7756, P2 = 0.9613;

    public const double Offset = A0 + A1 + A2;

    public const double RMI2 = 2.0 / 1.0 + 1.0;

    private double m_state0, m_state1, m_state2;

    public PinkNoise3(System.Random random)
      : base(random)
    {
    }
    public PinkNoise3()
      : base(null)
    {
    }

    public override double GenerateWave(double phase)
    {
      var temp1 = Rng.NextDouble();
      m_state0 = P0 * (m_state0 - temp1) + temp1;

      var temp2 = Rng.NextDouble();
      m_state1 = P1 * (m_state1 - temp2) + temp2;

      var temp3 = Rng.NextDouble();
      m_state2 = P2 * (m_state2 - temp3) + temp3;

      return ((A0 * m_state0 + A1 * m_state1 + A2 * m_state2) * RMI2 - Offset);
    }
  }
}
