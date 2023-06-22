namespace Flux.Dsp.AudioProcessor
{
  public record class MonoCurvature
    : IMonoWaveProcessable
  {
    private double m_contour, m_contourScaled;
    /// <summary>The contour is clamped in the range [-1, 1] and is used to transform the amplitude curve of the sample, where negative means convex/logarithmic, positive means concave/exponential, and 0 means linear.</summary>
    public double Contour
    {
      get => m_contour;
      set
      {
        m_contour = System.Math.Clamp(value, -1.0, 1.0);

        m_contourScaled = m_contour > Maths.EpsilonCpp32 || m_contour < -Maths.EpsilonCpp32 ? m_contour * 0.1 + 1 : 0;
      }
    }

    public MonoCurvature(double contour)
    {
      Contour = contour;
    }
    public MonoCurvature()
      : this(0)
    { }

    public double ProcessMonoWave(double wave)
      => (2 * ((System.Math.Pow(m_contourScaled, (wave + 1) * 50) - 1) / (System.Math.Pow(m_contourScaled, 100) - 1)) - 1);

    public IWaveMono<double> ProcessMonoWave(IWaveMono<double> mono) => (WaveMono<double>)ProcessMonoWave(mono.Wave);

    /// <summary>Apply curvature with the specified contour to an arbitrary mono signal sample.</summary>
    /// <param name="contour">The contour in the range [-1, 1] is used to transform the amplitude sample, where negative means convex/logarithmic, positive means concave/exponential, and 0 means linear.</param>
    /// <param name="sample">The mono sample in the range [-1, 1].</param>
    public static double ApplyCurvature(double sample, double contour)
      => (contour > Maths.EpsilonCpp32 || contour < -Maths.EpsilonCpp32) && contour * 0.1 + 1.0 is var contourScaled
      ? 2.0 * ((System.Math.Pow(contourScaled, (sample + 1.0) * 50.0) - 1.0) / (System.Math.Pow(contourScaled, 100.0) - 1.0)) - 1.0
      : sample;
  }
}
