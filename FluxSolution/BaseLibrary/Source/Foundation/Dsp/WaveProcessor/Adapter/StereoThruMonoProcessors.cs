namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Process a stereo signal using two mono audio processors (IAudioProcessorMono).</summary>
#if NET5_0
  public sealed class StereoThruMonoProcessors
#else
  public record struct StereoThruMonoProcessors
#endif
    : IWaveProcessorStereo
  {
    public IWaveProcessorMono Left { get; set; }
    public IWaveProcessorMono Right { get; set; }

    public StereoThruMonoProcessors(IWaveProcessorMono left, IWaveProcessorMono right)
    {
      Left = left;
      Right = right;
    }

    public SampleStereo ProcessAudio(SampleStereo sample)
      => new(Left.ProcessAudio(sample.FrontLeft), Right.ProcessAudio(sample.FrontRight));
  }
}
