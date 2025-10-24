namespace Flux.Dsp.WaveProcessors
{
  public record class StereoWidth
    : IStereoWaveProcessable
  {
    private double m_stereoCoefficient; // The coefficient of the transformation matrix (used for processing speed).

    private double m_width;
    ///<summar>The stereo width, represented in the range [-1, 1], where -1 = mono, <0 = decrese stereo width, 0 = no change, and >0 = increase stereo width.</summar>
    public double Width
    {
      get => m_width;
      set
      {
        m_width = double.Clamp(value, -1.0, 1.0);

        // var tmp = 1 / System.Math.Max(m_width + 1, 2); // This was present in my code, but I am unsure what it is suppose to do.

        m_stereoCoefficient = (m_width > XtensionSingle.MaxDefaultTolerance || m_width < XtensionSingle.MinDefaultTolerance) ? (m_width + 1) / 2 : 1;
      }
    }

    public (double leftWave, double rightWave) ProcessStereoWave(double leftWave, double rightWave)
    {
      var m = (leftWave + rightWave) / 2;
      var s = (rightWave - leftWave) * m_stereoCoefficient;

      return (m - s, m + s);
    }

    public Waves.IWaveStereo<double> ProcessStereoWave(Waves.IWaveStereo<double> stereo) => (Waves.WaveStereo<double>)ProcessStereoWave(stereo.SampleLeft, stereo.SampleRight);

    /// <summary>Apply stereo width to the sample.</summary>
    /// <param name="width">Stereo width in the range[-1, 1], where -1 = mono, <0 = decrease stereo width, 0 = no change, >0 increase stereo width.</param>
    /// <param name="left">Left stereo sample.</param>
    /// <param name="right">Right stereo sample.</param>
    public static (double left, double right) Apply(double width, double left, double right)
      => (left + right) / 2 is var m && (right - left) * (width > XtensionSingle.MaxDefaultTolerance || width < XtensionSingle.MinDefaultTolerance ? (width + 1) / 2 : 1) is var s ? (m - s, m + s) : (left, right);
  }
}
