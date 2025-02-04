namespace Flux.Dsp.AudioProcessor.Adapter
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

    public IWaveStereo<double> ProcessStereoWave(IWaveStereo<double> stereo)
      => new WaveStereo<double>(Left.ProcessMonoWave(new WaveMono<double>(stereo.SampleLeft)).Wave, Right.ProcessMonoWave(new WaveMono<double>(stereo.SampleRight)).Wave);
  }
}
