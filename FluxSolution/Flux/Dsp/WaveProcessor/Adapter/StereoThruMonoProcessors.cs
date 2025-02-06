namespace Flux.Dsp.WaveProcessor.Adapter
{
  /// <summary>Process a stereo signal using two mono audio processors (IAudioProcessorMono).</summary>
  public record class StereoThruMonoProcessors
    : IStereoWaveProcessable
  {
    public IMonoWaveProcessable Left { get; set; }
    public IMonoWaveProcessable Right { get; set; }

    public StereoThruMonoProcessors(IMonoWaveProcessable left, IMonoWaveProcessable right)
    {
      Left = left;
      Right = right;
    }

    public Waves.IWaveStereo<double> ProcessStereoWave(Waves.IWaveStereo<double> stereo)
      => new Waves.WaveStereo<double>(Left.ProcessMonoWave(new Waves.WaveMono<double>(stereo.SampleLeft)).Wave, Right.ProcessMonoWave(new Waves.WaveMono<double>(stereo.SampleRight)).Wave);
  }
}
