namespace Flux.Dsp.WaveGenerator
{
  /// <see cref="https://en.wikipedia.org/wiki/Sample_and_hold"/>
  public sealed class SampleAndHold
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private readonly System.Random m_rng;

    private double m_sample, m_hold = System.Math.PI;

    public SampleAndHold(System.Random rng)
      => m_rng = rng ?? throw new System.ArgumentNullException(nameof(rng));
    public SampleAndHold()
      : this(new System.Random())
    { }

    public double Sample(double phase)
    {
      if (phase < m_hold)
      {
        m_sample = m_rng.NextDouble() * 2.0 - 1.0;
      }

      m_hold = phase;

      return m_sample;
    }

    public double GenerateMonoWaveUi(double phaseUi)
      => Sample(phaseUi);
    public double GenerateMonoWavePi2(double phasePi2)
      => Sample(phasePi2);
  }

  // /// <summary>A sample and hold based on the frequencies corresponding to all MIDI notes, which is a subset of the chromatic scale.</summary>
  // /// <remarks>No random number generator is needed for this sample and hold.</remarks>
  // /// <remarks>Since this is based on the logarithmic scale of MIDI note frequencies, there are more frequencies on the lower end of the spectrum, which means more peaks will be generated on the negative side of the sample co-domain [-1, 1].</remarks>
  // /// <see cref="https://en.wikipedia.org/wiki/Sample_and_hold"/>
  // public struct SampleAndHoldCS : IWaveGenerator
  // {
  // 	private double _sample, _hold;

  // 	public double Generate(double phase)
  // 	{
  // 		if(phase <= _hold)
  // 		{
  // 			_sample = Media.Midi.Note.Frequencies[(int)((phase / Math.Pi.X2) * Media.Midi.Note.Frequencies.Length)] / 6271.92697570799 - 1.0;
  // 		}

  // 		_hold = phase;

  // 		return _sample;
  // 	}

  // 	public override string ToString() => GetType().Name;
  // }
}
