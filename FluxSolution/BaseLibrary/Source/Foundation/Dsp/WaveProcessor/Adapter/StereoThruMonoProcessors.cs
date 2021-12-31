namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Process a stereo signal using two mono audio processors (IAudioProcessorMono).</summary>
  public sealed class StereoThruMonoProcessors
    : IStereoWaveProcessable
  {
    public IMonoWaveProcessable Left { get; set; }
    public IMonoWaveProcessable Right { get; set; }

    public StereoThruMonoProcessors(IMonoWaveProcessable left, IMonoWaveProcessable right)
    {
      Left = left;
      Right = right;
    }

    public SampleStereo ProcessStereoWave(SampleStereo sample)
      => new(Left.ProcessMonoWave(sample.FrontLeft), Right.ProcessMonoWave(sample.FrontRight));
  }
}
