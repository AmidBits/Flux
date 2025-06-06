namespace Flux.Dsp.WaveFilters
{
  /// <comment>A biquad is a second order (two poles and two zeros) IIR filter. It is high enough order to be useful on its own, and—because of coefficient sensitivities in higher order filters—the biquad is often used as the basic building block for more complex filters. This implementation use the transposed direct form II architecture (which has good floating point characteristics, and requires only two unit delays).</comment>
  /// <see cref="http://www.earlevel.com/main/2012/11/26/biquad-c-source-code/"/>
  /// <seealso cref="http://www.earlevel.com/main/2003/02/28/biquads/"/>
  public record class BiQuad
    : IMonoWaveFilterable, WaveProcessors.IMonoWaveProcessable
  {
    public BiQuadFrequencyFunction Function { get; private set; }

    private double m_cutoffFrequency;
    /// <value>Typical audio range settings are between 20 to 20,000 Hz, but no restrictions are enforced.</value>
    public double Cutoff { get => m_cutoffFrequency; set => DialFilter(value, m_Q, m_gain, m_sampleRate); }

    private double m_gain;
    /// <value>Typical audio range settings are between -30 to 30 dB, but no restrictions are enforced.</value>
    public double Gain { get => m_gain; set => DialFilter(m_cutoffFrequency, m_Q, value, m_sampleRate); }

    private double m_Q;
    /// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
    /// <seealso cref="http://www.earlevel.com/main/2016/09/29/cascading-filters/"/>
    public double Q { get => m_Q; set => DialFilter(m_cutoffFrequency, value, m_gain, m_sampleRate); }

    private double m_sampleRate;
    /// <summary>Sets the sample rate used for filter calculations.</summary>
    public double SampleRate { get => m_sampleRate; set => DialFilter(m_cutoffFrequency, m_Q, m_gain, value); }

    public BiQuad(BiQuadFrequencyFunction frequencyFunction, double cutoffFrequency, double Q = 0.5, double gain = 0, double sampleRate = 44100)
    {
      ClearState();

      Function = frequencyFunction;

      DialFilter(cutoffFrequency, Q, gain, sampleRate);
    }

    private double z1, z2;

    public void ClearState()
    {
      z1 = 0.0;
      z2 = 0.0;
    }

    private double a0, a1, a2, b1, b2;

    private void SetCoefficientsBandPass()
    {
      double k = double.TanPi(m_cutoffFrequency / m_sampleRate), kk = k * k, kQ = k / m_Q, norm = 1.0 / (1.0 + kQ + kk);

      a0 = kQ * norm;
      a1 = 0;
      a2 = -a0;
      b1 = 2.0 * (kk - 1.0) * norm;
      b2 = (1.0 - kQ + kk) * norm;
    }
    private void SetCoefficientsHighPass()
    {
      double k = double.TanPi(m_cutoffFrequency / m_sampleRate), kk = k * k, kQ = k / m_Q, norm = 1.0 / (1.0 + kQ + kk);

      a0 = 1.0 * norm;
      a1 = -2.0 * a0;
      a2 = a0;
      b1 = 2.0 * (kk - 1.0) * norm;
      b2 = (1.0 - kQ + kk) * norm;
    }
    private void SetCoefficientsHighShelf()
    {
      double k = double.TanPi(m_cutoffFrequency / m_sampleRate), kk = k * k, sqrt2k = double.Sqrt(2.0) * k, v = double.Pow(10.0, double.Abs(m_gain) / 20.0), sqrt2vk = double.Sqrt(2.0 * v) * k;

      if (m_gain >= 0) // boost
      {
        var norm = 1.0 / (1.0 + sqrt2k + kk);
        a0 = (v + sqrt2vk + kk) * norm;
        a1 = 2.0 * (kk - v) * norm;
        a2 = (v - sqrt2vk + kk) * norm;
        b1 = 2.0 * (kk - 1.0) * norm;
        b2 = (1.0 - sqrt2k + kk) * norm;
      }
      else // cut
      {
        var norm = 1.0 / (v + sqrt2vk + kk);
        a0 = (1.0 + sqrt2k + kk) * norm;
        a1 = 2.0 * (kk - 1.0) * norm;
        a2 = (1.0 - sqrt2k + kk) * norm;
        b1 = 2.0 * (kk - v) * norm;
        b2 = (v - sqrt2vk + kk) * norm;
      }
    }
    private void SetCoefficientsLowPass()
    {
      double k = double.TanPi(m_cutoffFrequency / m_sampleRate), kk = k * k, kQ = k / m_Q, norm = 1.0 / (1.0 + kQ + kk);

      a0 = kk * norm;
      a1 = 2.0 * a0;
      a2 = a0;
      b1 = 2.0 * (kk - 1.0) * norm;
      b2 = (1.0 - kQ + kk) * norm;
    }
    private void SetCoefficientsLowShelf()
    {
      double k = double.TanPi(m_cutoffFrequency / m_sampleRate), kk = k * k, sqrt2k = double.Sqrt(2.0) * k, v = double.Pow(10.0, double.Abs(m_gain) / 20.0), vkk = v * kk, sqrt2vk = double.Sqrt(2.0 * v) * k;

      if (m_gain >= 0) // boost
      {
        var norm = 1.0 / (1.0 + sqrt2k + kk);
        a0 = (1.0 + sqrt2vk + vkk) * norm;
        a1 = 2.0 * (vkk - 1.0) * norm;
        a2 = (1.0 - sqrt2vk + vkk) * norm;
        b1 = 2.0 * (kk - 1.0) * norm;
        b2 = (1.0 - sqrt2k + kk) * norm;
      }
      else // cut
      {
        var norm = 1.0 / (1.0 + sqrt2vk + vkk);
        a0 = (1.0 + sqrt2k + kk) * norm;
        a1 = 2.0 * (kk - 1.0) * norm;
        a2 = (1.0 - sqrt2k + kk) * norm;
        b1 = 2.0 * (vkk - 1.0) * norm;
        b2 = (1.0 - sqrt2vk + vkk) * norm;
      }
    }
    private void SetCoefficientsNotch()
    {
      double k = double.TanPi(m_cutoffFrequency / m_sampleRate), kk = k * k, kQ = k / m_Q, norm = 1.0 / (1.0 + kQ + kk);

      a0 = (1.0 + kk) * norm;
      a1 = 2.0 * (kk - 1.0) * norm;
      a2 = a0;
      b1 = a1;
      b2 = (1.0 - kQ + kk) * norm;
    }
    private void SetCoefficientsPeak()
    {
      double k = double.TanPi(m_cutoffFrequency / m_sampleRate), kk = k * k, invQk = 1.0 / m_Q * k, v = double.Pow(10.0, double.Abs(m_gain) / 20.0), vQk = v / m_Q * k;

      if (m_gain >= 0.0) // boost
      {
        var norm = 1.0 / (1.0 + invQk + kk);
        a0 = (1.0 + vQk + kk) * norm;
        a1 = 2.0 * (kk - 1.0) * norm;
        a2 = (1.0 - vQk + kk) * norm;
        b1 = a1;
        b2 = (1.0 - invQk + kk) * norm;
      }
      else // cut
      {
        var norm = 1.0 / (1.0 + vQk + kk);
        a0 = (1.0 + invQk + kk) * norm;
        a1 = 2.0 * (kk - 1.0) * norm;
        a2 = (1.0 - invQk + kk) * norm;
        b1 = a1;
        b2 = (1.0 - vQk + kk) * norm;
      }
    }

    /// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
    /// <param name="Fc">Uses normalized (to sample rate) frequency, where 1.0 is the sample rate, i.e. divide the frequency you want to set (in Hz) by your sample rate to get normalized frequency (2438.0/44100 for 2438 Hz at a sample rate of 44100 Hz).</param>
    /// <param name="Q"></param>
    /// <param name="peakGain">Peak gain in dB, where negative numbers are for cut, and positive numbers for boost.</param>
    public void DialFilter(double cutoffFrequency, double q, double gain, double sampleRate = 44100)
    {
      m_cutoffFrequency = cutoffFrequency;
      m_Q = q;
      m_gain = gain;
      m_sampleRate = sampleRate;

      switch (Function)
      {
        case BiQuadFrequencyFunction.BandPass:
          SetCoefficientsBandPass();
          break;
        case BiQuadFrequencyFunction.HighPass:
          SetCoefficientsHighPass();
          break;
        case BiQuadFrequencyFunction.HighShelf:
          SetCoefficientsHighShelf();
          break;
        case BiQuadFrequencyFunction.LowPass:
          SetCoefficientsLowPass();
          break;
        case BiQuadFrequencyFunction.LowShelf:
          SetCoefficientsLowShelf();
          break;
        case BiQuadFrequencyFunction.Notch:
          SetCoefficientsNotch();
          break;
        case BiQuadFrequencyFunction.Peak:
          SetCoefficientsPeak();
          break;
        default:
          throw new System.NotImplementedException($"{nameof(Function)}={Function}");
      }

      // double k = System.Math.Tan(System.Math.PI * (cutoff / sampleRate));

      // double v, norm, sqrt2, sqrt2v;

      // switch (FilterType)
      // {
      //   case FrequencyFunction.LowPass:
      //     norm = 1.0 / (1.0 + k / Q + k * k);
      //     a0 = k * k * norm;
      //     a1 = 2.0 * a0;
      //     a2 = a0;
      //     b1 = 2.0 * (k * k - 1.0) * norm;
      //     b2 = (1.0 - k / Q + k * k) * norm;
      //     break;
      //   case FrequencyFunction.HighPass:
      //     norm = 1.0 / (1.0 + k / Q + k * k);
      //     a0 = 1.0 * norm;
      //     a1 = -2.0 * a0;
      //     a2 = a0;
      //     b1 = 2.0 * (k * k - 1.0) * norm;
      //     b2 = (1.0 - k / Q + k * k) * norm;
      //     break;
      //   case FrequencyFunction.BandPass:
      //     norm = 1.0 / (1.0 + k / Q + k * k);
      //     a0 = k / Q * norm;
      //     a1 = 0;
      //     a2 = -a0;
      //     b1 = 2.0 * (k * k - 1.0) * norm;
      //     b2 = (1.0 - k / Q + k * k) * norm;
      //     break;
      //   case FrequencyFunction.Notch:
      //     norm = 1.0 / (1.0 + k / Q + k * k);
      //     a0 = (1.0 + k * k) * norm;
      //     a1 = 2.0 * (k * k - 1.0) * norm;
      //     a2 = a0;
      //     b1 = a1;
      //     b2 = (1.0 - k / Q + k * k) * norm;
      //     break;
      //   case FrequencyFunction.Peak:
      //     v = System.Math.Pow(10.0, System.Math.Abs(gain) / 20.0);
      //     if (gain >= 0.0) // boost
      //     {
      //       norm = 1.0 / (1.0 + 1.0 / Q * k + k * k);
      //       a0 = (1.0 + v / Q * k + k * k) * norm;
      //       a1 = 2.0 * (k * k - 1.0) * norm;
      //       a2 = (1.0 - v / Q * k + k * k) * norm;
      //       b1 = a1;
      //       b2 = (1.0 - 1.0 / Q * k + k * k) * norm;
      //     }
      //     else // cut
      //     {
      //       norm = 1.0 / (1.0 + v / Q * k + k * k);
      //       a0 = (1.0 + 1.0 / Q * k + k * k) * norm;
      //       a1 = 2.0 * (k * k - 1.0) * norm;
      //       a2 = (1.0 - 1.0 / Q * k + k * k) * norm;
      //       b1 = a1;
      //       b2 = (1.0 - v / Q * k + k * k) * norm;
      //     }
      //     break;
      //   case FrequencyFunction.LowShelf:
      //     v = System.Math.Pow(10.0, System.Math.Abs(gain) / 20.0);
      //     sqrt2 = System.Math.Sqrt(2.0);
      //     sqrt2v = System.Math.Sqrt(2.0 * v);
      //     if (gain >= 0) // boost
      //     {
      //       norm = 1.0 / (1.0 + sqrt2 * k + k * k);
      //       a0 = (1.0 + sqrt2v * k + v * k * k) * norm;
      //       a1 = 2.0 * (v * k * k - 1.0) * norm;
      //       a2 = (1.0 - sqrt2v * k + v * k * k) * norm;
      //       b1 = 2.0 * (k * k - 1.0) * norm;
      //       b2 = (1.0 - sqrt2 * k + k * k) * norm;
      //     }
      //     else // cut
      //     {
      //       norm = 1.0 / (1.0 + sqrt2v * k + v * k * k);
      //       a0 = (1.0 + sqrt2 * k + k * k) * norm;
      //       a1 = 2.0 * (k * k - 1.0) * norm;
      //       a2 = (1.0 - sqrt2 * k + k * k) * norm;
      //       b1 = 2.0 * (v * k * k - 1.0) * norm;
      //       b2 = (1.0 - sqrt2v * k + v * k * k) * norm;
      //     }
      //     break;
      //   case FrequencyFunction.HighShelf:
      //     v = System.Math.Pow(10.0, System.Math.Abs(gain) / 20.0);
      //     sqrt2 = System.Math.Sqrt(2.0);
      //     sqrt2v = System.Math.Sqrt(2.0 * v);
      //     if (gain >= 0) // boost
      //     {
      //       norm = 1.0 / (1.0 + sqrt2 * k + k * k);
      //       a0 = (v + sqrt2v * k + k * k) * norm;
      //       a1 = 2.0 * (k * k - v) * norm;
      //       a2 = (v - sqrt2v * k + k * k) * norm;
      //       b1 = 2.0 * (k * k - 1.0) * norm;
      //       b2 = (1.0 - sqrt2 * k + k * k) * norm;
      //     }
      //     else // cut
      //     {
      //       norm = 1.0 / (v + sqrt2v * k + k * k);
      //       a0 = (1.0 + sqrt2 * k + k * k) * norm;
      //       a1 = 2.0 * (k * k - 1.0) * norm;
      //       a2 = (1.0 - sqrt2 * k + k * k) * norm;
      //       b1 = 2.0 * (k * k - v) * norm;
      //       b2 = (v - sqrt2v * k + k * k) * norm;
      //     }
      //     break;
      //   default:
      //     throw new System.NotImplementedException(nameof(FilterType));
      // }
    }

    public double FilterMonoWave(double wave)
    {
      var o = wave * a0 + z1;

      z1 = wave * a1 + z2 - b1 * o;
      z2 = wave * a2 - b2 * o;

      return o;
    }

    public Waves.IWaveMono<double> FilterMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)FilterMonoWave(mono.Wave);

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> sample) => FilterMonoWave(sample);
  }
}
