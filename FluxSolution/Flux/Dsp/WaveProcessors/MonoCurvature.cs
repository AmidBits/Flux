namespace Flux.Dsp.WaveProcessors
{
  /// <summary>
  /// <para>Apply curvature with a contour where -1 = convex/logarithmic, 0 = linear, +1 = concave/exponential] to an arbitrary mono signal sample.</para>
  /// </summary>
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
        m_contour = double.Clamp(value, -1.0, 1.0);

        m_contourScaled = m_contour > XtensionDouble.MaxDefaultTolerance || m_contour < XtensionDouble.MinDefaultTolerance ? (-m_contour * 0.1 + 1) : 0;
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
      => 2.0 * ((double.Pow(m_contourScaled, (wave + 1.0) * 50.0) - 1.0) / (double.Pow(m_contourScaled, 100.0) - 1.0)) - 1.0;

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)ProcessMonoWave(mono.Wave);

    /// <summary>
    /// <para>Apply curvature with the specified contour to an arbitrary mono signal sample.</para>
    /// </summary>
    /// <param name="contour">The contour in the range [-1, 1] is used to transform the amplitude sample, where negative means that the amplitude gravitate towards negative, positive means gravitate towards positive.</param>
    /// <param name="sample">The mono sample in the range [-1, 1].</param>
    public static double ApplyCurvature(double sample, double contour)
    {
      var contourScaled = contour * 0.1 + 1.0;

      return (double.Pow(contourScaled, (sample + 1.0) * 50.0) - 1.0) / (double.Pow(contourScaled, 100.0) - 1.0) * 2.0 - 1.0;
    }
  }
}
