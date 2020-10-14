namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Process a stereo signal using two mono audio processors (IAudioProcessorMono).</summary>
  public class StereoThruMonoProcessors
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
      => new SampleStereo(Left.ProcessAudio(sample.FrontLeft), Right.ProcessAudio(sample.FrontRight));
  }
}
