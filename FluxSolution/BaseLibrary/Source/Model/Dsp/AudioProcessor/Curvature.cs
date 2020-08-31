namespace Flux.Dsp.AudioProcessor
{
  public class CurvatureMono
    : IAudioProcessorMono
  {
    private double m_contour, m_contourScaled;
    /// <summary>The quadratic exponent can be set within the constrained range [-1, 1]. Below</summary>
    public double Contour
    {
      get => m_contour;
      set
      {
        m_contour = Maths.Clamp(value, -1.0, 1.0);

        m_contourScaled = m_contour > Maths.EpsilonCpp32 || m_contour < -Maths.EpsilonCpp32 ? m_contour * 0.1 + 1 : 0;
      }
    }

    public CurvatureMono(double contour)
    {
      Contour = contour;
    }
    public CurvatureMono()
      : this(0)
    {
    }

    public double ProcessAudio(double sample)
      => (2 * ((System.Math.Pow(m_contourScaled, (sample + 1) * 50) - 1) / (System.Math.Pow(m_contourScaled, 100) - 1)) - 1);

    /// <summary>Apply curvature with the specified contour to an arbitrary mono signal sample.</summary>
    /// <param name="contour">The contour in the range [-1, 1] is used to transform the amplitude sample, where negative means convex/logarithmic, positive means concave/exponential, and 0 means linear.</param>
    /// <param name="mono">The mono sample in the range [-1, 1].</param>
    public static double ApplyCurvature(double contour, double mono) => (contour > Maths.EpsilonCpp32 || contour < -Maths.EpsilonCpp32) && contour * 0.1 + 1.0 is var contourScaled ? 2.0 * ((System.Math.Pow(contourScaled, (mono + 1.0) * 50.0) - 1.0) / (System.Math.Pow(contourScaled, 100.0) - 1.0)) - 1.0 : mono;
  }

  public class CurvatureStereo
    : IAudioProcessorStereo
  {
    public CurvatureMono Left { get; }
    public CurvatureMono Right { get; }

    /// <summary>The quadratic exponent can be set within the constrained range [0, 10]. Below</summary>
    public double Exponent { get => Left.Contour; set => Right.Contour = Left.Contour = Maths.Clamp(value, -1.0, 1.0); }

    public CurvatureStereo(double contourL, double contourR)
    {
      Left = new CurvatureMono(contourL);
      Right = new CurvatureMono(contourR);
    }
    public CurvatureStereo(double contour)
      : this(contour, contour)
    {
    }
    public CurvatureStereo()
      : this(0.0)
    {
    }

    public StereoSample ProcessAudio(StereoSample sample)
      => new StereoSample(Left.ProcessAudio(sample.FrontLeft), Right.ProcessAudio(sample.FrontRight));
  }
}
