
namespace Flux.Dsp.AudioProcessor
{
  public enum QuadraticMode
  {
    Bypass,
    /// <summary>Apply the quadratic exponent asymmetrically (both peeks will mathematically react differently to the exponent) to the signal.</summary>
    Asymmetric,
    /// <summary>Apply the quadratic exponent asymmetrically (both peeks will mathematically react differently to the exponent) to the also inverted signal.</summary>
    InvertedAsymmetric,
    /// <summary>Apply the quadratic exponent symmetrically (peeks reacts differently) to both peeks.</summary>
    Symmetric,
    /// <summary>Apply the quadratic exponent symmetrically to both peeks.</summary>
    SymmetricInverse
  }

  public class QuadraticMono
    : IAudioProcessorMono
  {
    public QuadraticMode Mode { get; internal set; }

    private double m_exponent, m_exponentExpanded;
    /// <summary>The quadratic exponent can be set within the constrained range [-1, 1].</summary>
    public double Exponent
    {
      get => m_exponent;
      set
      {
        m_exponent = Maths.Clamp(value, -1.0, 1.0);

        m_exponentExpanded = (-m_exponent) switch
        {
          var exp when exp > Flux.Maths.EpsilonCpp32 => 1 + -m_exponent * 99,
          var exp when exp < -Flux.Maths.EpsilonCpp32 => 1 + -m_exponent,
          _ => 0,
        };
      }
    }

    public QuadraticMono(QuadraticMode mode, double exponent)
    {
      Mode = mode;

      Exponent = exponent;
    }
    public QuadraticMono()
      : this(QuadraticMode.Asymmetric, 0)
    {
    }

    public double ProcessAudio(double sample)
      => (Mode switch
      {
        QuadraticMode.Asymmetric => (System.Math.Pow(sample / 2 + 0.5, m_exponentExpanded) * 2 - 1),
        QuadraticMode.InvertedAsymmetric => (-(System.Math.Pow(-sample / 2 + 0.5, m_exponentExpanded) * 2 - 1)),
        QuadraticMode.Symmetric => (2.0 * ((System.Math.Pow(m_exponent, sample + 1) - 1) / (System.Math.Pow(m_exponent, 2.0) - 1.0)) - 1),
        QuadraticMode.SymmetricInverse => (sample < 0 ? -(2 * ((System.Math.Pow(m_exponent, -sample + 1) - 1) / (System.Math.Pow(m_exponent, 2) - 1.0)) - 1) : 2.0 * ((System.Math.Pow(m_exponent, sample + 1.0) - 1.0) / (System.Math.Pow(m_exponent, 2.0) - 1.0)) - 1),
        _ => (sample),
      });
  }

  public class QuadraticStereo : IAudioProcessorStereo
  {
    public QuadraticMono Left { get; private set; }
    public QuadraticMono Right { get; private set; }

    public QuadraticMode Mode { get => Left.Mode; set => Right.Mode = Left.Mode = value; }

    /// <summary>The quadratic exponent can be set within the constrained range [-1, 1].</summary>
    public double Exponent { get => Left.Exponent; set => Right.Exponent = Left.Exponent = Maths.Clamp(value, -1.0, 1.0); }

    public QuadraticStereo(QuadraticMode modeL, double exponentL, QuadraticMode modeR, double exponentR)
    {
      Left = new QuadraticMono(modeL, exponentL);
      Right = new QuadraticMono(modeR, exponentR);
    }
    public QuadraticStereo(QuadraticMode mode, double exponent)
      : this(mode, exponent, mode, exponent)
    {
    }
    public QuadraticStereo()
      : this(QuadraticMode.Asymmetric, 1)
    {
    }

    public StereoSample ProcessAudio(StereoSample sample)
      => new StereoSample(Left.ProcessAudio(sample.FrontLeft), Right.ProcessAudio(sample.FrontRight));
  }
}
