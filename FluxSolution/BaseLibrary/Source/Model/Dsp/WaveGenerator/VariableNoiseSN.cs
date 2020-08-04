namespace Flux.Dsp.WaveGenerator
{
  /// <summary>
  /// A class (Viir ~ variable IIR) that provides a source of pink noise with a power spectrum density(PSD) proportional to 1/f^alpha.
  /// "Regular" pink noise has a PSD proportional to 1/f, i.e. alpha=1.
  /// However, many natural systems may require a different PSD proportionality. 
  /// The value of alpha may be from 0 to 2, inclusive. The special case alpha=0 results in white noise (directly generated random numbers) and alpha = 2 results in brown noise (integrated white noise).
  /// </summary>
  /// <see cref="http://sampo.kapsi.fi/PinkNoise/"/>
  public class VariableNoiseSN
    : WhiteNoise
  {
    private readonly double[] m_coefficients, m_values;

    /// <summary>Generate pink noise specifying alpha and the number of poles. The larger the number of poles, the lower are the lowest frequency components that are amplified.</summary>
    /// <param name="alpha">The exponent of the pink noise, 1/f^alpha. 0 = white noise, 1 = pink noise, 2 = brown noise (integrated white noise).</param>
    /// <param name="poles">The number of poles to use. 1-3 = noise concentrated near zero, more poles allow more low frequency components to be included, leading to more variation from zero. However, the sequence is stationary, that is, it will always return to zero even with a large number of poles.</param>
    /// <param name="rng">The random number generator to use.</param>
    public VariableNoiseSN(double alpha, int poles, System.Random? rng) : base(rng)
    {
      if (alpha < 0 || alpha > 2)
      {
        throw new System.ArgumentOutOfRangeException(nameof(alpha));
      }
      else if (poles < 0 || poles > 256)
      {
        throw new System.ArgumentOutOfRangeException(nameof(poles));
      }

      m_coefficients = new double[poles];

      var accumulator = 1.0;

      for (var index = 0; index < poles; index++)
      {
        accumulator = (index - alpha / 2) * accumulator / (index + 1);

        m_coefficients[index] = accumulator;
      }

      m_values = new double[poles];

      for (var i = 0; i < (5 * m_values.Length); i++)
      {
        GenerateWave(default);
      }
    }
    public VariableNoiseSN() : this(1, 5, null) { }

    public override ISampleMono GenerateWave(double phase)
    {
      // ??? how can this work when it makes -0.5 to 0.5
      var value0 = Rng?.NextDouble() ?? (Rng = new System.Random()).NextDouble() - 0.5;
      // ??? in comparison to this one making 0.0 to 1.0
      // var value0 = _random.NextGaussian(); 

      for (var index = 0; index < m_coefficients.Length; index++)
      {
        value0 -= (m_coefficients[index] * m_values[index]);
      }

      System.Array.Copy(m_values, 0, m_values, 1, m_values.Length - 1);

      m_values[0] = value0;

      return new MonoSample(value0);
    }
  }
}
